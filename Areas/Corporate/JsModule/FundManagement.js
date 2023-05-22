
$(document).ready(function () {
    GetPanel();          
});

function GetPanel() {
    $('#tblPanel tbody').empty();
    var url = config.baseUrl + "/api/Corporate/PanelQuerie";
    var objBO = {};
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Logic = 'PanelInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table9).length > 0) {
                    $.each(data.ResultSet.Table9, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td>" + val.med_TemplateName + "</td>";
                        tbody += "<td>" + val.Description + "</td>";                
                        tbody += "</tr>";
                    });
                    $('#tblPanel tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetPanelItemByPanelId(pid) {
    var url = config.baseUrl + "/api/Corporate/PanelQueries";
    var objBO = {};
    objBO.panel_id = pid;
    objBO.Logic = 'GetPanelItemByPanelId';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data != '') {
                $('#tblItem tbody').empty();
                $.each(data.ResultSet.Table, function (key, val) {
                    $('<tr><td>' + val.item_id + '</td><td>' + val.item_name + '</td><td>' + val.login_id + '</td><td>' + val.cr_date + '</td><td style="color:' + val.IsActive_color + '">' + val.IsActive + '</td>' +
                        '<td class="text-green getManf" data-item_id=' + val.item_id + ' data-mfdid=' + val.item_name + '>' +
                        '<category type="button" data-autoid=' + val.autoid + ' data-isactive=' + val.IsActive + ' class="btn btn-warning IsActive"><i class="fa fa-refresh"></i></category>' +
                        '</td></tr>').appendTo($('#tblItem tbody'));
                });
            }
            else {
                alert("Error");
            };
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function SearchMedicine(key) {
    debugger;
    var url = config.baseUrl + "/api/IPDNursing/SearchMedicine";
    var objBO = {};
    objBO.searchKey = key;
    objBO.searchType = 'ETHICAL';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#tblnavigate tbody').empty();
            if (data != '') {
                console.log(data);
                $.each(data.ResultSet.Table, function (key, val) {
                    $('#tblnavigate').show();
                    $('<tr class="searchitems" data-AlertMsg="' + val.AlertMsg + '" data-itemid=' + val.item_id + '><td>' + val.item_name + "</td></tr>").appendTo($('#tblnavigate tbody'));
                });
            }
            else {
                alert("Error");
            };
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertPanelItemExclude() {
    if (Validate()) {
        debugger;
        var url = config.baseUrl + "/api/Corporate/ExcludeItemForPanel";
        var objBO = {};
        objBO.hosp_id = Active.unitId;
        objBO.panel_id = $('span[data-pid]').text();
        objBO.item_id = $('#txtItemID').val();
        objBO.login_id = Active.userId;
        objBO.Logic = 'Insert';

        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == 'Successfully Saved') {
                    Clear();
                    var val = $('span[data-pid]').text();
                    GetPanelItemByPanelId(val);
                }
                else {
                    alert(data);
                };
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function UpdateStatus(autoid, IsActive) {
    var url = config.baseUrl + "/api/Corporate/ExcludeItemForPanel";
    var objBO = {};
    objBO.auto_id = autoid;
    objBO.isActive = IsActive;
    objBO.Logic = 'UpdateStatus';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var val = $('span[data-pid]').text();
            GetPanelItemByPanelId(val);
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

//Validation
function Validate() {
    var product = $('#txtSearchProduct').val();
    var pid = $('span[data-id]');

    if (product == '') {
        $('#txtSearchProduct').css({ 'border-color': 'red' });
        alert('Please Provide Item Name..');
        return false;
    }
    else {
        $('#txtSearchProduct').removeAttr('style').focus();
    }
    if (pid == '') {
        alert('Please Panel From Left Side..');
        return false;
    }
    return true;
}
function Clear() {
    $('input[type=text]').val('');
    $('.msg').hide();
}





