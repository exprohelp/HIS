var _consentId = null;
$(document).ready(function () {
    $('#tblTestDetails tbody').on('change', 'input:checkbox', function () {
        var var1 = $(this).closest('tr').find('td:eq(1)').text();
        if ($(this).is(':checked'))
            CKEDITOR.instances['txtContent'].insertHtml('{<strong class="var">' + var1 + '</strong>}');
        else
            CKEDITOR.instances['txtContent'].insertText('');
    });
    GetConsentMaster()
});
function GetConsentMaster() {
    $("#tblConsentMaster tbody").empty();
    var url = config.baseUrl + "/api/Lab/mLabTemplateQueries";
    var objBO = {};
    objBO.Logic = "GetConsentMaster";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                var tbody = "";
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $("#ddlTestSubCategory").append($("<option></option>").val("0").html("Please Select"));
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += '<tr>';
                        tbody += '<td class="hide">' + val.var_list + '</td>';
                        tbody += '<td class="hide">' + val.t_content + '</td>';
                        tbody += '<td class="hide">' + val.ConsentId + '</td>';
                        tbody += "<td>" +
                            '<label class="switch">' +
                            '<input onchange=UpdatStatus("' + val.ConsentId + '") type="checkbox" id="chkActive" ' + val.checked + '>' +
                            '<span class="slider round"></span>' +
                            '</label>' +
                            "</td>";
                        tbody += '<td>' + val.ConsentName + '</td>';
                        tbody += '<td><button onclick=consentInfo(this) class="btn btn-warning btn-xs"><i class="fa fa-sign-in"></i></button></td>';

                    });
                    $("#tblConsentMaster tbody").append(tbody);
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
function consentInfo(elem) {
    selectRow($(elem));
    var varList = $(elem).closest('tr').find('td:eq(0)').text();
    var result = varList.split(',');
    var content = $(elem).closest('tr').find('td:eq(1)').text();
    var consentName = $(elem).closest('tr').find('td:eq(3)').text();
    _consentId = $(elem).closest('tr').find('td:eq(2)').text();
    CKEDITOR.instances['txtContent'].setData(content);
    $('#txtConsentTemplateName').val(consentName)
    $('#tblTestDetails tbody').find('input:checkbox').prop('checked', false);
    for (var i = 0; i <= result.length; i++) {
        $('#tblTestDetails tbody tr').each(function () {
            if ($(this).find('td:eq(1)').text() == result[i])
                $(this).find('td:eq(2)').find('input:checkbox').prop('checked', true);                           
        });
    }
    $('#btnSubmit').text('Update').addClass('btn-warning').removeClass('btn-success');
}
function InsertConsentForm() {
    if ($('#txtConsentTemplateName').val() == '') {
        alert('Please Provide Consent Name');
        return;
    }
    var objBO = {};
    var varList = [];
    var url = config.baseUrl + "/api/Lab/mLabTemplateInsertUpdate";
    var templatecontent = CKEDITOR.instances['txtContent'].getData();
    var result = templatecontent.match(/[^{]+(?=\})/g);
    for (var i = 0; i <= result.length; i++) {

        varList.push($(result[i]).text());
    }
    objBO.templatename = $('#txtConsentTemplateName').val();
    objBO.testcode = _consentId;
    objBO.templatecontent = templatecontent;
    objBO.prm1 = varList.join(',');
    objBO.login_id = Active.userId;
    objBO.hosp_id = Active.unitId;
    objBO.Logic = ($('#btnSubmit').text() == 'Submit') ? "InsertLabConsentForm" :"UpdateLabConsentForm";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {              
                alert(data);
                Clear();                       
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
function UpdatStatus(consentId) {    
    var objBO = {};   
    var url = config.baseUrl + "/api/Lab/mLabTemplateInsertUpdate";  
    objBO.templatename ='-';
    objBO.testcode = consentId;
    objBO.templatecontent = '-';
    objBO.prm1 = '-';
    objBO.login_id = Active.userId;
    objBO.hosp_id = Active.unitId;
    objBO.Logic = "UpdatStatus";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {               
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
function Clear() {
    _consentId = null;
    $('#txtConsentTemplateName').val('');
    $('#tblTestDetails tbody').find('input:checkbox').prop('checked', false);
    CKEDITOR.instances['txtContent'].setData('');
    $("#tblConsentMaster tbody tr").removeClass('select-row')
    $('#btnSubmit').text('Submit').addClass('btn-success').removeClass('btn-warning');
}