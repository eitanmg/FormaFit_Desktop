﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>פורמה פיט - כניסת משתמשים</title>
    <style>
        .logo {
            width: 226px;
            height: 142px;
            background: url('images/logo.jpg') no-repeat;
            margin: 30px auto;
        }

        .login-block input#username {
            background: #fff url('images/userpic.png') 20px top no-repeat;
            background-size: 16px 80px;
        }

            .login-block input#username:focus {
                background: #fff url('images/userpic.png') 20px bottom no-repeat;
                background-size: 16px 80px;
            }

        .login-block input#password {
            background: #fff url('images/passpic.png') 20px top no-repeat;
            background-size: 16px 80px;
        }

            .login-block input#password:focus {
                background: #fff url('images/passpic.png') 20px bottom no-repeat;
                background-size: 16px 80px;
            }
    </style>
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">

    <!-- jQuery library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <script src="http://code.jquery.com/jquery-1.11.3.min.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <link href="Style/StyleSheet.css" rel="stylesheet" />
    <script src="Script/LoginScript.js"></script>
    <script src="Script/ajaxCalls.js"></script>
</head>

<body onload="CheckForLocalStorage()">
    <div class="logo"></div>
    <div class="login-block">
        <h1 style="font-family:Arial">פורמה פיט - כניסה</h1>
        <input style="font-family:Arial;direction:rtl" type="text" placeholder="שם משתמש" id="username"  />
        <input style="font-family: Arial;direction:rtl" type="password" placeholder="ססמה" id="password" />
        <input type="checkbox" name="checkboxG5" id="checkboxG5" class="css-checkbox" checked="checked"  />
        <label for="checkboxG5" class="css-label">שמור פרטי התחברות</label><br />
        <button style="font-family: Arial;direction:rtl" id="loginBTN">כניסה</button>
        <img id="LoadingSpinner" src="images/loading_spinner.gif" />
        <br /><br />
        <div style="direction:rtl" id="CheckPH"></div>
        <input type="hidden" id="hidValue" value=""/>
    </div>
</body>
</html>
