var _StockId, _BloodCollectionId;
$(document).ready(function () {
    FillCurrentDate('txtFrom')
    FillCurrentDate('txtTo')
});
function StockInfo(logic) {
    $('#tblStockInfo tbody').empty();
    var url = config.baseUrl + "/api/BloodBank/BB_SelectQueries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.DonorId = '-';
    objBO.VisitId = '-';
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.IndentNo = '-';
    objBO.Prm1 = $('#txtExpiredDays').val();
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = logic;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = "";
                    var count = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        tbody += "<tr>";
                        tbody += "<td class='hide'>" + val.BloodCollection_Id + "</td>";
                        tbody += "<td class='hide'>" + val.Stock_Id + "</td>";
                        tbody += "<td style='padding: 2px 5px;'>" + count + "</td>";
                        tbody += "<td>" + val.donorName + "</td>";
                        tbody += "<td>" + val.BloodCollection_Id + "</td>";
                        tbody += "<td>" + val.ComponentName + "</td>";
                        tbody += "<td>" + val.BagType + "</td>";
                        tbody += "<td>" + val.BBTubeNo + "</td>";
                        tbody += "<td>" + val.BloodGroup + "</td>";
                        tbody += "<td>" + val.DiscardBy + "</td>";
                        tbody += "<td>" + val.DiscardedDate + "</td>";
                        tbody += "<td>" + val.DiscardedReason + "</td>";                     
                        if (val.DiscardBy == null)
                            tbody += "<td><button onclick=discardInit(this) style='height: 19px;line-height: 0;' class='btn btn-danger btn-xs'><i class='fa fa-sign-in'>&nbsp;</i>Discard</button></td>";
                        else
                            tbody += "<td>-</td>";

                        tbody += "</tr>";
                    });
                    $('#tblStockInfo tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function discardInit(elem) {
    selectRow($(elem));
    $('#modalDiscard').modal('show');
    _StockId = $(elem).closest('tr').find('td:eq(1)').text();
    _BloodCollectionId = $(elem).closest('tr').find('td:eq(0)').text();
}
function Discard() {
    if (confirm('Are you sure to discard?')) {
        if ($('#txtReason').val() == '') {
            alert('Please Provide Discard Reason.');
            $('#txtReason').css('border-color', 'red').focus();
            return
        }
        else {
            $('#txtReason').removeAttr('style');
        }
        var url = config.baseUrl + "/api/BloodBank/BB_ApproveDonation";
        var objBO = {};
        objBO.hosp_id = Active.HospId;
        objBO.donor_id = '-';
        objBO.Stock_Id = _StockId;
        objBO.visit_id = '-';
        objBO.BloodCollectionId = _BloodCollectionId;
        objBO.Prm1 = $('#txtReason').val();
        objBO.Prm2 = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = "DiscardBlood";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
                    StockInfo('Discard:StockList');
                    $('#modalDiscard').modal('hide')
                }
                else {
                    alert(data);
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
