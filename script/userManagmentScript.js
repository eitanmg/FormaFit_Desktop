

$(document).ready(function () {

    var request = {
    };

    var dataString = JSON.stringify(request);
    $.ajax({
        url: 'FormaFitWebService.asmx/getCurrentUsersFromDB',
        type: 'POST',
        contentType: 'application/json; charset = utf-8',
        dataType: 'json',
        data: dataString,
        success:
            function successCBGetUsers(myUsers) {
                myUsers = $.parseJSON(myUsers.d);
                $('#UserTable').DataTable({
                    data: myUsers,
                    "columns": [
            { "data": "FirstName" },
            { "data": "LastName" },
            { "data": "UserName" },
            { "data": "Password" },
            { "data": "UserType" },
            { "data": "Status" },
            { "data": "DOB" },
            { "data": "DateOfStart" },
            { "data": "DateOfFinish" },
            { "data": "PhoneNumber" },
            { "data": "EmailAaddress" }                    ]
                });
            },
    });

});


//----------------------------------------------------------Add new user code -----------------------------------------------------------

$(document).ready(function () {
    $("#AddUser").click(function () {


        var FirstName = $("#FirstName").val();
        var LastName = $("#LastName").val();
        var UserName = $("#UserName").val();
        var Password = $("#Password").val();
        var UserType = $("#typeDDL option:selected").val();
        var UserStatus = $("#statusDDL option:selected").val();
        var DOB = $("#DOB").val();
        var BeginDate = $("#BeginDate").val();
        var EndDate = $("#EndDate").val();
        var Mobile = $("#Mobile").val();
        var Email = $("#Email").val();



        if ($("#UserName").val() == "" ) {
            alert("תעודת זהות זה שדה חובה");
            return 0;
        }
        else if ($("#FirstName").val() == "" || $("#LastName").val() == "" || $("#Password").val() == "" || $("#DOB").val() == "" || $("#BeginDate").val() == "" || $("#Mobile").val() == "" || $("#Email").val() == "") {
            alert("חסרים פרטים בטופס");
            return 0;
        }

        else if (!confirm("?בטוח לגבי הוספת משתמש זה")) {
            return 0;
        }

        else {


            var request = {                     // send the requset to the server
                FirstName: FirstName,
                LastName: LastName,
                UserName: UserName,
                Password: Password,
                UserType: UserType,
                UserStatus: UserStatus,
                DOB: DOB,
                BeginDate: BeginDate,
                EndDate: EndDate,
                Mobile: Mobile,
                Email: Email

            };

            var dataString = JSON.stringify(request); // parsing the request above. 
            $.ajax({
                url: 'FormaFitWebService.asmx/addNewUserInDB', // function that creating the new event in DB. 
                type: 'POST',
                contentType: 'application/json; charset = utf-8',
                dataType: 'json',
                data: dataString,
                success:
                     function successCBCreatNewUser(myUsers) {
                         $('#addUserModal').modal('toggle');
                         updateTable();
                     },
            });
        };
    });



});







//------------------------------------------------------End of Add new user code --------------------------------------------------------


$(document).ready(function () {


    $("#removeBT").click(function () {

        var id = $('#idToRemove').val();

        if (!confirm("?בטוח למחוק משתמש זה")) {
            return 0;
        }

        var request = { Values: id };
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
    });


    //------------------------------------------------------User in-line Edit ----------------------------


    $("#UserTable").on('click', 'tbody td', function (e) {
        var col = $(this).parent().children().index($(this));
        var id = $(this).closest('tr').find('td:eq(2)').text();
        var currentVal = $(this).text();

        if (col == 5)
            $('#editUserStatusModal').modal('show');
            
        else if (col == 4)
            $('#editUserTypeModal').modal('show');

        else {
            $('#EditUserField').attr("value", currentVal);
            $('#editUserModal').modal('show');
        }

        $("#EditUserStatusModalBTN").on('click', function (e) {
            var newVal = $("#EditUserStatusField").val();
            updateUserVal(newVal,col,id,currentVal);
        });
        $("#EditUserModalBTN").on('click', function (e) {
            var newVal = $("#EditUserField").val();
            updateUserVal(newVal, col, id, currentVal);
            var colName = $('#UserTable').find('th').eq($td.index());
            
        });
        $("#EditUserTypeModalBTN").on('click', function (e) {
            var newVal = $("#EditUserTypeField").val();
            updateUserVal(newVal, col, id,currentVal);
        });
        
    });





    function updateUserVal(newVal, col, id, currentVal) {

        if (newVal == currentVal) {
            alert("כלום לא השתנה, למה סתם?!")
        }
        else {

        var newVal = newVal;
        var col = col;
        var id = id;
        var currentVal = currentVal;


        //if ($("#UserName").val() == "") {
        //    alert("תעודת זהות זה שדה חובה");
        //    return 0;
        //}
        //else if ($("#FirstName").val() == "" || $("#LastName").val() == "" || $("#Password").val() == "" || $("#DOB").val() == "" || $("#BeginDate").val() == "" || $("#Mobile").val() == "" || $("#Email").val() == "") {
        //    alert("חסרים פרטים בטופס");
        //    return 0;
        //}

        //else if (!confirm("?בטוח לגבי הוספת משתמש זה")) {
        //    return 0;
        //}

        //else {


            var request = {                     // send the requset to the server
                newVal: newVal,
                col: col,
                id: id,
                currentVal: currentVal
            };

            var dataString = JSON.stringify(request); // parsing the request above. 
            $.ajax({
                url: 'FormaFitWebService.asmx/updateExistingUserInDB', // function that updates the user in DB. 
                type: 'POST',
                contentType: 'application/json; charset = utf-8',
                dataType: 'json',
                data: dataString,
                success:
                     function successCBCreatNewUser(myUsers) {
                         $('#addUserModal').modal('toggle');
                         updateTable();
                     },
            });
            //};
        }
    };

});






function updateTable() {

    var request = {
    };

    var dataString = JSON.stringify(request);
    $.ajax({
        url: 'FormaFitWebService.asmx/getCurrentUsersFromDB',
        type: 'POST',
        contentType: 'application/json; charset = utf-8',
        dataType: 'json',
        data: dataString,
        success:
            function successCBgetCurrentUsersFromDB(doc) {
                doc = $.parseJSON(doc.d);
                $('#UserTable').DataTable({
                    data: doc
                });
            },
    });
    location.reload();
}