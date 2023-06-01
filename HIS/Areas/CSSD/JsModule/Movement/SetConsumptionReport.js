$(document).ready(function () {
    CloseSidebar();
    searchTable('txtSearch', 'tblSetInfo');
    GetSetInfo();
});
function GetSetInfo() {
    $("#tblSetInfo tbody").empty();
    var url = config.baseUrl + "/api/cssd/CSSD_MovementQueries";
    var objBO = {};
    objBO.Prm_1 = '-';
    objBO.Logic = 'SetUsesReport';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = "";
                    var count = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        if (val.SetStatus == 'Sterlized')
                            tbody += "<tr style='background:#e6fff0'>";
                        else if (val.SetStatus == 'InUse')
                            tbody += "<tr style='background:#ffe6ff'>";

                        tbody += "<tr>";
                        tbody += "<td><a href='#' onclick=PatientInfoForSet('" + val.SetId + "')>" + val.SetId  + "</a></td>";
                        tbody += "<td>" + val.SetName + "</td>";
                        tbody += "<td>" + val.UsedCount + "</td>";
                        tbody += "<td>" + val.FirstUseDate + "</td>";
                        tbody += "<td>" + val.LastUsedDate + "</td>";
                
                        tbody += "</tr>";
                    });
                    $("#tblSetInfo tbody").append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function PatientInfoForSet(SetId) {
    $('#txtBatchInfo').text('');
    $("#tblTrackingInfo tbody").empty();
    var url = config.baseUrl + "/api/cssd/CSSD_MovementQueries";
    var objBO = {};
    objBO.Prm_1 = SetId;
    objBO.Logic = 'PatientInfoForSet';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = "";
                    var temp = "";
                    var count = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        tbody += "<tr>";
                        tbody += "<td>" + val.IPDNo + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";
                        tbody += "<td>" + val.DoctorName + "</td>";
                        tbody += "<td>" + val.batch_no + "</td>";
                        tbody += "<td>" + val.exp_date.substring(0, 10) + "</td>";
                        tbody += "<td>" + val.SetUtilizeDate + "</td>";
                        tbody += "<td>" + val.CartName + "</td>";
                        tbody += "<td>" + val.emp_name + "</td>";
                    
                     
                        tbody += "</tr>";
                    });
                    $("#tblTrackingInfo tbody").append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}