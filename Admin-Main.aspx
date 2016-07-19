<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin-Main.aspx.cs" Inherits="Admin_Main" %>

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
    <script src="script/fullcalendar-2.6.0/lib/jquery.min.js"></script>
    <!----------------------------------------------------------------------------------------------------->
    <!---------------------------------------Bootstrap Scripts--------------------------------------------->
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-table/1.10.1/bootstrap-table.min.css"/>
    <!-- Latest compiled and minified JavaScript -->
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-table/1.10.1/bootstrap-table.min.js"></script>
    <script src="script/bootstrap-table-he-IL.js"></script>
    <link href="style/bootstrap-rtl.min.css" rel="stylesheet" />
    <!----------------------------------------------------------------------------------------------------->
    <!---------------------------------------MyCode and Style ------------------------------------------------>
    <link href="Style/StyleSheet.css" rel="stylesheet" />
    <!----------------------------------------------------------------------------------------------------->
    <!---------------------------------------AM Charts script ------------------------------------------------>
    <script src="script/amcharts_3.19.6.free/amcharts/amcharts.js"></script>
    <script src="script/amcharts_3.19.6.free/amcharts/serial.js"></script>
    <script src="script/chartsScript.js"></script>
    <!----------------------------------------------------------------------------------------------------->
    <!-----------------------------------------------font awesome-------------------------------------------->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.1/css/font-awesome.min.css"/>
    <link href="style/font-awesome-rtl.css" rel="stylesheet" />
     <!----------------------------------------------------------------------------------------------------->
</head>
	<body dir="rtl">
		<div id="header-wrapper">
			<div class="5grid">
				<div class="12u-first">
					<header id="header">
						<nav>
							<a href="Admin-Main.aspx" class="current-page-item">דף בית</a>
							<a href="userManagment-html.aspx">ניהול משתמשים</a>
							<a href="classManagment-Main.aspx">ניהול חוגים</a>
                            <a href="ProgramManagment-Main.aspx">ניהול יעדים</a>
							<a href="notificationManagment.aspx" >הודעות ושינויים</a>
						</nav>
					</header>
				</div>
			</div>
		</div>
        <div id="main">
            <div class="5grid">
                <section>
                    <div class="container" style="height:150px">
                        <div class="jumbotron" style="height:120px; padding-top:5px">
                            <h2>ברוך הבא לעמוד ניהול מערכת פורמה-פיט!</h2>
                            <p>במערכת זו תוכל <strong>לנהל </strong>את אפליקצית פורמה פיט</p>                       
                        </div>
                    </div>
                    <div>
                        <div id="chartdiv" style="width: 650px; height: 400px; float: left; margin-right: 40px"></div>
                        <div class="col-lg-4" style="width:500px">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><i class="fa fa-clock-o fa-fw"></i>שינויים אחרונים</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="list-group">
                                        <a class="list-group-item not-active">
                                            <span class="badge">ממש עכשיו</span>
                                            <i class="fa fa-fw fa-calendar"></i>חוג יוגה נערך
                                        </a>
                                        <a class="list-group-item not-active">
                                            <span class="badge">לפני 4 דקות</span>
                                            <i class="fa fa-fw fa-user"></i>המשתמש איתן נוסף
                                        </a>
                                        <a class="list-group-item not-active">
                                            <span class="badge">לפני 47 דקות</span>
                                            <i class="fa fa-fw fa-comment"></i>הודעת מוטיבציה הוחלפה
                                        </a>
                                        <a class="list-group-item not-active">
                                            <span class="badge">לפני 2 שעות</span>
                                            <i class="fa fa-fw fa-comment"></i>הודעת שינוי עודכנה
                                        </a>
                                        <a class="list-group-item not-active">
                                            <span class="badge">לפני 1 ימים</span>
                                            <i class="fa fa-fw fa-user"></i>המשתמש חזי נוסף
                                        </a>
                                        <a class="list-group-item not-active">
                                            <span class="badge">לפני 4 ימים</span>
                                            <i class="fa fa-fw fa-calendar"></i>עודכן חוג חדש - פילאטיס
                                        </a>
                                        <a class="list-group-item not-active">
                                            <span class="badge">לפני 6 ימים</span>
                                            <i class="fa fa-fw fa-calendar"></i>חוג ספינינג בוטל
                                        </a>
                                    </div>
                                    <div class="text-right">
                                        <a href="classManagment-Main.aspx">צפיה בכל החוגים <i class="fa fa-arrow-circle-right"></i></a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div>
                    <h1 style="visibility:hidden">divider</h1>
                        <hr style="border-width:3px" />
                        <h1 style="text-align:center"><u>מתאמנים הרשומים לחוגים</u></h1>
                    <table id="RegisteredUsersToClassesTable" class="table table-hover table-bordered" data-search="true" data-locale="he-IL">
                        <thead class="btn-info">
                            <tr>
                                <th data-field="ClassID"   class="text-right" data-sortable="true">מזהה חוג</th>
                                <th data-field="FirstName" class="text-right" data-sortable="true">שם פרטי</th>
                                <th data-field="LastName"  class="text-right" data-sortable="true">שם משפחה</th>
                                <th data-field="className" class="text-right" data-sortable="true">שם חוג</th>
                                <th data-field="guideName" class="text-right" data-sortable="true">שם מדריך</th>
                                <th data-field="classDate" class="text-right" data-sortable="true">תאריך חוג</th>
                                <th data-field="classStartTime" class="text-right" data-sortable="true">שעת התחלה</th>
                                <th data-field="classEndTime" class="text-right" data-sortable="true">שעת סיום</th>
                            </tr>
                        </thead>
                    </table>
                    </div>
                    <footer class="controls">
                    </footer>
                </section>
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
