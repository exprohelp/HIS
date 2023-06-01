var _BillNo = "";
var _DonorId, _VisitId, _bbGroupingId, _DonorName, _Gender, _BloodGroup, _BagType, _Quantity, _EntryDate, _BloodCollectionId = 'New', _Volume, _BBTubeNo, _IsScreened, _IsScreenedApproved, _IsDiscarded, _DonationType;
$(document).ready(function () {
    $('select').select2();
    $('#lblApproveStatus').hide();
    FillCurrentDate('txtFrom');
    FillCurrentDate('txtTo');
    ApprovedDonorInfo('ApprovedDonorInfo');
    $('.tblDonorInfoForProcess').on('mouseenter', 'i.healthStatus', function () {
        GetDonorHealthStatus();
        $('.healthStatusContainer').css({ top: '27%', right: '23%' }).show();
    }).on('mouseleave', 'i.healthStatus', function () {
        $('.healthStatusContainer*').find('td[id]').text('');
        $('.healthStatusContainer').hide();
    });
});
function GetDonorHealthStatus() {
    $('.healthStatusContainer*').find('td[id]').text('-');
    var url = config.baseUrl + "/api/BloodBank/BB_SelectQueries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.DonorId = _DonorId;
    objBO.VisitId = _VisitId;
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "GetDonorHealthStatus";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data)
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#txtHealthBP').text(val.Blood_Pressure);
                        $('#txtHealthWeight').text(val.Weight);
                        $('#txtHealthTemprature').text(val.Temprature);
                        $('#txtHealthHeight').text(val.Height);
                        $('#txtHealthPulse').text(val.Pulse);
                        $('#txtHealthHemoglobin').text(val.Hb);
                        $('#txtHealthGPE').text(val.GPE);
                        $('#txtHealthPlatelet').text(val.platelet);
                    });
                }
            }
        },
        complete: function () {

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ApprovedDonorInfo(logic) {
    $('#tblDonorProcessInfo tbody').empty();
    var url = config.baseUrl + "/api/BloodBank/BB_SelectQueries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.DonorId = '-';
    objBO.VisitId = '-';
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Prm1 = $('#txtSearchInput').val();
    objBO.Prm2 = '-';
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
                    var DoonorInfo = "";
                    var count = 0;
                    var counter = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        counter++;
                        if (val.IsDiscarded == 'Y')
                            DoonorInfo += "<tr class='discarded'>";
                        else
                            DoonorInfo += "<tr>";

                        DoonorInfo += "<td class='hide'>" + JSON.stringify(data.ResultSet.Table[count]) + "</td>";
                        DoonorInfo += "<td>" + counter + "</td>";
                        DoonorInfo += "<td class='hide'>" + val.donor_id + "</td>";
                        DoonorInfo += "<td class='hide'>" + val.visit_id + "</td>";
                        DoonorInfo += "<td>" + val.visit_id + "</td>";
                        DoonorInfo += "<td>" + val.entryDate + "</td>";
                        DoonorInfo += "<td>" + val.DonationType + "</td>";
                        DoonorInfo += "<td>" + val.donorName + "</td>";
                        DoonorInfo += "<td>" + val.Gender + "</td>";
                        DoonorInfo += "<td>" + val.DOB + "</td>";
                        DoonorInfo += "<td>" + val.contactNo + "</td>";
                        DoonorInfo += "<td><i onclick=ApprovalInfo(this) class='fa fa-check '></i></td>";

                        DoonorInfo += "</tr>";

                        count++;
                    });
                    $('#tblDonorProcessInfo tbody').append(DoonorInfo);
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
function ApprovalInfo(elem) {
    getProcess(elem);
    GetInfoForApproval();
    $('#modalApprove').modal('show');
}
function getProcess(elem) {
    var val = JSON.parse($(elem).closest('tr').find('td:eq(0)').text());
    $('.txtInfoDonorID').text(val.donor_id);
    $('.txtInfoDonorName').text(val.donorName);
    $('.txtInfoGender').text(val.Gender);
    $('.txtInfoBloodGroup').text(val.BloodGroup);
    $('.txtInfoBagType').text(val.BagType);
    $('.txtInfoQuantity').text(val.Qty);
    $('.txtInfoEntryDate').text(val.entryDate);
    $('.txtInfoTubeNo').text(val.BBTubeNo);
    $('.txtInfoVolume').text(val.Volume);
    $('.txtInfoBloodCollectionId').text(val.BloodCollection_Id);
    _DonorId = val.donor_id;
    _VisitId = val.visit_id;
    _DonorName = val.donorName;
    _Gender = val.Gender;
    _BloodGroup = val.BloodGroup;
    _BagType = val.BagType;
    _Quantity = val.Qty;
    _EntryDate = val.entryDate;
    _Volume = val.Volume;
    _BBTubeNo = val.BBTubeNo;
    _bbGroupingId = val.bb_Grouping_Id;
    _BloodCollectionId = val.BloodCollection_Id;
    _IsScreened = val.isScreened;
    _IsScreenedApproved = val.isScreenedApproved;
    _IsDiscarded = val.IsDiscarded;
    _DonationType = val.DonationType;
    var btnHealth = "<i class='fa fa-heartbeat healthStatus'>&nbsp;</i>";
    $('table[class*=tblDonorInfoForProcess] tr:last-child').find('td:last-child').find('i.healthStatus').remove();
    $('table[class*=tblDonorInfoForProcess] tr:last-child').find('td:last-child').append(btnHealth);
}

