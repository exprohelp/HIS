var status = "";
var _doctorId = '';
$(document).ready(function () {
    CloseSidebar();
    BindDoctorList();
});
function DisplayView() {
    var ids = [];
    $('#tblDoctorsList tbody').find('input:checkbox:checked').each(function () {
        ids.push($(this).closest('tr').find('td:eq(0)').text());
    });
    var DoctorIds = ids.join('|');
    var url = config.rootUrl + "/OPD/Appointment/DoctorDisplay?DoctorIds="+DoctorIds;
    $('#frameDisplayView').prop('src', url);
}
function BindDoctorList() {
    $('#tblDoctorsList tbody').empty();
    var url = config.baseUrl + "/api/master/DoctorMasterQueries";
    var objBO = {};
    objBO.hosp_id = Active.unitId;
    objBO.login_id = Active.userId;
    objBO.prm_1 = $("input[name='status']:checked").val();
    objBO.Logic = "GetDoctorsList";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            var Deptname = ""
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (Deptname != val.DepartmentName) {
                            tbody += '<tr>';
                            tbody += '<td colspan="4" style="font-weight:bold;background:#fbead1">' + val.DepartmentName + '</td>';
                            tbody += '</tr>';
                            Deptname = val.DepartmentName;
                        }
                        tbody += '<tr>';
                        tbody += '<td>' + val.DoctorId + '</td>';
                        tbody += '<td>' + val.DoctorName + '</td>';
                        tbody += '<td><input type="checkbox"/></td>';
                        //tbody += '<td><button class="btn-warning btn-tbl" onclick=selectRow(this);EditDoctor("' + val.DoctorId + '")><i class="fa fa-sign-in"></i></button></td>';
                        tbody += '</tr>';
                    });
                    $('#tblDoctorsList tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}