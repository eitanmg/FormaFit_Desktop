using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;

using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.IO;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    bool UserExistOrNot = false;
	public User()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable CheckUserInDB(string userType, string Password)
    {
        DBServices dbs = new DBServices();
        dbs = dbs.ReadUserFromDataBase("FormaFitConnectionString", "[formausers]", userType, Password);
        // save the dataset in a session object
        if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["UserDataSet"] != null)
        {
            HttpContext.Current.Session["UserDataSet"] = dbs;
        }
        return dbs.dt;
    }

    public string checkUser(string userName, string password)
    {
        User user = new User();
        DBServices CheckUser = new DBServices();
        DataTable UserDT = user.CheckUserInDB(userName, password); // read from the DataBase
        if (UserDT.Rows.Count == 0)
        {
            return "שם משתמש או ססמה אינם נכונים";
        }
        if (UserDT.Rows.Count == 1)
        {
            foreach (DataRow row in UserDT.Rows)
            {
                string isActive = row["Status"].ToString(); //check if the user is active in the system
                if (isActive == "לא פעיל")
                {
                    return "המשתמש לא פעיל";
                }
                else
                {
                    UserExistOrNot = true;
                    string TypeOfUser = row["UserType"].ToString();
                    switch (TypeOfUser)   //check which user type is that and redirect accordingly.
                    {
                        case "לקוח":
                            return "לקוח" + "," + row["FirstName"].ToString() + "," + row["LastName"].ToString() + "," + row["UserName"].ToString() + "," + row["Password"].ToString() + "," + row["DOB"].ToString() + "," + row["DateOfStart"].ToString() + "," + row["DateOfFinish"].ToString();
                            break;
                        case "מנהל":
                            return "מנהל" + "," + row["FirstName"].ToString() + "," + row["LastName"].ToString() + "," + row["UserName"].ToString() + "," + row["Password"].ToString() + "," + row["DOB"].ToString() + "," + row["DateOfStart"].ToString() + "," + row["DateOfFinish"].ToString();;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        return "לקוח";
    }

    public DataTable getCurrentUsersFromDB()
    {
        DBServices dbs = new DBServices();
        dbs = dbs.getCurrentUsersFromDB("FormaFitConnectionString");
        // save the dataset in a session object
        //   HttpContext.Current.Session["NotificationsDataSet"] = dbs;
        return dbs.dt;
    }

    public string addNewUserInDB(string FirstName, string LastName, string UserName, string Password, string UserType, string UserStatus, string DOB, string BeginDate, string EndDate, string Mobile, string Email)
    {
        DBServices dbs = new DBServices();
        string emailSubject = "תודה על הרשמתך לאפליקציית פורמה-פיט";
        string answer = dbs.addNewUserInDB("FormaFitConnectionString", "FormaUsers", FirstName, LastName, UserName, Password, UserType, UserStatus, DOB, BeginDate, EndDate, Mobile, Email);
        string builedEmailBody = PopulateBody(FirstName, UserName, Password);
        SendHtmlFormattedEmail(Email,emailSubject,builedEmailBody);
        return answer;
    }

    public string updateExistingUserInDB(string newVal, string col, string id, string currentVal)
    {
        DBServices dbs = new DBServices();
        string answer = dbs.updateExistingUserInDB("FormaFitConnectionString", "FormaUsers", newVal, col, id, currentVal);
        return answer;
    }

    public string DeleteUserFromDB(string values)
    {
        DBServices dbs = new DBServices();
        string answer = dbs.DeleteUserFromDB("FormaFitConnectionString", "FormaUsers", values);
        return answer;
    }

    public DataTable getRegisteredUsersFromDB()
    {
        DBServices dbs = new DBServices();
        dbs = dbs.getRegisteredUsersFromDB("FormaFitConnectionString");
        return dbs.dt;
    }

    private string PopulateBody(string FirstName, string UserName, string Password)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/email template/forma_email_template.html")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{FirstName}", FirstName);
        body = body.Replace("{UserName}", UserName);
        body = body.Replace("{Password}", Password);
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