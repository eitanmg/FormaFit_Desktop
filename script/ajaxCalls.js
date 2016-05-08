function CheckUserAjaxFunc(request, successCB, errorCB) {
    var dataString = JSON.stringify(request);
    $.ajax({
        url: 'FormaFitWebService.asmx/CheckUser',
        data: dataString,
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset = utf-8',
        success: successCB,
        error: errorCB
    })
}
