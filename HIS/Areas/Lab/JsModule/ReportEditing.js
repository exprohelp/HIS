var _VisitNo;
var _SubCat;
var _selectedTextGroup;
var _currentSelectedReport;
var _testCode;
var _autoTestId;
var _observationId;
$(document).ready(function () {
    CKEDITOR.replace("txtTestComment");
    CKEDITOR.editorConfig = function (config) {
        config.removePlugins = 'blockquote,save,flash,iframe,tabletools,pagebreak,templates,about,showblocks,newpage,language,print,div';
        config.removeButtons = 'Print,Form,TextField,Textarea,Button,CreateDiv,PasteText,PasteFromWord,Select,HiddenField,Radio,Checkbox,ImageButton,Anchor,BidiLtr,BidiRtl,Font,Format,Styles,Preview,Indent,Outdent';
    };
    RowSequence(['#tblObservationDetails']);
    CloseSidebar();
    $('select').select2();
    FillCurrentDate('txtFrom');
    FillCurrentDate('txtTo');
    $("#tblTestInfo tbody").on("change", 'select', function () {
        var value = $(this).find('option:selected').text();
        $(this).siblings('input:text').val(value);                
    });
    $("#tblReport tbody").on("click", 'button', function () {
        selectRow($(this));
        _currentSelectedReport = $(this);
        ReportDetail();
    });
    $("#tblTemplateInfo tbody").on("click", 'button', function () {
        var value = $(this).closest('tr').find('td:eq(0)').html();
        var id = $(_selectedTextGroup).closest('tr').next('tr.Text').find('td:eq(2)').find('textarea').attr('id');
        CKEDITOR.instances[id].setData(value);
        $('#modalTemplateInfo').modal('hide');
    });
    $("table thead").on("change", 'input:checkbox', function () {
        $(this).parents('table').find('tbody input:checkbox').prop('checked', $(this).is(':checked'));
    });
    $("table").on("click", '#testMore', function () {
        var value = $(this).data('test');
        $('#modalTestPreview').find('label').text(value);
        $('#modalTestPreview').modal('show');
    });
    $("table").on("click", 'i.test-comment', function () {
        //CKEDITOR.instances['txtTestComment'].setData();
        _autoTestId = $(this).closest('tr').find('td:eq(0)').text();
        _testCode = $(this).closest('tr').find('td:eq(1)').text();
        _observationId = $(this).closest('tr').find('td:eq(2)').text();
        var comment = $(this).closest('tr').find('td:last').html();
        CKEDITOR.instances['txtTestComment'].setData(comment);
        $('#modalTestComment').modal('show');
    });
    $("#tblTestInfo tbody").on("keyup", 'input.value', function () {
        var value = parseFloat($(this).val() | 0);
        var min = parseFloat($(this).data('min'));
        var max = parseFloat($(this).data('max'));
        if (value < 0) { $(this).closest('tr').find('td:eq(9)').text('-'); $(this).closest('tr').find('td:eq(9)').removeAttr('class'); }
        else if (value < min) { $(this).closest('tr').find('td:eq(9)').text('L'); $(this).closest('tr').find('td:eq(9)').addClass('lowFlag'); }
        else if (value > max) { $(this).closest('tr').find('td:eq(9)').text('H'); $(this).closest('tr').find('td:eq(9)').addClass('highFlag'); }
        else if (value >= min && value <= max) { $(this).closest('tr').find('td:eq(9)').text('N'); $(this).closest('tr').find('td:eq(9)').removeAttr('class'); }
    });
});
function LabReporting(logic) {
    $('#tblReport tbody').empty();
    $('#tblTestInfo tbody').empty();
    var url = config.baseUrl + "/api/sample/LabReporting_Queries";
    var objBO = {};
    objBO.LabCode = Active.HospId;
    objBO.IpOpType = $('#ddlIpOpType option:selected').text();
    objBO.ReportStatus = $('#ddlStatus option:selected').text();
    objBO.VisitNo = $('#txtInput').val();
    objBO.BarccodeNo = $('#txtInput').val();
    objBO.SubCat = '-';
    objBO.TestCategory = '-';
    objBO.AutoTestId = 0;
    objBO.TestCode = '-';
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Logic = logic;
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
                        if (temp != val.patient_name) {
                            tbody += "<tr style='background:#cfcfcf'>";
                            tbody += "<td colspan='5'>" + val.patient_name + "</td>";
                            tbody += "</tr>";
                            temp = val.patient_name;
                        }
                        if (val.ReportStatus == 'Approved')
                            tbody += "<tr style='background:#bbffc9'>";
                        else if (val.ReportStatus == 'Tested')
                            tbody += "<tr style='background:#fbc7a9'>";
                        else if (val.ReportStatus == 'Partialy-Approved')
                            tbody += "<tr style='background:#fbeda9'>";
                        else
                            tbody += "<tr>";

                        tbody += "<td class='hide'>" + val.SubCatId + "</td>";
                        tbody += "<td>" + val.RegDate + "</td>";
                        tbody += "<td>" + val.VisitNo + "</td>";
                        tbody += "<td>" + val.testCategory + "</td>";
                        tbody += "<td style=width:1%><button class='btn btn-success btn-xs'><span class='fa fa-arrow-right'></button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblReport tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ApprovedTestInfo() {
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
    objBO.Logic = 'ApprovedTestInfo';
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
                        tbody += "<tr>";
                        tbody += "<td style='width:1%;padding: 0'><input type='checkbox' checked/></td>";
                        tbody += "<td class='hide'>" + val.TestCode + "</td>";
                        tbody += "<td>" + val.TestName + "</td>";
                        tbody += "<td>" + val.ApproveBy + "</td>";
                        tbody += "<td>" + val.ApprovedDate + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblApproveTestInfo tbody').append(tbody);
                    $('#modalUnApproveInfo').modal('show');
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ReportDetail() {
    _SubCat = $(_currentSelectedReport).closest('tr').find('td:eq(0)').text();
    _VisitNo = $(_currentSelectedReport).closest('tr').find('td:eq(2)').text();
    $('#tblTestInfo tbody').empty();
    var url = config.baseUrl + "/api/sample/LabReporting_Queries";
    var objBO = {};
    objBO.LabCode = Active.HospId;
    objBO.IpOpType = $('#ddlIpOpType option:selected').text();
    objBO.ReportStatus = $('#ddlStatus option:selected').text();
    objBO.SubCat = _SubCat;
    objBO.VisitNo = _VisitNo;
    objBO.BarccodeNo = $('#txtInput').val();
    objBO.TestCategory = '-';
    objBO.AutoTestId = 0;
    objBO.TestCode = '-';
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Logic = 'ReportDetail';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data)
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('span[data-petientname]').text(val.patient_name);
                        $('span[data-gender]').text(val.gender);
                        $('span[data-age]').text(val.ageInfo);
                        $('span[data-ipop]').text(val.ipop_no);
                        $('span[data-visitno]').text(val.VisitNo);
                        $('span[data-regdate]').text(val.RegDate);
                        var more = "<button id='testMore' class='btn btn-warning btn-xs pull-right' data-test='" + val.TestList + "'>More</button>";
                        if (val.TestList.length > 110)
                            $('span[data-testname]').html("<b>Test Name : </b>" + val.TestList + more);
                        else
                            $('span[data-testname]').html("<b>Test Name : </b>" + val.TestList);
                    });
                }
            }
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    var tbody = '';
                    var temp = '';
                    $.each(data.ResultSet.Table1, function (key, val) {
                        if (val.r_type == 'Text') {
                            if (val.IsTested == 1)
                                tbody += "<tr style='background:#fbc7a9'>";
                            else
                                tbody += "<tr>";

                            tbody += "<td colspan='8'><i onclick=ShowHideEditor(this) class='fa fa-snowflake-o'>&nbsp;</i>" + val.TestName;
                            tbody += "<button data-testcode=" + val.testcode + " onclick=ApproveTest(this) class='btn btn-success btn-xs pull-right'><i class='fa fa-check-circle'>&nbsp;</i>Approve</button>";
                            tbody += "<button data-testcode=" + val.testcode + " onclick=TemplateByTestCode(this) class='btn btn-warning btn-xs pull-right'><i class='fa fa-file'>&nbsp;</i>Template</button>";
                            tbody += "</td>";
                            tbody += "</tr>";
                            tbody += "<tr class=" + val.r_type + ">";
                            tbody += "<td class='hide'>" + val.AutoTestId + "</td>";
                            tbody += "<td class='hide'>" + val.testcode + "</td>";
                            tbody += "<td colspan='8'><textarea id='txtTestContent" + val.AutoTestId + "' class='form-control'></textarea></td>";
                            tbody += "<td class='hide'>" + val.report_content + "</td>";
                            tbody += "</tr>";
                        } else {
                            if (val.ObsCount == 1) {
                                $.each(data.ResultSet.Table2, function (key, val1) {
                                    if (val.AutoTestId == val1.AutoTestId) {
                                        tbody += "<tr class=" + val.r_type + ">";
                                        if (val1.IsTested == 1)
                                            tbody += "<tr style='background:#fbc7a9' class=" + val.r_type + ">";

                                        if (val1.IsApproved == 1)
                                            tbody += "<tr style='background:#bbffc9' class=" + val.r_type + ">";

                                        tbody += "<td class='hide'>" + val1.AutoTestId + "</td>";
                                        tbody += "<td class='hide'>" + val1.testcode + "</td>";
                                        tbody += "<td class='hide'>" + val1.ObservationId + "</td>";
                                        tbody += "<td class='hide'>" + val1.min_value + "</td>";
                                        tbody += "<td class='hide'>" + val1.max_value + "</td>";
                                        tbody += "<td class='hide'>" + val1.result_unit + "</td>";
                                        var bgComment = (val1.test_comment != '') ? '#f98a01' : '#b3b0b0';
                                        if (val1.IsApproved == 1)
                                            tbody += "<td><i style='background:" + bgComment + "' class='test-comment'>cm</i>" + val1.ObservationName + "<i class='fa fa-check-circle pull-right text-success'></i></td>";
                                        else
                                            tbody += "<td><i style='background:" + bgComment + "' class='test-comment'>cm</i>" + val1.ObservationName + "</td>";

                                        tbody += "<td><input type='text' value='" + val1.read_1 + "' data-min='" + val1.min_value + "' data-max='" + val1.max_value + "' class='form-control value'/></td>";
                                        tbody += "<td>";
                                        tbody += "<select class='form-control textValue'>";

                                        for (var i = 0; i < val1.DefaultValue.split('|').length; i++)
                                            tbody += "<option>" + val1.DefaultValue.split('|')[i];

                                        tbody += "</select>";
                                        tbody += "<input value='" + val1.read_2 + "' class='form-control textValue'/>";
                                        tbody += "</td>";
                                        if (val1.ab_flag == 'L')
                                            tbody += "<td class='lowFlag'>" + val1.ab_flag + "</td>";
                                        else if (val1.ab_flag == 'H')
                                            tbody += "<td class='highFlag'>" + val1.ab_flag + "</td>";
                                        else
                                            tbody += "<td>" + val1.ab_flag + "</td>";

                                        tbody += "<td>" + val1.min_value + ' - ' + val1.max_value + ' ' + val1.result_unit + "</td>";
                                        tbody += "<td>" + val1.method_name + "</td>";
                                        tbody += "<td>" + val1.mac_name + "</td>";
                                        tbody += "<td>" + val1.mac_reading + "</td>";
                                        tbody += "<td class='hide'>" + val1.test_comment + "</td>";
                                        tbody += "</tr>";
                                    }
                                });

                            }
                            else {
                                tbody += "<tr style='background:#ddd'>";
                                tbody += "<td colspan='8'>" + val.TestName + "</td>";
                                tbody += "</tr>";

                                $.each(data.ResultSet.Table2, function (key, val1) {
                                    if (val.AutoTestId == val1.AutoTestId) {
                                        if (temp != val1.HeaderName && val1.HeaderName != '-') {
                                            tbody += "<tr style='background:#f9e7bf'>";
                                            tbody += "<td colspan='8'>" + val1.HeaderName + "</td>";
                                            tbody += "</tr>";
                                            temp = val1.HeaderName;
                                        }
                                        tbody += "<tr class=" + val.r_type + ">";
                                        if (val1.IsTested == 1)
                                            tbody += "<tr style='background:#fbc7a9' class=" + val.r_type + ">";

                                        if (val1.IsApproved == 1)
                                            tbody += "<tr style='background:#bbffc9' class=" + val.r_type + ">";

                                        tbody += "<td class='hide'>" + val1.AutoTestId + "</td>";
                                        tbody += "<td class='hide'>" + val1.testcode + "</td>";
                                        tbody += "<td class='hide'>" + val1.ObservationId + "</td>";
                                        tbody += "<td class='hide'>" + val1.min_value + "</td>";
                                        tbody += "<td class='hide'>" + val1.max_value + "</td>";
                                        tbody += "<td class='hide'>" + val1.result_unit + "</td>";
                                        var bgComment = (val1.test_comment != '') ? '#f98a01' : '#b3b0b0';
                                        if (val1.IsApproved == 1)
                                            tbody += "<td><i style='background:" + bgComment + "' class='test-comment'>cm</i>" + val1.ObservationName + "<i class='fa fa-check-circle pull-right text-success'></i></td>";
                                        else
                                            tbody += "<td><i style='background:" + bgComment + "' class='test-comment'>cm</i>" + val1.ObservationName + "</td>";

                                        tbody += "<td><input type='text' value='" + val1.read_1 + "' data-min='" + val1.min_value + "' data-max='" + val1.max_value + "' class='form-control value'/></td>";
                                        tbody += "<td>";
                                        tbody += "<select class='form-control textValue'>";

                                        for (var i = 0; i < val1.DefaultValue.split('|').length; i++)
                                            tbody += "<option>" + val1.DefaultValue.split('|')[i];

                                        tbody += "</select>";
                                        tbody += "<input value='" + val1.read_2 + "' class='form-control textValue'/>";
                                        tbody += "</td>";
                                        if (val1.ab_flag == 'L')
                                            tbody += "<td class='lowFlag'>" + val1.ab_flag + "</td>";
                                        else if (val1.ab_flag == 'H')
                                            tbody += "<td class='highFlag'>" + val1.ab_flag + "</td>";
                                        else
                                            tbody += "<td>" + val1.ab_flag + "</td>";

                                        tbody += "<td>" + val1.min_value + ' - ' + val1.max_value + ' ' + val1.result_unit + "</td>";
                                        tbody += "<td>" + val1.method_name + "</td>";
                                        tbody += "<td>" + val1.mac_name + "</td>";
                                        tbody += "<td>" + val1.mac_reading + "</td>";
                                        tbody += "<td class='hide'>" + val1.test_comment + "</td>";
                                        tbody += "</tr>";
                                    }
                                });
                            }
                        }
                    });
                    $('#tblTestInfo tbody').append(tbody);
                }
                $("#ddlApproveByDoctor").empty().append($("<option></option>").val("Select").html("Select")).select2();
                if (Object.keys(data.ResultSet).length > 0) {
                    if (Object.keys(data.ResultSet.Table3).length > 0) {
                        $.each(data.ResultSet.Table3, function (key, value) {
                            $("#ddlApproveByDoctor").append($("<option></option>").val(value.DoctorId).html(value.DoctorName));
                        });
                    }
                }
                CKEditor();
            }
        },
        complete: function (response) {
            //$('#tblTestInfo tbody').find('tr.Text').each(function () {
            //    // $(this).find('td:eq(2)').find('.ck-content').html("<b>est Reporrkjr</b>");
            //    const editor = ClassicEditor.create(document.querySelector('#txtTestContent' + $(this).find('td:eq(0)').text()));               
            //    editor.data.set("<p>Testing</p>");              

            //});
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function TemplateByTestCode(elem) {
    _selectedTextGroup = $(elem);
    $('#tblTemplateInfo tbody').empty();
    var url = config.baseUrl + "/api/sample/LabReporting_Queries";
    var objBO = {};
    objBO.LabCode = Active.HospId;
    objBO.IpOpType = '-';
    objBO.ReportStatus = '-';
    objBO.VisitNo = '-';
    objBO.BarccodeNo = '-';
    objBO.SubCat = '-';
    objBO.TestCategory = '-';
    objBO.AutoTestId = 0;
    objBO.TestCode = $(elem).data('testcode');
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Logic = 'TemplateByTestCode';
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
                        tbody += "<tr>";
                        tbody += "<td class='hide'>" + val.template_content + "</td>";
                        tbody += "<td>" + srno + "</td>";
                        tbody += "<td>" + val.template_name + "</td>";
                        tbody += "<td>" + val.cr_date + "</td>";
                        tbody += "<td style=width:1%><button class='btn btn-warning btn-xs'><span class='fa fa-sign-in'>&nbsp;</span>Select</button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblTemplateInfo tbody').append(tbody);
                    $('#modalTemplateInfo').modal('show');
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ShowHideEditor(elem) {
    var id = $(elem).closest('tr').next('tr.Text').find('td:eq(2)').find('textarea').attr('id');
    $('.cke_top').fadeToggle();
    $('.ml15').fadeToggle();
    $('.col-md-8').toggleClass('col-md-12');
}
function CKEditor() {
    let editor1;
    $('#tblTestInfo tbody').find('tr.Text').each(function () {
        var data = $(this).find('td:eq(3)').html();
        var id = '#txtTestContent' + $(this).find('td:eq(0)').text();
        var id1 = 'txtTestContent' + $(this).find('td:eq(0)').text();
        CKEDITOR.replace(id1);
        CKEDITOR.instances[id1].setData(data);
        CKEDITOR.editorConfig = function (config) {
            // Define changes to default configuration here. For example:
            // config.language = 'fr';
            // config.uiColor = '#AADC6E';
            config.removePlugins = 'blockquote,save,flash,iframe,tabletools,pagebreak,templates,about,showblocks,newpage,language,print,div';
            config.removeButtons = 'Print,Form,TextField,Textarea,Button,CreateDiv,PasteText,PasteFromWord,Select,HiddenField,Radio,Checkbox,ImageButton,Anchor,BidiLtr,BidiRtl,Font,Format,Styles,Preview,Indent,Outdent';
        };
    });
}
function SaveTestResultEntry(entrySaveType) {
    var objBO = [];
    var url = config.baseUrl + "/api/sample/Lab_ResultEntry";
    $('#tblTestInfo tbody tr').each(function () {
        if ($(this).attr('class') == 'Text') {
            objBO.push({
                'VisitNo': _VisitNo,
                'SubCat': _SubCat,
                'AutoTestId': $(this).find('td:eq(0)').text(),
                'TestCode': $(this).find('td:eq(1)').text(),
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
                'r_type': $(this).attr('class'),
                'report_text_content': CKEDITOR.instances["txtTestContent" + $(this).find('td:eq(0)').text()].getData(),
                'DoctorSignId': $('#ddlApproveByDoctor option:selected').val(),
                'EntrySaveType': '-',
                'login_id': Active.userId,
                'EntrySaveType': entrySaveType,
                'Logic': 'TestResultEntry'
            });
        }
        if ($(this).attr('class') == 'Value') {
            objBO.push({
                'VisitNo': _VisitNo,
                'SubCat': _SubCat,
                'AutoTestId': $(this).find('td:eq(0)').text(),
                'TestCode': $(this).find('td:eq(1)').text(),
                'ObservationId': $(this).find('td:eq(2)').text(),
                'ab_flag': $(this).find('td:eq(9)').text(),
                'read_1': $(this).find('td:eq(7)').find('input').val(),
                'read_2': $(this).find('td:eq(8)').find('input.textValue').val(),
                'test_comment': '-',
                'min_value': $(this).find('td:eq(3)').text(),
                'max_value': $(this).find('td:eq(4)').text(),
                'nr_range': '-',
                'result_unit': $(this).find('td:eq(5)').text(),
                'method_name': $(this).find('td:eq(11)').text(),
                'r_type': $(this).attr('class'),
                'report_text_content': '-',
                'DoctorSignId': $('#ddlApproveByDoctor option:selected').val(),
                'EntrySaveType': entrySaveType,
                'login_id': Active.userId,
                'Logic': 'TestResultEntry'
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
                alert(data)
                ReportDetail();
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
function ApproveTest(elem) {
    if (confirm('Are you sure to Approve?')) {
        var objBO = [];
        var url = config.baseUrl + "/api/sample/Lab_ResultEntry";
        objBO.push({
            'VisitNo': _VisitNo,
            'SubCat': _SubCat,
            'AutoTestId': 0,
            'TestCode': $(elem).data('testcode'),
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
            'r_type': "Text",
            'report_text_content': '-',
            'DoctorSignId': $('#ddlApproveByDoctor option:selected').val(),
            'EntrySaveType': '-',
            'login_id': Active.userId,
            'EntrySaveType': 'Approved',
            'Logic': 'TestResultEntry'
        });
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data)
                    ReportDetail();
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
function UnApproveTest() {
    if (confirm('Are you sure to Un-Approve?')) {
        var objBO = [];
        var url = config.baseUrl + "/api/sample/Lab_ResultEntry";
        $('#tblApproveTestInfo tbody tr').each(function () {
            if ($(this).find('td:eq(0)').find('input:checkbox:checked')) {
                objBO.push({
                    'VisitNo': _VisitNo,
                    'SubCat': _SubCat,
                    'AutoTestId': 0,
                    'TestCode': $(this).find('td:eq(1)').text(),
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
                    'DoctorSignId': $('#ddlApproveByDoctor option:selected').val(),
                    'EntrySaveType': '-',
                    'login_id': Active.userId,
                    'EntrySaveType': '-',
                    'Logic': 'Un-Approved'
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
                    $('#tblApproveTestInfo tbody').empty();
                    $('#modalUnApproveInfo').modal('hide');
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
function TestComment() {
    var objBO = [];
    var url = config.baseUrl + "/api/sample/Lab_ResultEntry";
    objBO.push({
        'VisitNo': _VisitNo,
        'SubCat': _SubCat,
        'AutoTestId': _autoTestId,
        'TestCode': _testCode,
        'ObservationId': _observationId,
        'ab_flag': '-',
        'read_1': '-',
        'read_2': '-',
        'test_comment': CKEDITOR.instances['txtTestComment'].getData(),
        'min_value': '-',
        'max_value': '-',
        'nr_range': '-',
        'result_unit': '-',
        'method_name': '-',
        'r_type': "Text",
        'report_text_content': '-',
        'DoctorSignId': $('#ddlApproveByDoctor option:selected').val(),
        'EntrySaveType': '-',
        'login_id': Active.userId,
        'EntrySaveType': '-',
        'Logic': 'InsertTestComment'
    });
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data)
                CKEDITOR.instances['txtTestComment'].setData('');
                $('#modalTestComment').modal('hide');
                ReportDetail();
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