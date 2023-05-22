$(document).ready(function () {
    BloodRequisitionPatientInfo();
    $('input[name=Transfusion]').on('change', function () {     
        if ($(this).val() == 'No')
            $(this).closest('div.flex').find('input.TransfusionHistory').val('').prop('disabled', true);
        else
            $(this).closest('div.flex').find('input.TransfusionHistory').val('').prop('disabled', false);
    });
});
function selectedReqTime(elem) {
    $('.reqBox').removeClass('selectedReqTime');
    $(elem).parents('.reqBox').addClass('selectedReqTime');
}
function ComponentChoose() {
    $('div.ComponentQuantity').empty();
    var componentId = '',ItemId = '', componentName = '', Qty = 0, list = '';
    $('#tblComponentsInfo tbody tr').each(function () {
        if (parseInt($(this).find('td:eq(4)').find('input:text').val()) > 0) {
            componentId = $(this).find('td:eq(0)').text();
            componentName = $(this).find('td:eq(2)').text();
            Qty = $(this).find('td:eq(4)').find('input:text').val();
            ItemId = $(this).find('td:eq(5)').text();
            list += "<span ondblclick=$(this).remove() data-componentid=" + componentId + " data-itemid=" + ItemId + ">" + componentName + ' : ' + Qty + "</span>";
        }
    });
    $('div.ComponentQuantity').append(list);
}
function BloodRequisitionPatientInfo() {
    $('#tblReportInfo tbody').empty();
    $('#tblPatientInfo tbody').empty();
    $('#ddlDoctorName').append($('<option></option>').val('Select').html('Select')).select2();
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '';
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '';
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'BloodRequisitionPatientInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table1).length) {
                    $.each(data.ResultSet.Table1, function (key, val) {
                        $('#ddlDoctorName').append($('<option data-sign=' + val.SignVirtualPath + '></option>').val(val.DoctorId).html(val.DoctorName));
                    });
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = "";
                    var count = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td class='hide'>" + JSON.stringify(data.ResultSet.Table[count]) + "</td>";
                        tbody += "<td>" + val.IPDNo + "</td>";
                        tbody += "<td>" + val.AdmitDate + "</td>";
                        tbody += "<td>" + val.PatientName + "</td>";
                        tbody += "<td>" + val.DoctorName + "</td>";
                        tbody += "<td><button onclick=getInfo(this) class='btn btn-warning btn-xs'><i class='fa fa-sign-in'></i></button></td>";
                        tbody += "</tr>";
                        count++;
                    });
                    $('#tblPatientInfo tbody').append(tbody);
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table2).length) {
                    var tbody = "";
                    var count = 0;
                    $.each(data.ResultSet.Table2, function (key, val) {
                        tbody += "<tr>";                                         
                        tbody += "<td>" + val.IPOPNo + "</td>";
                        tbody += "<td>" + val.IndentNo + "</td>";
                        tbody += "<td>" + val.PatientName + "</td>";
                        tbody += "<td>" + val.IndentDate + "</td>";                     
                        tbody += "<td><button onclick=PrintReport('" + val.IndentNo+"') class='btn btn-warning btn-xs'><i class='fa fa-print btn-xs'></i></button></td>";
                        tbody += "</tr>";
                        count++;
                    });
                    $('#tblReportInfo tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function PrintReport(IndentNo) {
    var url = "/IPD/Print/BloodRequisitionForm?IndentNo=" + IndentNo;
    window.open(url, '_blank');
}
function ComponentsInfo() {
    $('#tblComponentsInfo tbody').empty();
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '';
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '';
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'ComponentsInfoForBloodReq';
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
                        tbody += "<tr>";                      
                        tbody += "<td>" + val.ComponentID + "</td>";
                        tbody += "<td>" + val.ComponentName + "</td>";
                        tbody += "<td>" + val.AliasName + "</td>";
                        tbody += "<td>" + val.dtExpiry + "</td>";
                        tbody += "<td><input type='text' class='form-control' /></td>";
                        tbody += "<td class='hide'>" + val.itemId + "</td>";
                        tbody += "</tr>";
                        count++;
                    });
                    $('#tblComponentsInfo tbody').append(tbody);
                    $('#modalComponentQuantity').modal('show');
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function Insert() {
    var url = config.baseUrl + "/api/IPDNursingService/IPD_InsertBloodRequisition";
    var objBO = {};
    var bloodRequisitionInfo = {};
    var selectedComponent = [];
    if ($('#txtUHID').val()=='') {
        alert('Please Choose Patient Info.');
        return
    }
    if ($('div.ComponentQuantity span').length < 1) {
        alert('Please Choose Component Quantity.');
        return
    }
    $('div.ComponentQuantity span').each(function () {
        selectedComponent.push({
            'ComponentID': $(this).data('componentid'),
            'ComponentName': $(this).data('itemid'),
            'Qty': $(this).text().split(':')[1],
        });
    });
    bloodRequisitionInfo.UHID = $('#txtUHID').val();
    bloodRequisitionInfo.IPOPNo = $('#txtIPDNo').val();
    bloodRequisitionInfo.PatientName = $('#txtPatientName').val();
    bloodRequisitionInfo.IPOPType = $('#txtPatientName').val();
    bloodRequisitionInfo.IndentNo = '-';
    bloodRequisitionInfo.ItemId = '-';
    bloodRequisitionInfo.ComponentId = '-';
    bloodRequisitionInfo.IndentQty = '-';
    bloodRequisitionInfo.IndentBy = Active.userId;
    bloodRequisitionInfo.TnxId = '-';
    bloodRequisitionInfo.BloodGroupId = '-';
    bloodRequisitionInfo.BloodTestedBy = '-';
    bloodRequisitionInfo.BloodTestedDate = '1900/01/01';
    bloodRequisitionInfo.Diagnosis = $('#txtDiagnosis').val();
    bloodRequisitionInfo.TransfusionIndicator = $('#txtIndicationForTransfusion').val();
    bloodRequisitionInfo.HbPerc = $('#txtHb').val();
    bloodRequisitionInfo.PreviousTransfusion = ($('input[name=Transfusion]').is(':checked')) ? 'Y' : 'N';
    bloodRequisitionInfo.DonorNo = $('#txtDonorNumber').val();
    bloodRequisitionInfo.AboRH = $('#txtABORh').val();
    bloodRequisitionInfo.dateTransfuse = $('#txtdateTransfuse').val();
    bloodRequisitionInfo.ReactionAny = $('#txtReactionAny').val();
    bloodRequisitionInfo.IsPregnant = $('input[name=Pregnancy]:checked').val();
    bloodRequisitionInfo.haemolytic_disease = $('input[name=haemolytic]:checked').val();
    bloodRequisitionInfo.miscarriage = $('input[name=miscarriage]:checked').val();
    bloodRequisitionInfo.ReqType = $('.reqBox').find('input[name=requirement]:checked').val();
    bloodRequisitionInfo.ReqType_Remark1 = $('.reqBox').find('input[name=requirement]:checked').closest('label').siblings('.remark1').text();
    bloodRequisitionInfo.ReqType_Remark2 = $('.reqBox').find('input[name=requirement]:checked').closest('label').siblings('.remark2').text();
    bloodRequisitionInfo.ReqByDoctorId = $('#ddlDoctorName option:selected').val();
    bloodRequisitionInfo.login_id = Active.userId;
    bloodRequisitionInfo.Logic = 'Insert';

    objBO.bloodRequisitionInfo = bloodRequisitionInfo;
    objBO.componentInfo = selectedComponent;
    debugger
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data);
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
function doctorSign(val) {
    $('#imgDoctorSign').prop('src', val);
}
function getInfo(elem) {
    selectRow(elem)
    var info = JSON.parse($(elem).closest('tr').find('td:eq(0)').text());
    $('#txtPatientName').val(info.PatientName);
    $('#txtAge').val(info.ageInfo);
    $('#txtGender').val(info.Gender);
    $('#txtAddress').val('');
    $('#txtUHID').val(info.UHID);
    $('#txtIPDNo').val(info.IPDNo);
    $('#txtWard').val(info.RoomType);
    $('#txtBed').val(info.BedNo);
    $('#txtHospital').val('Chandan Hospital(Private) Reg. No. 0915700044');
    $('#txtOtherHospital').val('OPD');
    $('#txtConsultant').val(info.DoctorName);
    $('#txtConsultant').val(info.DoctorName);
}