function GetInfoForApproval() {
    $('#tblApprovalComponentInfo tbody').empty();
    $('#tblApproveScreenedInfo tbody').empty();
    $('#tblApprAntigen tbody tr').removeClass('bg-success');
    $('#tblApprCell tbody tr').removeClass('bg-success');
    $('.lblBBApprovalInfo* Alloted').text('');
    $('.lblBBApprovalInfo* Cell').text('');
    $('.tblApproveInfo .txtBagType').text('-');
    $('.tblApproveInfo .txtVolumeTaken').text('-');
    $('.tblApproveInfo .txtBBTubeNo').text('-');
    $('.tblApproveInfo .txtCollectionRemark').text('-');

    var url = config.baseUrl + "/api/BloodBank/BB_SelectQueries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.DonorId = _DonorId;
    objBO.VisitId = _VisitId;
    objBO.Prm1 = _BloodCollectionId;
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "GetInfoForApproval";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('.tblApproveInfo .txtBagType').text(val.BagType);
                        $('.tblApproveInfo .txtVolumeTaken').text(val.Volume);
                        $('.tblApproveInfo .txtBBTubeNo').text(val.BBTubeNo);
                        $('.tblApproveInfo .txtCollectionRemark').text(val.CollectionRemark);
                    });
                }
                if (Object.keys(data.ResultSet.Table1).length) {
                    var tbodyComponent = "";
                    $.each(data.ResultSet.Table1, function (key, val) {
                        tbodyComponent += "<tr>";
                        tbodyComponent += "<td>" + val.ComponentName + "</td>";
                        tbodyComponent += "<td class='text-right'>" + val.Qty + "</td>";
                        tbodyComponent += "<td>" + val.PackSize + "</td>";
                        tbodyComponent += "<td>" + val.ExpiryDate + "</td>";
                        tbodyComponent += "</tr>";
                    });
                    $('#tblApprovalComponentInfo tbody').append(tbodyComponent);
                }
                if (Object.keys(data.ResultSet.Table2).length) {
                    var tbodyScreened = "";
                    $('#DiscardedInfo').text('');
                    $.each(data.ResultSet.Table2, function (key, val) {
                        tbodyScreened += "<tr>";
                        tbodyScreened += "<td>" + val.TestName + "</td>";
                        tbodyScreened += "<td>" + val.BloodCollection_Id + "</td>";
                        tbodyScreened += "<td>" + val.Method + "</td>";
                        tbodyScreened += "<td>" + val.ResultValue + "</td>";
                        tbodyScreened += "<td>" + val.Result + "</td>";
                        tbodyScreened += "</tr>";
                    });
                    $('#tblApproveScreenedInfo tbody').append(tbodyScreened);
                }
                if (Object.keys(data.ResultSet.Table3).length) {
                    var Antigen = "";
                    var Cell = "";
                    $.each(data.ResultSet.Table3, function (key, val) {
                        Antigen += val.AntiA + val.AntiB + val.AntiAB + val.RH;
                        Cell += val.ACell + val.BCell + val.OCell;
                    });
                    $('#tblApprAntigen tbody tr').each(function () {
                        if ($(this).attr('class') == Antigen)
                            $(this).addClass('bg-success');
                    });
                    $('#tblApprCell tbody tr').each(function () {
                        if ($(this).attr('class') == Cell)
                            $(this).addClass('bg-success');
                    });

                    $('.lblBBApprovalInfo* Alloted').text($('#tblApprAntigen tbody tr.bg-success').find('td:eq(4)').text());
                    $('.lblBBApprovalInfo* Cell').text($('#tblApprAntigen tbody tr.bg-success').find('td:eq(5)').text());
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function UnApproveDonation() {
    if (confirm('Are you sure to Un-Approve?')) {
        var url = config.baseUrl + "/api/BloodBank/BB_ApproveDonation";
        var objBO = {};
        objBO.hosp_id = Active.HospId;
        objBO.donor_id = _DonorId;
        objBO.visit_id = _VisitId;
        objBO.BloodCollectionId = _BloodCollectionId;
        objBO.Prm1 = '-';
        objBO.Prm2 = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = "UnApprove";
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
}