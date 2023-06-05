var _photo_url = null;
$(document).ready(function () {
    $('.patientinfo-section-1,.patientinfo-section-2').addClass('blockUI');
    FillCurrentDate('txtOnlineBookingDate')
    let canvas = document.querySelector("#canvas");
    $('#ImgCaptured').hide();
   // $('#ImgCaptured').hide();

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
    $('#btnOldPatient').on('click', function () {
        $('#tblOldPatient tbody').empty();
        $('#txtSearchValue').val('');
        $('#modalOldPatient').modal('show');
    });
    $('#btnSearchOldPatient').on('click', function () {
        GetOldPatient();
    });
    $('select').select2();
    FillCurrentDate("txtAppointmentOn");
    $('#ddlCountry').on('change', function () {
        var cId = $(this).val();
        GetStateByCountry(cId, 'N');
    });
    $('#txtDOB').on('change', function () {
        var currentDate = new Date().getFullYear();
        var dob = $(this).val();
        var year = dob.split('-');
        var age = parseInt(currentDate) - parseInt(year[0]);
        $('#txtAge').val(age);
    });
    $('#ddlState').on('change', function () {
        var sId = $(this).val();
        GetCityByState(sId, 'N');
    });
    $('#ddlReferralState').on('change', function () {
        var sId = $(this).val();
        GetCityByState(sId, 'N');
    });

    $('#tblOldPatient tbody').on('click', 'button', function () {
        var uhid = $(this).closest('tr').find('td:eq(1)').text();
        $('#BasicInformation').find('.infosection').removeClass('blockInfo');
        GetOldPatientByUHID(uhid);
    });
    $('#tblOnlinePatientBooking tbody').on('click', 'button', function () {
        var info = JSON.parse($(this).closest('tr').find('td:eq(1)').text());
        FillOnlinePatientInfo(info);
        $('#modalOnlinePatient').hide();
    });
    $('#btnNewPatient').on('click', function () {
        $('#BasicInformation').find('.infosection').removeClass('blockInfo');
        $('#txtUHID').val('New');
        $('#txtMobileNo').val($('#txtSearchValue').val());
        $('#txtSearchValue').val('');
        $('#modalOldPatient').modal('hide');
        $('#btnNewPatient').hide();
        $('.patientinfo-section-1,.patientinfo-section-2').removeClass('blockUI');
    });
    $('#btnPrintReceipt').on('click', function () {
        var tnxid = $('#txtTnxIdForPrint').text();
        Receipt(tnxid);
    });
    GetCountry();
});
function FillOnlinePatientInfo(val) {
    $('#txtUHID').val(val.UHID);
    $('#txtFirstName').val(val.patient_name);
    $('#ddlGender option').map(function () {
        if ($(this).text() == val.gender) {
            $('#ddlGender').prop('selectedIndex', '' + $(this).index() + '').change();
        }
    });
    $('#txtMobileNo').val(val.mobile_no);
    if (val.dob != null) {
        var d = val.dob.split('-');
        var newDOB = d[0] + '-' + d[1] + '-' + d[2];
        $('#txtDOB').val(newDOB).change();
    }
    $('#txtAge').val(val.age);
    $('#ddlAgeType option').map(function () {
        if ($(this).text() == val.agetype) {
            $('#ddlAgeType').prop('selectedIndex', '' + $(this).index() + '').change();
        }
    });
    $('#txtAddress').val(val.pt_address);
    $('#txtEmailId').val(val.email_id);
}
function GetOldPatientByUHID(uhid) {
    var url = config.baseUrl + "/api/Appointment/Patient_MasterQueries";
    var objBO = {};
    var date = new Date();
    objBO.hosp_id = Active.unitId;
    objBO.UHID = uhid;
    objBO.MobileNo = '-';
    objBO.SearcKey = '-';
    objBO.SearchValue = '-';
    objBO.from = date;
    objBO.to = date;
    objBO.prm_1 = '-';
    objBO.prm_2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetOldPatientByUHID';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.ResultSet.Table.length > 0) {
                $.each(data.ResultSet.Table, function (key, val) {
                    $('#txtBarcode').val(val.barcodeno);
                    $('#txtUHID').val(val.UHID);
                    $('#ddlTitle option').map(function () {
                        if ($(this).text() == val.Title) {
                            $('#ddlTitle').prop('selectedIndex', '' + $(this).index() + '').change();
                        }
                    });
                    $('#txtFirstName').val(val.FirstName);
                    $('#txtLastName').val(val.LastName);
                    $('#ddlGender option').map(function () {
                        if ($(this).text() == val.gender) {
                            $('#ddlGender').prop('selectedIndex', '' + $(this).index() + '').change();
                        }
                    });
                    $('#txtMobileNo').val(val.mobile_no);
                    if (val.dob != null) {
                        var d = val.dob.split('-');
                        var newDOB = d[0] + '-' + d[1] + '-' + d[2];
                        $('#txtDOB').val(newDOB).change();
                    }
                    $('#txtAge').val(val.age);
                    $('#ddlAgeType option').map(function () {
                        if ($(this).text() == val.age_type) {
                            $('#ddlAgeType').prop('selectedIndex', '' + $(this).index() + '').change();
                        }
                    });
                    //$('#ddlCountry option').map(function () {
                    //	if ($(this).text() == val.country) {
                    //		$('#ddlCountry').prop('selectedIndex', '' + $(this).index() + '').change();
                    //	}
                    //});
                    $('#ddlState option').map(function () {
                        if ($(this).data('text') == val.state) {
                            $('#ddlState').val($(this).val()).change();
                            GetCityByState($(this).val(), 'N');
                        }
                    });
                    $('#txtAddress').val(val.address);
                    $('#txtEmailId').val(val.email_id);
                    $('#txtMembershipNo').val(val.member_id);
                    $('#ddlMaritalStatus option').map(function () {
                        if ($(this).text() == val.marital_status) {
                            $('#ddlMaritalStatus').prop('selectedIndex', '' + $(this).index() + '').change();
                        }
                    });
                    $('#ddlReligion option').map(function () {
                        if ($(this).text() == val.religion) {
                            $('#ddlReligion').prop('selectedIndex', '' + $(this).index() + '').change();
                        }
                    });
                    $('#ddlIdProof option').map(function () {
                        if ($(this).text() == val.idType) {
                            $('#ddlIdProof').prop('selectedIndex', '' + $(this).index() + '').change();
                        }
                    });
                    $('#txtIdNo').val(val.IDNo);
                    $('#ddlRelationOf option').map(function () {
                        if ($(this).text() == val.relation_of) {
                            $('#ddlRelationOf').prop('selectedIndex', '' + $(this).index() + '').change();
                        }
                    });
                    $('#txtRelationName').val(val.relation_name);
                    $('#txtRelationContact').val(val.relation_phone);
                    $('#modalOldPatient input:text').val('');
                    $('#modalOldPatient').modal('hide');
                    $('.patientinfo-section-2').removeClass('blockUI');
                });
            }
        },
        complete: function (com) {
            $.each(com.responseJSON.ResultSet.Table, function (key, val) {
                $('#ddlCity option').map(function () {
                    if ($(this).data('text') == val.district) {
                        $('#ddlCity').val($(this).val()).change();
                    }
                });
            });
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetBookingByAppNo(appno) {
    var url = config.baseUrl + "/api/Appointment/Patient_MasterQueries";
    var objBO = {};
    var date = new Date();
    objBO.MobileNo = '-';
    objBO.SearcKey = '-';
    objBO.SearchValue = appno;
    objBO.from = date;
    objBO.to = date;
    objBO.Logic = 'GetBookingByAppNo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.ResultSet.Table.length > 0) {
                $.each(data.ResultSet.Table, function (key, val) {
                    $('#txtUHID').val(val.UHID);
                    $('#txtFirstName').val(val.patient_name);
                    $('#ddlGender option').map(function () {
                        if ($(this).text() == val.gender) {
                            $('#ddlGender').prop('selectedIndex', '' + $(this).index() + '').change();
                        }
                    });
                    $('#txtMobileNo').val(val.mobile_no);
                    $('#txtAge').val(val.age);
                    $('#ddlAgeType option').map(function () {
                        if ($(this).text() == val.age_type) {
                            $('#ddlAgeType').prop('selectedIndex', '' + $(this).index() + '').change();
                        }
                    });
                    //$('#ddlVisitType option').map(function () {
                    //	if ($(this).text() == val.visitType) {
                    //		$('#ddlVisitType').prop('selectedIndex', '' + $(this).index() + '').change();
                    //	}
                    //});
                });
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetOldPatient() {
    var url = config.baseUrl + "/api/Appointment/Patient_MasterQueries";
    var objBO = {};
    var date = new Date();
    objBO.hosp_id = Active.unitId;
    objBO.UHID = '-';
    objBO.MobileNo = '-';
    objBO.SearcKey = $('#ddlSearchKey option:selected').val();
    objBO.SearchValue = $('#txtSearchValue').val();
    objBO.from = date;
    objBO.to = date;
    objBO.prm_1 = '-';
    objBO.prm_2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetOldPatient';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#tblOldPatient tbody').empty();
            var tbody = "";
            if (data.ResultSet.Table.length > 0) {
                $('#btnNewPatient').show();
                $.each(data.ResultSet.Table, function (key, val) {
                    tbody += "<tr>";
                    tbody += "<td><button class='btn-success btn-flat'>Select</button></td>";
                    tbody += "<td>" + val.UHID + "</td>";
                    tbody += "<td>" + val.patient_name + "</td>";
                    tbody += "<td>" + val.gender + "</td>";
                    tbody += "<td>" + val.dob + "</td>";
                    tbody += "<td>" + val.age + "</td>";
                    tbody += "<td>" + val.mobile_no + "</td>";
                    tbody += "<td>" + val.address + "</td>";
                    tbody += "<td>" + val.cr_date + "</td>";
                    tbody += "</tr>";
                });
                $('#tblOldPatient tbody').append(tbody);
            }
            else {
                $('#btnNewPatient').show();
                tbody += "<tr>";
                tbody += "<td class='text-danger text-center' colspan='8'>Patient Record Not Found..</td>";
                tbody += "</tr>";
                $('#tblOldPatient tbody').append(tbody);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetOnlinePatient() {
    $('#tblOnlinePatientBooking tbody').empty();
    var url = config.baseUrl + "/api/Appointment/Patient_MasterQueries";
    var objBO = {};
    objBO.hosp_id = Active.unitId;
    objBO.UHID = '-';
    objBO.MobileNo = '-';
    objBO.SearcKey = '-';
    objBO.SearchValue = '-';
    objBO.from = $('#txtOnlineBookingDate').val();
    objBO.to = '1900/01/01';
    objBO.prm_1 = '-';
    objBO.prm_2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetOnlinePatientByDate';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data)
            var tbody = "";
            var count = 0;
            if (data.ResultSet.Table.length > 0) {
                $.each(data.ResultSet.Table, function (key, val) {
                    if (val.IsConfirmed == 'Y')
                        tbody += "<tr style='background:#b4e7ba;'>";
                    else if (val.IsConfirmed == 'X')
                        tbody += "<tr style='background:#ffbfbf;pointer-events: none;'>";
                    else
                        tbody += "<tr>";

                    tbody += "<td style='width:1%;padding:2px 0'><button class='btn-success btn-flat'>Select</button></td>";
                    tbody += "<td class='hide'>" + JSON.stringify(data.ResultSet.Table[count]) + "</td>";
                    tbody += "<td>" + val.UHID + "</td>";
                    tbody += "<td>" + val.patient_name + "</td>";
                    tbody += "<td>" + val.gender + '/' + val.age + "</td>";
                    tbody += "<td>" + val.mobile_no + "</td>";
                    tbody += "<td>" + val.pt_address + "</td>";
                    tbody += "<td>" + val.Confirm_remark + "</td>";
                    tbody += "<td>" + val.booking_date + "</td>";
                    tbody += "</tr>";
                    count++;
                });
                $('#tblOnlinePatientBooking tbody').append(tbody);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetCityByState(sId, logic) {
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentQueries";
    var objBO = {};
    objBO.prm_1 = sId;
    objBO.Logic = 'GetCityByState';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        async: false,
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.ResultSet.Table.length > 0) {
                $('#ddlCity').empty().append($('<option>Select City</option>')).change();
                $('#ddlReferralCity').empty().append($('<option>Select City</option>')).change();
                $.each(data.ResultSet.Table, function (key, val) {
                    $('#ddlCity').append($('<option data-text="' + val.distt_name + '" data-stateid=' + val.state_code + '></option>').val(val.dist_code).html(val.distt_name));
                    $('#ddlReferralCity').append($('<option data-text="' + val.distt_name + '" data-stateid=' + val.state_code + '></option>').val(val.dist_code).html(val.distt_name));
                });
                $('#ddlCity').val(45).change();
                $('#ddlReferralCity').val(45).change();
            }
            else {
                //alert("Data Not Found..");
            };
        },
        complete: function (response) {

            if (logic == 'N') {

            }
            else {

            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetStateByCountry(cId, logic) {
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentQueries";
    var objBO = {};
    objBO.prm_1 = cId;
    objBO.Logic = 'GetStateByCountry';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        async: false,
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.ResultSet.Table.length > 0) {
                $('#ddlState').empty().append($('<option value="00">Select State</option>')).change();
                $('#ddlReferralState').empty().append($('<option value="00">Select State</option>')).change();
                $.each(data.ResultSet.Table, function (key, val) {
                    $('#ddlState').append($('<option data-text="' + val.state_name + '" data-countryid=' + val.country_id + '></option>').val(val.state_code).html(val.state_name));
                    $('#ddlReferralState').append($('<option data-text="' + val.state_name + '" data-countryid=' + val.country_id + '></option>').val(val.state_code).html(val.state_name));
                });
            }
            else {
                //alert("Data Not Found..");
            };
        },
        complete: function (data) {
            if (logic == 'N')
                return
            else
                $('#ddlState').val(32).change();
            GetCityByState(32, 'Y');
            $('#ddlReferralState').val(32).change();
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetCountry() {
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentQueries";
    var objBO = {};
    objBO.Logic = 'All';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        async: false,
        success: function (data) {
            if (Object.keys(data.ResultSet.Table5).length) {
                $('#ddlCountry').empty().append($('<option>Select</option>'));
                $.each(data.ResultSet.Table5, function (key, val) {
                    $('#ddlCountry').append($('<option></option>').val(val.CountryID).html(val.contry_Name));
                });
            }
            else {
                alert('No Record Found..');
            }
        },
        complete: function (response) {
            $('#ddlCountry').val(14).change();
            GetStateByCountry(14, 'Y');
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function getDOB(days) {
    var url = config.baseUrl + "/api/Service/Opd_ServiceQueries";
    var objBO = {};
    objBO.prm_1 = days;
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Logic = 'GetDOB';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        async: false,
        success: function (data) {

            $.each(data.ResultSet.Table, function (key, val) {
                var d = val.dob.split('T')[0];
                $('#txtDOB').val(d);
            });

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function Receipt(tnxid) {
    var url = "../Print/AppointmentReceipt?TnxId=" + tnxid + "&ActiveUser=" + Active.userName;
    window.open(url, '_blank');
}