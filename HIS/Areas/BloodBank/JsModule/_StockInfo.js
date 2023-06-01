$(document).ready(function () {
    TrackStockInfo('STK191053');
});
function TrackStockInfo(stockId) {
    $('#tblStockDetails tbody').empty();
    var url = config.baseUrl + "/api/BloodBank/BB_SelectQueries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.DonorId = '-';
    objBO.VisitId = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.IndentNo = '-'; //Indent No carries
    objBO.Prm1 = stockId; //Component Id carries
    objBO.Prm2 = '-'; //Blood Group Allotted carries
    objBO.login_id = Active.userId;
    objBO.Logic = "TrackStockInfo";
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
                    var counter = 0;
                    var count = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        counter++;
                        $('.tblDonorInfoForProcess .txtInfoDonorID').text(val.donor_id);
                        $('.tblDonorInfoForProcess .txtInfoDonorName').text(val.donorName);
                        $('.tblDonorInfoForProcess .txtInfoGender').text(val.Gender);
                        $('.tblDonorInfoForProcess .txtInfoBloodGroup').text(val.donor_BloodGroup);
                        $('.tblDonorInfoForProcess .txtInfoBagType').text(val.BagType);
                        $('.tblDonorInfoForProcess .txtInfoTubeNo').text(val.BBTubeNo);
                        $('.tblDonorInfoForProcess .txtInfoEntryDate').text(val.ExpDate);
                    });                   
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table1).length) {
                    var tbody = "";
                    var counter = 0;
                    var count = 0;
                    $.each(data.ResultSet.Table1, function (key, val) {
                        counter++;
                        tbody += "<tr>";
                        tbody += "<td>" + counter + "</td>";
                        tbody += "<td class='hide'>" + val.itemId + "</td>";
                        tbody += "<td class='hide'>" + val.ComponentID + "</td>";                       
                        tbody += "<td>" + val.Stock_Id + "</td>";                       
                        tbody += "<td>" + val.ComponentName + "</td>";
                        tbody += "<td class='text-right'>" + val.Qty + "</td>";
                        tbody += "<td>" + val.ExpDate + "</td>";
                        tbody += "<td>" + val.BBTubeNo + "</td>";
                        tbody += "<td>" + val.BloodGroup + "</td>";
                        tbody += "<td>" + val.IssueHoldTo + "</td>";
                        tbody += "</tr>";
                        count++;
                    });
                    $('#tblStockDetails tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}