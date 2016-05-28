
function actionFormatter(value, row, index) {
    return [
        '<a class="btn btn-danger remove" href="javascript:void(0)" title="מחיקה">',
        '<i class="glyphicon glyphicon-trash"></i>',
        '</a>',
        '<a class="btn btn-primary ml10 edit" href="javascript:void(0)" title="עריכה">',
        '<i class="glyphicon glyphicon-pencil"></i>',
        '</a>',
        '<a class="btn btn-primary ml10 mail" href="javascript:void(0)" title="שליחת מייל">',
        '<i class="glyphicon glyphicon-envelope"></i>',
        '</a>'
    ].join('');
}

window.actionEvents = {
    'click .mail': function (e, value, row, index)
    {
        var rowParsed = JSON.stringify(row);
        if (row.EmailAaddress == "")
        {
            //alert("אנא הזן כתובת מייל תקינה");
            swal("", "אנא הזן כתובת מייל תקינה");
            return false;
        }
        else if (!confirm("?" + "לשלוח אימייל עם פרטי המשתמש וסיסמה אל " + row.FirstName + " " + row.LastName)) {
            return false;
        }
        else
        {
            var UserName = row.UserName;
            var FirstName = row.FirstName;
            var userPassword = row.Password.trim();
            var emailAddress = row.EmailAaddress.trim();
            var request = {
                UserName: UserName,
                FirstName: FirstName,
                userPassword: userPassword,
                emailAddress: emailAddress
            };
            var dataString = JSON.stringify(request);
            $.ajax({
                url: 'FormaFitWebService.asmx/SendEmailWithCredentials',
                type: 'POST',
                contentType: 'application/json; charset = utf-8',
                dataType: 'json',
                data: dataString,
                success:
                    function successCBDeleteUserFromDB(doc) {
                        doc = $.parseJSON(doc.d);
                        swal(doc, "", "success");
                    },
            });
        }
    },
    'click .edit': function (e, value, row, index)
    {
        //initial values
        var _firstName = row.FirstName;
        var _lastName = row.LastName;
        var _sex = row.Sex;
        var _userName = row.UserName;
        var _password = row.Password;
        var _userType = row.UserType;
        var _status = row.Status;
        var _DOB = row.DOB;
        var _DateOfStart = row.DateOfStart;
        var _DateOfFinish = row.DateOfFinish;
        var _mobile = row.PhoneNumber;
        var _email = row.EmailAaddress;
        var _emailNotification = row.mailNotification;
        $('#editUserModal').modal('show');

        //Dates Parsing

        var DOBParsed = row.DOB.split('-');
        var DateOfStartParsed = row.DateOfStart.split('-');
        var DateOfFinishParsed = row.DateOfFinish.split('-');
        DOBParsed = DOBParsed[2] + "/" + DOBParsed[1] + "/" + DOBParsed[0];
        DateOfStartParsed = DateOfStartParsed[2] + "/" + DateOfStartParsed[1] + "/" + DateOfStartParsed[0];
        DateOfFinishParsed = DateOfFinishParsed[2] + "/" + DateOfFinishParsed[1] + "/" + DateOfFinishParsed[0];

        //push data to modal

        $('#FirstNameEdit').val(row.FirstName);
        $('#LastNameEdit').val(row.LastName);
        $('#sexDDLEdit option:contains(' + row.Sex + ')').prop('selected', true);
        $('#UserNameEdit').val(row.UserName);
        $('#PasswordEdit').val(row.Password);
        $('#typeDDLEdit option:contains(' + row.UserType + ')').prop('selected', true);
        $('#statusDDLEdit option:contains(' + row.Status + ')').prop('selected', true);
        $('#DOBEdit').val(DOBParsed);
        $('#BeginDateEdit').val(DateOfStartParsed);
        $('#EndDateEdit').val(DateOfFinishParsed);
        $('#MobileEdit').val(row.PhoneNumber);
        $('#EmailEdit').val(row.EmailAaddress);
        $('#mailNotificationDDLEdit option:contains(' + row.mailNotification + ')').prop('selected', true);

        //once click 'save'

        $("#UpdateUser").click(function () {

            var DOBsplitted = $('#DOBEdit').val().split("/");
            var DOBtoDB = DOBsplitted[2] + "-" + DOBsplitted[1] + "-" + DOBsplitted[0];
            var BeginDate = $('#BeginDateEdit').val().split("/");
            var BeginDatetoDB = BeginDate[2] + "-" + BeginDate[1] + "-" + BeginDate[0];
            var EndDate = $('#EndDateEdit').val().split("/");
            var EndDatetoDB = EndDate[2] + "-" + EndDate[1] + "-" + EndDate[0];

            var pattern = /^([0-9]{2})\/([0-9]{2})\/([0-9]{4})$/;
            var dataToUpdate = [];

            //validators

            if ($('#FirstNameEdit').val() == "") {
                alert("אנא מלא שם פרטי");
                return false;
            }
            else if ($('#LastNameEdit').val() == "") {
                alert("אנא מלא שם משפחה");
                return false;
            }
            else if ($("#sexDDLEdit option:selected").text().trim() == "מין") {
                alert("אנא בחר מין");
                return false;
            }
            else if ($('#UserNameEdit').val() == "") {
                alert("אנא מלא שם משתמש");
                return false;
            }
            else if ($('#PasswordEdit').val() == "") {
                alert("אנא מלא סיסמה");
                return false;
            }
            else if ($("#typeDDLEdit option:selected").text().trim() == "סוג משתמש") {
                alert("אנא בחר סוג משתמש");
                return false;
            }
            else if ($("#statusDDLEdit option:selected").text().trim() == "סטאטוס") {
                alert("אנא בחר סטאטוס משתמש");
                return false;
            }
            else if ((!pattern.test($('#DOBEdit').val())) || ($('#DOBEdit').val() == "")) {
                alert("בחירת תאריך לא חוקית");
                return false;
            }
            else if ((!pattern.test($('#BeginDateEdit').val())) || ($('#BeginDateEdit').val() == "")) {
                alert("בחירת תאריך לא חוקית");
                return false;
            }
            else if ((!pattern.test($('#EndDateEdit').val())) || ($('#EndDateEdit').val() == "")) {
                alert("בחירת תאריך לא חוקית");
                return false;
            }
            else if ($('#MobileEdit').val() == "" || isNaN($('#MobileEdit').val()) == true || $('#MobileEdit').val().length < 8) {
                alert("אנא מלא מספר טלפון תקין");
                return false;
            }
            else if ($('#EmailEdit').val() == "" || $('#EmailEdit').val().indexOf("@") == -1) {
                alert("אנא מלא כתובת מייל תקינה");
                return false;
            }
            else if ($("#mailNotificationDDLEdit option:selected").text().trim() == "הודעות ועדכונים באימייל") {
                alert("אנא בחר אם ברצונך לקבל הודעות לאימייל");
                return false;
            }

            //which field need to be updated
            
            if ($('#FirstNameEdit').val() != _firstName)
            {
                dataToUpdate.push("[FirstName]:" + "'" + $('#FirstNameEdit').val() + "'" + ",");
            }
            if ($('#LastNameEdit').val() != _lastName)
            {
                dataToUpdate.push("[LastName]:" + "'" + $('#LastNameEdit').val() + "'" + ",");
            }
            if ($("#sexDDLEdit option:selected").text().trim() != _sex)
            {
                dataToUpdate.push("[Sex]:" + "'" + $("#sexDDLEdit option:selected").text().trim() + "'" + ",");
            }
            if ($('#UserNameEdit').val() != _userName)
            {
                dataToUpdate.push("[UserName]:" + "'" + $('#UserNameEdit').val() + "'" + ",");
            }
            if ($('#PasswordEdit').val() != _password)
            {
                dataToUpdate.push("[Password]:" + "'" + $('#PasswordEdit').val() + "'" + ",");
            }
            if ($("#typeDDLEdit option:selected").text().trim() != _userType)
            {
                dataToUpdate.push("[UserType]:" + "'" + $("#typeDDLEdit option:selected").text().trim() + "'" + ",");
            }
            if ($("#statusDDLEdit option:selected").text().trim() != _status)
            {
                dataToUpdate.push("[Status]:" + "'" + $("#statusDDLEdit option:selected").text().trim() + "'" + ",");
            }
            if ($('#DOBEdit').val() != DOBParsed)
            {
                dataToUpdate.push("[DOB]:" + "'" + DOBtoDB + "'" + ",");
            }
            if ($('#BeginDateEdit').val() != DateOfStartParsed)
            {
                dataToUpdate.push("[DateOfStart]:" + "'" + BeginDatetoDB + "'" + ",");
            }
            if ($('#EndDateEdit').val() != DateOfFinishParsed)
            {
                dataToUpdate.push("[DateOfFinish]:" + "'" + EndDatetoDB + "'" + ",");
            }
            if ($('#MobileEdit').val() != _mobile)
            {
                dataToUpdate.push("[PhoneNumber]:" + "'" + $('#MobileEdit').val() + "'" + ",");
            }
            if ($('#EmailEdit').val() != _email)
            {
                dataToUpdate.push("[EmailAaddress]:" + "'" + $('#EmailEdit').val() + "'" + ",");
            }
            if ($("#mailNotificationDDLEdit option:selected").text().trim() != _emailNotification)
            {
                dataToUpdate.push("[mailNotification]:" + "'" + $("#mailNotificationDDLEdit option:selected").text().trim() + "'");
            }

            if (dataToUpdate.length == 0)
            {
                swal("", "לא חל שינוי");
                return false;
            }
            else
            {
                var updateStr = "";
                for (var i = 0; i < dataToUpdate.length; i++)
                {
                    updateStr += dataToUpdate[i];
                }
                var request = { updateStr: updateStr, _userName: _userName };
                var dataString = JSON.stringify(request);
                $.ajax({
                    url: 'FormaFitWebService.asmx/updateExistingUserInDB',
                    type: 'POST',
                    contentType: 'application/json; charset = utf-8',
                    dataType: 'json',
                    data: dataString,
                    success:
                        function successCBupdateExistingUserInDB(doc) {
                            doc = $.parseJSON(doc.d);
                            swal(doc, "", "success");
                        },
                });
            }
        });


    },
    'click .remove': function (e, value, row, index)
    {
        var rowParsed = JSON.stringify(row);
        if (!confirm("?בטוח לגבי מחיקת " + row.FirstName + " " + row.LastName + "\n\n" + "סטאטוס משתמש זה יהפוך להיות לא פעיל *"))
        {
            return false;
        }
        else
        {
            var UserName = row.UserName;
            var request = { UserName: UserName };
            var dataString = JSON.stringify(request);
            $.ajax({
                url: 'FormaFitWebService.asmx/DeleteUserFromDB',
                type: 'POST',
                contentType: 'application/json; charset = utf-8',
                dataType: 'json',
                data: dataString,
                success:
                    function successCBDeleteUserFromDB(doc) {
                        doc = $.parseJSON(doc.d);
                        alert(doc);
                        updateTable();
                    },
            });
        }
    }
};

