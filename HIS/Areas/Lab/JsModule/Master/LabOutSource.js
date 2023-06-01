var _labCode;
$(document).ready(function () {    
    GetLabOutSource();
    $('select').select2(); 
});
function LabOutSource() {
    var objBO = {};
    var url = config.baseUrl + "/api/sample/LabInsertUpdateOutSource";
    objBO.LabCode = _labCode;
    objBO.LabType = $('#ddlLabtype option:selected').text();
    objBO.LabName = $('#txtName').val();
    objBO.address = $('#txtdegree').val();
    objBO.cstate = $('#ddlLabState option:selected').text();
    objBO.city = $('#ddlLabDistrict option:selected').text();
    objBO.contact_no = $('#txtContactNo').val();
    objBO.cin = $('#txtCinNo').val();
    objBO.login_id = Active.userId;
    objBO.Logic = ($('#btnSubmit').text()=='Submit')?'Insert':'Update';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data);
                GetLabOutSource();
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
function UpdatStatus(labCode) {
    var objBO = {};
    var url = config.baseUrl + "/api/sample/LabInsertUpdateOutSource";
    objBO.LabCode = labCode;
    objBO.LabType = '-';
    objBO.LabName = '-';
    objBO.address = '-';
    objBO.cstate = '-';
    objBO.city = '-';
    objBO.contact_no = '-';
    objBO.cin = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'UpdatStatus';
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
function GetLabOutSource() {
    $('#tblLabOutSource tbody').empty();
    var url = config.baseUrl + "/api/Lab/LabOutSourceQueries";
    var objBO = {};
    objBO.logic = "GetLabOutSource";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data)
            var tbody = '';
            var count = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (k, val) {
                        count++;
                        tbody += "<tr>";
                        tbody += "<td>" +
                            '<category onclick=selectRow(this);GetLabOutSourceInfo("' + val.LabCode+'") type="button" class="btn btn-warning btn-xs getCat" ><i class="fa fa-edit"></i></category>' +
                            "</td>";
                        tbody += "<td>" + val.LabCode + "</td>";
                        tbody += "<td>" + val.LabName + "</td>";
                        tbody += "<td>" +
                            '<label class="switch">' +
                            '<input onchange=UpdatStatus("' + val.LabCode+'") type="checkbox" id="chkActive" ' + val.checked + '>' +
                            '<span class="slider round"></span>' +
                            '</label>' +
                            "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblLabOutSource tbody').append(tbody);

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
function GetLabOutSourceInfo(labCode) {
    Clear();
    var url = config.baseUrl + "/api/Lab/LabOutSourceQueries";
    var objBO = {};
    objBO.prm1 = labCode;
    objBO.logic = "GetLabOutSourceByLabCode";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {   
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (k, val) {
                        _labCode = val.LabCode;
                        $('#ddlLabtype option').map(function () {
                            if ($(this).text() == val.LabType)
                                $(this).parents('select').prop('selectedIndex', '' + $(this).index() + '').change();                           
                        });
                        $('#txtName').val(val.LabName);
                        $('#txtdegree').val(val.address);
                        $('#ddlLabState option').map(function () {
                            if ($(this).text() == val.cstate)
                                $(this).parents('select').prop('selectedIndex', '' + $(this).index() + '').change();                            
                        });        
                        $('#ddlLabDistrict option').map(function () {
                            if ($(this).text() == val.city)
                                $(this).parents('select').prop('selectedIndex', '' + $(this).index() + '').change();                           
                        });                         
                        $('#txtContactNo').val(val.contact_no);
                        $('#txtCinNo').val(val.cin);    
                        $('#btnSubmit').text('Update').removeClass('btn-success').addClass('btn-warning');
                    });                   
                }
            }            
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function Clear() {    
    _labCode=''   
    $('#txtName').val('');
    $('#txtdegree').val('');
    $('.panel-body select').prop('selectedIndex', '0').change(); 
    $('#txtContactNo').val('');
    $('#txtCinNo').val(''); 
    $('#btnSubmit').text('Submit').removeClass('btn-warning').addClass('btn-success');
}