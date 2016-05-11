$(document).ready(function () {
    var currentLangCode = 'he'; // set the calendar to heb by default 
    var dateObj = new Date();
    var month = ('0' + (dateObj.getMonth() + 1)).slice(-2); //  bulid the correct date in the specfic format (leading zero's)
    var day = ('0' + dateObj.getDate()).slice(-2);//  bulid the correct date in the specfic format (leading zero's) - same as here
    var year = dateObj.getUTCFullYear();
    myDate = year + "-" + month + "-" + day;

    function renderCalendar() {
        $('#calendar').fullCalendar({
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            defaultDate: myDate, // open the calendar at the current date that i set earlier. 
            lang: currentLangCode,
            buttonIcons: false, // show the prev/next text
            weekNumbers: true,
            editable: true,
           // defaultView: 'agendaWeek',
            eventLimit: true, // allow "more" link when too many events
            events: function (start, end, timezone, callback) {  //start the ajax call to retrive all the data events from DB
                var request = {}; // its an empty requset because i send nothing to the DB 
                var dataString = JSON.stringify(request);
                $.ajax({
                    url: 'FormaFitWebService.asmx/getEventsFromDB',
                    type: 'POST',
                    contentType: 'application/json; charset = utf-8',
                    dataType: 'json',
                    data: dataString,
                    success:
                        function successCBgetEventsFromDB(doc) {
                            doc = $.parseJSON(doc.d);
                            var event = [];
                            $(doc).each(function () { // push the events json's to the calendar
                                event.push({
                                    id: $(this).attr('id'),
                                    title: $(this).attr('title') + " - " + $(this).attr('guide Name'),
                                    start: $(this).attr('start'),
                                    end: $(this).attr('end')
                                });
                            });
                            callback(event);
                        }
                });
            },
            eventResize: function (event, dayDelta, revertFunc) {
                startTime = event.start.format().split('T')[1]; // start timr cannot be change thats why i took the endtime
                var guideName = event.title.split(' - ')[1];
                var className = event.title.split(' - ')[0];
                endTime = event.end.format().split('T');
                var oldEndTime = event.end._i.split('T')[1];
                if (!confirm("הארוע יסתיים כעת בתאריך " + endTime[0] + " בשעה " + endTime[1])) {
                    revertFunc();
                }
                else {
                    var request = { // in case i will forget - i wantet to change only those parameters - because in the resize func the start time cannot be changed! 
                        id: event.id, // use it later in the DB 
                        Date: endTime[0], // it doesn't matter because in the resize function the startTime and endTime variable contain the same date 
                        endTime: endTime[1],
                        guideName: guideName,
                        className: className,
                        startTime: startTime,
                        oldEndTime: oldEndTime
                    }
                    var dataString = JSON.stringify(request); // parsing the request above. 
                    $.ajax({
                        url: 'FormaFitWebService.asmx/UpdateEventsAfterEditInDB', // function that only updating the edits. 
                        type: 'POST',
                        contentType: 'application/json; charset = utf-8',
                        dataType: 'json',
                        data: dataString,
                        success:
                            function successCBgetEventsFromDB(doc) {
                                doc = $.parseJSON(doc.d);
                                alert(doc);
                                $('#calendar').fullCalendar('refetchEvents');
                            }
                    });
                }
            },
            eventDrop: function (event, delta, revertFunc) {
                startTimeAndDate = event.start.format().split('T');
                endTimeAndDate = event.end.format().split('T');
                var OldDate = event.start._i.split('T')[0];
                var guideName = event.title.split(' - ')[1];
                var className = event.title.split(' - ')[0];
                if (!confirm("להזיז את חוג " + event.title + " לתאריך " + startTimeAndDate[0] + " שיתחיל בשעה " + startTimeAndDate[1] + " ויסתיים בשעה " + endTimeAndDate[1] + "?")) {
                    revertFunc();
                }
                else {
                    var request = {
                        id: event.id,
                        Date: startTimeAndDate[0],
                        startTime: startTimeAndDate[1],
                        endTime: endTimeAndDate[1],
                        OldDate: OldDate,
                        guideName: guideName,
                        className: className
                    }
                    var dataString = JSON.stringify(request); // parsing the request above. 
                    $.ajax({
                        url: 'FormaFitWebService.asmx/UpdateEventsAfterEditInDBbyDragging', // function that only updating the draggble events in DB. 
                        type: 'POST',
                        contentType: 'application/json; charset = utf-8',
                        dataType: 'json',
                        data: dataString,
                        success:
                            function successCBgetEventsFromDB(doc) {
                                doc = $.parseJSON(doc.d);
                                alert(doc);
                                $('#calendar').fullCalendar('refetchEvents');
                            }
                    });
                }
            },
//----------------------------------------------------------Edit exisiting event code --------------------------------------------------------------
            eventClick: function (calEvent, jsEvent, view) {

                //$("#myModalEditEvent").on('hidden.bs.modal', function ()
                //{
                //    $(this).data('bs.modal', null).unbind();
                //});

                var onlyClassNameToShowOnDDL = calEvent.title.split('-')[0].trim();
                var onlyGuideNameToShowOnDDL = calEvent.title.split('-')[1].trim();
                $('#myModalEditEvent').modal('show');
                classID = calEvent.id;
                eventStartTimeAndDateObj = calEvent.start._i.split("T");
                eventEndTimeAndDateObj = calEvent.end._i.split("T");

                $('#classStartTimeEdit').timepicker({
                    showMeridian: false
                });

                $('#classEndTimeEdit').timepicker({
                    showMeridian: false
                });

                $('#classStartTimeEdit').timepicker('setTime', eventStartTimeAndDateObj[1]);
                $('#classEndTimeEdit').timepicker('setTime', eventEndTimeAndDateObj[1]);

                var request = {};
                var dataString = JSON.stringify(request);
                $.ajax({
                    url: 'FormaFitWebService.asmx/getClassesFromDB',
                    type: 'POST',
                    contentType: 'application/json; charset = utf-8',
                    dataType: 'json',
                    data: dataString,
                    success:
                        function successCBgetClassesFromDB(result) {
                            result = $.parseJSON(result.d);
                            var $ddl = $('#ClassesDDLEdit');
                            $ddl.empty(); // remove old options
                            $ddl.append($("<option></option>")
                                    .attr("value", '').text('עריכת שם חוג'));
                            $.each(result, function (value, key) {
                                $ddl.append($('<option + value = "' + key.ClassID + '" > ' + key.ClassName + ' </option>'));
                                $('select option:contains('+ onlyClassNameToShowOnDDL +')').prop('selected', true);
                            });
                        }
                });
                $.ajax({
                    url: 'FormaFitWebService.asmx/getGuidesFromDB',
                    type: 'POST',
                    contentType: 'application/json; charset = utf-8',
                    dataType: 'json',
                    data: dataString,
                    success:
                        function successCBgetGuidesFromDB(result) {
                            result = $.parseJSON(result.d);
                            var $ddl = $('#GuidesDDLEdit');
                            $ddl.empty(); // remove old options
                            $ddl.append($("<option></option>")
                                    .attr("value", '').text('עריכת שם מדריך'));
                            $.each(result, function (value, key) {
                                $ddl.append($('<option + value = "' + key.guideID + '" > ' + key.guideName + ' </option>'));
                                $('select option:contains(' + onlyGuideNameToShowOnDDL + ')').prop('selected', true);
                            });
                        }
                });
                var MaxUserPerClassRequest = { classID: classID };
                var dataString = JSON.stringify(MaxUserPerClassRequest);
                $.ajax({
                    url: 'FormaFitWebService.asmx/getMaxUserPerClassFromDB',
                    type: 'POST',
                    contentType: 'application/json; charset = utf-8',
                    dataType: 'json',
                    data: dataString,
                    success:
                        function successCBgetGuidesFromDB(result) {
                            result = $.parseJSON(result.d);
                            $('#MaximumUsersPerClassEdit').val(result[0].MaximumRegistered);
                        }
                });


                //initial values
                classNameInitial = onlyClassNameToShowOnDDL.trim();
                guideNameInitial = onlyGuideNameToShowOnDDL.trim();
                eventStartTimeInitial = eventStartTimeAndDateObj[1];
                eventEndTimeInitial = eventEndTimeAndDateObj[1];

                eventDate = eventStartTimeAndDateObj[0];



                $('#EditEventHeader').text(" עריכת חוג " + calEvent.title + " בתאריך " + eventStartTimeAndDateObj[0] + " המתחיל בשעה " + eventStartTimeAndDateObj[1]);
                // $("#deleteEventfromEditModal").click(function () {
                $("#deleteEventfromEditModal").unbind("click").click(function () {
                    if (!confirm("?בטוח לגבי מחיקת אירוע זה")) {
                        return 0;
                    }
                    else {
                        var classID = calEvent.id;

                        var deleteRequest = {
                            classID: classID,
                        };

                        var dataString = JSON.stringify(deleteRequest); // parsing the request above. 
                        $.ajax({
                            url: 'FormaFitWebService.asmx/deleteEventFromDB', // function that only deleting the event in DB. 
                            type: 'POST',
                            contentType: 'application/json; charset = utf-8',
                            dataType: 'json',
                            data: dataString,
                            success:
                                function successCBdeleteEventFromDB(doc) {
                                    doc = $.parseJSON(doc.d);
                                    alert(doc);
                                    $('#calendar').fullCalendar('refetchEvents');
                                },
                        })
                    };
                });
                $("#AddEventAfetEditing").unbind("click").click(function () {

                    var classID = calEvent.id;
                    var className = $("#ClassesDDLEdit option:selected").val();
                    var guideID = $("#GuidesDDLEdit option:selected").val();
                    var classStartTime = $('#classStartTimeEdit').val();
                    var classEndTime = $('#classEndTimeEdit').val();
                    var newMaximumUsersPerClassEdit = $('#MaximumUsersPerClassEdit').val();

                    var classNameText = $("#ClassesDDLEdit option:selected").text().trim();
                    var guideNameText = $("#GuidesDDLEdit option:selected").text().trim();

                    var NeedToSendEmail = "";
                    var WhatHasChanged = [];
                    if (classNameText != classNameInitial || guideNameText != guideNameInitial || classStartTime != eventStartTimeInitial || classEndTime != eventEndTimeInitial)
                    {
                        NeedToSendEmail = "Yes";
                        if (classNameText != classNameInitial)
                        {
                            WhatHasChanged.push("className");
                        }
                        if (guideNameText != guideNameInitial)
                        {
                            WhatHasChanged.push("guideName");
                        }
                        if (classStartTime != eventStartTimeInitial)
                        {
                            WhatHasChanged.push("classStartTime");
                        }
                        if (classEndTime != eventEndTimeInitial)
                        {
                            WhatHasChanged.push("classEndTime");
                        }
                    }
                    else
                    {
                        NeedToSendEmail = "No";
                    }

                    var stt = new Date("November 13, 2013 " + classStartTime); // create a datetime stamp that will allow to validate more easily...
                    stt = stt.getTime();

                    var endt = new Date("November 13, 2013 " + classEndTime);  // create a datetime stamp that will allow to validate more easily...
                    endt = endt.getTime();

                    if ($("#ClassesDDLEdit option:selected").text().trim() == "עריכת שם חוג")
                    {
                        alert("בחירת חוג לא חוקית");
                        return false;
                    }
                    else if ($("#GuidesDDLEdit option:selected").text().trim() == "עריכת שם מדריך")
                    {
                        alert("בחירת מדריך לא חוקית");
                        return false;
                    }
                    else if ((stt > endt) || (classStartTime == "") || (classEndTime == "") || (classStartTime == classEndTime))
                    {
                        alert("בחירת זמן לא חוקית");
                        return false;
                    }
                    else if (isNaN(newMaximumUsersPerClassEdit) || newMaximumUsersPerClassEdit < 1) // check if the max participates is a number 
                    {
                        alert("בחירת מקסימום משתתפים לא חוקית");
                        return false;
                    }
                    else if (!confirm("?בטוח לגבי עידכון אירוע זה"))
                    {
                        return false;
                    }
                    else {

                            var whatHasChangedParsed = "";

                            for (var i = 0; i < WhatHasChanged.length; i++)
                            {
                                whatHasChangedParsed += WhatHasChanged[i] + ";";
                            }

                            var updateRequest =
                                {
                                classID: classID,
                                className: className,
                                guideID: guideID,
                                classStartTime: classStartTime,
                                classEndTime: classEndTime,
                                newMaximumUsersPerClassEdit: newMaximumUsersPerClassEdit,
                                NeedToSendEmail: NeedToSendEmail,
                                classNameInitial: classNameInitial,
                                classNameText: classNameText,
                                guideNameInitial: guideNameInitial,
                                guideNameText: guideNameText,
                                eventStartTimeInitial: eventStartTimeInitial,
                                eventEndTimeInitial: eventEndTimeInitial,
                                whatHasChangedParsed: whatHasChangedParsed,
                                eventDate: eventDate
                                };

                        var dataString = JSON.stringify(updateRequest); // parsing the request above. 
                        $.ajax({
                            url: 'FormaFitWebService.asmx/updateEventInDB', // function that only deleting the event in DB. 
                            type: 'POST',
                            contentType: 'application/json; charset = utf-8',
                            dataType: 'json',
                            data: dataString,
                            success:
                                function successCBupdateEventInDB(doc) {
                                    doc = $.parseJSON(doc.d);
                                    alert(doc);
                                    $('#calendar').fullCalendar('refetchEvents');
                                },
                        })
                    };
                });
            }
//----------------------------------------------------------------------End of edit exisiting event code -----------------------------------------------------------
        });
    }
    renderCalendar();

});



