function openSuccessModal(strMessage) {
    var myDiv = document.getElementById("myModalSuccessAlertBody");
    myDiv.innerHTML = strMessage;
    $('#myModalSuccess').modal('show');
}

function openErrorModal(strMessage) {
    var myDiv = document.getElementById("myModalErrorAlertBody");
    myDiv.innerHTML = strMessage;
    $('#myModalError').modal('show');
}
