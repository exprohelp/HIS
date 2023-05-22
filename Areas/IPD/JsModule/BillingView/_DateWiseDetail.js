$(document).ready(function () {
    $('#dash-dynamic-section').find('label.title').text('Date Wise Detail').show();
    //$('#txtFrom').val(FillCurrentDateTime())
    //$('#txtTo').val(FillCurrentDateTime())
    GetCategory();
});

function GetCategory() {
    $('#ddlCategory').empty().append($('<option></option>').val('ALL').html('ALL')).select2();
    $('#ddlSubCategory').empty().append($('<option></option>').val('ALL').html('ALL')).select2();
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = _IPDNo;
    objBO.DoctorId = '';
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '';
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetBillingCategorySubCat';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlCategory').append($('<option></option>').val(val.CatID).html(val.CatName));
                        $('#txtFrom').val(val.currentDateTime);
                        $('#txtTo').val(val.currentDateTime);
                    });
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table1).length) {
                    $.each(data.ResultSet.Table1, function (key, val) {
                        $('#ddlSubCategory').append($('<option></option>').val(val.SubCatID).html(val.SubCatName));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetBillDetailInfo() {
    $("#tblBillInfo tbody").empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '-';
    objBO.IPDNo = _IPDNo;
    objBO.DoctorId = '';
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Prm1 = $('#ddlCategory option:selected').val();
    objBO.Prm2 = $('#ddlSubCategory option:selected').val();
    objBO.OutPutType = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetBillDetailInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = "";
                    var temp = "";
                    var count = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (temp != val.SubCatName) {
                            tbody += "<tr style='background:#ddd'>";                            
                            tbody += "<td colspan='12'>" + val.SubCatName + "</td>";
                            tbody += "<tr>";
                            temp = val.SubCatName;
                        }
                        count++;
                        tbody += "<tr>";
                        tbody += "<td class='hide'>" + val.ItemId + "</td>";
                        tbody += "<td>" + count + "</td>";
                        tbody += "<td>" + val.tnxDate + "</td>";
                        tbody += "<td>" + val.ItemName + "</td>";
                        tbody += "<td class='text-right'>" + val.Qty + "</td>";
                        tbody += "<td class='text-right'>" + val.GrossAmount + "</td>";
                        tbody += "<td class='text-right'>" + val.panel_discount + "</td>";
                        tbody += "<td class='text-right'>" + val.adl_discount + "</td>";
                        tbody += "<td class='text-right'>" + val.Discount + "</td>";
                        tbody += "<td class='text-right'>" + val.NetAmount + "</td>";
                        tbody += "<td>" + val.doctorName + "</td>";
                        tbody += "<td>" + val.EntryBy + "</td>";
                        tbody += "<td>" + val.Remark + "</td>";
                        tbody += "</tr>";
                    });
                    $("#tblBillInfo tbody").append(tbody);
                }
                else {
                    alert("Data Not Found");
                };
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function FillCurrentDateTime() {
    var today = '';
    var date = new Date();
    var day = date.getDate();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();
    var hour = date.getHours();
    var Minute = date.getMinutes();
    var Second = date.getSeconds();
    if (month < 10) month = "0" + month;
    if (day < 10) day = "0" + day;
    today = year + "-" + month + "-" + day + 'T' + hour + ':' + Minute + ':' + Second;
    return today;
}
function DownloadExcel(logic) {
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '-';
    objBO.IPDNo = _IPDNo;
    objBO.DoctorId = '';
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Prm1 = $('#ddlCategory option:selected').val();
    objBO.Prm2 = $('#ddlSubCategory option:selected').val();
    objBO.OutPutType = 'Excel';
    objBO.login_id = Active.userId;
    objBO.Logic = logic;
    Global_DownloadExcel(url, objBO, logic + ".xlsx");
}