//----------------------------------------------------------Add new event code -----------------------------------------------------------

$(document).ready(function () {

    $("#classDate").click(function () {
        $(this).datepicker().datepicker("show")
    });

    $('#classStartTime').timepicker({
        defaultTime: false,
        showMeridian: false
    });

    $('#classEndTime').timepicker({
        defaultTime: false,
        showMeridian: false
    });

    $("#startModalForAddEvent").click(function () {
        var request = {};
        var dataString = JSON.stringify(request);
        $.ajax({
            url: 'FormaFitWebService.asmx/getGuidesFromDB',
            type: 'POST',
            contentType: 'application/json; charset = utf-8',
            dataType: 'json',
            data: dataString,
            success:
                function successCBgetGuidesFromDB(result) {
                    result = $.parseJSON(result.d);
                    var $ddl = $('#GuidesDDL');
                    $ddl.empty(); // remove old options
                    $ddl.append($("<option></option>")
                            .attr("value", '').text('שם מדריך'));
                    $.each(result, function (value, key) {
                        $ddl.append($('<option + value = "' + key.guideID + '" > ' + key.guideName + ' </option>'));
                    });
                }
        });
        $.ajax({
            url: 'FormaFitWebService.asmx/getClassesFromDB',
            type: 'POST',
            contentType: 'application/json; charset = utf-8',
            dataType: 'json',
            data: dataString,
            success:
                function successCBgetClassesFromDB(result) {
                    result = $.parseJSON(result.d);
                    var $ddl = $('#ClassesDDL');
                    $ddl.empty(); // remove old options
                    $ddl.append($("<option></option>")
                            .attr("value", '').text('שם חוג'));
                    $.each(result, function (value, key) {
                        $ddl.append($('<option + value = "' + key.ClassID + '" > ' + key.ClassName + ' </option>'));
                    });
                }
        });
    });

    $("#AddEvent").click(function () {

        var pattern = /^([0-9]{2})\/([0-9]{2})\/([0-9]{4})$/; 
        var isRecurring = "No" // will used later
        var classID = $("#ClassesDDL option:selected").val();
        //     var className = $("#ClassesDDL option:selected").text().trim(); //trim for delete the space that created --> --> didn't used that because in the db the id of the class is matter, not the name
        var classDate = $('#classDate').val().split("/");
        var NewClassDate = classDate[2] + "-" + classDate[1] + "-" + classDate[0]; //class date after parsing to DB/fullcalendar format
        var classStartTime = $('#classStartTime').val();
        var classEndTime = $('#classEndTime').val();
        var guideID = $("#GuidesDDL option:selected").val();
        var MaximumUsersPerClass = $('#MaximumUsersPerClass').val();
        //     var guideName = $("#GuidesDDL option:selected").text().trim(); //trim for delete the space that created  --> didn't used that because in the db the id of the class is matter, not the name

        var stt = new Date("November 13, 2013 " + classStartTime); // create a datetime stamp that will allow to validate more easily...
        stt = stt.getTime();

        var endt = new Date("November 13, 2013 " + classEndTime);  // create a datetime stamp that will allow to validate more easily...
        endt = endt.getTime();

        if ($("#ClassesDDL option:selected").text().trim() == "שם חוג")
        {
            alert("בחירת חוג לא חוקית");
            return false;
        }
        else if ((!pattern.test($('#classDate').val())) || ($('#classDate').val() == ""))
        {
            alert("בחירת תאריך לא חוקית");
            return false;
        }
        else if ((stt > endt) || (classStartTime == "") || (classEndTime == "") || (classStartTime == classEndTime))
        {
            alert("בחירת זמן לא חוקית");
            return false;
        }
        else if ($("#GuidesDDL option:selected").text().trim() == "שם מדריך")
        {
            alert("בחירת מדריך לא חוקית");
            return false;
        }
        else if (isNaN(MaximumUsersPerClass) || MaximumUsersPerClass < 1) // check if the max participates is a number 
        {
            alert("בחירת מקסימום משתתפים לא חוקית");
            return false;
        }
        else if ($('input.RecurringEventCheckbox').is(':checked'))
        {
            isRecurring = "Yes";
        }

        var request = {
            classID: classID,
            NewClassDate: NewClassDate,
            classStartTime: classStartTime,
            classEndTime: classEndTime,
            guideID: guideID,
            MaximumUsersPerClass: MaximumUsersPerClass,
            isRecurring: isRecurring
        };

        var dataString = JSON.stringify(request); // parsing the request above. 
        $.ajax({
            url: 'FormaFitWebService.asmx/createNewEventInDB', // function that creating the new event in DB. 
            type: 'POST',
            contentType: 'application/json; charset = utf-8',
            dataType: 'json',
            data: dataString,
            success:
                function successCBcreateNewEventInDB(doc) {
                    doc = $.parseJSON(doc.d);
                    alert(doc);
                    $('#calendar').fullCalendar('refetchEvents');
                    $('#classDate').val('');
                    $('#classStartTime').val('');
                    $('#classEndTime').val('');
                    $('#MaximumUsersPerClass').val('');
                    $('.RecurringEventCheckbox').attr('checked', false);
                },
        });
    });
});

