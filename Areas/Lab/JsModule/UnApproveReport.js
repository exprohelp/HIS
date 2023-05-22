var _VisitNo;
$(document).ready(function () {
    $('#tblApproveTestInfo tbody').on('keyup', 'input:text', function () {
        var remark = $(this).val();
        if ($(this).closest('tr').attr('class') != '') {
            var dept = $(this).closest('tr').attr('class');
            $('#tblApproveTestInfo tbody').find('tr[data-dept=' + dept + ']').find('input:text').val(remark);
            $('#tblApproveTestInfo tbody').find('tr').each(function () {
                if ($(this).find('input:text').val().trim() != '')
                    $(this).closest('tr').find('input:checkbox').prop('checked', true);
                else
                    $(this).closest('tr').find('input:checkbox').prop('checked', false);
            });
        } else if ($(this).closest('tr').data('dept') != '') {                   
            $('#tblApproveTestInfo tbody').find('tr').each(function () {
                if ($(this).find('input:text').val().trim() != '')
                    $(this).closest('tr').find('input:checkbox').prop('checked', true);
                else
                    $(this).closest('tr').find('input:checkbox').prop('checked', false);
            });
        }             
    });
});
function ApprovedTestInfo() {
    _VisitNo = $('#txtVisitNo').val();
    $('#tblApproveTestInfo tbody').empty();
    var url = config.baseUrl + "/api/sample/LabReporting_Queries";
    var objBO = {};
    objBO.LabCode = Active.HospId;
    objBO.IpOpType = '-';
    objBO.ReportStatus = '-';
    objBO.VisitNo = _VisitNo;
    objBO.BarccodeNo = '-';
    objBO.SubCat = '-';
    objBO.TestCategory = '-';
    objBO.AutoTestId = 0;
    objBO.TestCode = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Logic = 'ApprovedTestForUnApprove';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = '';
                    var temp = '';
                    var srno = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        srno++;
                        if (temp != val.testCategory) {
                            tbody += "<tr class=" + val.testCategory + " style='background:#fff0c5'>";
                            tbody += "<td colspan='5'>" + val.testCategory + "";
                            tbody += "<div class='flex'><input type='text' class='form-control' placeholder='Remark..'/>&nbsp;<input type='checkbox'/></div></td>";
                            tbody += "</tr>";
                            temp = val.testCategory;
                        }

                        tbody += "<tr data-dept=" + val.testCategory + ">";
                        tbody += "<td>" + val.TestCode + "</td>";
                        tbody += "<td>" + val.TestName + "</td>";
                        tbody += "<td>" + val.ApproveBy + "</td>";
                        tbody += "<td>" + val.ApprovedDate + "</td>";
                        tbody += "<td><div class='flex'><input type='text' class='form-control' placeholder='Remark..'/>&nbsp;<input type='checkbox'/></div></td>";
                        tbody += "</tr>";
                    });
                    $('#tblApproveTestInfo tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function UnApproveTest() {
    if (confirm('Are you sure to Un-Approve?')) {
        var objBO = [];
        var url = config.baseUrl + "/api/sample/Lab_ResultEntry";
        $('#tblApproveTestInfo tbody tr[data-dept]').each(function () {
            if ($(this).find('td:eq(4)').find('input:checkbox').is(':checked')) {
                objBO.push({
                    'VisitNo': _VisitNo,
                    'SubCat': '-',
                    'AutoTestId': 0,
                    'TestCode': $(this).find('td:eq(0)').text(),
                    'ObservationId': '-',
                    'ab_flag': '-',
                    'read_1': '-',
                    'read_2': '-',
                    'test_comment': '-',
                    'min_value': '-',
                    'max_value': '-',
                    'nr_range': '-',
                    'result_unit': '-',
                    'method_name': '-',
                    'r_type': "-",
                    'report_text_content': '-',
                    'DoctorSignId': '-',
                    'EntrySaveType': '-',
                    'login_id': Active.userId,
                    'EntrySaveType': '-',
                    'Logic': 'UnApproveTestByApproved'
                });
            }
        });
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    ApprovedTestInfo();
                }
                else {
                    alert(data)
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}