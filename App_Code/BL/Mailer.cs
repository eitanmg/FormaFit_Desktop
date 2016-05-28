using System;
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
                SendHtmlFormattedEmail(recepientEmail, emailSubject, builedEmailBody);
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
                SendHtmlFormattedEmail(recepientEmail, emailSubject, builedEmailBody);
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

    public void getMailDataForEventEdit(DBServices RegisteredUsersTBL, string classNameInitial, string classNameText, string guideNameInitial, string guideNameText, string eventStartTimeInitial, string classStartTime, string eventEndTimeInitial, string classEndTime, string whatHasChangedParsed, string eventDate)
    {
        //string[] whatHasChangedSplit = whatHasChangedParsed.Split(';');

        string emailSubject = "";
        string placeHolderHTML = "";

        switch (whatHasChangedParsed)
        {
            case "className":
                emailSubject = "פורמה-פיט - שינוי חוג " + classNameInitial + " בתאריך " + eventDate;
                placeHolderHTML = "<span> החוג " + classNameInitial + "</span> עם <span>" + guideNameInitial + " </span> בתאריך <span>" + eventDate + "</span> בשעה <span>" + eventStartTimeInitial + "</span>  עד שעה  <span>" + eventEndTimeInitial + "</span><br />הינו כעת חוג <span>" + classNameText + "</span> עם " + guideNameText + " אשר מתקיים באותה שעה ";
                break;
            case "guideName":
                emailSubject = "פורמה-פיט - שינוי מדריך בחוג " + classNameInitial + " בתאריך " + eventDate;
                placeHolderHTML = "<span> החוג " + classNameInitial + "</span> עם <span>" + guideNameInitial + " </span> בתאריך <span>" + eventDate + "</span> בשעה <span>" + eventStartTimeInitial + "</span>  עד שעה  <span>" + eventEndTimeInitial + "</span><br />מועבר כעת על ידי <span>" + guideNameText;
                break;
            case "classStartTime":
                emailSubject = "פורמה-פיט - שינוי שעת התחלה בחוג " + classNameInitial + " בתאריך " + eventDate;
                placeHolderHTML = "<span> החוג " + classNameInitial + "</span> עם <span>" + guideNameInitial + " </span> בתאריך <span>" + eventDate + "</span> בשעה <span>" + eventStartTimeInitial + "</span>  עד שעה  <span>" + eventEndTimeInitial + "</span><br /> עבר לשעה <span>" + classStartTime + "</span> עד " + classEndTime;
                break;
            case "classEndTime":
                emailSubject = "פורמה-פיט - שינוי שעת סיום בחוג " + classNameInitial + " בתאריך " + eventDate;
                placeHolderHTML = "<span> החוג " + classNameInitial + "</span> עם <span>" + guideNameInitial + " </span> בתאריך <span>" + eventDate + "</span> בשעה <span>" + eventStartTimeInitial + "</span>  עד שעה  <span>" + eventEndTimeInitial + "</span><br /> עבר לשעה <span>" + classStartTime + "</span> עד " + classEndTime;
                break;
            case "className;guideName":
                emailSubject = "פורמה-פיט - עדכון לגבי חוג " + classNameInitial + " בתאריך " + eventDate;
                placeHolderHTML = "<span> החוג " + classNameInitial + "</span> עם <span>" + guideNameInitial + " </span> בתאריך <span>" + eventDate + "</span> בשעה <span>" + eventStartTimeInitial + "</span>  עד שעה  <span>" + eventEndTimeInitial + "</span><br />הינו כעת חוג <span>" + classNameText + "</span> עם " + guideNameText + " אשר מתקיים באותה שעה ";
                break;
            case "className;guideName;classStartTime":
                emailSubject = "פורמה-פיט - עדכון לגבי חוג " + classNameInitial + " בתאריך " + eventDate;
                placeHolderHTML = "<span> החוג " + classNameInitial + "</span> עם <span>" + guideNameInitial + " </span> בתאריך <span>" + eventDate + "</span> בשעה <span>" + eventStartTimeInitial + "</span>  עד שעה  <span>" + eventEndTimeInitial + "</span><br />הינו כעת חוג <span>" + classNameText + "</span> עם " + guideNameText + " אשר מתקיים משעה " + classStartTime + " עד שעה " + classEndTime;
                break;
            case "className;guideName;classStartTime;classEndTime":
                emailSubject = "פורמה-פיט - עדכון לגבי חוג " + classNameInitial + " בתאריך " + eventDate;
                placeHolderHTML = "<span> החוג " + classNameInitial + "</span> עם <span>" + guideNameInitial + " </span> בתאריך <span>" + eventDate + "</span> בשעה <span>" + eventStartTimeInitial + "</span>  עד שעה  <span>" + eventEndTimeInitial + "</span><br />הינו כעת חוג <span>" + classNameText + "</span> עם " + guideNameText + " אשר מתקיים משעה " + classStartTime + " עד שעה " + classEndTime;
                break;
            case "className;classStartTime":
                emailSubject = "פורמה-פיט - שינוי חוג " + classNameInitial + " בתאריך " + eventDate;
                placeHolderHTML = "<span> החוג " + classNameInitial + "</span> עם <span>" + guideNameInitial + " </span> בתאריך <span>" + eventDate + "</span> בשעה <span>" + eventStartTimeInitial + "</span>  עד שעה  <span>" + eventEndTimeInitial + "</span><br />הינו כעת חוג <span>" + classNameText + "</span> עם " + guideNameText + " אשר מתקיים משעה " + classStartTime + " עד שעה " + classEndTime;
                break;
            case "className;classEndTime":
                emailSubject = "פורמה-פיט - שינוי חוג " + classNameInitial + " בתאריך " + eventDate;
                placeHolderHTML = "<span> החוג " + classNameInitial + "</span> עם <span>" + guideNameInitial + " </span> בתאריך <span>" + eventDate + "</span> בשעה <span>" + eventStartTimeInitial + "</span>  עד שעה  <span>" + eventEndTimeInitial + "</span><br />הינו כעת חוג <span>" + classNameText + "</span> עם " + guideNameText + " אשר מתקיים משעה " + classStartTime + " עד שעה " + classEndTime;
                break;
            case "className;classStartTime;classEndTime":
                emailSubject = "פורמה-פיט - שינוי חוג " + classNameInitial + " בתאריך " + eventDate;
                placeHolderHTML = "<span> החוג " + classNameInitial + "</span> עם <span>" + guideNameInitial + " </span> בתאריך <span>" + eventDate + "</span> בשעה <span>" + eventStartTimeInitial + "</span>  עד שעה  <span>" + eventEndTimeInitial + "</span><br />הינו כעת חוג <span>" + classNameText + "</span> עם " + guideNameText + " אשר מתקיים משעה " + classStartTime + " עד שעה " + classEndTime;
                break;
            case "guideName;classStartTime":
                emailSubject = "פורמה-פיט - שינוי מדריך ושעה בחוג " + classNameInitial + " בתאריך " + eventDate;
                placeHolderHTML = "<span> החוג " + classNameInitial + "</span> עם <span>" + guideNameInitial + " </span> בתאריך <span>" + eventDate + "</span> בשעה <span>" + eventStartTimeInitial + "</span>  עד שעה  <span>" + eventEndTimeInitial + "</span><br />מועבר כעת על ידי <span>" + guideNameText + " אשר מתקיים משעה " + classStartTime + " עד שעה " + classEndTime;
                break;
            case "guideName;classEndTime":
                emailSubject = "פורמה-פיט - שינוי מדריך ושעה בחוג " + classNameInitial + " בתאריך " + eventDate;
                placeHolderHTML = "<span> החוג " + classNameInitial + "</span> עם <span>" + guideNameInitial + " </span> בתאריך <span>" + eventDate + "</span> בשעה <span>" + eventStartTimeInitial + "</span>  עד שעה  <span>" + eventEndTimeInitial + "</span><br />מועבר כעת על ידי <span>" + guideNameText + " אשר מתקיים משעה " + classStartTime + " עד שעה " + classEndTime;
                break;
            case "guideName;classStartTime;classEndTime":
                emailSubject = "פורמה-פיט - שינוי מדריך ושעה בחוג " + classNameInitial + " בתאריך " + eventDate;
                placeHolderHTML = "<span> החוג " + classNameInitial + "</span> עם <span>" + guideNameInitial + " </span> בתאריך <span>" + eventDate + "</span> בשעה <span>" + eventStartTimeInitial + "</span>  עד שעה  <span>" + eventEndTimeInitial + "</span><br />מועבר כעת על ידי <span>" + guideNameText + " אשר מתקיים משעה " + classStartTime + " עד שעה " + classEndTime;
                break;
            case "classStartTime;classEndTime":
                emailSubject = "פורמה-פיט - שינוי זמן בחוג " + classNameInitial + " בתאריך " + eventDate;
                placeHolderHTML = "<span> החוג " + classNameInitial + "</span> עם <span>" + guideNameInitial + " </span> בתאריך <span>" + eventDate + "</span> בשעה <span>" + eventStartTimeInitial + "</span>  עד שעה  <span>" + eventEndTimeInitial + "</span><br /> עבר לשעה <span>" + classStartTime + "</span> עד " + classEndTime;
                break;
            default:
                break;
        }


        //if (whatHasChangedSplit.Length == 1)
        //{
        //    foreach (string str in whatHasChangedSplit)
        //    {

        //        if (str.Contains("className"))
        //        {
        //            emailSubject = "פורמה-פיט - שינוי חוג " + classNameInitial + " בתאריך " + eventDate;
        //            emailBodyType = "ClassAndGuide";
        //        }
        //        else if (str.Contains("guideName"))
        //        {
        //            emailSubject = "פורמה-פיט - שינוי מדריך בחוג " + classNameInitial + " בתאריך " + eventDate;
        //            emailBodyType = "ClassAndGuide";
        //        }
        //        else if (str.Contains("classStartTime"))
        //        {
        //            emailSubject = "פורמה-פיט - שינוי שעת התחלה בחוג " + classNameInitial + " בתאריך " + eventDate;
        //            emailBodyType = "Time";
        //        }
        //        else if (str.Contains("classEndTime"))
        //        {
        //            emailSubject = "פורמה-פיט - שינוי שעת סיום בחוג " + classNameInitial + " בתאריך " + eventDate;
        //            emailBodyType = "Time";
        //        }
        //    }
        //}

        //if (whatHasChangedSplit.Length > 1)
        //{
        //    emailSubject = "פורמה-פיט - עדכון לגבי חוג " + classNameInitial + " בתאריך " + eventDate;
        //    foreach (string str in whatHasChangedSplit)
        //    {
        //        if (str.Contains("className"))
        //        {
        //            emailBodyType = "ClassAndGuide";
        //        }
        //        else if (str.Contains("guideName"))
        //        {
        //            emailBodyType = "ClassAndGuide";
        //        }
        //        else
        //        {
        //            emailBodyType = "Time";
        //        }
        //    }
        //}


        foreach (DataRow row in RegisteredUsersTBL.dt.Rows)
        {
            string recepientEmail = row["EmailAaddress"].ToString();
            string recepientName = row["FirstName"].ToString();
            if (recepientEmail != null && recepientEmail != "")
            {
                string builedEmailBody = PopulateBodyForEventEdit(recepientName, classNameInitial, classNameText, guideNameInitial, guideNameText, eventStartTimeInitial, classStartTime, eventEndTimeInitial, classEndTime, whatHasChangedParsed, eventDate, placeHolderHTML);
                SendHtmlFormattedEmail(recepientEmail, emailSubject, builedEmailBody);
            }
        }
    }

    public string PopulateBodyForEventEdit(string recepientName, string classNameInitial, string classNameText, string guideNameInitial, string guideNameText, string eventStartTimeInitial, string classStartTime, string eventEndTimeInitial, string classEndTime, string whatHasChangedParsed, string eventDate, string placeHolderHTML)
    {
        string body = string.Empty;
        string path = Path.Combine(HttpRuntime.AppDomainAppPath, "email template/forma_email_change_modal_temp.html");
        StreamReader reader = new StreamReader(path);
        body = reader.ReadToEnd();
        body = body.Replace("{FirstName}", recepientName);
        body = body.Replace("{PlaceHolder}", placeHolderHTML);
        return body;
    }



    //----------------------------------------------------end of Edit Mailer---------------------------------------------------


    //----------------------------------------------------Delete-Cancel Mailer---------------------------------------------------

    public void getMailDataForDeleteEvent(DBServices RegisteredUsersTBL, string classNameInitial, string guideNameInitial, string eventStartTimeInitial, string eventEndTimeInitial, string eventDate)
    {

        string emailSubject = "פורמה-פיט - ביטול חוג " + classNameInitial + " בתאריך " + eventDate;
        foreach (DataRow row in RegisteredUsersTBL.dt.Rows)
        {
            string recepientEmail = row["EmailAaddress"].ToString();
            string recepientName = row["FirstName"].ToString();
            if (recepientEmail != null && recepientEmail != "")
            {
                string builedEmailBody = PopulateBodyForDeleteEvent(recepientName, classNameInitial, guideNameInitial, eventDate, eventStartTimeInitial, eventEndTimeInitial);
                SendHtmlFormattedEmail(recepientEmail, emailSubject, builedEmailBody);
            }
        }
    }

    public string PopulateBodyForDeleteEvent(string recepientName, string classNameInitial, string guideNameInitial, string eventDate, string eventStartTimeInitial, string eventEndTimeInitial)
    {
        string body = string.Empty;
        string path = Path.Combine(HttpRuntime.AppDomainAppPath, "email template/forma_email_change_delete_temp.html");
        StreamReader reader = new StreamReader(path);
        body = reader.ReadToEnd();
        body = body.Replace("{FirstName}", recepientName);
        body = body.Replace("{ClassName}", classNameInitial);
        body = body.Replace("{GuideName}", guideNameInitial);
        body = body.Replace("{oldClassDate}", eventDate);
        body = body.Replace("{oldClassStartTime}", eventStartTimeInitial);
        body = body.Replace("{oldClassEndTime}", eventEndTimeInitial);
        return body;
    }

    //----------------------------------------------------end of Delete-Cancel Mailer---------------------------------------------------

    //----------------------------------------------------New User Mailer---------------------------------------------------

    public void getMailDataForNewUserEmail(string Email, string FirstName, string UserName, string Password)
    {
        string emailSubject = "תודה על הרשמתך לאפליקציית פורמה-פיט";
        string recepientEmail = Email;
        string recepientName = FirstName;
        string builedEmailBody = PopulateBodyForNewUserEmail(recepientName, UserName, Password);
        SendHtmlFormattedEmail(recepientEmail, emailSubject, builedEmailBody);
    }



    private string PopulateBodyForNewUserEmail(string FirstName, string UserName, string Password)
    {
        string body = string.Empty;
        string path = Path.Combine(HttpRuntime.AppDomainAppPath, "email template/forma_email_template.html");
        StreamReader reader = new StreamReader(path);
        body = reader.ReadToEnd();
        body = body.Replace("{FirstName}", FirstName);
        body = body.Replace("{UserName}", UserName);
        body = body.Replace("{Password}", Password);
        return body;
    }

    //----------------------------------------------------end of New User Mailer---------------------------------------------------

    //----------------------------------------------------Credentials Mailer---------------------------------------------------

    public string SendEmailWithCredentials(string UserName, string FirstName, string userPassword, string emailAddress)
    {
        string emailSubject = "פרטי כניסה לאפליקציית פורמה-פיט";
        string recepientEmail = emailAddress;
        string recepientName = FirstName;
        string builedEmailBody = PopulateBodyForExistingUserEmail(recepientName, UserName, userPassword);
        SendHtmlFormattedEmail(recepientEmail, emailSubject, builedEmailBody);
        return "נשלח בהצלחה";
    }



    private string PopulateBodyForExistingUserEmail(string FirstName, string UserName, string Password)
    {
        string body = string.Empty;
        string path = Path.Combine(HttpRuntime.AppDomainAppPath, "email template/forma_email_template.html");
        StreamReader reader = new StreamReader(path);
        body = reader.ReadToEnd();
        body = body.Replace("{FirstName}", FirstName);
        body = body.Replace("{UserName}", UserName);
        body = body.Replace("{Password}", Password);
        return body;
    }


    //----------------------------------------------------end of Credentials Mailer---------------------------------------------------

    //----------------------------------------------------Mailer Function---------------------------------------------------

    public void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
    {
        try
        {
            MailMessage mailMessage = new MailMessage();
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
        catch (Exception ex)
        {
            //string msg = "Mail cannot be sent because of the server problem:";
            //msg += ex.Message;
            Console.WriteLine(ex.Message);
            throw ex;
        }
    }


    //public void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
    //{
    //    using (MailMessage mailMessage = new MailMessage())
    //    {
    //        mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"], "פורמה-קלאב");
    //        mailMessage.Subject = subject;
    //        mailMessage.Body = body;
    //        mailMessage.IsBodyHtml = true;
    //        mailMessage.To.Add(new MailAddress(recepientEmail));
    //        SmtpClient smtp = new SmtpClient();
    //        smtp.Host = ConfigurationManager.AppSettings["Host"];
    //        smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
    //        System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
    //        NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
    //        NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
    //        smtp.UseDefaultCredentials = true;
    //        smtp.Credentials = NetworkCred;
    //        smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
    //        smtp.Send(mailMessage);
    //    }
    //}
}

//--------------------------------------------------------------------------------------------------------------------------