//------------------------------------------------------End of Add new event code --------------------------------------------------------



//------------------------------------------------------Add/Delete Class code --------------------------------------------------------

$(document).ready(function () {

    $("#AddClassfromEditModal").click(function () {

        var className = $('#AddNewClassTxtbox').val();

        if (className ==  "") {
            alert("אנא ציין את שם החוג שברצונך להוסיף");
        }
        else {
            var createNewClassRequset = {
                className: className
            };

            var dataString = JSON.stringify(createNewClassRequset); // parsing the request above. 
            $.ajax({
                url: 'FormaFitWebService.asmx/createNewClassInDB', // function that deleting the class in DB. 
                type: 'POST',
                contentType: 'application/json; charset = utf-8',
                dataType: 'json',
                data: dataString,
                success:
                    function successCBcreateNewClassInDB(doc) {
                        doc = $.parseJSON(doc.d);
                        alert(doc);
                    },
            });
        }
    });

    $("#startModalForDeleteExitingClass").click(function () {

        var request = {};
        var dataString = JSON.stringify(request);
        $.ajax({
            url: 'FormaFitWebService.asmx/getClassesFromDB',
            type: 'POST',
            contentType: 'application/json; charset = utf-8',
            dataType: 'json',
            data: dataString,
            success:
                function successCBgetClassesFromDB(result) {
                    result = $.parseJSON(result.d);
                    var $ddl = $('#ClassDDLDelete');
                    $ddl.empty(); // remove old options
                    $ddl.append($("<option></option>")
                            .attr("value", '').text('שם חוג'));
                    $.each(result, function (value, key) {
                        $ddl.append($('<option + value = "' + key.ClassID + '" > ' + key.ClassName + ' </option>'));
                    });
                }
        });
    });

    $("#deleteClassfromEditModal").click(function () {
        var ClassToDelete = $("#ClassDDLDelete option:selected").text();
        var ClassToDeleteID = $("#ClassDDLDelete option:selected").val();
        if (ClassToDelete == "שם חוג") {
            alert("בחירת חוג למחיקה לא חוקית");
            return 0;
        }
        else if (!confirm("?בטוח לגבי מחיקת חוג זה")) {
            return 0;
        }
        else {
            var deleteRequest = {
                ClassToDeleteID: ClassToDeleteID
            };
            var dataString = JSON.stringify(deleteRequest);
            $.ajax({
                url: 'FormaFitWebService.asmx/DeleteClassFromDB',
                type: 'POST',
                contentType: 'application/json; charset = utf-8',
                dataType: 'json',
                data: dataString,
                success:
                    function successCBDeleteClassFromDB(doc) {
                        doc = $.parseJSON(doc.d);
                        alert(doc);
                    }
            });

        }
    });
    
});
//-----------------------------------------------------------------------------------------------------------------------------------


