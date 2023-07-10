$(document).ready(function () {
    CloseSidebar();
    $('#tblTestDetails tbody').on('change', 'input:checkbox', function () {
        var var1 = $(this).closest('tr').find('td:eq(1)').text();
        if ($(this).is(':checked'))
            CKEDITOR.instances['txtContent'].insertHtml('{<b class="var">' + var1 + '</b>}');
        else
            CKEDITOR.instances['txtContent'].insertText('');
    });
});
function InsertConsentForm() {
    var objBO = {};
    var varList = [];
    var url = config.baseUrl + "/api/Lab/mLabTemplateInsertUpdate";   
    var templatecontent = CKEDITOR.instances['txtContent'].getData();
    var result = templatecontent.match(/[^{]+(?=\})/g);
    for (var i = 0; i <= result.length; i++) {

        varList.push($(result[i]).text());
    }
    objBO.templatename = $('#txtConsentTemplateName').val();
    objBO.testcode = '-';
    objBO.templatecontent = templatecontent;
    objBO.prm1 = varList.join(',');
    objBO.login_id = Active.userId;
    objBO.hosp_id = Active.unitId;
    objBO.Logic = "InsertLabConsentForm";           
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data == 'success') {
                CKEDITOR.instances['txtContent'].setData('');
                alert('Inserted Successfully');
                $('#txtConsentTemplateName').val('');
                $('#tblTestDetails tbody input:checkbox').prop('checked',false);
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