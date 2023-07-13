$(document).ready(function () {
    FillCurrentDate("txtFrom");
    FillCurrentDate("txtTo");
    ToBereceivedByHosp();
});
function ToBereceivedByHosp() {
    $('#tblToBereceivedByHosp tbody').empty();
    var url = config.baseUrl + "/api/Account/HOTO_Queries";
    var objBO = {};
    objBO.unit_id = 'MS-H0048';
    objBO.login_id = 'chl-00542';
    objBO.shiftID = '-';
    objBO.prm_1 = '-';
    objBO.prm_2 = '-';
    objBO.logic = "ToBereceivedByHosp";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = '';
            var count = 0;
            if (data.ResultSet.Table.length > 0) {
                $.each(data.ResultSet.Table, function (key, val) {
                    count++;
                    tbody += "<tr>";
                    tbody += "<td><button onclick=ReceivedByHosp(this)  class='btn btn-warning btn-xs'><i class='fa fa-check-circle'>&nbsp;</i>Receive</button></td>";
                    tbody += "<td class='hide'>" + val.autoId + "</td>";
                    tbody += "<td>" + val.colldate + "</td>";
                    tbody += "<td>" + val.tnxTo + "</td>";
                    tbody += "<td>" + val.tnxByName + "</td>";
                    tbody += "<td class='text-right'>" + val.Amount + "</td>";
                    tbody += "<td>" + val.Remarks + "</td>";
                    tbody += "</tr>";
                });
            }
            $('#tblToBereceivedByHosp tbody').append(tbody);
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function HospRcvdInfoByDate() {
    $('#tblHospRcvdInfoByDate tbody').empty();
    var url = config.baseUrl + "/api/Account/HOTO_Queries";
    var objBO = {};
    objBO.unit_id = 'MS-H0048';
    objBO.login_id = 'chl-00542';
    objBO.shiftID = '-';
    objBO.prm_1 = $('#txtFrom').val();
    objBO.prm_2 = $('#txtTo').val();
    objBO.logic = "HospRcvdInfoByDate";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = '';
            var count = 0;
            if (data.ResultSet.Table.length > 0) {
                $.each(data.ResultSet.Table, function (key, val) {
                    count++;
                    tbody += "<tr>";
                    tbody += "<td><button class='btn btn-warning btn-xs'><i class='fa fa-check-circle'>&nbsp;</i>Receive</button></td>";
                    tbody += "<td class='hide'>" + val.autoId + "</td>";
                    tbody += "<td>" + val.colldate + "</td>";
                    tbody += "<td>" + val.tnxTo + "</td>";
                    tbody += "<td>" + val.tnxByName + "</td>";
                    tbody += "<td class='text-right'>" + val.Amount + "</td>";
                    tbody += "<td>" + val.Remarks + "</td>";
                    tbody += "</tr>";
                });
            }
            $('#tblHospRcvdInfoByDate tbody').append(tbody);
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ReceivedByHosp(elem) {
    var autoId = $(elem).closest('tr').find('td:eq(1)').text();
    if (confirm('Are you sure?')) {
        var url = config.baseUrl + "/api/Account/Insert_Modify_AC_Collection_Info";
        var objBO = {};
        objBO.autoId = autoId;
        objBO.unit_id = 'MS-H0048';
        objBO.tnxDate = '1900-01-01';
        objBO.tnxBy = '-';
        objBO.tnxTo = '-';
        objBO.amount = '-';
        objBO.shiftID = '-';
        objBO.prm_1 = '-';
        objBO.prm_2 = '-';
        objBO.logic = "ReceivedByHosp";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data)
                    $(elem).closest('tr').remove();
                }
                else {
                    alert(data)
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}