﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userManagment-html.aspx.cs" Inherits="userManagment_html" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>פורמה פיט - ניהול משתמשים</title>
    <meta http-equiv="content-type" content="text/html" charset="utf-8" dir="rtl" />
    <meta name="viewport" content="width=device-width, inital-scale=1.0" />
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
    <!---------------------------------------Bootstrap Table Scripts--------------------------------------------->
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-table/1.10.1/bootstrap-table.min.css" />
    <!-- Latest compiled and minified JavaScript -->
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-table/1.10.1/bootstrap-table.min.js"></script>
    <script src="script/bootstrap-table-he-IL.js"></script>
    <!---------------------------------------My style + script-------------------------------------------------------->
    <script src="script/userManagmentScript.js"></script>
    <link href="style/StyleSheet.css" rel="stylesheet" />
    <!----------------------------------------------------------------------------------------------------->
    <!---------------------------------------Hebrew DatePicker--------------------------------------------->
    <script src="script/datepicker-he.js"></script>
    <!---------------------------------------------------------------------------------------------------->
    <!---------------------------------------sweetalert script--------------------------------------------->
    <link href="script/sweetalert/sweetalert.css" rel="stylesheet" />
    <script src="script/sweetalert/sweetalert.min.js"></script>
    <!---------------------------------------------------------------------------------------------------->
