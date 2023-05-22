$(document).ready(function () {

});

function SampleTrackingInfo() {
    $('#tblSampleTrackingInfo tbody').empty();
    var url = config.baseUrl + "/api/Lab/SampleDispatchQueries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.VisitNo = $('#txtTrackingBarcodeNo').val();
    objBO.BarcodeNo = $('#txtTrackingBarcodeNo').val();
    objBO.DispatchLabCode = '-';
    objBO.TestCode = '-';    
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '-';      
    objBO.logic = "SampleTrackingInfo";
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
                        //if (DispatchLab != val.DispatchLab) {
                        //    tbody += "<tr class='lab-group'>";
                        //    tbody += "<td style='padding:0' colspan='6' class='lab-group'>&nbsp;&nbsp;" + val.DispatchLab + " <input type='checkbox' class='pull-right'/><button onclick=$('#modalDispatch').modal('show') class='pull-right btn btn-warning btn-xs btnDispatchGroup'>Dispatch</button></td>";
                        //    tbody += "</tr>";
                        //    DispatchLab = val.DispatchLab;
                        //    count1 = 0;
                        //}
                        if (patientname != val.patient_name) {
                            tbody += "<tr class='pat-group'>";
                            tbody += "<td colspan='10' class='pat-group'>" + val.VisitNo + ' [ ' + val.patient_name + ' ]' + ' [ ' + val.RegDate + ' ]' + "</td>";
                            tbody += "</tr>";
                            patientname = val.patient_name;
                            count1 = 0;
                        }
                        count++;
                        tbody += "<tr>";
                        tbody += "<td>" + count + "</td>";             
                        tbody += "<td>" + val.TestName + "</td>";                       
                        tbody += "<td>" + val.RegDate + "</td>";                       
                        tbody += "<td>" + val.SampleColected + "</td>";                       
                        tbody += "<td>" + val.SampleCollectedBy + "</td>";                       
                        tbody += "<td>" + val.SampleDistributed + "</td>";                       
                        tbody += "<td>" + val.SampleDistributedBy + "</td>";                       
                        tbody += "<td>" + val.LabReceived + "</td>";                       
                        tbody += "<td>" + val.LabReceivedBy + "</td>";                       
                        tbody += "<td>" + val.IsDispatched + "</td>";                       
                        tbody += "</tr>";
                    });
                    $('#tblSampleTrackingInfo tbody').append(tbody);
                    $('#modalTrackingInfo').modal('show');
                   // $('#txtTrackingBarcodeNo').val('');
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