$(document).ready(function () {
    FillCurrentMonth('txtMonth');   
    $('#iframe').addEventListener("load", function () {
        console.log("loaded");
    });
});
function GetReport() {
    $('#iframe').attr('src','-');
    var month = $('#txtMonth').val();
    var url = "/dietician/Print/ServiceReportSlip?month" + month;
    $('#iframe').attr('src', url);
}
function FillCurrentMonth(elementid) {
    var date = new Date();
    var month = date.getMonth()+1;
    var year = date.getFullYear();
    if (month < 10) month = "0" + month;
    var today = year + "-" + month;
    $("#" + elementid).attr("value", today);
}