var _BillNo = "";
$(document).ready(function () {
    ReceiptInfoByIPDNo();
    $('table thead').on('change', 'input:checkbox', function () {
        var IsCheck = $(this).is(':checked');  
        if (IsCheck)
            $(this).parents('table').find('tbody').find('input:checkbox').prop('checked', true);
        else
            $(this).parents('table').find('tbody').find('input:checkbox').prop('checked', false);
    });
});

function ReceiptInfoByIPDNo() {
    $('#tblBillInfo tbody').empty();
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
    objBO.Prm1 = '-';
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'ReceitInfoByIpdNo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = '';
                    var count = 0;
                    var temp = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        tbody += "<tr>";
                        tbody += "<td><input type='checkbox'/></td>";
                        tbody += "<td>" + val.ReceiptNo + "</td>";
                        tbody += "<td>" + val.receiptDate + "</td>";
                        tbody += "<td class='text-right'>" + val.Amount + "</td>";
                        tbody += "<td>" + val.PayMode + "</td>";
                        tbody += "<td>" + val.ReceiptType + "</td>";
                        tbody += "<td>" + val.EntryBy + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblBillInfo tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function BillPrint_CategoryWiseOnlyBill() {
    var url = "../Print/IPDBillSummary?_BillNo=" + window.btoa(_BillNo) + "&_IPDNo=" + window.btoa(_IPDNo) + "&_BillPrintType=CategorywiseOnly";
    window.open(url, '_blank');
}
function BillPrint_ItemWise() {
    var url = "../Print/IPDBillSummary?_BillNo=" + window.btoa(_BillNo) + "&_IPDNo=" + window.btoa(_IPDNo) + "&_BillPrintType=ItemWise";
    window.open(url, '_blank');
}
function BillPrint_DateWise() {
    var url = "../Print/IPDBillSummary?_BillNo=" + window.btoa(_BillNo) + "&_IPDNo=" + window.btoa(_IPDNo) + "&_BillPrintType=DateWise";
    window.open(url, '_blank');
}
function BillPrint_IncludingPackagedItem() {
    var url = "../Print/IPDBillSummary?_BillNo=" + window.btoa(_BillNo) + "&_IPDNo=" + window.btoa(_IPDNo) + "&_BillPrintType=IncludingPackagedItem";
    window.open(url, '_blank');
}