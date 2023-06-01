var _ipdNo = '';
$(document).ready(function () {
    // PushDataSummary();
    $('#tblPushDataSummary tbody').on('click', 'button', function () {
        _ipdNo = $(this).data('ipopno');
        HISPushPendency('HIS-PushPendencyByIPDNo');
        $('.nav-tabs li').find('a[href=#Pending]').trigger('click');
    });
});

function PushDataSummary(prm1) {
    if (prm1 == 'ByIPDNo' && $('#txtIPDNo').val() == '') {
        alert('Please Provide IPD No.');
        return
    }
    $('#tblPushDataSummary tbody').empty();
    var url = config.baseUrl + "/api/Pharmacy/Hospital_BillPushReport";
    var objBO = {};
    objBO.IPDNos = $('#txtIPDNo').val() + '|';
    objBO.prm_1 = prm1;
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.sale_inv_no = '-';
    objBO.Logic = 'HIS:PushDataSummary';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = '';
                    var temp = '';
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (eval(val.PendInv) == 0)
                            tbody += "<tr style='background:#cff1b0'>";
                        else
                            tbody += "<tr>";

                        tbody += "<td>" + val.hosp_ipop_no + "</td>";
                        tbody += "<td>" + val.pt_name + "</td>";
                        tbody += "<td class='text-right'>" + val.TInvoice + "</td>";
                        tbody += "<td class='text-right'>" + val.PushInv + "</td>";
                        if (eval(val.PendInv) == 0)
                            tbody += "<td class='text-right'>" + val.PendInv + "</td>";
                        else
                            tbody += "<td><button data-ipopno=" + val.hosp_ipop_no + " onclick=PushPendencyByIPDNo(this) style='width: 35px;' class='btn btn-warning btn-xs btntblCustom pull-right'>" + val.PendInv + "</button></td>";

                        tbody += "<td class='text-right'>" + val.TotAmt + "</td>";
                        tbody += "<td class='text-right'>" + val.PushAmount + "</td>";
                        tbody += "<td class='text-right'>" + val.PendAmount + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblPushDataSummary tbody').append(tbody);
                }
                else {
                    alert('Record Not Found.');
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function HISPushPendency(logic) {
    $('#tblPendingDetails tbody').empty();
    var url = config.baseUrl + "/api/Pharmacy/Hospital_BillPushReport";
    var objBO = {};
    objBO.IPDNos = _ipdNo;
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.sale_inv_no = '-';
    objBO.Logic = logic;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = '';
                    var temp = '';
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (temp != val.ipop_no) {
                            tbody += "<tr style='background:#c9e7f9'>";
                            tbody += "<td colspan='7'><b>IPOP No. : </b>" + val.ipop_no + ", <b>Patient Name : </b>" + val.pt_name + "</td>";
                            tbody += "</tr>";
                            temp = val.ipop_no;
                        }
                        tbody += "<tr>";
                        tbody += "<td>" + val.indent_no + "</td>";
                        tbody += "<td>" + val.sale_inv_no + "</td>";
                        tbody += "<td>" + val.sale_date + "</td>";
                        tbody += "<td class='text-right'>" + val.total + "</td>";
                        tbody += "<td class='text-right'>" + val.discount + "</td>";
                        tbody += "<td class='text-right'>" + val.net + "</td>";
                        tbody += "<td><button data-saleinvno=" + val.sale_inv_no + " onclick=UpdateHISPushFlag(this) class='btn btn-warning btn-xs btntblCustom'><i class='fa fa-check-circle'>&nbsp;</i>Done</button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblPendingDetails tbody').append(tbody);
                }
                else {
                    alert('Record Not Found.');
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function UpdateHISPushFlag(elem) {
    if (confirm('Are you sure?')) {
        var url = config.baseUrl + "/api/Pharmacy/Hospital_VerifyIPDBill";
        var objBO = {};
        objBO.IPDNo = '-';
        objBO.SaleInvNos = $(elem).data('saleinvno');
        objBO.prm_1 = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = "UpdateHISPushFlag";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    //alert(data);
                    $(elem).closest('tr').remove();
                }
                else {
                    alert(data);
                }
            },
            complete: function () {
                $('#div#skill .circle').hide();
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}