</head>
<body dir="rtl">
    <div id="header-wrapper">
        <div class="5grid">
            <div class="12u-first">
                <header id="header">
                    <nav>
                        <a href="Admin-Main.aspx">דף בית</a>
                        <a href="userManagment-html.aspx" class="current-page-item">ניהול משתמשים</a>
                        <a href="classManagment-Main.aspx">ניהול חוגים</a>
                        <a href="ProgramManagment-Main.aspx">ניהול יעדים</a>
                        <a href="notificationManagment.aspx">הודעות ושינויים</a>
                    </nav>
                </header>
            </div>
        </div>
    </div>
    <div id="banner-wrapper">
        <div class="5grid">
            <div class="12u-first">
                <button type="button" class="btn btn-info" data-toggle="modal" data-target="#addUserModal"><span class="glyphicon glyphicon-plus"></span>&nbsp;הוספת משתמש חדש</button>
            </div>
            <br />
            <label><input id="isActivefilter" type="checkbox">הצג רק משתמשים פעילים</label>

            <table id="UserTable" class="table table-hover table-bordered" data-search="true" data-locale="he-IL">
                <thead class="btn-info">
                    <tr>
                        <th data-field="action" data-formatter="actionFormatter" data-events="actionEvents" class="text-center">עריכה</th>
                        <th data-field="FirstName" class="text-right" data-sortable="true">שם פרטי</th>
                        <th data-field="LastName" class="text-right" data-sortable="true">שם משפחה</th>
                        <th data-field="Sex" class="text-right">מין</th>
                        <th data-field="UserName" class="text-right">שם משתמש</th>
                        <th data-field="Password" class="text-right">סיסמה</th>
                        <th data-field="UserType" class="text-right">סוג משתמש</th>
                        <th data-field="Status" class="text-right">סטאטוס</th>
                        <th data-field="DOB" class="text-right" data-sortable="true">תאריך לידה</th>
                        <th data-field="DateOfStart" class="text-right" data-sortable="true">ת. תחילת מנוי</th>
                        <th data-field="DateOfFinish" class="text-right" data-sortable="true">ת. סוף מנוי</th>
                        <th data-field="PhoneNumber" class="text-right">נייד</th>
                        <th data-field="EmailAaddress" class="text-right">דוא"ל</th>
                        <th data-field="mailNotification" class="text-right">עדכונים</th>
                    </tr>
                </thead>
                <tbody class="searchable"></tbody>
            </table>

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


    <!--modal for adding new user-->
    <div class="modal fade" id="addUserModal" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">הוספת משתמש</h4>
                </div>
                <form class="modal-body">
                    <input id="FirstName" placeholder="שם פרטי" class="form-control form-group"/>
                    <input id="LastName" placeholder="שם משפחה" class="form-control form-group"/>
                    <select id="sexDDL" class="form-control form-group">
                        <option value="סוג">מין</option>
                        <option value="זכר">זכר</option>
                        <option value="נקבה">נקבה</option>
                    </select>
                    <input id="UserName" placeholder="שם משתמש" class="form-control form-group"/>
                    <input id="Password" placeholder="סיסמא" class="form-control form-group"/>
                    <select id="typeDDL" class="form-control form-group">
                        <option value="סוג">סוג משתמש</option>
                        <option value="משתמש">מתאמן</option>
                        <option value="מנהל">מנהל</option>
                    </select>
                    <select id="statusDDL" class="form-control form-group">
                        <option value="סטאטוס">סטאטוס</option>
                        <option value="פעיל">פעיל</option>
                        <option value="לא פעיל">לא פעיל</option>
                    </select>
                    <input type="text" class="form-control DateField" id="DOB" placeholder="תאריך לידה"/><br />
                    <input type="text" class="form-control DateField" id="BeginDate" placeholder="תאריך תחילת מנוי"/><br />
                    <input type="text" class="form-control DateField" id="EndDate" placeholder="תאריך סוף מנוי"/><br />
                    <input id="Mobile" class="form-control form-group" placeholder="נייד">
                    <input id="Email" type="email" class="form-control form-group" placeholder="כתובת דואר אלקטרוני">
                    <select id="mailNotificationDDL" class="form-control form-group">
                        <option value="הודעות">הודעות ועדכונים באימייל</option>
                        <option value="פעיל">פעיל</option>
                        <option value="לא פעיל">לא פעיל</option>
                    </select>
                    <div class="modal-footer">
                        <button type="submit" id="AddUser" class="btn btn-info" data-toggle="modal">הוסף</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
    <!--End of Modal-->
    <!--Edit modal-->
    <div class="modal fade" id="editUserModal" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">עריכת משתמש</h4>
                </div>
                <form class="modal-body">
                    <input id="FirstNameEdit" placeholder="שם פרטי" class="form-control form-group" />
                    <input id="LastNameEdit" placeholder="שם משפחה" class="form-control form-group" />
                    <select id="sexDDLEdit" class="form-control form-group">
                        <option value="מין">מין</option>
                        <option value="זכר">זכר</option>
                        <option value="נקבה">נקבה</option>
                    </select>
                    <input id="UserNameEdit" placeholder="שם משתמש" class="form-control form-group" />
                    <input id="PasswordEdit" placeholder="סיסמא" class="form-control form-group" />
                    <select id="typeDDLEdit" class="form-control form-group">
                        <option value="סוג">סוג משתמש</option>
                        <option value="משתמש">משתמש</option>
                        <option value="מנהל">מנהל</option>
                    </select>
                    <select id="statusDDLEdit" class="form-control form-group">
                        <option>סטאטוס</option>
                        <option>לא פעיל</option>
                        <option>פעיל</option>
                    </select>
                    <input type="text" class="form-control DateField" id="DOBEdit" placeholder="תאריך לידה" /><br />
                    <input type="text" class="form-control DateField" id="BeginDateEdit" placeholder="תאריך תחילת מנוי" /><br />
                    <input type="text" class="form-control DateField" id="EndDateEdit" placeholder="תאריך סוף מנוי" /><br />
                    <input id="MobileEdit" class="form-control form-group" placeholder="נייד">
                    <input id="EmailEdit" type="email" class="form-control form-group" placeholder="כתובת דואר אלקטרוני">
                    <select id="mailNotificationDDLEdit" class="form-control form-group">
                        <option>הודעות ועדכונים באימייל</option>
                        <option>לא פעיל</option>
                        <option>פעיל</option>
                    </select>
                    <div class="modal-footer">
                        <button type="submit" id="UpdateUser" class="btn btn-info" data-toggle="modal">שמור</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</body>
</html>

