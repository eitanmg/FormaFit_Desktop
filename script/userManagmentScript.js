
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
            alert("אנא הזן כתובת מייל תקינה");
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
                        alert(doc);
                    },
            });
        }
    },
    'click .edit': function (e, value, row, index)
    {
        $('#editUserModal').modal('show');
        var DOBParsed = row.DOB.split('-');
        var DateOfStartParsed = row.DateOfStart.split('-');
        var DateOfFinishParsed = row.DateOfFinish.split('-');
        DOBParsed = DOBParsed[2] + "/" + DOBParsed[1] + "/" + DOBParsed[0];
        DateOfStartParsed = DateOfStartParsed[2] + "/" + DateOfStartParsed[1] + "/" + DateOfStartParsed[0];
        DateOfFinishParsed = DateOfFinishParsed[2] + "/" + DateOfFinishParsed[1] + "/" + DateOfFinishParsed[0];

        $('#FirstNameEdit').val(row.FirstName);
        $('#LastNameEdit').val(row.LastName);
        $('select option:contains(' + row.Sex + ')').prop('selected', true);
        $('#UserNameEdit').val(row.UserName);
        $('#PasswordEdit').val(row.Password);
        $('select option:contains(' + row.UserType + ')').prop('selected', true);
        $('select option:contains(' + row.UserStatus + ')').prop('selected', true);
        $('#DOBEdit').val(DOBParsed);
        $('#BeginDateEdit').val(DateOfStartParsed);
        $('#EndDateEdit').val(DateOfFinishParsed);
        $('#MobileEdit').val(row.PhoneNumber);
        $('#EmailEdit').val(row.EmailAaddress);
        $('select option:contains(' + row.mailNotification + ')').prop('selected', true);

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
                    pageSize: 15,
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
                    alert(result);
                },
        });

    });

});


//----------------------------------------------------------Add new user code -----------------------------------------------------------



//------------------------------------------------------End of Add new user code --------------------------------------------------------


//        var request = { Values: id };
//        var dataString = JSON.stringify(request);
//        $.ajax({
//            url: 'FormaFitWebService.asmx/DeleteUserFromDB',
//            type: 'POST',
//            contentType: 'application/json; charset = utf-8',
//            dataType: 'json',
//            data: dataString,
//            success:
//                function successCBDeleteUserFromDB(doc) {
//                    doc = $.parseJSON(doc.d);
//                    alert(doc);
//                    updateTable();
//                },
//        });
//    });


//------------------------------------------------------User Edit ----------------------------


//var dataString = JSON.stringify(request);
//$.ajax({
//    url: 'FormaFitWebService.asmx/updateExistingUserInDB',
//    type: 'POST',
//    contentType: 'application/json; charset = utf-8',
//    dataType: 'json',
//    data: dataString,
//    success:
//         function successCBCreatNewUser(myUsers) {
//             $('#addUserModal').modal('toggle');
//             updateTable();
//         },
//});

function updateTable() {

    location.reload();

}