﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProgramManagment-Main.aspx.cs" Inherits="ProgramManagment_Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>פורמה פיט - ניהול</title>
    <meta http-equiv="content-type" content="text/html; charset=utf-8" dir="rtl" />
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <script src="style/css/5grid/viewport.js"></script>
    <!--[if lt IE 9]><script src="css/5grid/ie.js"></script><![endif]-->
    <link rel="stylesheet" href="style/css/5grid/core.css" />
    <link rel="stylesheet" href="style/css/style.css" />
    <!--[if IE 9]><link rel="stylesheet" href="css/style-ie9.css" /><![endif]-->
    <!---------------------------------------Jquery Scripts------------------------------------------------>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
    <!---------------------------------------Bootstrap Scripts--------------------------------------------->
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <!---------------------------------------Jquery Scripts------------------------------------------------>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/themes/smoothness/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js"></script>
    <!----------------------------------------------------------------------------------------------------->
    <!---------------------------------------Bootstrap table Scripts--------------------------------------------->
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-table/1.10.1/bootstrap-table.min.css" />
    <!-- Latest compiled and minified JavaScript -->
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-table/1.10.1/bootstrap-table.min.js"></script>
    <script src="script/bootstrap-table-he-IL.js"></script>
    <!---------------------------------------my script--------------------------------------------->
    <script src="script/ProgramManagment.js"></script>
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
                        <a href="ProgramManagment-Main.aspx" class="current-page-item">ניהול יעדים</a>
                        <a href="notificationManagment.aspx">הודעות ושינויים</a>
                    </nav>
                </header>
            </div>
        </div>
    </div>
    <div id="banner-wrapper">
        <div class="5grid">
            <div class="12u-first">
                <h2 style="text-align:center"><u>ניהול יעדים</u></h2>
                <button id="addnewGoalBTN" type="button" class="btn btn-info" data-toggle="modal" data-target="#"><span class="glyphicon glyphicon-plus"></span>&nbsp; הוספת יעד חדש</button>
                <br /><br />
                <table id="GoalsTable" class="table table-hover table-bordered" data-locale="he-IL">
                    <colgroup>
                        <col span="1" style="width: 15%;" />
                        <col span="1" style="width: 5%;" />
                        <col span="1" style="width: 30%;" />
                        <col span="1" style="width: 20%;" />
                        <col span="1" style="width: 20%;" />
                    </colgroup>
                    <thead class="btn-info">
                        <tr>
                            <th data-field="action" data-formatter="actionFormatter" data-events="actionEvents" class="text-center">עריכה</th>
                            <th data-field="GoalID" class="text-right" id="testid">מזהה יעד</th>
                            <th data-field="GoalName" class="text-right">יעד</th>
                            <th data-field="GoalStatus" class="text-right">סטאטוס</th>
                            <th data-field="UnitType" class="text-right">יחידת מידה</th>
                        </tr>
                    </thead>
                </table>
                <hr style="border-width:3px" />
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

    <div class="modal fade" id="addNewGoalModal" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">הוספת יעד</h4>
                </div>
                <form class="modal-body">
                    <input id="GoalName" placeholder="שם היעד" class="form-control form-group" />
                    <select id="statusDDL" class="form-control form-group">
                        <option>סטאטוס</option>
                        <option>לא פעיל</option>
                        <option>פעיל</option>
                    </select>
                    <input id="unitType" placeholder="יחידת מידה" class="form-control form-group" />
                    <div class="modal-footer">
                        <button type="submit" id="saveNewGoalBTN" class="btn btn-info" data-toggle="modal">הוסף</button>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <div class="modal fade" id="editGoalModal" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">עריכת יעד</h4>
                </div>
                <form class="modal-body">
                    <input id="GoalNameID" placeholder="שם יעד" class="form-control form-group" />
                    <select id="statusDDLEdit" class="form-control form-group">
                        <option>סטאטוס</option>
                        <option>לא פעיל</option>
                        <option>פעיל</option>
                    </select>
                    <input id="unitTypeEdit" placeholder="יחידת מידה" class="form-control form-group" />
                    <div class="modal-footer">
                        <button type="submit" id="UpdateGoalBTN" class="btn btn-info" data-toggle="modal">שמור</button>
                    </div>
                </form>
            </div>
        </div>
    </div>

</body>
</html>

