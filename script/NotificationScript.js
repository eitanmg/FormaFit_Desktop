$(document).ready(function () {

    //###################### Change DDL part #########################

    var motivationArea = document.getElementById("allMotivationPage");
    motivationArea.style.display = "none";

    $('#notificationMotivationSelect').on('change', function () {

        var whoAmI = $(this).val();
        var notificationArea = document.getElementById("allNotificationPage");
        if (whoAmI == "motivation") {
            motivationArea.style.display = "block";
            notificationArea.style.display = "none";
        }
        else {
            notificationArea.style.display = "block";
            motivationArea.style.display = "none";
        }
    });

    //###################### End of Change DDL part #########################

    //######################## Notification code ##############################

    var request = {
    };

    var dataString = JSON.stringify(request);
    $.ajax({
        url: 'FormaFitWebService.asmx/getNotificationsFromDB', 
        type: 'POST',
        contentType: 'application/json; charset = utf-8',
        dataType: 'json',
        data: dataString,
        success:
            function successCBgetNotificationsFromDB(doc) {
                doc = $.parseJSON(doc.d);
                $('#NotificationTable').bootstrapTable({
                    data: doc,
                    pagination: true,
                    pageSize: 5,
                    pageList: [5, 10, 25, 50, 100, 'All'],
                    pageNumber: 1
                });
            },
    });

    $.ajax({
        url: 'FormaFitWebService.asmx/getCurrentNumberOfNotificationFromDB',
        type: 'POST',
        contentType: 'application/json; charset = utf-8',
        dataType: 'json',
        data: dataString,
        success:
            function successCBgetCurrentNumberOfNotificationFromDB(doc) {
                doc = $.parseJSON(doc.d);
                $('#currentNumberOfNotification').text(doc[0].NotificationsToShow);
            },
    });

    var $table = $('#NotificationTable'),
    $remove = $('#remove');

    $(function () {
        $table.on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            $remove.prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $remove.click(function () {

            if (!confirm("?בטוח\\ה לגבי מחיקת הודעה\\ות אלו")) {
            return 0;
        }

            var ids = $.map($table.bootstrapTable('getSelections'), function (row) {
                return row.id
            });

            $table.bootstrapTable('remove', {
                field: 'id',
                values: ids
            });

            var parseIds = ids.join(); // parsing the ids to send it to DB in the right format

            var request = { Values: parseIds };
            var dataString = JSON.stringify(request);
            $.ajax({
                url: 'FormaFitWebService.asmx/DeleteNotificationsFromDB', // mark as inActive.. 
                type: 'POST',
                contentType: 'application/json; charset = utf-8',
                dataType: 'json',
                data: dataString,
                success:
                    function successCBDeleteNotificationsFromDB(doc) {
                        doc = $.parseJSON(doc.d);
                        alert(doc);
                    },
            });
            $remove.prop('disabled', true);
        });
    });


    $("#addMessage").click(function () {

        var messageText = $('#NotificationText').val();

        if (messageText == "") {
            alert("הודעה אינה יכולה להיות ריקה");
            return false;
        }
        else if ($('#NotificationText').val().length > 1000)
        {
            alert("הודעה אינה יכולה להיות מעל 1000 תווים");
            return false;
        }

        var dateObj = new Date();
        var month = ('0' + (dateObj.getMonth() + 1)).slice(-2);
        var day = ('0' + dateObj.getDate()).slice(-2);
        var year = dateObj.getUTCFullYear();
        myDate = year + "-" + month + "-" + day;
        timeText = dateObj.toTimeString();
        myTime = timeText.split(' ')[0];
        finalDateAndTime = myDate + " " + myTime;
        var request = { messageText: messageText, finalDateAndTime: finalDateAndTime };
        var dataString = JSON.stringify(request);
        $.ajax({
            url: 'FormaFitWebService.asmx/addNewMessageToDB',
            type: 'POST',
            contentType: 'application/json; charset = utf-8',
            dataType: 'json',
            data: dataString,
            success:
                function successCBaddNewMessageToDB(result) {
                    result = $.parseJSON(result.d);
                    alert(result);
                    $('#NotificationText').val('');
                    updateMessageTable();
                }
        });
    });

    $("#updateHowManyMessagesToShowBTN").click(function () {

        var messageToShowValue = $('#messageChooseSelect').find(":selected").text();

        if (messageToShowValue == "לחץ לשינוי") {
            alert("אנא בחר כמות מספר הודעות לתצוגה");
            return 0;
        }

        var request = { messageToShowValue: messageToShowValue };
        var dataString = JSON.stringify(request);
        $.ajax({
            url: 'FormaFitWebService.asmx/changeCurrentNumberOfNotificationOnDB',
            type: 'POST',
            contentType: 'application/json; charset = utf-8',
            dataType: 'json',
            data: dataString,
            success:
                function successCBaddNewMessageToDB(result) {
                    result = $.parseJSON(result.d);
                    alert(result);
                    $('#currentNumberOfNotification').text(messageToShowValue);
                }
        });
    });

    $("#NotificationTable").on("click", ".notificationTD", function () {

        $.fn.editable.defaults.mode = 'inline';

        $.fn.editableform.buttons =
            '<button id="EditMessageBTN" type="submit" class="btn btn-primary editable-submit"><span class="glyphicon glyphicon-ok"></span></button>'
            + '&nbsp&nbsp' +
            '<button id="CancelMessageBTN" type="button" class="btn btn-danger editable-cancel"><span class="glyphicon glyphicon-remove"></span></button>'

        $('.notificationTD').editable({
            tpl: "<input type='text' style='width: 800px; height: 35px'>",        
            success: function (data, config)
            {
                var newCellTEXT = config;
                var rowID = $(this).closest('tr').children('td.idTD').text();

                if (newCellTEXT == "")
                {
                    alert("הודעה אינה יכולה להיות ריקה");
                    return false;
                }

                var request = {
                    newCellTEXT: newCellTEXT,
                    rowID: rowID
                };
                var dataString = JSON.stringify(request);
                $.ajax({
                    url: 'FormaFitWebService.asmx/EditNotificationMessageInDB',
                    type: 'POST',
                    contentType: 'application/json; charset = utf-8',
                    dataType: 'json',
                    data: dataString,
                    success:
                           function successCBEditNotificationMessageInDB(result) {
                            result = $.parseJSON(result.d);
                            //alert(result);
                        },
                    error: function errorCBEditNotificationMessageInDB(e) {
                        alert("something went wrong... :) " + e.responseText)
                    }
                });
            }
        });
    });

    //######################## End Of Notification code #####################################


    //################################### Motivation sentences part ######################### 

    var MotivationSentencesrequest = {
    };

    var dataString = JSON.stringify(MotivationSentencesrequest);
    $.ajax({
        url: 'FormaFitWebService.asmx/getMotivationSentencesFromDB',
        type: 'POST',
        contentType: 'application/json; charset = utf-8',
        dataType: 'json',
        data: dataString,
        success:
            function successCBgetMotivationSentencesFromDB(res) {
                res = $.parseJSON(res.d);
                $('#motivationTable').bootstrapTable({
                    data: res,
                    pagination: true,
                    pageSize: 10,
                    pageList: [10, 25, 50],
                    pageNumber: 1
                });
            },
    });
    
    $("#addMotivationSentenceBTN").click(function () {

        var SentenceText = '"' + $('#motivationTextArea').val() + '"';

        if (SentenceText == '""') {
            alert("משפט אינו יכול להיות ריק");
            return false;
        }
        else if ($('#motivationTextArea').val().length > 1000) {
            alert("משפט אינו יכול להיות מעל 1000 תווים");
            return false;
        }

        var dateObj = new Date();
        var month = ('0' + (dateObj.getMonth() + 1)).slice(-2);
        var day = ('0' + dateObj.getDate()).slice(-2);
        var year = dateObj.getUTCFullYear();
        myDate = year + "-" + month + "-" + day;
        timeText = dateObj.toTimeString();
        myTime = timeText.split(' ')[0];
        finalDateAndTime = myDate + " " + myTime;

        var request = { SentenceText: SentenceText, finalDateAndTime: finalDateAndTime };
        var dataString = JSON.stringify(request);
        $.ajax({
            url: 'FormaFitWebService.asmx/addNewMotivationSentenceToDB',
            type: 'POST',
            contentType: 'application/json; charset = utf-8',
            dataType: 'json',
            data: dataString,
            success:
                function addNewMotivationSentenceToDB(result) {
                    result = $.parseJSON(result.d);
                    alert(result);
                    $('#motivationTextArea').val('');
                    var $table = $('#motivationTable');
                    $table.bootstrapTable('insertRow', {
                        index: 0,
                        row: {
                            content: SentenceText,
                            DateAndTimeOfPublish: finalDateAndTime
                        }
                    });
                }
        });
    });

    var $motivationtable = $('#motivationTable'),
    $removeMotivationSentence = $('#removeMotivationSentence');

    $(function () {
        $motivationtable.on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            $removeMotivationSentence.prop('disabled', !$motivationtable.bootstrapTable('getSelections').length);
        });

        $removeMotivationSentence.click(function () {

            if (!confirm("?בטוח\\ה לגבי מחיקת משפט\\ים אלה")) {
                return false;
            }

            var ids = $.map($motivationtable.bootstrapTable('getSelections'), function (row) {
                return row.id
            });

            $motivationtable.bootstrapTable('remove', {
                field: 'id',
                values: ids
            });

            var parseIds = ids.join(); // parsing the ids to send it to DB in the right format

            var request = { Values: parseIds };
            var dataString = JSON.stringify(request);
            $.ajax({
                url: 'FormaFitWebService.asmx/DeleteMotivationSentenceFromDB',
                type: 'POST',
                contentType: 'application/json; charset = utf-8',
                dataType: 'json',
                data: dataString,
                success:
                    function successCBDeleteMotivationSentenceFromDB(doc) {
                        doc = $.parseJSON(doc.d);
                        alert(doc);
                    },
            });
            $removeMotivationSentence.prop('disabled', true);
        });
    });
});

function updateMessageTable() {

    location.reload();
}

function updateMotivationTable() {

    var MotivationSentencesrequest = {};
    var dataString = JSON.stringify(MotivationSentencesrequest);
    $.ajax({
        url: 'FormaFitWebService.asmx/getMotivationSentencesFromDB',
        type: 'POST',
        contentType: 'application/json; charset = utf-8',
        dataType: 'json',
        data: dataString,
        success:
            function successCBgetMotivationSentencesFromDB(res) {
                res = $.parseJSON(res.d);
                $('#motivationTable').bootstrapTable({
                    data: res,
                    pagination: true,
                    pageSize: 5, //specify 5 here
                    pageList: [10, 25, 50, 100, 'All'],//list can be specified here
                    pageNumber: 1
                });
            },
    });
    location.reload();
}