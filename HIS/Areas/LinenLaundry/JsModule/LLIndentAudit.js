var order = true;
$(document).ready(function () {
    GetCartListByLoginId();
    FillCurrentDate('txtFrom')
    FillCurrentDate('txtTo')
});
function GetCartListByLoginId() {
    $("ddlCartList").empty();
    var url = config.baseUrl + "/api/LinenLaundry/LL_TransferAndReceiveQueries";
    var objIdentBO = {};
    objIdentBO.login_id = Active.userId;
    objIdentBO.Logic = "LL_StoreListByLogin";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objIdentBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.ResultSet.Table.length > 0) {
                $("#ddlCartList").append($("<option></option>").val("ALL").html("ALL"));
                $.each(data.ResultSet.Table, function (key, value) {
                    $("#ddlCartList").append($("<option></option>").val(value.CartId).html(value.CartName));
                });
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function IndentMovement(logic) {
    $('#tblIndentInfo tbody').empty();
    var url = config.baseUrl + "/api/LinenLaundry/LL_AuditQry";
    var objBO = {};
    objBO.CartId = $('#ddlCartList option:selected').val();
    objBO.from = $('#txtFrom').val();
    objBO.To = $('#txtTo').val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.IndentNo = '-';
    objBO.Logic = logic;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var htmldata = "";
            var CartId = "";
            var count = 0;
            if (data.ResultSet.Table.length > 0) {
                $.each(data.ResultSet.Table, function (key, val) {
                    if (CartId != val.CartId) {
                        htmldata += '<tr class="bg1">';
                        htmldata += '<td colspan="6" style="font-weight:bold">Cart Name : ' + val.CartName + '</td>';
                        htmldata += '</tr>';
                        CartId = val.CartId;
                        count = 0;
                    }
                    count++;
                    if (val.MisMatchFlag == 'Y')
                        htmldata += '<tr class="mismatch">';
                    else
                        htmldata += '<tr>';

                    htmldata += '<td>' + val.IndentDate + '</td>';
                    htmldata += '<td>' + val.IndentNo + '</td>';
                    htmldata += '<td class="text-right">' + val.IndentQty + '</td>';
                    htmldata += '<td class="text-right">' + val.TrfQty + '</td>';
                    htmldata += '<td class="text-right">' + val.RcvdQty + '</td>';
                    htmldata += "<td><button class='btn btn-warning btn-xs' onclick=selectRow(this);IndentItemMovementInfo('" + val.IndentNo + "')><i class='fa fa-eye';'></i></button></td>";
                    htmldata += '</tr>';
                });
                $('#tblIndentInfo tbody').append(htmldata);

            }
            else {
                //alert('Data Not Found..');
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });

}
function IndentItemMovementInfo(indentNo) {
    $('#tblIssue tbody').empty();
    $('#tblTransfer tbody').empty();
    $('#tblReceived tbody').empty();
    var url = config.baseUrl + "/api/LinenLaundry/LL_AuditQry";
    var objBO = {};
    objBO.CartId = $('#ddlCartList option:selected').val();
    objBO.from = $('#txtFrom').val();
    objBO.To = $('#txtTo').val();
    objBO.IndentNo = indentNo;
    objBO.Logic = "IndentItemMovementInfo";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                var htmldata = "";
                var CartId = "";
                var count = 0;
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        htmldata += '<tr>';
                        htmldata += '<td>' + val.item_name + '</td>';
                        htmldata += '<td class="text-right">' + val.qty + '</td>';
                        htmldata += '<td class="text-right">' + val.issue_qty + '</td>';
                        htmldata += '<td>' + val.issue_date + '</td>';
                        htmldata += '</tr>';
                    });
                    $('#tblIssue tbody').append(htmldata);
                }
            }
            if (Object.keys(data.ResultSet).length > 0) {
                var htmldata1 = "";
                var CartId = "";
                var count = 0;
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    $.each(data.ResultSet.Table1, function (key, val) {
                        count++;
                        htmldata1 += '<tr>';
                        htmldata1 += '<td>' + val.tran_id + '</td>';
                        htmldata1 += '<td>' + val.item_name + '</td>';
                        htmldata1 += '<td class="text-right">' + val.qty + '</td>';
                        htmldata1 += '<td>' + val.doc_date + '</td>';
                        htmldata1 += '<td>' + val.rcpt_date + '</td>';
                        htmldata1 += '</tr>';
                    });
                    $('#tblTransfer tbody').append(htmldata1);
                }
            }
            if (Object.keys(data.ResultSet).length > 0) {
                var htmldata2 = "";
                var CartId = "";
                var count = 0;
                if (Object.keys(data.ResultSet.Table2).length > 0) {
                    $.each(data.ResultSet.Table2, function (key, val) {
                        count++;
                        htmldata2 += '<tr>';
                        htmldata2 += '<td>' + val.tran_id + '</td>';
                        htmldata2 += '<td>' + val.item_name + '</td>';
                        htmldata2 += '<td class="text-right">' + val.qty + '</td>';
                        htmldata2 += '<td>' + val.doc_date + '</td>';
                        htmldata2 += '</tr>';
                    });
                    $('#tblReceived tbody').append(htmldata2);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });

}
function DownloadExcel() {
    var url = config.baseUrl + "/api/LinenLaundry/LL_AuditQry";
    var objBO = {};
    objBO.CartId = $('#ddlCartList option:selected').val();
    objBO.from = $('#txtFrom').val();
    objBO.To = $('#txtFrom').val();
    objBO.IndentNo = '-';
    objBO.OutPutType = "Excel";
    objBO.Logic = "IndentMovement";
    Global_DownloadExcel(url, objBO, "IndentReport.xlsx");
}
