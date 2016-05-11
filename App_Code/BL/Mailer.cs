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
/// Summary description for Mailer
/// </summary>
public class Mailer
{
	public Mailer()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //----------------------------------------------------ResizeMailer---------------------------------------------------

    public void getMailDataForEventResize(DBServices RegisteredUsersTBL, string className, string guideName, string Date, string startTime, string endTime, string oldEndTime) 
    {

        string emailSubject = "פורמה-פיט - שינוי שעת חוג " + className + " בתאריך " + Date;
        foreach (DataRow row in RegisteredUsersTBL.dt.Rows)
        {
            string recepientEmail = row["EmailAaddress"].ToString();
            string recepientName = row["FirstName"].ToString();
            if (recepientEmail != null && recepientEmail != "")
            {
                string builedEmailBody = PopulateBodyForEventResize(recepientName, className, guideName, Date, startTime, endTime, oldEndTime);
                SendHtmlFormattedEmailForEventResize(recepientEmail, emailSubject, builedEmailBody); 
            }
        }
    }

    public string PopulateBodyForEventResize(string recepientName, string className, string guideName, string Date, string startTime, string endTime, string oldEndTime)
    {
        string body = string.Empty;
        //using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/email template/forma_email_change_temp.html")))
        //{
        //    body = reader.ReadToEnd();
        //}
    //    StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/email template/forma_email_change_temp.html"));
        string path = Path.Combine(HttpRuntime.AppDomainAppPath, "email template/forma_email_change_resize_temp.html");
        StreamReader reader = new StreamReader(path);
        body = reader.ReadToEnd();
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

    //----------------------------------------------------end of ResizeMailer---------------------------------------------------


    //----------------------------------------------------DraggingMailer---------------------------------------------------

    public void getMailDataForEventDragging(DBServices RegisteredUsersTBL, string className, string guideName, string Date, string startTime, string endTime, string OldDate) 
    {

        string emailSubject = "פורמה-פיט - שינוי מועד חוג " + className + " בתאריך " + Date;
        foreach (DataRow row in RegisteredUsersTBL.dt.Rows)
        {
            string recepientEmail = row["EmailAaddress"].ToString();
            string recepientName = row["FirstName"].ToString();
            if (recepientEmail != null && recepientEmail != "")
            {
                string builedEmailBody = PopulateBodyForEventDragging(recepientName, className, guideName, Date, startTime, endTime, OldDate);
                SendHtmlFormattedEmailForEventResize(recepientEmail, emailSubject, builedEmailBody); 
            }
        }
    }

    public string PopulateBodyForEventDragging(string recepientName, string className, string guideName, string Date, string startTime, string endTime, string OldDate)
    {
        string body = string.Empty;
        string path = Path.Combine(HttpRuntime.AppDomainAppPath, "email template/forma_email_change_dragging_temp.html");
        StreamReader reader = new StreamReader(path);
        body = reader.ReadToEnd();
        body = body.Replace("{FirstName}", recepientName);
        body = body.Replace("{ClassName}", className);
        body = body.Replace("{GuideName}", guideName);
        body = body.Replace("{oldClassDate}", OldDate);
        body = body.Replace("{oldClassStartTime}", startTime);
        body = body.Replace("{oldClassEndTime}", endTime);
        body = body.Replace("{Date}", Date);
        return body;
    }

    //----------------------------------------------------end of DraggingMailer---------------------------------------------------

    //----------------------------------------------------Edit Mailer---------------------------------------------------

    public void getMailDataForEventEdit(DBServices RegisteredUsersTBL, string classNameInitial, string classNameText, string guideNameInitial, string guideNameText, string eventStartTimeInitial, string eventEndTimeInitial, string whatHasChangedParsed, string eventDate)
    {
        string[] whatHasChangedSplit = whatHasChangedParsed.Split(';');

        //string emailSubject = "פורמה-פיט - עדכון לגבי חוג " + className + " בתאריך " + Date;
        string emailSubject = "";


        if (whatHasChangedSplit.Length == 2)
        {
            foreach (string str in whatHasChangedSplit)
            {

                if (str.Contains("className"))
                {
                    emailSubject = "פורמה-פיט - שינוי חוג " + classNameInitial + " בתאריך " + eventDate;
                }
                else if (str.Contains("guideName"))
                {
                    emailSubject = "פורמה-פיט - שינוי מדריך בחוג " + classNameInitial + " בתאריך " + eventDate;
                }
                else if (str.Contains("classStartTime"))
                {
                    emailSubject = "פורמה-פיט - שינוי שעת התחלה בחוג " + classNameInitial + " בתאריך " + eventDate;
                }
                else if (str.Contains("classEndTime"))
                {
                    emailSubject = "פורמה-פיט - שינוי שעת סיום בחוג " + classNameInitial + " בתאריך " + eventDate;
                }
            }
        }

        //if (whatHasChangedSplit.Length == 3)
        //{
        //    foreach (string str in whatHasChangedSplit)
        //    {
        //        if (str.Contains("className") && str.Contains("guideName"))
        //        {
        //            emailSubject = "פורמה-פיט - שינוי חוג ומדריך " + classNameInitial + " בתאריך " + eventDate;
        //        }
        //    }      
        //}


                        // "className"
                        // "guideName"
                        // "classStartTime"
                        // "classEndTime"  

        foreach (DataRow row in RegisteredUsersTBL.dt.Rows)
        {
            string recepientEmail = row["EmailAaddress"].ToString();
            string recepientName = row["FirstName"].ToString();
            //if (recepientEmail != null && recepientEmail != "")
            //{
            //    string builedEmailBody = PopulateBodyForEventDragging(recepientName, className, guideName, Date, startTime, endTime, OldDate);
            //    SendHtmlFormattedEmailForEventResize(recepientEmail, emailSubject, builedEmailBody);
            //}
        }
    }

    public string PopulateBodyForEventEdit(string recepientName, string className, string guideName, string Date, string startTime, string endTime, string OldDate)
    {
        string body = string.Empty;
        string path = Path.Combine(HttpRuntime.AppDomainAppPath, "email template/forma_email_change_dragging_temp.html");
        StreamReader reader = new StreamReader(path);
        body = reader.ReadToEnd();
        body = body.Replace("{FirstName}", recepientName);
        body = body.Replace("{ClassName}", className);
        body = body.Replace("{GuideName}", guideName);
        body = body.Replace("{oldClassDate}", OldDate);
        body = body.Replace("{oldClassStartTime}", startTime);
        body = body.Replace("{oldClassEndTime}", endTime);
        body = body.Replace("{Date}", Date);
        return body;
    }



    //----------------------------------------------------end of Edit Mailer---------------------------------------------------



    public void SendHtmlFormattedEmailForEventResize(string recepientEmail, string subject, string body)
    {
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"], "פורמה-קלאב");
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