$(document).ready(function () {

    $('#isActivefilter').on('change', function () {

        if (this.checked)
        {
            $('.searchable tr').hide();
            $('.searchable tr').filter(function ()
            {
                return $(this).find('td').eq(7).text() !== "לא פעיל"
            }).show();
        }
        else
        {
            $('.searchable tr').show();
        }
    });

    var request = {};

    var dataString = JSON.stringify(request);
    $.ajax({
        url: 'FormaFitWebService.asmx/getCurrentUsersFromDB',
        type: 'POST',
        contentType: 'application/json; charset = utf-8',
        dataType: 'json',
        data: dataString,
        success:
            function successCBGetUsers(result) {
                result = $.parseJSON(result.d);
                $('#UserTable').bootstrapTable({
                    data: result,
                    pagination: true,
                    pageSize: 10,
                    pageList: [15, 30, 50, 100, 'All'],
                    pageNumber: 1
                });
            },
    });

    $(".DateField").click(function () {
        $(this).datepicker().datepicker("show")
    });

    $("#AddUser").click(function () {

        var pattern = /^([0-9]{2})\/([0-9]{2})\/([0-9]{4})$/;
        var FirstName = $("#FirstName").val();
        var LastName = $("#LastName").val();
        var userSex = $("#sexDDL option:selected").val();
        var UserName = $("#UserName").val();
        var userPassword = $("#Password").val();
        var UserType = $("#typeDDL option:selected").val();
        var UserStatus = $("#statusDDL option:selected").val();
        var DOB = $("#DOB").val();
        var BeginDate = $("#BeginDate").val();
        var EndDate = $("#EndDate").val();
        var Mobile = $("#Mobile").val();
        var Email = $("#Email").val();
        var EmailNotification = $("#mailNotificationDDL option:selected").val();

        var DOBsplitted = $('#DOB').val().split("/");
        var DOBtoDB = DOBsplitted[2] + "-" + DOBsplitted[1] + "-" + DOBsplitted[0];

        var BeginDate = $('#BeginDate').val().split("/");
        var BeginDatetoDB = BeginDate[2] + "-" + BeginDate[1] + "-" + BeginDate[0];

        var EndDate = $('#EndDate').val().split("/");
        var EndDatetoDB = EndDate[2] + "-" + EndDate[1] + "-" + EndDate[0];

        if (FirstName == "") {
            alert("אנא מלא שם פרטי");
            return false;
        }
        else if (LastName == "") {
            alert("אנא מלא שם משפחה");
            return false;
        }
        else if ($("#sexDDL option:selected").text().trim() == "מין") {
            alert("אנא בחר מין");
            return false;
        }
        else if (UserName == "") {
            alert("אנא מלא שם משתמש");
            return false;
        }
        else if (userPassword == "") {
            alert("אנא מלא סיסמה");
            return false;
        }
        else if ($("#typeDDL option:selected").text().trim() == "סוג משתמש") {
            alert("אנא בחר סוג משתמש");
            return false;
        }
        else if ($("#statusDDL option:selected").text().trim() == "סטאטוס") {
            alert("אנא בחר סטאטוס משתמש");
            return false;
        }
        else if ((!pattern.test($('#DOB').val())) || ($('#DOB').val() == "")) {
            alert("בחירת תאריך לא חוקית");
            return false;
        }
        else if ((!pattern.test($('#BeginDate').val())) || ($('#BeginDate').val() == "")) {
            alert("בחירת תאריך לא חוקית");
            return false;
        }
        else if ((!pattern.test($('#EndDate').val())) || ($('#EndDate').val() == "")) {
            alert("בחירת תאריך לא חוקית");
            return false;
        }
        else if (Mobile == "" || isNaN(Mobile) == true || Mobile.length < 8) {
            alert("אנא מלא מספר טלפון תקין");
            return false;
        }
        else if (Email == "" || Email.indexOf("@") == -1) {
            alert("אנא מלא כתובת מייל תקינה");
            return false;
        }
        else if ($("#mailNotificationDDL option:selected").text().trim() == "הודעות ועדכונים באימייל") {
            alert("אנא בחר אם ברצונך לקבל הודעות לאימייל");
            return false;
        }
        else if (!confirm("?בטוח לגבי הוספת משתמש זה")) {
            return false;
        }


        var request = {
            FirstName: FirstName,
            LastName: LastName,
            userSex: userSex,
            UserName: UserName,
            userPassword: userPassword,
            UserType: UserType,
            UserStatus: UserStatus,
            DOBtoDB: DOBtoDB,
            BeginDatetoDB: BeginDatetoDB,
            EndDatetoDB: EndDatetoDB,
            Mobile: Mobile,
            Email: Email,
            EmailNotification: EmailNotification
        };

        var dataString = JSON.stringify(request);
        $.ajax({
            url: 'FormaFitWebService.asmx/addNewUserInDB',
            type: 'POST',
            contentType: 'application/json; charset = utf-8',
            dataType: 'json',
            data: dataString,
            success:
                function successaddNewUserInDB(result)
                {
                    result = $.parseJSON(result.d);
                    swal(result, "", "success");
                },
        });

    });

});


//----------------------------------------------------------Add new user code -----------------------------------------------------------



function updateTable() {

    location.reload();

}