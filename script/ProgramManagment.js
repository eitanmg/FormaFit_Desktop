function actionFormatter(value, row, index) {
    return [
        '<a class="btn btn-danger remove" href="javascript:void(0)" title="מחיקה">',
        '<i class="glyphicon glyphicon-trash"></i>',
        '</a>',
        '<a class="btn btn-primary ml10 edit" href="javascript:void(0)" title="עריכה">',
        '<i class="glyphicon glyphicon-pencil"></i>',
        '</a>'
    ].join('');
}


$(document).ready(function () {

    var request = {
    };

    var dataString = JSON.stringify(request);
    $.ajax({
        url: 'FormaFitWebService.asmx/getGoalsFromDB',
        type: 'POST',
        contentType: 'application/json; charset = utf-8',
        dataType: 'json',
        data: dataString,
        success:
            function successCBgetGoalsFromDB(doc)
            {
                doc = $.parseJSON(doc.d);
                $('#GoalsTable').bootstrapTable({
                    data: doc,
                    pagination: true,
                    pageSize: 5,
                    pageList: [5, 10, 25, 50, 100, 'All'],
                    pageNumber: 1
                });
            },
    });

    $("#addnewGoalBTN").click(function () {

        $('#addNewGoalModal').modal('show');

        $("#saveNewGoalBTN").unbind("click").click(function ()
        {

            var newGoalName = $('#GoalName').val();
            var newGoalStatus = $("#statusDDL option:selected").text().trim();
            var newGoalunitType = $('#unitType').val();

            if (newGoalName == "")
            {
                alert("אנא מלא שם יעד");
                return false;
            }
            else if (newGoalStatus == "סטאטוס")
            {
                alert("אנא בחר סטאטוס יעד");
                return false;
            }
            else if (newGoalunitType == "")
            {
                alert("אנא מלא יחידת מידה");
                return false;
            }
            else
            {
                var request = { newGoalName: newGoalName, newGoalStatus: newGoalStatus, newGoalunitType: newGoalunitType };
                var dataString = JSON.stringify(request);
                $.ajax({
                    url: 'FormaFitWebService.asmx/addNewGoalInDB',
                    type: 'POST',
                    contentType: 'application/json; charset = utf-8',
                    dataType: 'json',
                    data: dataString,
                    success:
                        function successCBupdateExistingGoalInDB(doc)
                        {
                            doc = $.parseJSON(doc.d);
                            alert(doc);
                            //updateTable();
                        },
                });
            }
        });
    });
});




window.actionEvents = {

    'click .edit': function (e, value, row, index) {

        //initial values
        var _GoalID = row.GoalID;
        var _GoalName = row.GoalName;
        var _GoalStatus = row.GoalStatus;
        var _UnitType = row.UnitType;

        //push data to modal

        $('#GoalNameID').val(row.GoalName);
        $('#statusDDLEdit option:contains(' + row.GoalStatus + ')').prop('selected', true);
        $('#unitTypeEdit').val(row.UnitType);
        $('#editGoalModal').modal('show');

        $("#UpdateGoalBTN").unbind("click").click(function () {

            if ($('#GoalNameID').val() == "") {
                alert("אנא מלא שם יעד");
                return false;
            }
            else if ($("#statusDDLEdit option:selected").text().trim() == "סטאטוס")
            {
                alert("אנא בחר סטאטוס יעד");
                return false;
            }
            else if ($('#unitTypeEdit').val() == "")
            {
                alert("אנא מלא יחידת מידה");
                return false;
            }
            else
            {
                var newGoalName = $('#GoalNameID').val();
                var newGoalStatus = $("#statusDDLEdit option:selected").text().trim();
                var newUnitType = $('#unitTypeEdit').val();

                var request = { _GoalID: _GoalID, newGoalName: newGoalName, newGoalStatus: newGoalStatus, newUnitType: newUnitType };
                var dataString = JSON.stringify(request);
                $.ajax({
                    url: 'FormaFitWebService.asmx/updateExistingGoalInDB',
                    type: 'POST',
                    contentType: 'application/json; charset = utf-8',
                    dataType: 'json',
                    data: dataString,
                    success:
                        function successCBupdateExistingGoalInDB(doc)
                        {
                            doc = $.parseJSON(doc.d);
                            alert(doc);
                            updateTable();
                        },
                });
            }
        });
    },
    'click .remove': function (e, value, row, index) {
        var rowParsed = JSON.stringify(row);

        if (row.GoalStatus == "לא פעיל")
        {
            alert("שגיאה! סטאטוס יעד זה כבר לא פעיל");
            return false;
        }
        else if (!confirm("?בטוח לגבי מחיקת " + row.GoalName + "\n\n" + "סטאטוס יעד זה יהפוך להיות לא פעיל *")) {
            return false;
        }
        else
        {
            var GoalID = row.GoalID;
            var request = { GoalID: GoalID };
            var dataString = JSON.stringify(request);
            $.ajax({
                url: 'FormaFitWebService.asmx/DeleteGoalsFromDB',
                type: 'POST',
                contentType: 'application/json; charset = utf-8',
                dataType: 'json',
                data: dataString,
                success:
                    function successCBDeleteGoalsFromDB(doc) {
                        doc = $.parseJSON(doc.d);
                        alert(doc);
                        updateTable();
                    },
            });
        }
    }
};

function updateTable()
{
    location.reload();
}