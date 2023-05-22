var _BillNo = "";
var _photo_url = null;
$(document).ready(function () {
    let camera_button = document.querySelector("#start-camera");
    let video = document.querySelector("#video");
    let click_button = document.querySelector("#click-photo");
    let canvas = document.querySelector("#canvas");
    $('#ImgCaptured').hide();
    $('select').select2();
    FillCurrentDate('txtDOB')
    OnLoad();

    var toggleButton = "<button class='btn btn-warning btn-xs pull-right fullSlide'><i class='fa fa-expand'></i></button>";
    $('.section:eq(3) .title').append(toggleButton);
    $('.section .fullSlide').on('click', function () {
        $('.section').slideToggle('slow');
        $(this).parents('.section').slideToggle('slow');
        $('.Questionnair').toggleClass('fullHeight');
        $(this).parents('.section').toggleClass('fullHeight');
    });
    $(document).on('blur', 'input[id=txtBP]', function () {
        var val = $(this).val();
        if (!Number(val.split('/')[1]) ? true : false) {
            alert('Invalid BP Value');
            $('#txtBP').css('border-color', 'red');
        }
        else {
            $('#txtBP').removeAttr('style');
        }
    });
    $(document).on('change', 'input[name=gender]', function () {
        var val = $('input[name=gender]:checked').val();
        (val == 'Male') ? $('.F').addClass('hide') : $('.F').removeClass('hide');
    });
    $('#start-camera').on('click', async function () {
        let stream = await navigator.mediaDevices.getUserMedia({ video: true, audio: false });
        video.srcObject = stream;
        $('#ImgCaptured').hide("slide", { direction: "right" }, 500, function () {
            $('#liveCamera').show("slide", { direction: "left" }, 500);
        });
        $('#liveCamera label').text('Recording..');
    });
    $('#click-photo').on('click', function () {
        canvas.getContext('2d').drawImage(video, 0, 0, canvas.width, canvas.height);
        let image_data_url = canvas.toDataURL('image/jpeg');
        $('#liveCamera').hide("slide", { direction: "left" }, 500, function () {
            $('#ImgCaptured').show("slide", { direction: "right" }, 500);
        });
        _photo_url = image_data_url;
    });
});

