var _ActivePageName;
var _IPDNo;
var _PatientName;
$(document).ready(function () {
    FloorAndPanelList();
    $('select').select2();
    FillCurrentDate('txtSearchFrom');
    FillCurrentDate('txtSearchTo');
    FillCurrentDate('txtApprDate');
   _ActivePageName = window.location.pathname.split('/')[3].toLowerCase()
});
function FloorAndPanelList() {
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
    objBO.Logic = 'FloorAndPanelList';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $('#ddlFloor').append($('<option></option>').val('ALL').html('ALL')).select2();
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlFloor').append($('<option></option>').val(val.FloorName).html(val.FloorName));
                    });
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table1).length) {
                    $('#ddlPanel').append($('<option></option>').val('ALL').html('ALL')).select2();
                    $.each(data.ResultSet.Table1, function (key, val) {
                        $('#ddlPanel').append($('<option></option>').val(val.PanelId).html(val.PanelName));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function AdmittedPatientList(logic) {
    $('#tblServiceRegister tbody').empty();
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '';
    objBO.Floor = $('#ddlFloor option:selected').val();
    objBO.PanelId = $('#ddlPanel option:selected').val();
    objBO.from = $('#txtSearchFrom').val();
    objBO.to = $('#txtSearchTo').val();
    objBO.Prm1 = '';
    objBO.Prm2 = '';
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
                    $.each(data.ResultSet.Table, function (key, val) {                        
                        if (val.PaymentStatus =='Zero-Advance')
                            tbody += "<tr style='background:#ffb9b9'>";
                        else if (val.PaymentStatus == 'Below-Threshold')
                            tbody += "<tr style='background:#f2fba6'>";
                        else
                            tbody += "<tr>";
                        tbody += "<td><button onclick=SelectPatient(this) class='btn btn-warning btn-xs'><i class='fa fa-sign-in'></i></button></td>";
                        tbody += "<td>" + val.UHID + "</td>";
                        tbody += "<td>" + val.IPDNo+"</td>";
                        tbody += "<td>" + val.patient_name+"</td>";
                        tbody += "<td>" + val.ageInfo+"</td>";
                        tbody += "<td>" + val.AdmitDate+"</td>";
                        tbody += "<td>" + val.roomFullName+"</td>";
                        tbody += "<td>" + val.PanelName+"</td>";
                        tbody += "<td>" + val.DepartmentName+"</td>";
                        tbody += "</tr>";
                    });
                    $('#tblServiceRegister tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

function IPD_TPApprovalEntry() {

    if (parseFloat($('#txtAmount').val()) <= 0) {
        alert("Amount should be greater than Zero")
        return;
    }
    var FilePath = $('#docFile').val();
    var FileExtension = GetFileExtension(FilePath)

    var url = config.baseUrl + "/api/IPDBilling/IPD_TPApprovalEntry";
    var objBO = {};
    objBO.AutoId = 0;
    objBO.IPDNo = _IPDNo;
    objBO.ApprovalDate = $('#txtApprDate').val();
    objBO.ApprovalType = $('#ddlApprType option:selected').val();
    objBO.ApprovalAmount = $('#txtAmount').val();
    objBO.CoPayAmount = $('#txtCoPayAmt').val();
    objBO.Discount = $('#txtDiscount').val();
    objBO.ClaimNo = $('#txtClaimNo').val();
    objBO.Remark = $('#txtRemark').val();
    objBO.login_id = Active.userId;
    objBO.FileExtension = FileExtension;
    objBO.Logic = "Insert";

    var data = new FormData();
    data.append('obj', JSON.stringify(objBO));
    data.append('ImageByte',$('input[id=docFile]')[0].files[0]);

    var UploadDocumentInfo = new XMLHttpRequest();
    UploadDocumentInfo.upload.addEventListener('progress', function (e) {
        $('#LineLoader').show();
        var val = (e.loaded / e.total) * 100;
        var percent = val.toFixed(0);
        //$('#UploadingStatus').css('width', '' + percent + '%').text(percent + '%');
        if (percent == 100) {
            //$('#UploadingStatus').text('Uploading Completed');
            $('#LineLoader').hide();
        }
    });
    UploadDocumentInfo.onreadystatechange = function () {
        if (UploadDocumentInfo.status) {
            if (UploadDocumentInfo.status == 200 && (UploadDocumentInfo.readyState == 4)) {
                var json = JSON.parse(UploadDocumentInfo.responseText);
                alert(json);
                TPAApprovalList()
             
            }
        }
    }
    UploadDocumentInfo.open('POST', url, true);
    UploadDocumentInfo.send(data);
}

function SelectPatient(element) {
    _IPDNo = $(element).closest('tr').find('td:eq(2)').text();
    _PatientName= $(element).closest('tr').find('td:eq(3)').text();
    TPAApprovalList();
}

function TPAApprovalList() {
    $('#tblTpaApprInfo tbody').empty();
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
    objBO.Prm1 = "";
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'TPAApprovalListByIpdNo';
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
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#txtNetAmt').val(val.NetAmount.toFixed(2));
                        $('#txtAdvanceAmt').val(val.AdvanceAmount.toFixed(2));
                        $('#txtBalanceAmt').val(val.BalanceAmount.toFixed(2));
                        $('#txtApprovalAmt').val(val.ApprovalAmount.toFixed(2));
                        $('#txtPatientName').val(_IPDNo+" : "+_PatientName);
                        
                    });
                    $.each(data.ResultSet.Table1, function (key, val) {
                        if (val.IsRejected == 'Y')
                            tbody += "<tr style='background:#ffaa52'>";
                        else
                            tbody += "<tr>";
                        tbody += "<td><button onclick=RejectEntry('" + val.auto_id + "') class='btn btn-danger btn-xs'><i class='fa fa-trash'></i></button></td>";
                        tbody += "<td>" + val.ApprovalDate + "</td>";
                        tbody += "<td>" + val.ApprovalType + "</td>";
                        tbody += "<td>" + val.ClaimNo + "</td>";
                        tbody += "<td>" + val.ApprovalAmount + "</td>";
                        tbody += "<td>" + val.CoPayAmount + "</td>";
                        tbody += "<td>" + val.Discount + "</td>";
                        tbody += "<td>" + val.Remark + "</td>";
                        tbody += "<td>" + val.emp_name + "</td>";
                        tbody += "<td>" + val.cr_date + "</td>";
                        if (val.doc_path=='N/A')
                            tbody += "<td></td>";
                        else
                            tbody += "<td><a target='_blank' href='" + config.documentServerUrl +"/" +val.doc_path + "' > View Document <a/> </td>";
                        tbody += "</tr>";
                    });
                    $('#tblTpaApprInfo tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function RejectEntry(AutoId) {
    var url = config.baseUrl + "/api/IPDBilling/IPD_TPApprovalRejectEntry";
    var objBO = {};
    objBO.AutoId = AutoId;
    objBO.IPDNo = _IPDNo;
    objBO.ApprovalDate = $('#txtApprDate').val();
    objBO.ApprovalType = $('#ddlApprType option:selected').val();
    objBO.ApprovalAmount = "0";
    objBO.CoPayAmount = "0";
    objBO.ClaimNo = "-";
    objBO.Remark = "-";
    objBO.login_id = Active.userId;
    objBO.Logic = "RejectEntry";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data.includes('Success'))
                TPAApprovalList();
            else
                alert(data);
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}