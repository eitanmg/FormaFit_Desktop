﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Data;

/// <summary>
/// Summary description for FormaFitWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class FormaFitWebService : System.Web.Services.WebService
{

    public FormaFitWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //--------------------------------------------------------------------------
    // Method must be public
    // The method must return a string
    // The name of the parameters in the window must be identical to the names
    // passed in the Ajax call
    //--------------------------------------------------------------------------
    public string CheckUser(string name, string password)
    {
        User user = new User();
        JavaScriptSerializer js = new JavaScriptSerializer();
        string res = user.checkUser(name, password);
        string jsonString = js.Serialize(res);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string getEventsFromDB()
    {
        DataEvents evnt = new DataEvents();
        JavaScriptSerializer js = new JavaScriptSerializer();
        DataTable dt = evnt.getEventsFromDB();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        string jsonString = js.Serialize(rows);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string getGuidesFromDB()
    {
        Guides guide = new Guides();
        JavaScriptSerializer js = new JavaScriptSerializer();
        DataTable dt = guide.getGuidesFromDB();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        string jsonString = js.Serialize(rows);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string getClassesFromDB()
    {
        Classes formaClasses = new Classes();
        JavaScriptSerializer js = new JavaScriptSerializer();
        DataTable dt = formaClasses.getClassesFromDB();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        string jsonString = js.Serialize(rows);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string UpdateEventsAfterEditInDB(string id, string Date, string endTime, string guideName, string className, string startTime, string oldEndTime)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        DataEvents evnt = new DataEvents();
        string answer = evnt.UpdateEventsAfterEdit(id, Date, endTime, guideName, className, startTime, oldEndTime);
        string jsonString = js.Serialize(answer);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string UpdateEventsAfterEditInDBbyDragging(string id, string Date, string startTime, string endTime)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        DataEvents evnt = new DataEvents();
        string answer = evnt.UpdateEventsAfterEditInDBbyDragging(id, Date, startTime, endTime);
        string jsonString = js.Serialize(answer);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string createNewEventInDB(string classID, string NewClassDate, string classStartTime, string classEndTime, string MaximumUsersPerClass, string guideID, string isRecurring)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        DataEvents evnt = new DataEvents();
        string answer = evnt.createNewEventInDB(classID, NewClassDate, classStartTime, classEndTime, MaximumUsersPerClass, guideID, isRecurring);
        string jsonString = js.Serialize(answer);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string deleteEventFromDB(string classID)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        DataEvents evnt = new DataEvents();
        string answer = evnt.deleteEventFromDB(classID);
        string jsonString = js.Serialize(answer);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string updateEventInDB(string classID, string className, string guideID, string classStartTime, string classEndTime, string newMaximumUsersPerClassEdit)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        DataEvents evnt = new DataEvents();
        string answer = evnt.updateEventInDB(classID, className, guideID, classStartTime, classEndTime, newMaximumUsersPerClassEdit);
        string jsonString = js.Serialize(answer);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string createNewClassInDB(string className)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        DataEvents evnt = new DataEvents();
        string answer = evnt.createNewClassInDB(className);
        string jsonString = js.Serialize(answer);
        return jsonString;
    }
   
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string DeleteClassFromDB(string ClassToDeleteID)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        DataEvents evnt = new DataEvents();
        string answer = evnt.DeleteClassFromDB(ClassToDeleteID);
        string jsonString = js.Serialize(answer);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string createNewGuideInDB(string guideName)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        DataEvents evnt = new DataEvents();
        string answer = evnt.createNewGuideInDB(guideName);
        string jsonString = js.Serialize(answer);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string DeleteGuideFromDB(string guideToDeleteID)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        DataEvents evnt = new DataEvents();
        string answer = evnt.DeleteGuideFromDB(guideToDeleteID);
        string jsonString = js.Serialize(answer);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string getNotificationsFromDB()
    {
        Notifications notification = new Notifications();
        JavaScriptSerializer js = new JavaScriptSerializer();
        DataTable dt = notification.getNotificationsFromDB();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        string jsonString = js.Serialize(rows);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string addNewMessageToDB(string messageText,string finalDateAndTime)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        Notifications notification = new Notifications();
        string answer = notification.addNewMessageToDB(messageText, finalDateAndTime);
        string jsonString = js.Serialize(answer);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string DeleteNotificationsFromDB(string Values)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        Notifications notification = new Notifications();
        string answer = notification.DeleteNotificationsFromDB(Values);
        string jsonString = js.Serialize(answer);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string getCurrentNumberOfNotificationFromDB()
    {
        Notifications notification = new Notifications();
        JavaScriptSerializer js = new JavaScriptSerializer();
        DataTable dt = notification.getCurrentNumberOfNotificationFromDB();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        string jsonString = js.Serialize(rows);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string changeCurrentNumberOfNotificationOnDB(string messageToShowValue)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        Notifications notification = new Notifications();
        string answer = notification.changeCurrentNumberOfNotificationOnDB(messageToShowValue);
        string jsonString = js.Serialize(answer);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string getMotivationSentencesFromDB()
    {
        Notifications notification = new Notifications();
        JavaScriptSerializer js = new JavaScriptSerializer();
        DataTable dt = notification.getMotivationSentencesFromDB();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        string jsonString = js.Serialize(rows);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string addNewMotivationSentenceToDB(string SentenceText, string finalDateAndTime)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        Notifications notification = new Notifications();
        string answer = notification.addNewMotivationSentenceToDB(SentenceText, finalDateAndTime);
        string jsonString = js.Serialize(answer);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string DeleteMotivationSentenceFromDB(string Values)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        Notifications notification = new Notifications();
        string answer = notification.DeleteMotivationSentenceFromDB(Values);
        string jsonString = js.Serialize(answer);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string getMaxUserPerClassFromDB(string classID)
    {
        DataEvents evnt = new DataEvents();
        DataTable dt = evnt.getMaxUserPerClassFromDB(classID);
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(rows);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string EditNotificationMessageInDB(string newCellTEXT, string rowID)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        Notifications notification = new Notifications();
        string answer = notification.EditNotificationMessageInDB(newCellTEXT, rowID);
        string jsonString = js.Serialize(answer);
        return jsonString;
    }
    
    

    //------------------------------------------USERS---------------------------------------





    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //--------------------------------------------------------------------------
    // Method must be public
    // The method must return a string
    // The name of the parameters in the window must be identical to the names
    // passed in the Ajax call
    //--------------------------------------------------------------------------
    public string getCurrentUsersFromDB()
    {
        User user = new User();
        DataTable dt = user.getCurrentUsersFromDB();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(rows);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //--------------------------------------------------------------------------
    // Method must be public
    // The method must return a string
    // The name of the parameters in the window must be identical to the names
    // passed in the Ajax call
    //--------------------------------------------------------------------------
    public string addNewUserInDB(string FirstName, string LastName, string UserName, string Password, string UserType, string UserStatus, string DOB, string BeginDate, string EndDate, string Mobile, string Email)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        User user = new User();
        string answer = user.addNewUserInDB(FirstName, LastName, UserName, Password, UserType, UserStatus, DOB, BeginDate, EndDate, Mobile, Email);
        string jsonString = js.Serialize(answer);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //--------------------------------------------------------------------------
    // Method must be public
    // The method must return a string
    // The name of the parameters in the window must be identical to the names
    // passed in the Ajax call
    //--------------------------------------------------------------------------
    public string updateExistingUserInDB(string newVal, string col, string id, string currentVal)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        User user = new User();
        string answer = user.updateExistingUserInDB(newVal, col, id, currentVal);
        string jsonString = js.Serialize(answer);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //--------------------------------------------------------------------------
    // Method must be public
    // The method must return a string
    // The name of the parameters in the window must be identical to the names
    // passed in the Ajax call
    //--------------------------------------------------------------------------
    public string DeleteUserFromDB(string Values)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        User user = new User();
        string answer = user.DeleteUserFromDB(Values);
        string jsonString = js.Serialize(answer);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string getRegisteredUsersFromDB()
    {
        User user = new User();
        DataTable dt = user.getRegisteredUsersFromDB();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(rows);
        return jsonString;
    }
    

}