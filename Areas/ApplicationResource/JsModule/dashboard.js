
$(document).ready(function () {
    IsInternetConnected();
});
function IsInternetConnected() {
    const online = window.navigator.onLine;
    if (online) {
        $('html').removeAttr('style');
    }
    else {
        $('html').css('filter','blur(2px)')
    }
}