var _tnxid = "";
$(document).ready(function () {
    CloseSidebar();
    TriggerEnter();
    $('select').select2();
    _vacantLogic = "IPDReg";
    OnLoadQueries();
    $('input[type=checkbox]').on('change', function () {
        isCheck = $(this).is(':checked');
        var val = $(this).val();
        var len = $('input[name=PaymentMode]:checked').length;
        var pay = $('#txtPayable').val();

        var tbody = "";
        if (isCheck) {
            $('#tblPaymentDetails tbody tr').each(function () {
                if ($(this).find('td:eq(0)').text().toLowerCase() == val.toLowerCase()) {
                    $(this).show();
                    $(this).addClass('pay');
                }
            });
            $('#tblPaymentDetails tbody').find('input[type=text]').val(0);
            $('#tblPaymentDetails tbody').find('tr').filter('.pay').first().find('td:eq(1)').find('input[type=text]').val(pay);
        }
        else {
            $('#tblPaymentDetails tbody tr').each(function () {
                if ($(this).find('td:eq(0)').text().toLowerCase() == val.toLowerCase()) {
                    $(this).hide();
                    $(this).removeClass('pay');
                }
            });
            $('#tblPaymentDetails tbody').find('input[type=text]').val(0);
            $('#tblPaymentDetails tbody').find('tr').filter('.pay').first().find('td:eq(1)').find('input[type=text]').val(pay);
        }
    }); 
    $('#tblPaymentDetails tbody').on('keyup', 'input[type=text]', function () {
        var netAmount = parseFloat($('#txtNetAmount').val());
        var cash = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(0)').find('td:eq(1)').find('input[type=text]').val());
        var cheque = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(1)').find('td:eq(1)').find('input[type=text]').val());
        var cc = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(2)').find('td:eq(1)').find('input[type=text]').val());
        var ntfs = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(3)').find('td:eq(1)').find('input[type=text]').val());
        var total = cash + cheque + cc + ntfs;
        if (total > netAmount) {
            $(this).css('border-color', 'red');
            $('#txtError').text('Paid Amount can not be more than Net Amount...!').css({ 'color': 'red', 'font-size': '11px' });
        }
        else if (total < netAmount) {
            $(this).css('border-color', 'red');
            $('#txtError').text('Paid Amount can not be less than Net Amount...!').css({ 'color': '#bb8202', 'font-size': '11px' });
        }
        else {
            $('#tblPaymentDetails tbody').find('tr:eq(0),tr:eq(1),tr:eq(2),tr:eq(3)').find('td:eq(1)').find('input[type=text]').removeAttr('style');
            $('#txtError').text('').removeAttr('style');
        }
    });
      
    $('#txtSearchReferral').on('keyup', function (e) {
        var referral = $(this).val();
        ReferralSearch(referral);
        var tbody = $('#tblReferral tbody');
        var tr = $(tbody).find('tr.select-row');
        if (e.keyCode == 40) {
            if (tr.length == 0) {
                $(tbody).removeClass('select-row');
                $(tbody).find('tr:first').addClass('select-row');
            }
            $(tbody).removeClass('select-row');
            $(tr).next().find('tr:eq(' + index + ')').addClass('select-row');
        }
        else if (e.keyCode == 38) {
            index--;
            $(tbody).removeClass('select-row');
            $(tbody).find('tr:eq(' + index + ')').addClass('select-row');
        }
    });
    $('#tblReferral tbody').on('click', 'button', function () {
        var RefBy = $(this).closest('tr').find('td:eq(1)').text();
        var RefName = $(this).closest('tr').find('td:eq(2)').text();
        $('#txtRefBy').val(RefBy);
        $('#txtRefName').val(RefName);
        $('#modalReferral').modal('hide');
        $('#modalReferral input[type=text]').val('');
        $('#tblReferral tbody').empty();
    });
});
function OnLoadQueries() {
    var url = config.baseUrl + "/api/IPDNursing/IPD_RegistrationQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '';
    objBO.DoctorId = '';
    objBO.Prm1 = '';
    objBO.Prm2 = '';
    objBO.login_id = '';
    objBO.Logic = 'All';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        async: false,
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $('#ddlDept').empty().append($('<option>All</option>'));
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlDept').append($('<option></option>').val(val.DeptId).html(val.DepartmentName));
                    });
                }               
                if (Object.keys(data.ResultSet.Table2).length) {
                    $('#ddlReferralSource').empty().append($('<option>Select</option>')).change();
                    $.each(data.ResultSet.Table2, function (key, val) {
                        $('#ddlReferralSource').append($('<option></option>').val(val.ref_code).html(val.ref_name));
                    });
                }
                if (Object.keys(data.ResultSet.Table3).length) {
                    $('#tblPaymentDetails tbody .BankName').empty().append($('<option></option>')).change();
                    $.each(data.ResultSet.Table3, function (key, val) {
                        $('#tblPaymentDetails tbody .BankName').append($('<option></option>').val(val.BankId).html(val.BankName));
                    });
                }
                if (Object.keys(data.ResultSet.Table4).length) {
                    $('#tblPaymentDetails tbody .MachineName').empty().append($('<option></option>')).change();
                    $.each(data.ResultSet.Table4, function (key, val) {
                        $('#tblPaymentDetails tbody .MachineName').append($('<option></option>').val(val.machineId).html(val.machineName));
                    });
                }
                if (Object.keys(data.ResultSet.Table6).length) {
                    $('#ddlPanel').empty().append($('<option>Select</option>')).change();
                    $.each(data.ResultSet.Table6, function (key, val) {
                        $('#ddlPanel').append($('<option></option>').val(val.PanelId).html(val.PanelName));
                    });
                }
                if (Object.keys(data.ResultSet.Table7).length) {
                    $('#ddlBillingCategory').empty().append($('<option>Select</option>')).change();
                    $.each(data.ResultSet.Table7, function (key, val) {
                        $('#ddlBillingCategory').append($('<option></option>').val(val.RoomBillingCategory).html(val.RoomBillingCategory));
                    });
                }
            }
            else {
                alert('No Record Found..');
            }
        },
        complete: function (response) {
            $('#ddlDept').prop('selectedIndex', '0').change();
            $('#ddlReferralSource').val('RC1624').change();           
            $('#ddlPanel').val(1).change();         
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function RoomBillingType() {
    var url = config.baseUrl + "/api/IPDNursing/IPD_RegistrationQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '';
    objBO.DoctorId = '';
    objBO.Prm1 = $('#ddlRoomType option:selected').val();
    objBO.Prm2 = '';
    objBO.login_id = '';
    objBO.Logic = 'RoomBillingType';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $('#ddlBillingCategory').empty().append($('<option>Select</option>')).change();
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlBillingCategory').append($('<option></option>').val(val.RoomBillingCategory).html(val.RoomBillingCategory));
                    });
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table1).length) {
                    $('#ddlRoomBed').empty().append($('<option>Select</option>')).change();
                    $.each(data.ResultSet.Table1, function (key, val) {
                        $('#ddlRoomBed').append($('<option></option>').val(val.RoomBedId).html(val.roomFullName));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetDoctorByDept() {
    $('#ddlDoctor').empty().append($('<option>Select</option>')).change();
    var url = config.baseUrl + "/api/IPDNursing/IPD_RegistrationQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '';
    objBO.DoctorId = '';
    objBO.Prm1 = $('#ddlDept option:selected').val();
    objBO.Prm2 = '';
    objBO.login_id = '';
    objBO.Logic = 'GetDoctorByDept';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet.Table).length) {
                $.each(data.ResultSet.Table, function (key, val) {
                    $('#ddlDoctor').append($('<option></option>').val(val.DoctorId).html(val.DoctorName));
                });
            }
            else {
                //alert('Data Not Found...!');
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function paymentCalculationByAmt() {
    //Initialize
    var grossAmt = parseFloat($('#txtGrossAmount').val());
    $('#txtDiscountByPer').val(0);
    var discountByAmt = parseInt($('#txtDiscountByAmt').val());

    if ($('#txtDiscountByAmt').val() == '') {
        paymentCal('amount', 0);
    }
    else if (discountByAmt > grossAmt) {
        $('#txtDiscountByAmt').val(0);
        paymentCal('amount', 0);
        alert('Discount Amount can not be greater then Gross Amount');
    }
    else {
        paymentCal('amount', discountByAmt);
    }

    if (discountByAmt > 0) {
        $('#txtError').text('');
        $('#txtDisResason').addClass('border-imp');
        $('span.selection').find('span[aria-labelledby=select2-ddlApprovedBy-container]').addClass('border-imp');
        $('span.selection').find('span[aria-labelledby=select2-ddlDiscountType-container]').addClass('border-imp');
    }
    else {
        $('#txtDisResason').removeClass('border-imp');
        $('span.selection').find('span[aria-labelledby=select2-ddlApprovedBy-container]').removeClass('border-imp');
        $('span.selection').find('span[aria-labelledby=select2-ddlDiscountType-container]').removeClass('border-imp');
    }
}
function paymentCalculationByPer() {
    $('#txtDiscountByAmt').val(0);
    var discountByPer = parseInt($('#txtDiscountByPer').val());

    if ($('#txtDiscountByPer').val() == '') {
        paymentCal('percent', 0);
    }
    else if (discountByPer > 100) {
        $('#txtDiscountByPer').val(0);
        paymentCal('percent', 0);
        alert('Discount can not be greater then 100%');
    }
    else {
        paymentCal('percent', discountByPer);
    }
    if (discountByPer > 0) {
        $('#txtError').text('');
        $('#txtDisResason').addClass('border-imp');
        $('span.selection').find('span[aria-labelledby=select2-ddlApprovedBy-container]').addClass('border-imp');
        $('span.selection').find('span[aria-labelledby=select2-ddlDiscountType-container]').addClass('border-imp');
    }
    else {
        $('#txtDisResason').removeClass('border-imp');
        $('span.selection').find('span[aria-labelledby=select2-ddlApprovedBy-container]').removeClass('border-imp');
        $('span.selection').find('span[aria-labelledby=select2-ddlDiscountType-container]').removeClass('border-imp');
    }
}
function paymentCal(logic, val) {
    var grossAmt = parseFloat($('#txtGrossAmount').val());
    if (logic == 'amount') {
        var discountByAmt = parseFloat(val);
        var discountedAmt = grossAmt - parseFloat(val);
        $('#txtDiscount').val(discountByAmt);
        var netAmt = discountedAmt;
        $('#txtNetAmount').val(netAmt);
        $('input[name=PaymentMode]:not(.cash)').prop('checked', false).change();
        $('#tblPaymentDetails tbody').find('input[type=text]').val(0);
        $('#tblPaymentDetails tbody').find('tr:eq(0)').find('td:eq(1)').find('input[type=text]').val(netAmt);
        $('#txtPayable').val(netAmt);
    }
    else if (logic == 'percent') {
        var discountByPer = parseInt(val);
        var discountAmt = grossAmt - (grossAmt * discountByPer / 100);
        $('#txtDiscount').val(grossAmt * discountByPer / 100);
        var netAmt = discountAmt;
        $('#txtNetAmount').val(netAmt);
        $('input[name=PaymentMode]:not(.cash)').prop('checked', false).change();
        $('#tblPaymentDetails tbody').find('input[type=text]').val(0);
        $('#tblPaymentDetails tbody').find('tr:eq(0)').find('td:eq(1)').find('input[type=text]').val(netAmt);
        $('#txtPayable').val(netAmt);
    }
}
//IPD Save Registration Info
function IPDRegistration() {
    if (confirm('Are you sure to IPD Registration?')) {
        if (ValidateRegistration()) {
            var url = config.baseUrl + "/api/IPDNursing/IPD_RegistrationInsertUpdate";
            var objPatient = {};
            var objBooking = {};

            //IPD Patient Info
            objPatient.hosp_id = Active.unitId;
            objPatient.UHID = $('#txtUHID').val().toUpperCase();
            objPatient.barcodeno = $('#txtBarcode').val().toUpperCase();
            objPatient.Title = $('#ddlTitle option:selected').text();
            objPatient.FirstName = $('#txtFirstName').val().toUpperCase();
            objPatient.LastName = $('#txtLastName').val().toUpperCase();
            objPatient.patient_name = objPatient.FirstName + ' ' + objPatient.LastName;
            if ($('#ddlGender option:selected').text() == 'Select')
                objPatient.gender = '-';
            else
                objPatient.gender = $('#ddlGender option:selected').text();
            objPatient.dob = ($('#txtDOB').val() == '') ? null : $('#txtDOB').val();
            objPatient.age = $('#txtAge').val();
            objPatient.ageType = $('#ddlAgeType option:selected').text();
            objPatient.mobile_no = $('#txtMobileNo').val();
            objPatient.email_id = $('#txtEmailId').val();
            if ($('#ddlMaritalStatus option:selected').text() == 'Select')
                objPatient.marital_status = '-';
            else
                objPatient.marital_status = $('#ddlMaritalStatus option:selected').text();
            if ($('#ddlReligion option:selected').text() == 'Select')
                objPatient.religion = '-';
            else
                objPatient.religion = $('#ddlReligion option:selected').text();
            if ($('#ddlRelationOf option:selected').text() == 'Select')
                objPatient.relation_of = '-';
            else
                objPatient.relation_of = $('#ddlRelationOf option:selected').text();
            objPatient.relation_name = $('#txtRelationName').val();
            objPatient.relation_phone = $('#txtRelationContact').val();
            objPatient.country = $('#ddlCountry option:selected').text();
            objPatient.state = $('#ddlState option:selected').text();
            objPatient.district = $('#ddlCity option:selected').text();
            objPatient.locality = '-';
            objPatient.village = '-';
            objPatient.address = $('#txtAddress').val();
            if ($('#ddlIdProof option:selected').text() == 'Select')
                objPatient.idType = '-';
            else
                objPatient.idType = $('#ddlIdProof option:selected').text();
            objPatient.IDNo = $('#txtIdNo').val();
            objPatient.CardNo = '-';
            objPatient.member_id = $('#txtMembershipNo').val().toUpperCase();
            objPatient.IPNo = '-';
            objPatient.login_id = Active.userId;
            objPatient.Logic = 'ItdoseNewEntry';

            //IPD Registration Info
            objBooking.hosp_id = Active.unitId;
            objBooking.UHID = $('#txtUHID').val().toUpperCase();
            objBooking.IsNewPatient = $('#txtUHID').val().toUpperCase();
            objBooking.Title = objPatient.Title;
            objBooking.patient_name = objPatient.patient_name;
            objBooking.email = '-';
            objBooking.age = $('#txtAge').val();
            objBooking.ageType = $('#ddlAgeType option:selected').text();
            if ($('#ddlGender option:selected').text() == 'Select')
                objBooking.gender = '-';
            else
                objBooking.gender = $('#ddlGender option:selected').text();
            objBooking.dob = ($('#txtDOB').val() == '') ? null : $('#txtDOB').val();
            objBooking.ageInDays = 0;
            objBooking.mobile_no = $('#txtMobileNo').val();
            objBooking.IDProofType = $('#ddlIdProof option:selected').text();
            objBooking.IDProofNO = $('#txtIdNo').val();
            objBooking.IPDNo = '-';
            objBooking.AdmitDate = $('#txtAdmissionDate').val();
            objBooking.AdmitTime = $('#txtAdmissionTime').val();
            objBooking.AdmissionType = $('#ddlAdmissionType option:selected').val();
            objBooking.DoctorId = $('#ddlDoctor option:selected').val();
            objBooking.PanelId = $('#ddlPanel option:selected').val();
            objBooking.PatientType = $('#ddlPatientType option:selected').text();
            objBooking.PolicyNo = $('#txtPolicyNo').val();
            objBooking.ReferencNo = $('#txtReferenceNo').val();
            objBooking.SourceName = $('#ddlSource option:selected').text();
            objBooking.StaffCode = '-';
            objBooking.RoomBedId = $('#txtRoomBedId').val();
            objBooking.BedNo = '-';
            objBooking.RoomBillingCategory = $('#ddlBillingCategory option:selected').val();
            objBooking.RoomTypeRequest = $('#ddlRquestRoom option:selected').val();
            objBooking.MLCType = $('#ddlMLCType option:selected').val();
            objBooking.MLCNo = '-';
            objBooking.Remark = '-';
            objBooking.RefBy = $('#txtRefBy').val();
            objBooking.RefName = $('#txtRefName').val();
            objBooking.AttendantName = $('#txtAttendantName').val();
            objBooking.AttendantContactNo1 = $('#txtAttendantContactNo1').val();
            objBooking.AttendantContactNo2 = $('#txtAttendantContactNo2').val();
            objBooking.login_id = Active.userId;
            objBooking.Logic = 'NewIPDReg';
            var MasterObject = {};
            MasterObject.objPatient = objPatient;
            MasterObject.objBooking = objBooking;
            $.ajax({
                method: "POST",
                url: url,
                data: JSON.stringify(MasterObject),
                contentType: "application/json;charset=utf-8",
                dataType: "JSON",
                success: function (data) {
                    if (data.includes('Success')) {
                        alert('Successfully Registration Confirmed..!');
                        _tnxid = data.split('|')[1];
                        IPDRegisteredInfo(_tnxid);
                        Receipt(_tnxid);
                        Clear();
                        $('.AdvanceInfo').removeClass('block');
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
}
function IPDRegisteredInfo(IPDNo) {
    var url = config.baseUrl + "/api/IPDNursing/IPD_RegistrationQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = IPDNo;
    objBO.DoctorId = '';
    objBO.Prm1 = '';
    objBO.Prm2 = '';
    objBO.login_id = '';
    objBO.Logic = 'IPDRegisteredInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#txtPrevIPDNo').text(val.IPDNo);
                        $('#txtPrevPatientName').text(val.patient_name);
                        $('#txtPrevDoctor').text(val.DoctorName);
                        $('#txtPrevRoomBed').text(val.RoomName);
                        $('#txtPrevBillingCategory').text(val.RoomBillingCategory);
                        $('#txtPrevRquestRoom').text(val.RoomTypeRequest);
                        $('#txtPrevAttendantName').text(val.Attendant);
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function PatientAdvance() {
    if (confirm('Are you sure?')) {
        if ($('#txtPrevIPDNo').text() == '') {
            alert('IPD No Not Found.');
            return
        }
        var url = config.baseUrl + "/api/IPDNursing/IPD_TakeAdvance";
        var objBooking = {};
        var objPayment = [];

        $('#tblPaymentDetails tbody tr.pay').each(function () {
            objPayment.push({
                'ReceiptNo': '-',
                'PayMode': $(this).find('td:eq(0)').text(),
                'CardNo': '-',
                'BankName': $(this).find('td:eq(3)').find('select option:selected').text(),
                'RefNo': $(this).find('td:eq(4)').find('input[type=text]').val(),
                'MachineId': $(this).find('td:eq(5)').find('select option:selected').val(),
                'MachineName': $(this).find('td:eq(5)').find('select option:selected').text(),
                'Amount': $(this).find('td:eq(1)').find('input[type=text]').val(),
                'OnlPaymentId': '',
                'OnlPayStatus': '',
                'OnlPayResponse': '',
                'OnlPaymentDate': new Date(),
                'login_id': Active.userId
            });
        });
        objBooking.IPDNo = $('#txtPrevIPDNo').text();
        objBooking.hosp_id = Active.unitId;
        objBooking.IPOPType = 'IPD';
        objBooking.ReceiptType = 'Advance';
        objBooking.Prm1 = '-';
        objBooking.Prm2 = '-';
        objBooking.Remark = '-';
        objBooking.login_id = Active.userId;
        objBooking.Logic = 'IPD-PatientAdvance';
        var MasterObject = {};
        MasterObject.objBooking = objBooking;
        MasterObject.objPayment = objPayment;
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(MasterObject),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    alert('Successfully Saved..!');
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
function ReferralSearch(referral) {
    var url = config.baseUrl + "/api/Appointment/Referral_Search";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.referral_name = referral;
    objBO.Logic = 'ReferralSearch';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        async: false,
        success: function (data) {
            $('#tblReferral tbody').empty();
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td><button class='btn-success btn-flat'>Select</button></td>";
                        tbody += "<td>" + val.ref_code + "</td>";
                        tbody += "<td>" + val.ref_name + "</td>";
                        tbody += "<td>" + val.mobile_no + "</td>";
                        tbody += "<td>" + val.degree + "</td>";
                        tbody += "<td>" + val.address + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblReferral tbody').append(tbody);
                }
            }
            else {
                alert('No Record Found..');
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ValidateBooking() {
    var Title = $('#ddlTitle option:selected').text();
    var FirstName = $('#txtFirstName').val();
    var Gender = $('#ddlGender option:selected').text();
    var MobileNo = $('#txtMobileNo').val();
    var Age = $('#txtAge').val();
    var Country = $('#ddlCountry option:selected').text();
    var State = $('#ddlState option:selected').text();
    var City = $('#ddlCity option:selected').text();
    var Address = $('#txtAddress').val();
    var Referral = $('#ddlReferral option:selected').text();
    var Panel = $('#ddlPanel option:selected').text();
    var Doctor = $('#ddlDoctor option:selected').text();
    var ItemName = $('#ddlItemName option:selected').text();
    var Discount = parseFloat($('#txtDiscount').val());
    var DisResason = $('#txtDisResason').val();
    var ApprovedBy = $('#ddlApprovedBy option:selected').text();
    var DiscountType = $('#ddlDiscountType option:selected').text();
    var VisitType = $('#ddlVisitType option:selected').text();
    var Error = $('#txtError').text();
    var Payable = parseFloat($('#txtPayable').text());

    if (FirstName == '') {
        $('#txtFirstName').css('border-color', 'red').focus();
        alert('Please Provide First Name..');
        return false;
    }
    else {
        $('#txtFirstName').removeAttr('style');
    }
    if (Gender == 'Select') {
        $('span.selection').find('span[aria-labelledby=select2-ddlGender-container]').css('border-color', 'red').focus();
        alert('Please Select Gender..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlGender-container]').removeAttr('style');
    }
    if (MobileNo == '') {
        $('#txtMobileNo').css('border-color', 'red').focus();
        alert('Please Provide Mobile No..');
        return false;
    }
    else {
        $('#txtMobileNo').removeAttr('style');
    }
    if (Age == '') {
        $('#txtAge').css('border-color', 'red').focus();
        alert('Please Provide Age..');
        return false;
    }
    else {
        $('#txtAge').removeAttr('style');
    }
    if (Country == 'Select') {
        $('span.selection').find('span[aria-labelledby=select2-ddlCountry-container]').css('border-color', 'red').focus();
        alert('Please Select Country..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlCountry-container]').removeAttr('style');
    }
    if (State == 'Select State') {
        $('span.selection').find('span[aria-labelledby=select2-ddlState-container]').css('border-color', 'red').focus();
        alert('Please Select State..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlState-container]').removeAttr('style');
    }
    if (City == 'Select City') {
        $('span.selection').find('span[aria-labelledby=select2-ddlCity-container]').css('border-color', 'red').focus();
        alert('Please Select City..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlCity-container]').removeAttr('style');
    }
    //if (Address == '') {
    //	$('#txtAddress').css('border-color', 'red').focus();
    //	alert('Please Provide Address..');
    //	return false;
    //}
    //else {
    //	$('#txtAddress').removeAttr('style');
    //}
    if (Referral == 'Select') {
        $('span.selection').find('span[aria-labelledby=select2-ddlReferral-container]').css('border-color', 'red').focus();
        alert('Please Select Referral..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlReferral-container]').removeAttr('style');
    }
    if (Panel == 'Select') {
        $('span.selection').find('span[aria-labelledby=select2-ddlPanel-container]').css('border-color', 'red').focus();
        alert('Please Select Panel..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlPanel-container]').removeAttr('style');
    }
    if (Doctor == 'Select') {
        $('span.selection').find('span[aria-labelledby=select2-ddlDoctor-container]').css('border-color', 'red').focus();
        alert('Please Select Doctor..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlDoctor-container]').removeAttr('style');
    }
    if (ItemName == 'Select') {
        $('span.selection').find('span[aria-labelledby=select2-ddlItemName-container]').css('border-color', 'red').focus();
        alert('Please Select Item Name..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlItemName-container]').removeAttr('style');
    }
    if (Discount > 0 && DiscountType == 'Select' && VisitType == 'Walk-In') {
        $('span.selection').find('span[aria-labelledby=select2-ddlDiscountType-container]').css('border-color', 'red').focus();
        alert('Please Select Discount Type..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlDiscountType-container]').removeAttr('style');
    }
    if (Discount > 0 && DisResason == '' && VisitType == 'Walk-In') {
        $('#txtDisResason').css('border-color', 'red').focus();
        alert('Please Provide Discount Reason..');
        return false;
    }
    else {
        $('#txtDisResason').removeAttr('style');
    }
    if (Discount > 0 && ApprovedBy == 'Select' && VisitType == 'Walk-In') {
        $('span.selection').find('span[aria-labelledby=select2-ddlApprovedBy-container]').css('border-color', 'red').focus();
        alert('Please Select Approved By..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlApprovedBy-container]').removeAttr('style');
    }
    if (Error.length > 0 && VisitType == 'Walk-In') {
        alert('Please Provide Correct Payment Amount Details..');
        return false;
    }
    if (Payable <= 0 && VisitType == 'Walk-In') {
        alert('Please Provide Payment Amount Details..');
        return false;
    }
    return true;
}
function Clear() {
    $('#BasicInformation').find('input[type=text],input[type=date]').val('');
    $('#BasicInformation').find('select:not(#ddlCountry)').prop('selectedIndex', '0').change();
    $('#ddlCountry').val(14).change();
    GetStateByCountry(14, 'Y');
    $('#ddlReferral').val('RC0008').change();
    $('#AnonymousInformation  #ddlPatientType').val(1).change();
    $('#AnonymousInformation #ddlPanel').val(1).change();
    $('#AnonymousInformation #ddlSource').prop('selectedIndex', '0').change();
    $('#AnonymousInformation #ddlVisitPurpose').prop('selectedIndex', '0').change();
    $('#AnonymousInformation #ddlDept').prop('selectedIndex', '0').change();
    $('#AnonymousInformation #ddlDoctor').prop('selectedIndex', '0').change();
    $('#AnonymousInformation #ddlItemName').prop('selectedIndex', '0').change();
    $('#AnonymousInformation #ddlVisitType').prop('selectedIndex', '0').change();
    $('#AnonymousInformation').find('input[type=text],input[type=date]:not(#txtAppointmentOn)').val('');
    $('#PaymentInformation').find('input[type=text]:not(#txtDisResason)').val(0);
    $('#PaymentInformation').find('input[id=txtDisResason]').val('');
    $('#PaymentInformation').find('select').prop('selectedIndex', '0').change();
    $('#tblPaymentDetails tbody').find('input[type=text]').val(0);
    $('input[name=PaymentMode]:not(.cash)').prop('checked', false).change();
    FillCurrentDate("txtAppointmentOn");
    $('#txtUHID').val('New');
}
function query() {
    var vars = [], hash;
    var url = window.location.href.replace('#', '');
    var hashes = url.slice(url.indexOf('?') + 1).split('&');
    for (i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[decodeURIComponent(hash[0])] = decodeURIComponent(hash[1]);
    }
    return vars;
}
function TriggerEnter() {
    $('input[id=txtSearchValue]').on('keyup', function (e) {

        if (e.keyCode == 13) {
            $('#btnSearchOldPatient').trigger('click');
        }
    });
}
function ValidateRegistration() {
    var Panel = $('#ddlPanel option:selected').text();
    var Doctor = $('#ddlDoctor option:selected').text();
    var BillingCategory = $('#ddlBillingCategory option:selected').text();
    var ReferralSource = $('#ddlReferralSource option:selected').text();
    var AttendantName = $('#txtAttendantName').val();
    var AttendantContactNo1 = $('#txtAttendantContactNo1').val();
    if (Panel == 'Select') {
        $('span.selection').find('span[aria-labelledby=select2-ddlPanel-container]').css('border-color', 'red').focus();
        alert('Please Select Panel..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlPanel-container]').removeAttr('style');
    }
    if (Doctor == 'Select') {
        $('span.selection').find('span[aria-labelledby=select2-ddlDoctor-container]').css('border-color', 'red').focus();
        alert('Please Select Doctor..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlDoctor-container]').removeAttr('style');
    }
    if (BillingCategory == 'Select') {
        $('span.selection').find('span[aria-labelledby=select2-ddlBillingCategory-container]').css('border-color', 'red').focus();
        alert('Please Select Billing Category..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlBillingCategory-container]').removeAttr('style');
    }
    if (ReferralSource == 'Select') {
        $('span.selection').find('span[aria-labelledby=select2-ddlReferralSource-container]').css('border-color', 'red').focus();
        alert('Please Select Referral Source..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlReferralSource-container]').removeAttr('style');
    }
    if (AttendantName == '') {
        $('#txtAttendantName').css('border-color', 'red').focus();
        alert('Please Provide Attendant Name..');
        return false;
    }
    else {
        $('#txtAttendantName').removeAttr('style');
    }
    if (AttendantContactNo1 == '') {
        $('#txtAttendantContactNo1').css('border-color', 'red').focus();
        alert('Please Provide Attendant Contact No..');
        return false;
    }
    else {
        $('#txtAttendantContactNo1').removeAttr('style');
    }
    return true;
}