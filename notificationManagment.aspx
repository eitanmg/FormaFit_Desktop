﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="notificationManagment.aspx.cs" Inherits="notificationManagment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>פורמה פיט - ניהול</title>
    <meta http-equiv="content-type" content="text/html" charset="utf-8" dir="rtl" />
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <script src="style/css/5grid/viewport.js"></script>
    <!--[if lt IE 9]><script src="css/5grid/ie.js"></script><![endif]-->
    <link rel="stylesheet" href="style/css/5grid/core.css" />
    <link rel="stylesheet" href="style/css/style.css" />
    <!--[if IE 9]><link rel="stylesheet" href="css/style-ie9.css" /><![endif]-->
    <!---------------------------------------Jquery Scripts------------------------------------------------>
    <!----------------------------------------------------------------------------------------------------->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
    <!---------------------------------------Bootstrap Scripts--------------------------------------------->
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-table/1.10.1/bootstrap-table.min.css" />
    <!-- Latest compiled and minified JavaScript -->
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-table/1.10.1/bootstrap-table.min.js"></script>
    <script src="script/bootstrap-table-he-IL.js"></script>
     <!----------------------------------------------------------------------------------------------------->
    <link href="//cdnjs.cloudflare.com/ajax/libs/x-editable/1.4.6/bootstrap-editable/css/bootstrap-editable.css" rel="stylesheet"/>
    <script src="//cdnjs.cloudflare.com/ajax/libs/x-editable/1.4.6/bootstrap-editable/js/bootstrap-editable.min.js"></script>
    <!----------------------------------------------------------------------------------------------------->
    <!---------------------------------------My style and code------------------------------------------------------->
    <script src="script/NotificationScript.js"></script>
    <link href="style/StyleSheet.css" rel="stylesheet" />
    <!----------------------------------------------------------------------------------------------------->
</head>
<body dir="rtl">
    <div id="header-wrapper">
        <div class="5grid">
            <div class="12u-first">
                <header id="header">
                    <nav>
                        <a href="Admin-Main.aspx">דף בית</a>
                        <a href="userManagment-html.aspx">ניהול משתמשים</a>
                        <a href="classManagment-Main.aspx">ניהול חוגים</a>
                        <a href="ProgramManagment-Main.aspx">ניהול יעדים</a>
                        <a href="notificationManagment.aspx" class="current-page-item">הודעות ושינויים</a>
                    </nav>
                </header>
            </div>
        </div>
    </div>
    <div id="banner-wrapper">
        <div class="5grid">
            <div class="12u-first">
                <br />
                <select id="notificationMotivationSelect" class="form-control input-lg btn-info">
                    <option id="notificationOption" value="notification">כתיבת הודעות ועדכונים למנויי חדר הכושר</option>
                    <option id="motivationOption" value="motivation">עדכון ושינוי משפטי מוטיבציה</option>
                </select>
                <div id="allNotificationPage" class="form-group">
                    <div class="form-group"><br />
                        <textarea rows="6" id="NotificationText" class="form-control" placeholder="הקלד/י הודעה חדשה כאן"></textarea>
                    </div>
                    <button id="addMessage" type="button" class="btn btn-success">
                        <span class="glyphicon glyphicon-plus"></span>&nbsp; הוספת הודעה
                    </button>
                    <br />
                    <hr style="border-width:3px" />
                    <h3><u>הודעות אחרונות</u></h3>
                    <table id="NotificationTable" class="table table-hover table-bordered" data-search="true" data-locale="he-IL">
                        <thead class="btn-info">
                            <tr>
                                <th data-field="state" data-checkbox="true"></th>
                                <th data-field="id" class="text-right idTD" id="testid">מזהה הודעה</th>
                                <th data-field="content" class="text-right notificationTD">תוכן הודעה</th>
                                <th data-field="DateAndTimeOfPublish" class="text-right">תאריך פרסום</th>       
                            </tr>
                        </thead>
                    </table>
                    <button id="remove" class="btn btn-danger glyphicon glyphicon-trash" disabled></button>
                    <hr style="border-width:3px" />
                    <h3><u>בחירת מספר הודעות אחרונות לתצוגה באפלקציית פורמה פיט</u></h3>
                    <span class="messageChoose">כעת מוצגות</span> <strong class="text-primary" id="currentNumberOfNotification" ></strong> <span class="messageChoose">הודעות האחרונות</span><br /><br />
                    <select id="messageChooseSelect" class="btn btn-info">
                        <option>לחץ לשינוי</option>
                        <option>1</option>
                        <option>2</option>
                        <option>3</option>
                        <option>4</option>
                        <option>5</option>
                        <option>6</option>
                    </select>
                    <button id="updateHowManyMessagesToShowBTN" class="btn btn-success">
                        <span class="glyphicon glyphicon-ok"></span>&nbsp; שמור שינוי
                    </button>
                </div>
                
                <div id="allMotivationPage" class="form-group">
                    <div class="form-group">
                        <br />
                        <textarea rows="6" id="motivationTextArea" class="form-control" placeholder="הקלד/י משפט מוטיבציה חדש כאן"></textarea>
                    </div>
                    <button id="addMotivationSentenceBTN" type="button" class="btn btn-success">
                        <span class="glyphicon glyphicon-plus"></span>&nbsp; הוספת משפט
                    </button><br />
                    <hr style="border-width:3px" />
                    <table id="motivationTable" class="table table-hover table-bordered" data-search="true" data-locale="he-IL">
                        <colgroup>
                            <col span="1" style="width: 3%;" />
                            <col span="1" style="width: 82%;" />
                            <col span="1" style="width: 15%;" />
                        </colgroup>
                        <thead class="btn-info">
                            <tr>
                                <th data-field="state" data-checkbox="true"></th>
                                <th data-field="content" class="text-right">תוכן משפט</th>
                                <th data-field="DateAndTimeOfPublish" class="text-right">תאריך פרסום</th>
                            </tr>
                        </thead>
                    </table>
                    <button id="removeMotivationSentence" class="btn btn-danger glyphicon glyphicon-trash" disabled></button>
                </div>
            </div>
        </div>
    </div>
    <div id="footer-wrapper">
        <div class="5grid">
            <div class="12u-first">
                <div id="copyright">
                    &copy; 2016 איתן, עוז וחזי
                </div>
            </div>
        </div>
    </div>
</body>
</html>