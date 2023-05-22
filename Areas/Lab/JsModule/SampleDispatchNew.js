$(document).ready(function () {
    FillCurrentDate('txtFrom');
    FillCurrentDate('txtTo');
    FillCurrentDate('txtFromDispatched');
    FillCurrentDate('txtToDispatched');
    
});

function GetTestInfo() {
    $('#tblDispatchList tbody').empty();
    var url = config.baseUrl + "/api/Lab/LabOutSourceQueries";
    var objBO = {};
    objBO.subcatId = '-';
    objBO.BarcodeNo = $('#txtBarcodeNo').val();
    objBO.from = $('#txtFromDispatched').val();
    objBO.to = $('#txtToDispatched').val();
    objBO.prm1 = "-";
    objBO.prm2= "-";
    objBO.logic = "GetTestByBarcodeNo";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (patientname != val.patientname) {
                            tbody += "<tr class='pat-group'>";
                            tbody += "<td colspan='6' class='pat-group'>" + val.visitNo + ' [ ' + val.patient_name + ' ]' + ' [ ' + val.date + ' ]' + "</td>";
                            tbody += "</tr>";
                            patientname = val.patient_name;
                            count1 = 0;
                        }
                        count++;
                        tbody += "<tr>";
                        tbody += "<td>" + count + "</td>";
                        tbody += "<td style='display:none'>" + val.AutoTestId + "</td>";
                        tbody += "<td style='display:none'>" + val.visitNo + "</td>";
                        tbody += "<td style='display:none'>" + val.DispatchLabCode + "</td>";
                        tbody += "<td style='display:none'>" + val.barcodeNo + "</td>";
                        tbody += "<td>" + val.TestCode + "</td>";
                        tbody += "<td>" + val.testName + "</td>";
                        tbody += "<td>" + val.samp_type + "</td>";
                        tbody += "<td>" + '<input type=checkbox>' + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblDispatchList tbody').append(tbody);
                }
            }
            else {
                alert('No Data Found')
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetTestListForDispatch() {
    var url = config.baseUrl + "/api/Lab/SampleDispatchQueries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.DispatchLabCode = $('#ddlCategory option:selected').val();
    objBO.login_id = Active.userId;
    objBO.logic = "TestListForDispatch";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#tblDispatch tbody').empty();
            var tbody = '';
            var count = 0;
            var patientname = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (patientname != val.patientname) {
                            tbody += "<tr class='pat-group'>";
                            tbody += "<td colspan='6' class='pat-group'>" + val.visitNo + ' [ ' + val.patient_name + ' ]' + ' [ ' + val.date + ' ]' + "</td>";
                            tbody += "</tr>";
                            patientname = val.patient_name;
                            count1 = 0;
                        }
                        count++;
                        tbody += "<tr>";
                        tbody += "<td>" + count + "</td>";
                        tbody += "<td style='display:none'>" + val.AutoTestId + "</td>";
                        tbody += "<td style='display:none'>" + val.visitNo + "</td>";
                        tbody += "<td style='display:none'>" + val.DispatchLabCode + "</td>";
                        tbody += "<td style='display:none'>" + val.barcodeNo + "</td>";
                        tbody += "<td>" + val.TestCode + "</td>";
                        tbody += "<td>" + val.testName + "</td>";
                        tbody += "<td>" + val.samp_type + "</td>";
                        tbody += "<td>" + '<input type=checkbox>' + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblDispatch tbody').append(tbody);
                }
            }
            else {
                alert('No Data Found')
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function Dispatch() {
    //var len = $('#tblDispatch tbody tr').length;
    //alert(len);
    if ($('#tblDispatch tbody tr').length == 0) {
        alert('Please Choose Set..');
        return;
    }
    if ($('#tblDispatch tbody tr').find('input[type=checkbox]:checked').length == 0) {
        alert('Please select any Set..');
        return;
    }
    if ($('#ddlEmployee option:selected').text() == 'Select Employee') {
        alert('Please Select Employee..');
        return;
    }
    if (confirm('Are you sure to Dispatch Sample....')) {
        var url = config.baseUrl + "/api/Lab/Lab_SampleDispatchAndReceive";
        var objBO = [];
        $('#tblDispatch tbody tr:not(.pat-group)').find('input[type=checkbox]:checked').each(function () {
            objBO.push({
                'AutoTestId': $(this).closest('tr').find('td:eq(1)').text(),
                'VisitNo': $(this).closest('tr').find('td:eq(2)').text(),
                'testcode': $(this).closest('tr').find('td:eq(5)').text(),
                'LabCode': $(this).closest('tr').find('td:eq(3)').text(),
                'DispatchLab': $(this).closest('tr').find('td:eq(3)').text(),
                'DispatchNo': '-',
                'BarcodeNo': $(this).closest('tr').find('td:eq(4)').text(),
                'deliveryBoyid': $('#ddlEmployee option:selected').val(),
                'hosp_id': Active.unitId,
                'login_id': Active.userId,
                'Logic': 'SampleDispatch',
            });
        });
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
                    GetDispatchNoList();
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
function GetDispatchNoList() {
    var url = config.baseUrl + "/api/Lab/SampleDispatchQueries";
    var objBO = {}
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.logic = "GetDispatchNoList";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#tblDispatchNo tbody').empty();
            var tbody = '';
            var count = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        tbody += "<tr>";
                        tbody += "<td>" + count + "</td>";
                        tbody += "<td>" + val.DispatchNo + "</td>";
                        tbody += "<td class='text-right'>" + val.NoOfItem + "</td>";
                        tbody += "<td>" + val.date + "</td>";
                        tbody += "<td>" + val.deliveryBoy + "</td>";
                        tbody += "<td><button class='btn-warning' onclick=Print('" + val.DispatchNo + "')>Print</button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblDispatchNo tbody').append(tbody);
                }
            }
            else {
                alert('No Data Found')
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function Print(DispatchNo) {
    var url = "../Print/SampleTransferSheet?DispatchNo=" + DispatchNo;
    window.open(url, "_blank");
}


