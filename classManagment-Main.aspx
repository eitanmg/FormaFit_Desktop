﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="classManagment-Main.aspx.cs" Inherits="classManagment_Main" %>

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
    <!---------------------------   ------Full calendar Scripts-------------------------------------------->
    <link href="script/fullcalendar-2.6.0/fullcalendar.css" rel="stylesheet" />
    <link href="script/fullcalendar-2.6.0/fullcalendar.print.css" rel="stylesheet" media="print" />
    <link href="script/fullcalendar-2.6.0/lib/cupertino/jquery-ui.min.css" rel="stylesheet" />
    <script src="script/fullcalendar-2.6.0/lib/moment.min.js"></script>
    <script src="script/fullcalendar-2.6.0/lib/jquery.min.js"></script>
    <script src="script/fullcalendar-2.6.0/lib/jquery-ui.custom.min.js"></script>
    <script src="script/fullcalendar-2.6.0/fullcalendar.min.js"></script>
    <script src="script/fullcalendar-2.6.0/lang-all.js"></script>
    <!----------------------------------------------------------------------------------------------------->
    <!---------------------------------------Bootstrap Scripts--------------------------------------------->
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <!----------------------------------------------------------------------------------------------------->
    <!---------------------------------------Jquery Scripts------------------------------------------------>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/themes/smoothness/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js"></script>
    <!----------------------------------------------------------------------------------------------------->
    <!---------------------------------------timepicker Scripts------------------------------------------------>
    <link href="Script/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" />
    <script src="Script/bootstrap-timepicker/js/bootstrap-timepicker.min.js"></script>
    <!-------------------------------------------------------------------------------------------------->
    <!---------------------------------------MyCode and Style ------------------------------------------------>
    <script src="Script/MyCalendarScript.js"></script>
    <link href="Style/StyleSheet.css" rel="stylesheet" />
    <!---------------------------------------------------------------------------------------------------->
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
                        <a href="userManagment-html.aspx">ניהול משתמשים</a>
                        <a href="classManagment-Main.aspx" class="current-page-item">ניהול חוגים</a>
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
                <button id="startModalForAddEvent" type="button" class="btn btn-info" data-toggle="modal" data-target="#myModal"><span class="glyphicon glyphicon-plus"></span>&nbsp; הוספת אירוע חדש</button>
                <div class="btn-group">
                    <button type="button" id="startModalForDeleteExitingClass" class="btn btn-info dropdown-toggle" data-toggle="dropdown">
                        <span class="glyphicon glyphicon-pencil"></span>&nbsp;
                        עריכת חוגים
                    </button>
                    <ul class="dropdown-menu dropdown-menu-right" role="menu">
                        <li><a href="#" id="startModalForAddNewClass" data-toggle="modal" data-target="#myModalAddNewClass"><span class="glyphicon glyphicon-plus pull-right"></span>הוספת חוג חדש</a></li>
                        <li role="separator" class="divider"></li>
                        <li><a href="#" data-toggle="modal" data-target="#myModalDeleteClass"><span class="glyphicon glyphicon-trash pull-right"></span>מחיקת חוג קיים</a></li>
                    </ul>
                </div>
                <div class="btn-group">
                    <button type="button" id="startModalForDeleteExitingGuide" class="btn btn-info dropdown-toggle" data-toggle="dropdown">
                        <span class="glyphicon glyphicon-pencil"></span>&nbsp;
                        עריכת מדריכים
                    </button>
                    <ul class="dropdown-menu dropdown-menu-right" role="menu">
                        <li><a href="#" id="startModalForAddNewGuide" data-toggle="modal" data-target="#myModalAddNewGuide"><span class="glyphicon glyphicon-plus pull-right"></span>הוספת מדריך חדש</a></li>
                        <li role="separator" class="divider"></li>
                        <li><a href="#" data-toggle="modal" data-target="#myModalDeleteGuide"><span class="glyphicon glyphicon-trash pull-right"></span>מחיקת מדריך קיים</a></li>
                    </ul>
                </div><br /><br />
            </div>
            <div id='calendar'></div>
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

    <!--this is my modal for Add new Event-->
    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">הוספת חוג</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <select id="ClassesDDL" class="form-control"></select>
                    </div>
                    <input type="datetime" class="form-control" id="classDate" placeholder="תאריך" /><br />
                    <div class="input-group bootstrap-timepicker timepicker">
                        <input id="classStartTime" type="text" class="form-control input-small" placeholder="שעת התחלה"/>
                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                    </div><br />
                    <div class="input-group bootstrap-timepicker timepicker">
                        <input id="classEndTime" type="text" class="form-control input-small" placeholder="שעת סיום" />
                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                    </div>
                    <div class="checkbox">
                        <input id="RecurringEventCheckboxID" class="RecurringEventCheckbox" type="checkbox"/>
                        <label for="RecurringEventCheckboxID">
                            <span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;סמן כאירוע חוזר לחודש זה</span>
                        </label>
                    </div>
                    <input type="number" id="MaximumUsersPerClass" class="form-control" placeholder="מספר משתתפים מקסמילי" /><br />
                    <div class="form-group">
                        <select id="GuidesDDL" class="form-control"></select>
                    </div>
                    <div class="modal-footer">
                        <button type="button" id="AddEvent" class="btn btn-info" data-toggle="modal" data-target="#myModal">הוסף</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--End of Modal-->
    <!--this is my modal for edit Event-->
    <div class="modal fade" id="myModalEditEvent" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" id="EditEventHeader"></h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <select id="ClassesDDLEdit" class="form-control"></select>
                    </div>
                    <div class="form-group">
                        <select id="GuidesDDLEdit" class="form-control"></select>
                    </div>
                    <h4><small>שעת התחלה</small></h4>
                    <div class="input-group bootstrap-timepicker timepicker">
                        <input id="classStartTimeEdit" type="text" class="form-control input-small" placeholder="שעת התחלה">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                    </div>
                    <h4><small>שעת סיום</small></h4>
                    <div class="input-group bootstrap-timepicker timepicker">
                        <input id="classEndTimeEdit" type="text" class="form-control input-small" placeholder="שעת סיום">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                    </div>
                    <h4><small>מספר משתתפים מקסמילי</small></h4>
                    <input type="number" id="MaximumUsersPerClassEdit" class="form-control" placeholder="מספר משתתפים מקסמילי" /><br />
                    <div class="modal-footer">
                        <button type="button" id="AddEventAfetEditing" class="btn btn-info center-block" data-toggle="modal" data-target="#myModalEditEvent">שמור שינוי</button><br />
                        <button type="button" id="deleteEventfromEditModal" class="btn btn-danger glyphicon glyphicon-trash center-block" data-toggle="modal" data-target="#myModalEditEvent"></button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--End of Modal-->
    <!--this is my modal for Adding new Guide-->
    <div class="modal fade" id="myModalAddNewGuide" role="dialog">
        <div class="vertical-alignment-helper">
            <div class="modal-dialog vertical-align-center">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">הוספת מדריך חדש</h4>
                        </div>
                        <div class="modal-body">
                            <input type="text" id="AddNewGuideTxtbox" class="form-control" placeholder="שם המדריך" /><br />
                            <div class="modal-footer">
                                <button type="button" id="AddGuidefromEditModal" class="btn btn-success glyphicon glyphicon-plus center-block" data-toggle="modal" data-target="#myModalAddNewGuide">&nbsp;הוסף</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--End of Modal-->
    <!--this is my modal for Delete exisiting Guide-->
    <div class="modal fade" id="myModalDeleteGuide" role="dialog">
        <div class="vertical-alignment-helper">
            <div class="modal-dialog vertical-align-center">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">מחיקת מדריך קיים</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <select id="GuidesDDLDelete" class="form-control"></select>
                            </div>
                            <div class="modal-footer">
                                <button type="button" id="deleteGuidefromEditModal" class="btn btn-danger glyphicon glyphicon-trash center-block" data-toggle="modal" data-target="#myModalDeleteGuide"></button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--End of Modal-->
    <!--this is my modal for Adding new Class-->
    <div class="modal fade" id="myModalAddNewClass" role="dialog">
        <div class="vertical-alignment-helper">
            <div class="modal-dialog vertical-align-center">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">הוספת חוג חדש</h4>
                        </div>
                        <div class="modal-body">
                            <input type="text" id="AddNewClassTxtbox" class="form-control" placeholder="שם החוג" /><br />
                            <div class="modal-footer">
                                <button type="button" id="AddClassfromEditModal" class="btn btn-success glyphicon glyphicon-plus center-block" data-toggle="modal" data-target="#myModalAddNewClass">&nbsp;הוסף</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--End of Modal-->
    <!--this is my modal for Delete exisiting Class-->
    <div class="modal fade" id="myModalDeleteClass" role="dialog">
        <div class="vertical-alignment-helper">
            <div class="modal-dialog vertical-align-center">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">מחיקת חוג קיים</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <select id="ClassDDLDelete" class="form-control"></select>
                            </div>
                            <div class="modal-footer">
                                <button type="button" id="deleteClassfromEditModal" class="btn btn-danger glyphicon glyphicon-trash center-block" data-toggle="modal" data-target="#myModalDeleteClass"></button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--End of Modal-->
</body>
</html>

