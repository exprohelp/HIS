﻿
$(document).ready(function () {
    FillCurrentDate('txtFrom');
    FillCurrentDate('txtTo');
    PendingCollection('Pending_ByDate');
    $('#tblCollect').on('click', function () {
        LabSampleCollection();
    });
    $('#tblSampleInfoOfPatient tbody').on('click', '#btnHelp', function () {
        var testcode = $(this).closest('tr').find('td:eq(1)').text();
        SampleTakenHelp(testcode);
    });
    $('#tblPendingCollection tbody').on('click', 'button', function () {
        var name = $(this).closest('tr').find('td:eq(1)').text();
        var visitno = $(this).closest('tr').find('td:eq(3)').text();
        var age = $(this).closest('tr').find('td:eq(2)').text();
        selectRow($(this))
        $('#txtPatientName').text(name);
        $('#txtPatientVisitNo').text(visitno);
        $('#txtPatientAge').text(age);
        SampleInfoOfPatient(visitno);
        //$('#tblPendingCollection thead tr').find('th:eq(4)').find('input[type:text]').val('');       
    });
    $('#tblSampleInfoOfPatient thead').on('keyup', 'input:text', function () {
        var barcode = $(this).val();
        $('#tblSampleInfoOfPatient tbody tr').find('td:eq(4)').find('input:text').val(barcode);
        checkOnBarode();
    });
    $('#tblSampleInfoOfPatient tbody').on('keyup', 'input:text', function () {
        checkOnBarode();
    });
    $('#tblSampleInfoOfPatient tbody').on('change', 'input[type=checkbox]', function () {
        var isCheck = $(this).is(':checked');
        if (!isCheck) {
            $(this).closest('tr').find('td:eq(4)').find('input:text').val('');
        }
    });
});
function checkOnBarode() {
    $('#tblSampleInfoOfPatient tbody tr:not(.sampleTaken)').each(function () {
        if ($(this).find('td:eq(4)').find('input:text').val().length > 3) {
            $(this).closest('tr').find('td:eq(6)').find('input[type=checkbox]').prop('checked', true);
        }
        else {
            $(this).closest('tr').find('td:eq(6)').find('input[type=checkbox]').prop('checked', false);
        }
    });
}
function Clear() {
    $('#txtPatientName').text('-');
    $('#txtPatientVisitNo').text('-');
    $('#txtPatientAge').text('-');
    $('#tblSampleInfoOfPatient tbody').empty();
    $('#tblSampleTakenHelp tbody').empty();
}
function PendingCollection(logic) {
    Clear();
    $('#tblPendingCollection tbody').empty();
    var url = config.baseUrl + "/api/sample/Lab_SampleCollectionQueries";
    var objBO = {};
    objBO.SampleCode = $('#ddlIPOPType option:selected').text();
    objBO.VisitNo = $('#txtVisitNo').val();
    objBO.Prm1 = $('#ddlPendCol option:selected').text();
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
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
                    var srno = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        srno++;
                        if (temp != val.IPOPType) {
                            tbody += "<tr style='background:#cfcfcf'>";
                            tbody += "<td colspan='5'>" + val.IPOPType + "</td>";
                            tbody += "</tr>";
                            temp = val.IPOPType;
                        }
                        if (val.Tc == val.SCC)
                            tbody += "<tr style='background:#b9e39f'>";
                        else if (val.SCC > 0 && val.Tc != val.SCC)
                            tbody += "<tr style='background:#f1dd7b'>";
                        else
                            tbody += "<tr>";

                        tbody += "<td>" + val.ipop_no + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";
                        tbody += "<td>" + val.Age + '/' + val.gender + "</td>";
                        tbody += "<td>" + val.visitNo + "</td>";
                        tbody += "<td style=width:1%><button class='btn btn-success btn-xs'><span class='fa fa-arrow-right'></button></td>";
                        tbody += "</tr>";
                    })
                    $('#tblPendingCollection tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function SampleInfoOfPatient(visitno) {
    var url = config.baseUrl + "/api/sample/Lab_SampleCollectionQueries";
    var objBO = {};
    objBO.VisitNo = visitno;
    objBO.Logic = "SampleInfoOfPatient";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#tblSampleInfoOfPatient tbody').empty();
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = '';
                    var srno = 0;
                    var testcode = "";
                    var option = "";
                    var defaultSample = "";
                    var IsSampleReceived = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        testcode = val.testcode;
                        defaultSample = val.default_sample;
                        $.each(data.ResultSet.Table1, function (key, val) {
                            if (testcode == val.testcode) {
                                if (val.default_sample == val.samp_type) {
                                    option += "<option selected value=" + val.default_sample + ">" + val.samp_type + "</option>";
                                }
                                else {
                                    option += "<option value=" + val.samp_type + ">" + val.samp_type + "</option>";
                                }
                                testcode = val.testcode;
                            }
                        });
                        srno++;
                        if (val.IsSampleCollected == '1') {
                            tbody += "<tr class='sampleTaken'>";
                        }
                        else {
                            tbody += "<tr>";
                        }
                        tbody += "<td style='padding:0'><button id='btnHelp' class='btn-success' data-autotestid=" + val.AutoTestId + ">Help</button></td>";
                        tbody += "<td>" + val.testcode + "</td>";
                        tbody += "<td>" + val.TestName + "</td>";
                        tbody += "<td><select class='form-control'>" + option + "</select></td>";
                        if (val.IsSampleCollected == '1') {
                            tbody += "<td>" + val.barcodeNo + "</td>";
                        }
                        else {
                            tbody += "<td><input type='text' class='form-control' margin-top: 2px; placeholder='BarCode' value='" + val.barcodeNo + "'/></td>";
                        }
                        tbody += "<td><input type='number' class='form-control' min='0' max='10' margin-top: 2px;' value=1></td>";
                        if (val.IsSampleCollected == '1') {
                            tbody += "<td>-</td>";
                        }
                        else {
                            tbody += "<td style='padding:0'><input type='checkbox'/></td>";
                        }
                        tbody += "</tr>";
                        option = "";
                    });
                    $('#tblSampleInfoOfPatient tbody').append(tbody);
                }
                else {
                    alert('Record Not Found..');
                }
            }
        },
        error: function (response) {
            alert('Server Error....!');
        }
    })
}
function SampleTakenHelp(testcode) {
    var url = config.baseUrl + "/api/sample/Lab_SampleCollectionQueries";
    var objBO = {};
    objBO.Logic = "SampleTakenHelp";
    objBO.TestCode = testcode;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#tblSampleTakenHelp tbody').empty();
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = '';
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td>" + val.test_preperation + "</td>";
                        tbody += "<td>" + val.samp_storage + "</td>";
                        tbody += "<td>" + val.sample_container + "</td>";
                        tbody += "<td>" + val.sample_qty + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblSampleTakenHelp tbody').append(tbody);
                }
                else {
                    alert('Record Not Found.');
                }
            }

        }
    });

}
function LabSampleCollection() {
    var total = $('#tblSampleInfoOfPatient tbody tr:not(.sampleTaken)').find('input:checkbox:checked').length;
    if (total < 1) {
        alert('Please Select Test');
        return;
    }
    if (ValidateBarcode()) {
        var url = config.baseUrl + "/api/sample/Lab_SampleCollection";
        var objBO = [];
        $('#tblSampleInfoOfPatient tbody tr:not(.sampleTaken)').find('input:checkbox:checked').each(function () {
            objBO.push({
                'VisitNo': $('#txtPatientVisitNo').text(),
                'AutotestId': $(this).closest('tr').find('td:eq(0)').find('button[id=btnHelp]').data('autotestid'),
                'TestCode': $(this).closest('tr').find('td:eq(1)').text(),
                'SampleCode': $(this).closest('tr').find('td:eq(3)').find('select option:selected').val(),
                'BarcodeNo': $(this).closest('tr').find('td:eq(4)').find('input:text').val(),
                'VialQty': $(this).closest('tr').find('td:eq(6)').find('input').val(),
                'login_id': Active.userId
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
                    //alert(data);
                    SampleInfoOfPatient($('#txtPatientVisitNo').text());
                    $('#tblSampleInfoOfPatient thead input').val('');
                }
                else {
                    alert(data);
                }
            }
        });
    }
}
function ValidateBarcode() {
    var count = 0;
    var total = $('#tblSampleInfoOfPatient tbody tr:not(.sampleTaken)').find('input:checkbox:checked').length;
    $('#tblSampleInfoOfPatient tbody tr:not(.sampleTaken)').find('input:checkbox:checked').each(function () {
        if ($(this).closest('tr').find('td:eq(4)').find('input:text').val().length > 3) {
            count++;
        }
    });
    if (eval(total) != eval(count)) {
        alert('Please Provide Correct Barcode of selected Test.');
        return false;
    }
    else {
        return true;
    }
}
function PendingByVisitNo() {
    Clear();
    if ($('#txtVisitNo').val() == '') {
        alert('Please Provide VisitNo.');
        return;
    }
    var url = config.baseUrl + "/api/sample/Lab_SampleCollectionQueries";
    var objBO = {};
    objBO.VisitNo = $('#txtVisitNo').val();
    objBO.Logic = "Pending_ByVisitNo";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#tblPendingCollection tbody').empty();
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = '';
                    var srno = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        srno++;
                        tbody += "<tr>";
                        tbody += "<td>" + srno + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";
                        tbody += "<td>" + val.Age + "</td>";
                        tbody += "<td>" + val.visitNo + "</td>";
                        tbody += "<td style=width:1%><button class='btn btn-success btn-xs'><span class='fa fa-arrow-right'></button></td>";
                        tbody += "</tr>";
                    })
                    $('#tblPendingCollection tbody').append(tbody);
                    $('#txtVisitNo').val('');
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

