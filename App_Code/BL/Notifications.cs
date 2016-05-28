using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;

/// <summary>
/// Summary description for Notifications
/// </summary>
public class Notifications
{
	public Notifications()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public DataTable getNotificationsFromDB()
    {
        DBServices dbs = new DBServices();
        dbs = dbs.getNotificationsFromDB("FormaFitConnectionString");
        // save the dataset in a session object
     //   HttpContext.Current.Session["NotificationsDataSet"] = dbs;
        return dbs.dt;
    }

    public string addNewMessageToDB(string messageText, string finalDateAndTime)
    {
        DBServices dbs = new DBServices();
        string answer = dbs.addNewMessageToDB("FormaFitConnectionString", "FormaNotificationsAndUpdates", messageText, finalDateAndTime);
        return answer;
    }

    public string DeleteNotificationsFromDB(string values)
    {
        DBServices dbs = new DBServices();
        string answer = dbs.DeleteNotificationsFromDB("FormaFitConnectionString", "FormaNotificationsAndUpdates", values);
        return answer;
    }

    public DataTable getCurrentNumberOfNotificationFromDB()
    {
        DBServices dbs = new DBServices();
        dbs = dbs.getCurrentNumberOfNotificationFromDB("FormaFitConnectionString");
        // save the dataset in a session object
     //   HttpContext.Current.Session["NotificationsDataSet"] = dbs;
        return dbs.dt;
    }

    public string changeCurrentNumberOfNotificationOnDB(string messageToShowValue)
    {
        DBServices dbs = new DBServices();
        string answer = dbs.changeCurrentNumberOfNotificationOnDB("FormaFitConnectionString", "FormaNotificationsToShow", messageToShowValue);
        return answer;
    }

    public DataTable getMotivationSentencesFromDB()
    {
        DBServices dbs = new DBServices();
        dbs = dbs.getMotivationSentencesFromDB("FormaFitConnectionString");
        return dbs.dt;
    }

    public string addNewMotivationSentenceToDB(string SentenceText, string finalDateAndTime)
    {
        DBServices dbs = new DBServices();
        string answer = dbs.addNewMotivationSentenceToDB("FormaFitConnectionString", "FormaNotificationsAndUpdates", SentenceText, finalDateAndTime);
        return answer;
    }

    public string DeleteMotivationSentenceFromDB(string values)
    {
        DBServices dbs = new DBServices();
        string answer = dbs.DeleteMotivationSentenceFromDB("FormaFitConnectionString", "FormaNotificationsAndUpdates", values);
        return answer;
    }

    public string EditNotificationMessageInDB(string newCellTEXT, string rowID)
    {
        DBServices dbs = new DBServices();
        string answer = dbs.EditNotificationMessageInDB("FormaFitConnectionString", "FormaNotificationsAndUpdates", newCellTEXT, rowID);
        return answer;
    }
}