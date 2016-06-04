//*******************Global Variables********************//
FullName = "";
//******************* End of Global Variables***********//

function CheckForLocalStorage()  //Check if there is a local storage for this user , this function triggered from body onload
{
    document.getElementById("LoadingSpinner").style.display = "none";
    if (localStorage['UserDetails'] != null)
    {
        prodArr = JSON.parse(localStorage['UserDetails']);
        document.getElementById("username").value = prodArr[0].usernameStorage;
        document.getElementById("password").value = prodArr[0].passwordStorage;
    }
}


$(document).ready(function () {

    $("#loginBTN").click(function () {
        p1 = document.getElementById("username").value;
        p2 = document.getElementById("password").value;
        if (p1 == null || p1 == "" || p2 == null || p2 == "")
        {
            alert("השדות הינם שדות חובה");
            return false;
        }
        if (p1.match(/[a-z]/i))
        {
            alert("שם משתמש חייב להכיל רק ספרות");
            return false;
        }
        else {
            var request = {
                name: p1,
                password: p2
            }
            document.getElementById("LoadingSpinner").style.display = "block";
            CheckUserAjaxFunc(request, successCB, errorCB); // verfication in front of the DB
        }
    });
});

function successCB(resutls) {
    if ($('#checkboxG5').prop('checked'))
    {
        UserArr = new Array();
        usernameStorage = document.getElementById('username').value;
        passwordStorage = document.getElementById('password').value;
        obj = new Object();
        obj.usernameStorage = usernameStorage;
        obj.passwordStorage = passwordStorage;
        UserArr[UserArr.length] = obj;
        localStorage['UserDetails'] = JSON.stringify(UserArr);
    }
    resutls = $.parseJSON(resutls.d)
    myarray = resutls.split(',');
    if (myarray[0] == "לקוח")
    {
        document.getElementById("LoadingSpinner").style.display = "none";
        tmpStr = '<div class="alert alert-danger" role="alert"><span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span><span class="sr-only">Error:</span>&nbsp&nbsp&nbspהכניסה למערכת הינה עבור עובדים בלבד</div>';
        document.getElementById("CheckPH").innerHTML = tmpStr;
    }
    else if (myarray[0] == "מנהל")
    {
        window.location.href = 'Admin-Main.aspx';
    }
    else
    {
        document.getElementById("LoadingSpinner").style.display = "none";
        tmpStr = '<div class="alert alert-danger" role="alert"><span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span><span class="sr-only">Error:</span>&nbsp&nbsp&nbsp' + resutls + '</div>';
        document.getElementById("CheckPH").innerHTML = tmpStr;
    }
}

function errorCB(e) {
    alert("Something went wrong... :( \n\n\n The exception message is : \n\n " + e.responseText);
}