function OnLoad() {
    $('.Questionnair').empty();
    var url = config.baseUrl + "/api/BloodBank/BB_SelectQueries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.DonorId = '-';
    objBO.VisitId = '-';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "OnLoad";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var Question = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (val.Gender == 'F')
                            Question += "<div class='QuestionList hide " + val.Gender + "' data-type=" + val.Gender + ">";
                        else
                            Question += "<div class='QuestionList' data-type=" + val.Gender + ">";

                        Question += "<label class='hide'>" + val.QuestId + "</label>";
                        Question += "<label class='lblQuest'>" + val.Question + "</label>";
                        if (val.QType != 'NA') {
                            Question += "<label><input type='radio' name='" + val.QuestId + "' value='Yes' checked />&nbsp;Yes</label>";
                            Question += "<label><input type='radio' name='" + val.QuestId + "' value='No' />&nbsp;No</label>";
                            Question += "<label class='lblRemark'>Remark : </label>";
                            Question += "<input type='text'' class='form-control remark' placeholder='Reamrk..' />";
                        }
                        Question += "</div>";
                    });
                    $('.Questionnair').append(Question);
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table1).length) {
                    $('#ddlBloodGroup').empty().append($('<option></option>').val('NA').html('NA')).select2();
                    $.each(data.ResultSet.Table1, function (key, val) {
                        $('#ddlBloodGroup').append($('<option></option>').val(val.BloodGroupId).html(val.BloodGroupAllotted));
                    });
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table2).length) {
                    $('#ddlCountry').empty().append($('<option></option>').val('Select').html('Select')).select2();
                    $.each(data.ResultSet.Table2, function (key, val) {
                        $('#ddlCountry').append($('<option></option>').val(val.CountryID).html(val.contry_Name));
                    });
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table3).length) {
                    $('#ddlCity').empty().append($('<option></option>').val('Select').html('Select')).select2();
                    $.each(data.ResultSet.Table3, function (key, val) {
                        $('#ddlCity').append($('<option></option>').val(val.dist_code).html(val.distt_name));
                    });
                }
            }
        },
        complete: function () {
            $('#ddlCountry').val(14).change();
            $('#ddlCity').val(45).change();
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function SearchDonor() {
    $('#tblDonorSearchInfo tbody').empty();
    var url = config.baseUrl + "/api/BloodBank/BB_SelectQueries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.DonorId = '-';
    objBO.VisitId = '-';
    objBO.Prm1 = $('#ddlSearchInput option:selected').text();
    objBO.Prm2 = $('#txtSearchValue').val();
    objBO.login_id = Active.userId;
    objBO.Logic = "SearchDonor";
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
                    if (data.ResultSet.Table.length > 0) {
                        $.each(data.ResultSet.Table, function (key, val) {
                            tbody += "<tr>";
                            tbody += "<td>" + val.donor_id + "</td>";
                            tbody += "<td>" + val.donorName + "</td>";
                            tbody += "<td>" + val.Gender + "</td>";
                            tbody += "<td>" + val.DOB + "</td>";
                            tbody += "<td>" + val.contactNo + "</td>";
                            tbody += "<td>" + val.aadharNo + "</td>";
                            tbody += "<td><button class='btn btn-success btn-xs'>Select</button></td>";
                            tbody += "</tr>";
                        });
                        $('#tblDonorSearchInfo tbody').append(tbody);
                    }
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function FinalSubmit() {
    if (Validate()) {
        if (confirm('Are you sure?')) {
            var url = config.baseUrl + "/api/BloodBank/SaveDonorInfo";
            var objDonorInfo = {};
            var objDonorAnswers = [];

            $('.Questionnair .QuestionList').each(function () {
                objDonorAnswers.push({
                    'QuestId': $(this).find('label.hide').text(),
                    'Answer': $(this).find('input[type=radio]:checked').val(),
                    'Remarks': $(this).find('input.remark').val(),
                });
            });
            objDonorInfo.Hosp_Id = Active.unitId;
            objDonorInfo.Donor_Id = $('#txtDonorId').val();
            objDonorInfo.Title = $('#ddlTitle option:selected').text();
            objDonorInfo.Dfirstname = $('#txtFirstName').val();
            objDonorInfo.Dlastname = $('#txtLastName').val();
            objDonorInfo.Dtbirth = $('#txtDOB').val();
            objDonorInfo.Dob = $('#txtDOB').val();
            objDonorInfo.Gender = $('input[name=gender]:checked').val();
            objDonorInfo.Relative_Name = $('#txtRelativeName').val();
            objDonorInfo.Relation = $('#ddlRelation option:selected').text();
            objDonorInfo.BloodGroupId = $('#ddlBloodGroup option:selected').val();
            objDonorInfo.Donor_Address = $('#txtAddress').val();
            objDonorInfo.Country = $('#ddlCountry option:selected').text();
            objDonorInfo.City = $('#ddlCity option:selected').text();
            objDonorInfo.Pincode = $('#txtPinCode').val();
            objDonorInfo.Contactno = $('#txtContactNo').val();
            objDonorInfo.Aadharno = $('#txtAadharNo').val();
            objDonorInfo.Email = $('#txtEmail').val();
            objDonorInfo.BloodDonate = $('#ddlHowOften option:selected').val();
            objDonorInfo.Entryby = Active.userId;
            objDonorInfo.Isrevisit = 0;
            objDonorInfo.Blood_Pressure = $('#txtBP').val();
            objDonorInfo.Weight = $('#txtWeight').val();
            objDonorInfo.Pulse = $('#txtPulse').val();
            objDonorInfo.Gpe = $('#txtGPE').val();
            objDonorInfo.Height = $('#txtHeight').val();
            objDonorInfo.Temprature = $('#txtTemp').val();
            objDonorInfo.Hb = $('#ddlHemoglobin option:selected').val();
            objDonorInfo.isFit = $('input[name=Fit]:checked').val();
            objDonorInfo.DonationType = $('#ddlDonationType option:selected').val();
            objDonorInfo.DonorType = $('#ddlDonorType option:selected').text();
            objDonorInfo.uhid = '-';
            objDonorInfo.Platelet = $('#txtPlateletCount').val();
            objDonorInfo.Remark = $('#txtRemark').val();
            objDonorInfo.objDonorAnswers = objDonorAnswers;
            objDonorInfo.Base64String = _photo_url;
            objDonorInfo.photo_path = (_photo_url == null) ? 'N' : 'Y';
            objDonorInfo.Logic = 'New';

            var UploadDocumentInfo = new XMLHttpRequest();
            var data = new FormData();
            data.append('obj', JSON.stringify(objDonorInfo));
            data.append('ImageByte', objDonorInfo.Base64String);
            UploadDocumentInfo.onreadystatechange = function () {
                if (UploadDocumentInfo.status) {
                    if (UploadDocumentInfo.status == 200 && (UploadDocumentInfo.readyState == 4)) {
                        var json = JSON.parse(UploadDocumentInfo.responseText);
                        if (json.Message.includes('Success')) {
                            var res = json.Message.split("|");
                            alert(res[0]);
                            _photo_url = null;
                            $('#ImgCaptured').hide("slide", { direction: "right" }, 500, function () {
                                $('#liveCamera').show("slide", { direction: "left" }, 500);
                            });
                            $('#liveCamera label').text('Start Camera.');
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
    }
}
function Validate() {
    var FirstName = $('#txtFirstName').val();
    var LastName = $('#txtLastName').val();
    var DOB = $('#txtDOB').val();
    var Age = $('#txtAge').val();
    var Address = $('#txtAddress').val();
    var ContactNo = $('#txtContactNo').val();   
    var Hemoglobin = $('#ddlHemoglobin option:selected').text();
    var Weight = $('#txtWeight').val();
    var Temp = $('#txtTemp').val();
    var Pulse = $('#txtPulse').val();
    var GPE = $('#txtGPE').val();
    var PlateletCount = $('#txtPlateletCount').val();

    if (FirstName == '') {
        alert('Please Provide First Name.');
        $('#txtFirstName').css('border-color', 'red');
        return false;
    }   
    else {
        $('#txtFirstName').removeAttr('style');
    }
    if (LastName == '') {
        alert('Please Provide Last Name.');
        $('#txtLastName').css('border-color', 'red');
        return false;
    }
    else {
        $('#txtLastName').removeAttr('style');
    }
    if (DOB == '') {
        alert('Please Provide DOB.');
        $('#txtDOB').css('border-color', 'red');
        return false;
    }
    else {
        $('#txtDOB').removeAttr('style');
    }
    if (Age=='') {
        alert('Please Provide Age.');
        $('#txtAge').css('border-color', 'red');
        return false;
    }
   else if (parseFloat(Age) < 18) {
        alert('Age should not be less then 18 Years');
        $('#txtAge').css('border-color', 'red');
        return false;
    }
    else {
        $('#txtAge').removeAttr('style');
    }
    if (Address == '') {
        alert('Please Provide Address.');
        $('#txtAddress').css('border-color', 'red');
        return false;
    }
    else {
        $('#txtAddress').removeAttr('style');
    }
    if (ContactNo == '') {
        alert('Please Provide ContactNo.');
        $('#txtContactNo').css('border-color', 'red');
        return false;
    }
    else if (eval(ContactNo) < 10) {
        alert('Contact No should be 10 digit.');
        $('#txtContactNo').css('border-color', 'red');
        return false;
    }
    else {
        $('#txtContactNo').removeAttr('style');
    }  
    if (!Number($('#txtBP').val().split('/')[1]) ? true : false) {
        alert('Invalid BP Value');
        $('#txtBP').css('border-color', 'red');
        return false;
    }
    else {
        var sys = $('#txtBP').val().split('/')[0];
        var dias = $('#txtBP').val().split('/')[1];
        if (parseFloat(sys) < 100) {
            alert('Systolic Value should not be less then 100');
            $('#txtBP').css('border-color', 'red');
            return false;
        }
        if (parseFloat(dias) < 60) {
            alert('Diastolic Value should not be less then 60');
            $('#txtBP').css('border-color', 'red');
            return false;
        }
        $('#txtBP').removeAttr('style');
    }
    if (Weight == '') {
        alert('Please Provide Weight.');
        $('#txtWeight').css('border-color', 'red');
        return false;
    }
    else if (parseFloat(Weight) < 42) {
        alert('Weight should not be less then 42');
        $('#txtWeight').css('border-color', 'red');
        return false;
    }
    else {
        $('#txtWeight').removeAttr('style');
    }
    if (Temp == '') {
        alert('Please Provide Temperature.');
        $('#txtTemp').css('border-color', 'red');
        return false;
    }
    else if (parseFloat(Temp) < 99.5) {
        alert('Temperature should not be less then 99.5');
        $('#txtTemp').css('border-color', 'red');
        return false;
    }
    else {
        $('#txtTemp').removeAttr('style');
    }
    if (Pulse == '') {
        alert('Please Provide Pulse.');
        $('#txtPulse').css('border-color', 'red');
        return false;
    }
    else if (parseFloat(Pulse) < 60) {
        alert('Pulse should not be less then 60');
        $('#txtPulse').css('border-color', 'red');
        return false;
    }
    else {
        $('#txtPulse').removeAttr('style');
    }
    if (GPE == '') {
        alert('Please Provide GPE.');
        $('#txtGPE').css('border-color', 'red');
        return false;
    }
    else {
        $('#txtGPE').removeAttr('style');
    }
    if (PlateletCount == '') {
        alert('Please Provide PlateletCount.');
        $('#txtPlateletCount').css('border-color', 'red');
        return false;
    }
    else {
        $('#txtPlateletCount').removeAttr('style');
    }
    if (Hemoglobin == 'Select') {
        alert('Please Select Hemoglobin.');
        $('span.selection').find('span[aria-labelledby=select2-ddlHemoglobin-container]').css('border-color', 'red').focus();
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlHemoglobin-container]').removeAttr('style');
    }
    return true;
}