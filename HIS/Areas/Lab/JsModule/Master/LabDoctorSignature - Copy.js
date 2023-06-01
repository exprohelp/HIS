
$(document).ready(function () {
    CloseSidebar();
    Onload();    
    $("#Uploadsign").change(function () {
        readURL(this);
    });
});
function readURL(input) {
    if (input.files && input.files[0]) {
        var ext = $('#Uploadsign').val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'bmp']) == -1) {
            alert('invalid fileextension!');
            return false;
        }
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#signimg').removeAttr('src', '');
            $('#signimg').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]); // convert to base64 string
        var formData = new FormData();
        var files = $('#Uploadsign').get(0).files;
        if (files.length > 0) {
            formData.append('Photo', 'Signature');
            formData.append('Imagefile', files[0]);
        }
        $.ajax({
            url: '/UploadImage/FileUpload/UploadFiles',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                $("#hidcombinepath").val(response);
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function Onload() {
    $("#ddlDepartment").empty().append($("<option></option>").val("All").html("All")).select2();
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
    objBO.deptid = $("#ddlDepartment option:selected").val();
    objBO.Logic = "GetDoctorsByDepartment";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var htmldata = "";         
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (k, v) {
                        htmldata += '<tr>';
                        htmldata += '<td>' + v.DoctorName + '</td>';
                        htmldata += '<td>' + v.DoctorName + '</td>';
                        htmldata += '<td><button style="height: 15px;line-height:0" data-doctorid="' + v.DoctorId + '" data-doctorname="' + v.DoctorName + '" data-degree="' + v.degree2 + '" onclick=selectRow(this);ViewDoctorDetails("' + v.DoctorId + '") class="btn btn-warning btn-xs"><i class="fa fa-sign-in">&nbsp;</i></button></td>';                        
                        htmldata += '</tr>';
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
function ViewDoctorDetails(doctorid) { 
    Clear();
    var objBO = {};
    var url = config.baseUrl + "/api/Lab/mDoctorSignatireQueries";
    objBO.doctorid = doctorid;
    objBO.Logic = "DoctorDetails"
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {          
            var htmldata = "";          
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var v = data.ResultSet.Table[0];
                    $("#txtDoctorName").val(v.DoctorName);
                    $("#txtDegree").val(v.degree);
                    $("#hidsignvirpath").val(v.SignVirtualPath);
                    $("#hidsignphypath").val(v.SignPhysicalPath);
                    $("#hidsignid").val(v.SignId);
                    $("#signimg").removeAttr('src', '');
                    htmldata += '<tr>';
                    htmldata += '<td><a href = "javascript:void(0)" data-signid="' + v.SignId + '" onclick = "selectRow(this);DeleteDoctorSignature()"><i class="fa fa-trash fa-lg text-red"></i></a ></td>';
                    htmldata += '<td>' + v.DoctorId + '</td>';
                    htmldata += '<td>' + v.DoctorName + '</td>';
                    htmldata += '<td>' + v.degree + '</td>';
                    if (v.SignVirtualPath == "" || v.SignVirtualPath == null || typeof v.SignVirtualPath == 'undefined') {
                        htmldata += '<td><img src="http://localhost:54687/images/sigreq.jpg" alt="signature"</td>';
                        $('#signimg').attr('src', "http://localhost:54687/images/sigreq.jpg");
                    }
                    else {
                        htmldata += '<td><img src=' + v.SignVirtualPath + ' alt="signature"</td>';
                        $('#signimg').attr('src', v.SignVirtualPath);
                    }

                    htmldata += '</tr>';
                    $("#tblSignatureInfo tbody").append(htmldata);
                    $("#btnSaveUpdate").val('');
                    $("#btnSaveUpdate").removeClass();
                    $("#btnSaveUpdate").val('Update');
                    $("#btnSaveUpdate").addClass('btn btn-success btn-xs pull-right');
                }
                else {                     
                    $('#signimg').attr('src', "http://localhost:54687/images/sigreq.jpg");
                    $("#btnSaveUpdate").val('');
                    $("#btnSaveUpdate").removeClass();
                    $("#btnSaveUpdate").val('Save');
                    $("#btnSaveUpdate").addClass('btn btn-info btn-xs pull-right');
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
function SaveDoctorSignature() {    
    var objBO = {};
    var url = config.baseUrl + "/api/Lab/mDoctorSignatireInsertUpdate";
    var signid = $("#hidsignid").val();
    var combinepath = $("#hidcombinepath").val();
    if (signid == "" || typeof signid == 'undefined' || signid == null) {
        if (combinepath != "") {
            var splitpath = combinepath.split("#");
            objBO.SignVirtualPath = splitpath[0];
            objBO.SignPhysicalPath = splitpath[1];
        }
        else {
            objBO.SignVirtualPath = "-";
            objBO.SignPhysicalPath = "-";
        }
        objBO.signid = "0";
        objBO.Logic = "Insert";
    }
    else {
        if (combinepath != "") {
            var splitpath = combinepath.split("#");
            objBO.SignVirtualPath = splitpath[0];
            objBO.SignPhysicalPath = splitpath[1];
        }
        else {
            objBO.SignVirtualPath = $("#hidsignvirpath").val();
            objBO.SignPhysicalPath = $("#hidsignphypath").val();
        }
        objBO.signid = $("#hidsignid").val();
        objBO.Logic = "Update";
    }
    var doctorid = $("#tblDoctors tbody").find('tr.select-row').find('td:eq(1)').find('button').data('doctorid');
    objBO.signid = doctorid;
    objBO.doctorid = doctorid;
    objBO.doctorname = $("#txtDoctorName").val();
    objBO.degree = $("#txtDegree").val();
    objBO.signvirtualpath = $("#txtDegree").val();
    objBO.signphysicalpath = $("#txtDegree").val();
    objBO.login_id = Active.userId;
    objBO.hosp_id = Active.unitId;
    if (validate()) {
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                if (response == "success") {
                    alert('Signature created successfully');
                    ViewDoctorDetails(doctorid);
                }
                else {
                    alert(response);
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }

}
function DeleteDoctorSignature() {
    debugger;
    var objBO = {};
    var url = config.baseUrl + "/api/Lab/mDoctorSignatireInsertUpdate";
    var signid = $("#hidsignid").val();

    var doctorid = $("#tblDoctors tbody").find('tr.select-row').find('td:eq(0)').find('a').data('doctorid');
    objBO.signid = signid;
    objBO.doctorid = "0";
    objBO.doctorname = "-";
    objBO.degree = "-";
    objBO.SignVirtualPath = "-";
    objBO.SignPhysicalPath = "-"
    objBO.login_id = Active.userId;
    objBO.hosp_id = Active.unitId;
    objBO.Logic = "Delete";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response == "success") {
                alert('Signature deleted successfully');
                ViewDoctorDetails(doctorid);
            }
            else {
                alert(response);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetAllDoctorList() {    
    var objBO = {};
    var url = config.baseUrl + "/api/Lab/mDoctorSignatireQueries";
    objBO.Logic = "DoctorDetails"
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            debugger;
            var htmldata = "";
            $("#tblSignatureInfo tbody").empty();
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (k, v) {
                        htmldata += '<tr>';
                        htmldata += '<td><a href = "javascript:void(0)" data-signid="' + v.SignId + '" onclick = "selectRow(this);DeleteDoctorSignature()"><i class="fa fa-trash fa-lg text-red"></i></a ></td>';
                        htmldata += '<td>' + v.DoctorId + '</td>';
                        htmldata += '<td>' + v.DoctorName + '</td>';
                        htmldata += '<td>' + v.degree + '</td>';
                        if (v.SignVirtualPath == "" || v.SignVirtualPath == null || typeof v.SignVirtualPath == 'undefined') {
                            htmldata += '<td><img src="http://localhost:54687/images/sigreq.jpg" alt="signature"</td>';
                        }
                        else {
                            htmldata += '<td><img src=' + v.SignVirtualPath + ' alt="signature"</td>';
                            //$('#signimg').attr('src', v.SignVirtualPath);
                        }
                        htmldata += '</tr>';
                    })
                    $("#tblSignatureInfo tbody").append(htmldata);
                }
                else {
                    debugger;
                    htmldata += '<tr>';
                    htmldata += '<td colspan="10" class="text-center text-red"> No Data Found</td>';
                    htmldata += '</tr>';
                    $("#tblSignatureInfo tbody").append(htmldata);
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
function validate() {
    var docname = $("#txtDegree").val();
    if (docname == "") {
        alert('please enter doctor name');
        return false;
    }
    return true;
}
function Clear() {
    $("#tblSignatureInfo tbody").empty();
    $("#txtDoctorName").val('');
    $("#txtDegree").val('');
    $("#hidsignvirpath").val('');
    $("#hidsignphypath").val('');
    $("#hidsignid").val('');
}