//------------------------------------------------------Add/Delete Guide code --------------------------------------------------------

$(document).ready(function () {

    $("#AddGuidefromEditModal").click(function () {

        var guideName = $('#AddNewGuideTxtbox').val();
        if (guideName == "") {
            alert("אנא ציין את שם המדריך שברצונך להוסיף");
        }
        else {
            var createNewGuideRequset = {
                guideName: guideName
            };

            var dataString = JSON.stringify(createNewGuideRequset); // parsing the request above. 
            $.ajax({
                url: 'FormaFitWebService.asmx/createNewGuideInDB', // function that only deleting the event in DB. 
                type: 'POST',
                contentType: 'application/json; charset = utf-8',
                dataType: 'json',
                data: dataString,
                success:
                    function successCBcreateNewGuideInDB(doc) {
                        doc = $.parseJSON(doc.d);
                        alert(doc);
                    },
            });
        }
    });

    $("#startModalForDeleteExitingGuide").click(function () {

        var request = {};
        var dataString = JSON.stringify(request);
        $.ajax({
            url: 'FormaFitWebService.asmx/getGuidesFromDB',
            type: 'POST',
            contentType: 'application/json; charset = utf-8',
            dataType: 'json',
            data: dataString,
            success:
                function successCBgetGuidesFromDB(result) {
                    result = $.parseJSON(result.d);
                    var $ddl = $('#GuidesDDLDelete');
                    $ddl.empty(); // remove old options
                    $ddl.append($("<option></option>")
                            .attr("value", '').text('שם מדריך'));
                    $.each(result, function (value, key) {
                        $ddl.append($('<option + value = "' + key.guideID + '" > ' + key.guideName + ' </option>'));
                    });
                }
        });
    });

    $("#deleteGuidefromEditModal").click(function () {
        var guideToDelete = $("#GuidesDDLDelete option:selected").text().trim();
        var guideToDeleteID = $("#GuidesDDLDelete option:selected").val();
        if (guideToDelete == "שם מדריך") {
            alert("בחירת מדריך למחיקה לא חוקית");
            return 0;
        }
        else if (!confirm("?בטוח לגבי מחיקת מדריך זה")) {
            return 0;
        }
        else {
            var deleteRequest = {
                guideToDeleteID: guideToDeleteID
            };
            var dataString = JSON.stringify(deleteRequest);
            $.ajax({
                url: 'FormaFitWebService.asmx/DeleteGuideFromDB',
                type: 'POST',
                contentType: 'application/json; charset = utf-8',
                dataType: 'json',
                data: dataString,
                success:
                    function successCBDeleteGuideFromDB(doc) {
                        doc = $.parseJSON(doc.d);
                        alert(doc);
                    },
                error: function errorCBDeleteGuideFromDB(e) {
                    alert("something went wrong... :) " + e.responseText)
                }
            });

        }
    });

});