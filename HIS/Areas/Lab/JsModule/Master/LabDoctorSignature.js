var _signid = '';
var _doctorId = '';
var _docName = '';
var _degree = '';
$(document).ready(function () {
    Onload();
    $("#Uploadsign").change(function () {
        readURL(this);
    });
});
function Onload() {
    $("#ddlDepartment").empty().append($("<option></option>").val("ALL").html("ALL")).select2();
    var url = config.baseUrl + "/api/Lab/mDoctorSignatireQueries";
    var objBO = {};
    objBO.Logic = "OnLoad";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, value) {
                        $("#ddlDepartment").append($("<option></option>").val(value.DeptId).html(value.DepartmentName));
                    });
                }
                GetDoctors();
            }
            else {
                MsgBox('No Data Found')
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetDoctors() {
    $("#tblDoctors tbody").empty();
    var url = config.baseUrl + "/api/Lab/mDoctorSignatireQueries";
    var objBO = {};
    objBO.deptid = $('#ddlDepartment option:selected').val();
    objBO.Logic = "GetDoctorsByDepartment";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var htmldata = "";
            var temp = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (k, val) {
                        if (temp != val.DepartmentName) {
                            htmldata += "<tr style='background:#fff9ce'>";
                            htmldata += "<td colspan='4'><b>" + val.DepartmentName + "</b></td>";
                            htmldata += "</tr>";
                            temp = val.DepartmentName;
                        }
                        if (val.SignVirtualPath != null)
                            htmldata += "<tr style='background: #e1ffdd;'>";
                        else
                            htmldata += "</tr>";

                        htmldata += "<td class='hide'>" + config.documentDownloadServerUrl+ val.SignVirtualPath + "</td>";
                        htmldata += "<td>" + val.DoctorId + "</td>";
                        if (val.SignVirtualPath != null)
                            htmldata += "<td>" + val.DoctorName + "<i class='fa fa-check-circle pull-right text-success' style='font-size: 13px;'>&nbsp;</i></td>";
                        else
                            htmldata += "<td>" + val.DoctorName + "</td>";

                        htmldata += "<td>" + val.degree + "</td>";
                        htmldata += "<td class='hide'>" + val.SignId + "</td>";
                        htmldata += "<td><button onclick=selectDoctor(this) class='btn btn-warning btn-xs'><i class='fa fa-sign-in'>&nbsp;</i></button></td>";
                        htmldata += "</tr>";
                    });
                    $("#tblDoctors tbody").append(htmldata);
                }
            }
            else {
                MsgBox('No Data Found')
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function selectDoctor(elem) {
    selectRow(elem);
    var sign = $(elem).closest('tr').find('td:eq(0)').text();
    _doctorId = $(elem).closest('tr').find('td:eq(1)').text();
    _docName = $(elem).closest('tr').find('td:eq(2)').text();
    _degree = $(elem).closest('tr').find('td:eq(3)').text();
    _signid = $(elem).closest('tr').find('td:eq(4)').text();
    $('.docInfo').html('Doctor Name : <span style="color: #14871d;">' + _docName + '</span>').show();

    if (sign.length > 6)
        $('#imgSign').prop('src', sign);
    else
        $('#imgSign').prop('src', 'https://exprohelp.com/his/images/uploadIcon.jpg');
}
function UploadSign() {
    var objBO = {};
    var url = config.baseUrl + "/api/Lab/UploadDoctorSignature";
    if (_doctorId.length < 3) {
        alert('Please Select Doctor');
        return;
    }
    objBO.signid = _signid;
    objBO.doctorid = _doctorId;
    objBO.doctorname = _docName;
    objBO.degree = _degree;
    objBO.photo_path = ($('#imgSign').attr('src').length > 40) ? 'Y' : 'N';
    objBO.Base64String = $('#imgSign').attr('src');    
    objBO.login_id = Active.userId;
    objBO.Logic = (_signid.length < 6) ? 'Insert' : 'Update';
    console.log(objBO)
    var UploadDocumentInfo = new XMLHttpRequest();
    var data = new FormData();
    data.append('obj', JSON.stringify(objBO));
    data.append('ImageByte', objBO.Base64String);
    UploadDocumentInfo.onreadystatechange = function () {
        if (UploadDocumentInfo.status) {
            if (UploadDocumentInfo.status == 200 && (UploadDocumentInfo.readyState == 4)) {
                var json = JSON.parse(UploadDocumentInfo.responseText);
                if (json.Message.includes('Success')) {                   
                    alert('Successfully Uploaded..!');     
                    GetDoctors();
                    Clear();
                }
                else {
                    alert(json.Message);
                }
            }
        }
    }
    UploadDocumentInfo.open('POST', url, true);
    UploadDocumentInfo.send(data);
}
function Clear() {
    _signid = '';
    _doctorId = '';
    _docName = '';
    _degree = '';
    $('#imgSign').prop('src', 'https://exprohelp.com/his/images/uploadIcon.jpg');
}
function readURL(input) {
    if (input.files && input.files[0]) {
        var ext = $('#uploadSign').val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'bmp']) == -1) {
            alert('invalid fileextension!');
            return false;
        }
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imgSign').removeAttr('src', '');
            $('#imgSign').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]); // convert to base64 string
        var formData = new FormData();
        var files = $('#uploadSign').get(0).files;
    }
}
