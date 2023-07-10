$(document).ready(function () {
    CloseSidebar();
    Onload();
    searchTable('txtSeachRatePanel', 'tblRatePanelDetails');
    $("select").select2();
    $("#tblRateListInfo tbody").on('keyup', 'input:text', function (e) {
        if (/[^\d.]/g.test(this.value)) {
            this.value = this.value.replace(/[^\d.]/g, '');
        }
    });
    $("#tblRateListInfo tbody").on('keyup', 'input:text', function () {
        var rate = parseFloat($(this).val());
        if (rate < 0) {
            $(this).val('');
        }
    });
    $("#tblRateListInfo tbody").on('click', 'button', function () {
        var ItemId = $(this).closest('tr').find('td:eq(0)').text();
        var rate = $(this).siblings('input').val();
        $("#tblRateListInfo tbody tr").each(function () {
            if ($(this).find('td:eq(0)').text() == ItemId)
                $(this).find('input:text').val(rate);
        });
    });
});

function Onload() {
    var url = config.baseUrl + "/api/EDP/PanelRateQueries";
    var objBO = {};
    objBO.Logic = "OnLoad";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $("#ddlCategory").append($("<option></option>").val("0").html("Select")).select2();
                    $.each(data.ResultSet.Table, function (key, value) {
                        $("#ddlCategory").append($("<option></option>").val(value.CatID).html(value.CatName));
                    });
                }
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    $("#ddlSubCategory").append($("<option selected></option>").val("0").html("Select")).select2();
                    $.each(data.ResultSet.Table1, function (key, value) {
                        $("#ddlSubCategory").append($("<option></option>").val(value.SubCatID).html(value.SubCatName));
                    });
                }
                if (Object.keys(data.ResultSet.Table2).length > 0) {
                    $("#ddlRateList").append($("<option></option>").val("0").html("Select")).select2();
                    //$("#ddlRateList").append($("<option selected></option>").val("ALL").html("ALL"));
                    $.each(data.ResultSet.Table2, function (key, value) {
                        $("#ddlRateList").append($("<option></option>").val(value.RateListId).html(value.RateListName));
                    });
                }
                //if (Object.keys(data.ResultSet.Table4).length > 0) {
                //    //Room Type
                //}
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
function GetItemList() {
    $("#tblItemList tbody").empty();
    var url = config.baseUrl + "/api/EDP/PanelRateQueries";
    var objBO = {};
    objBO.RateListId = $("#ddlRateList option:selected").val();
    objBO.subcatid = $("#ddlSubCategory option:selected").val();
    objBO.prm_1 = $("#ddlRoomType option:selected").val();
    objBO.catid = $("#ddlCategory option:selected").val();
    objBO.Logic = "GetItemList";
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
                    $.each(data.ResultSet.Table, function (key, val) {
                        htmldata += '<tr>';
                        htmldata += '<td><input type="checkbox" data-itemid="' + val.ItemId + '"/></td>';
                        htmldata += '<td>' + val.ItemId + '</td>';
                        htmldata += '<td>' + val.ItemName + '</td>';
                        htmldata += '</tr>';
                    });
                    $("#tblItemList tbody").append(htmldata);
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
function BindSubCategory() {
    $("#ddlSubCategory").empty().append($("<option></option>").val("0").html("Please Select"));
    var url = config.baseUrl + "/api/EDP/PanelRateQueries";
    var objBO = {};
    objBO.Logic = "GetSubCategoryByCategory";
    objBO.catid = $("#ddlCategory option:selected").val();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
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
function GetItemRateList() {
    var itemid = [];
    $("#tblItemList tbody input:checkbox:checked").map(function () {
        itemid.push($(this).data('itemid'));
    });
    $("#tblRateListInfo tbody").empty();
    var url = config.baseUrl + "/api/EDP/PanelRateQueries";
    var objBO = {};
    objBO.Logic = "GetRateListForUpdate";
    objBO.RateListId = $("#ddlRateList option:selected").val();
    objBO.subcatid = $("#ddlSubCategory option:selected").val();
    objBO.prm_1 = itemid.join('|');
    objBO.catid = $("#ddlCategory option:selected").val();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data)
            var htmldata = "";
            var temp = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (k, v) {
                        if (temp != v.ItemId) {
                            htmldata += '<tr class="itemGroup" style="background:#f7f0cb">';
                            htmldata += '<td class="hide">' + v.ItemId + '</td>';
                            htmldata += '<td colspan="1">' + v.ItemName + '</td>';
                            htmldata += '<td class="flex"><input  type="text" class="rate"/><button class="btnRate btn btn-warning btn-xs"><i class="fa fa-refresh"></i></button></td>';
                            htmldata += '</tr>';
                            temp = v.ItemId;
                        }
                        htmldata += '<tr>';
                        htmldata += '<td class="hide">' + v.ItemId + '</td>';
                        htmldata += '<td>' + v.RoomBillingCategory + '</td>';
                        if (v.rate == null)
                            htmldata += '<td><input type="text" value="" /></td>';
                        else
                            htmldata += '<td><input type="text" value="' + v.rate + '"/></td>';

                        htmldata += '</tr>';
                    });
                    $("#tblRateListInfo tbody").append(htmldata);
                }
            }
        },
        complete: function (response) {         
            var index = -1;
            $("#tblRateListInfo tbody tr").each(function () {
                if ($(this).hasClass('itemGroup'))
                    index++;
                    
                if ($(this).find('td:eq(1)').text() == 'OPD') {
                    var content = $(this).closest('tr').html();
                    $(this).closest('tr').remove();
                    $('#tblRateListInfo > tbody > tr.itemGroup').eq(index).after("<tr class='bg-success'>" + content+"</tr>");                    
                }
            });
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function RoomChargesUpdate() {
    var url = config.baseUrl + "/api/EDP/PanelItemRateInsertUpdate";
    var objBO = [];
    $('#tblRateListInfo tbody tr').each(function () {
        if ($(this).attr('style') != 'background:#f7f0cb') {
            var rate = parseFloat($(this).find("td:eq(2) input").val());
            if (rate > 0) {
                objBO.push({
                    'RateListId': $("#ddlRateList option:selected").val(),
                    'ItemId': $(this).find("td:eq(0)").text(),
                    'rate': $(this).find("td:eq(2) input").val(),
                    'ItemCode': '-',
                    'ItemName': '-',
                    'RoomBillCategory': $(this).find("td:eq(1)").text(),
                    'hosp_id': Active.HospId,
                    'login_id': Active.userId,
                    'Logic': 'RoomChargesUpdate',
                });
            }
        }
    });
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data == 'Success') {
                alert('Successfully Updated');
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
function UpdateItemRateListSingle(element) {
    var url = config.baseUrl + "/api/EDP/PanelItemRateInsertUpdate";
    var objBO = [];
    if ($(element).closest('tr').find("td:eq(6) input").val() == '') {
        alert('Please Provide Rate.');
        return
    }
    objBO.push({
        'RateListId': $(element).data('rateid'),
        'ItemId': $(element).closest('tr').find("td:eq(2)").text(),
        'rate': $(element).closest('tr').find("td:eq(6) input").val(),
        'ItemCode': $(element).closest('tr').find("td:eq(8) input").val(),
        'ItemName': $(element).closest('tr').find("td:eq(7) input").val(),
        'RoomBillCategory': $(element).data('roombillcat'),
        'hosp_id': Active.HospId,
        'login_id': Active.userId,
        'Logic': 'Insert',
    });
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var splitval = data.split('|');
            if (splitval[0] == '1' && splitval[1] == 'success') {
                alert('PanelItemRate List created successfully');
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
function ValidateRatePanel() {
    var ratelist = $("#ddlRateList option:selected").val();
    var subcatid = $("#ddlSubCategory option:selected").val();
    if (ratelist == "0") {
        alert('Please select rate list');
        return false;
    }
    if (subcatid == "0") {
        alert('Please select sub category id');
        return false;
    }
    return true;
}

