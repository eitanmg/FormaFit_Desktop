﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.IO;
using System.Configuration;
using System.Net;
using System.Net.Mail;

/// <summary>
/// Summary description for Events
/// </summary>
public class DataEvents
{
    public DataEvents()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable getEventsFromDB()
    {
        DBServices dbs = new DBServices();
        dbs = dbs.getEventsFromDB("FormaFitConnectionString");
        // save the dataset in a session object
        if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["UserDataSet"] != null)
        {
            HttpContext.Current.Session["UserDataSet"] = dbs;
        }
        return dbs.dt;
    }

    public string UpdateEventsAfterEdit(string id, string Date, string endTime, string guideName, string className, string startTime, string oldEndTime)
    {
        DBServices dbs = new DBServices();
        string answer = dbs.UpdateEventsAfterEditInDatabase("FormaFitConnectionString", "FormaClasses", id, Date, endTime);
        DBServices RegisteredUsersTBL = dbs.whoIsRegisteredToThisClass("FormaFitConnectionString", id);
        if (RegisteredUsersTBL.dt.Rows.Count > 0)
        {
            string emailSubject = Date + " בתאריך " + className + " פורמה-פיט - שינוי מועד חוג ";
            string recepientEmail = "";
            string recepientName  = "";
            foreach (DataRow row in RegisteredUsersTBL.dt.Rows)
            {
                recepientEmail = row["EmailAaddress"].ToString();
                recepientName =  row["FirstName"].ToString();
                string builedEmailBody = PopulateBody(recepientName, className, guideName, Date, startTime, endTime, oldEndTime);
                SendHtmlFormattedEmail(recepientEmail, emailSubject, builedEmailBody);
                return answer;
            }
        }
        return answer;
    }

    public string UpdateEventsAfterEditInDBbyDragging(string id, string Date, string startTime, string endTime)
    {
        DBServices dbs = new DBServices();
        string answer = dbs.UpdateEventsAfterEditInDBbyDragging("FormaFitConnectionString", "FormaClasses", id, Date, startTime, endTime);
        return answer;
    }

    public string createNewEventInDB(string classID, string NewClassDate, string classStartTime, string classEndTime, string MaximumUsersPerClass, string guideID, string isRecurring)
    {
        DBServices dbs = new DBServices();
        string answer = "";
        if (isRecurring == "Yes")
        {
            string[] dateSplit = NewClassDate.Split('-');
            int dateSplitYear = Convert.ToInt32(dateSplit[0]);   // spliting the date
            int dateSplitMonth = Convert.ToInt32(dateSplit[1]);  
            int dateSplitDay = Convert.ToInt32(dateSplit[2]);    
            int MonthLimit = 0;

            switch (dateSplitMonth)
            {                       
                case 1: case 3: case 5: case 7: case 8: case 10: case 12:
                    MonthLimit = 31;
                    break;
                case 4: case 6: case 9: case 11:
                    MonthLimit = 30;
                    break;
                case 2:
                    MonthLimit = 28;
                    break;
            }

            if ( (MonthLimit == 28) && (dateSplitYear % 4 == 0) ) // in case that February have 29 days (occurs once of 4 years)
            {
                MonthLimit = 29; 
            }

            if (dateSplitDay > 24) // cant be more than 24 for recurring event
            {
                isRecurring = "No";
                answer = dbs.createNewEventInDB("FormaFitConnectionString", "FormaClasses", classID, NewClassDate, classStartTime, classEndTime, MaximumUsersPerClass, guideID, isRecurring);                
            }
            else
            {
                List<int> DatesArray = new List<int>(); 
                DatesArray.Add(dateSplitDay);
                string allDatesSeperatedByComma = "";
                for (int i = dateSplitDay; i <= 24; i += 7)
                {
                    if (dateSplitDay + 7 < MonthLimit)
                    {
                        DatesArray.Add(i + 7);
                    }        
                }
                for (int i = 0; i < DatesArray.Count; i++)
                {
                    allDatesSeperatedByComma += dateSplitYear + "-" + dateSplitMonth + "-" + DatesArray[i] + ",";
                }
                if (DatesArray.Count <= 1) // array contain 1 obj -> not recurring event
                {
                    isRecurring = "No";
                    allDatesSeperatedByComma = NewClassDate; // give back the original date (the only one)..
                }
                answer = dbs.createNewEventInDB("FormaFitConnectionString", "FormaClasses", classID, allDatesSeperatedByComma, classStartTime, classEndTime, MaximumUsersPerClass, guideID, isRecurring);
            }           
        }
        else
        {
            answer = dbs.createNewEventInDB("FormaFitConnectionString", "FormaClasses", classID, NewClassDate, classStartTime, classEndTime, MaximumUsersPerClass, guideID, isRecurring);
        }
        return answer;
    }

    public string deleteEventFromDB(string classID)
    {
        DBServices dbs = new DBServices();
        string answer = dbs.deleteEventFromDB("FormaFitConnectionString", "FormaClasses", classID);
        return answer;
    }

    public string updateEventInDB(string classID, string className, string guideID, string classStartTime, string classEndTime, string newMaximumUsersPerClassEdit)
    {
        DBServices dbs = new DBServices();
        string answer = dbs.updateEventInDB("FormaFitConnectionString", classID, className, guideID, classStartTime, classEndTime, newMaximumUsersPerClassEdit);
        return answer;
    }

    public string createNewClassInDB(string className)
    {
        DBServices dbs = new DBServices();
        string answer = dbs.createNewClassInDB("FormaFitConnectionString", "FormaActualClasses", className);
        return answer;
    }

    public string DeleteClassFromDB(string ClassToDeleteID)
    {
        DBServices dbs = new DBServices();
        string answer = dbs.DeleteClassFromDB("FormaFitConnectionString", "FormaActualClasses", ClassToDeleteID);
        return answer;
    }

    public string createNewGuideInDB(string guideName)
    {
        DBServices dbs = new DBServices();
        string answer = dbs.createNewGuideInDB("FormaFitConnectionString", "FormaGuides", guideName);
        return answer;
    }

    public string DeleteGuideFromDB(string guideToDeleteID)
    {
        DBServices dbs = new DBServices();
        string answer = dbs.DeleteGuideFromDB("FormaFitConnectionString", "FormaGuides", guideToDeleteID);
        return answer;
    }

    public DataTable getMaxUserPerClassFromDB(string classID)
    {
        DBServices dbs = new DBServices();
        dbs = dbs.getMaxUserPerClassFromDB("FormaFitConnectionString", classID);
        // save the dataset in a session object
        if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["UserDataSet"] != null)
        {
            HttpContext.Current.Session["UserDataSet"] = dbs;
        }
        return dbs.dt;
    }

    private string PopulateBody(string recepientName, string className, string guideName, string Date, string startTime, string endTime, string oldEndTime)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/email template/forma_email_change_temp.html")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{FirstName}", recepientName);
        body = body.Replace("{ClassName}", className);
        body = body.Replace("{GuideName}", guideName);
        body = body.Replace("{oldClassDate}", Date);
        body = body.Replace("{oldClassStartTime}", startTime);
        body = body.Replace("{oldClassEndTime}", oldEndTime);
        body = body.Replace("{newClassDate}", Date);
        body = body.Replace("{newClassStartTime}", startTime);
        body = body.Replace("{newClassEndTime}", endTime);
        return body;
    }

    private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
    {
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(new MailAddress(recepientEmail));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["Host"];
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
            NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            smtp.Send(mailMessage);
        }
    }
}
