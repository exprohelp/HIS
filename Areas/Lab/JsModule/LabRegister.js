$(document).ready(function () {
    FillCurrentDate('txtFrom')
    FillCurrentDate('txtTo')
    CloseSidebar();
});
function LabRegister() {
    $('#tblLabRegister tbody').empty();
    var url = config.baseUrl + "/api/Lab/SampleDispatchQueries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.VisitNo = '-';
    objBO.BarcodeNo = '-';
    objBO.DispatchLabCode = '-';
    objBO.TestCode = '-';
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Prm1 = $('#ddlIPOPType option:selected').val();
    objBO.logic = "LabRegister";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = '';
            var count = 0;
            var patientname = "";
            var DispatchLab = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        tbody += "<tr>";
                        tbody += "<td>" + count + "</td>";
                        tbody += "<td>" + val.VisitNo + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";
                        tbody += "<td>" + val.ageInfo + "</td>";
                        tbody += "<td>" + val.ItemName + "</td>";
                        tbody += "<td>" + val.Dept + "</td>";
                        if (val.InFlag == null)
                            tbody += "<td><button data-logic='LabPatientIn' onclick=InOutMarking(this) class='btn btn-warning btn-xs'><i class='fa fa-sign-in'>&nbsp;</i>In</button></td>";
                        else
                            tbody += "<td>" + val.InFlag + "</td>";

                        if (val.OutFlag == null)
                            tbody += "<td><button data-logic='LabPatientOut' onclick=InOutMarking(this) class='btn btn-danger btn-xs'><i class='fa fa-sign-out'>&nbsp;</i>Out</button></td>";
                        else
                            tbody += "<td>" + val.OutFlag + "</td>";

                        tbody += "</tr>";
                    });
                    $('#tblLabRegister tbody').append(tbody);
                }
            }
            else {
                alert('No Data Found')
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}