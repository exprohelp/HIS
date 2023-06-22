$(document).ready(function () {
    GetSubCat();
    RowSequence(['#tblInvestigation']);
    $("#btnGet").on('click', function () {
        var val = $(this).val();
        GetTestBySubCat();
    });
});
function GetSubCat() {
    $("#ddlSubCategory").empty().append($("<option></option>").val('Select').html('Select')).select2();
    var url = config.baseUrl + "/api/Lab/mObservationQueries";
    var objBO = {};
    objBO.subcatid = '-';
    objBO.prm_1 = '-';
    objBO.Logic = "GetSubCatForSeq";
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
                    $.each(data.ResultSet.Table, function (key, value) {
                        $("#ddlSubCategory").append($("<option></option>").val(value.SubCatID).html(value.SubCatName));
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
function GetTestBySubCat() {
    $("#tblInvestigation tbody").empty();
    if ($("#ddlSubCategory option:selected").val() == 'Select' && $("input[type=radio]:checked").val() =='investigation') {
        alert('Please Select Sub Category.')
        return
    }
    var url = config.baseUrl + "/api/Lab/mObservationQueries";
    var objBO = {};
    objBO.subcatid = $("#ddlSubCategory option:selected").val();
    objBO.prm_1 = $("input[type=radio]:checked").val();
    objBO.Logic = "GetTestBySubCat";
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
                    $.each(data.ResultSet.Table, function (key, value) {
                        htmldata += '<tr>';
                        htmldata += '<td>' + value.code + '</td>';
                        htmldata += '<td>' + value.seqNo + '</td>';
                        htmldata += '<td>' + value.names + '</td>';
                        htmldata += '</tr>';
                    });
                    $("#tblInvestigation tbody").append(htmldata);
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
function SetSeqOfInvestigation() {
    if (confirm('Are you sure?')) {
        var objBO = {};
        var SeqInfo = [];
        var url = config.baseUrl + "/api/Lab/mInsertUpdateTestInterpretation";
        $("#tblInvestigation tbody tr").each(function () {
            SeqInfo.push({
                'code': $(this).find('td:eq(0)').text(),
                'SeqNo': $(this).index()
            })
        });
        objBO.investcode = '-';
        objBO.text = JSON.stringify(SeqInfo);
        objBO.login_id = Active.userId;
        objBO.Logic = ($("input[type=radio]:checked").val() == 'investigation') ? "SetSeqOfInvestigation" :'SetSeqOfSubCat';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    alert('Set Successfully');
                    $("#btnGet").trigger('click');
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
