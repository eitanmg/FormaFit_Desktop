using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;

/// <summary>
/// Summary description for DBServices
/// </summary>
public class DBServices
{
    public DataTable dt;
    public SqlDataAdapter da;
    public DBServices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    public DBServices ReadUserFromDataBase(string conString, string tableName, string userType, string Password)
    {

        DBServices dbS = new DBServices(); // create a helper class
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString); // open the connection to the database/

            String selectStr = "SELECT * FROM " + tableName + " WHERE username like " + userType + " AND [password] like " + "'" + Password + "'"; // create the select that will be used by the adapter to select data from the DB
            SqlDataAdapter da = new SqlDataAdapter(selectStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds);                        // Fill the datatable (in the dataset), using the Select command

            DataTable dt = ds.Tables[0];

            // add the datatable and the dataa adapter to the dbS helper class in order to be able to save it to a Session Object
            dbS.dt = dt;
            dbS.da = da;

            return dbS;
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public DBServices getEventsFromDB(string conString)
    {

        DBServices dbS = new DBServices();
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString);
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("spGetFormaClassesAndGuides", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.StoredProcedure;
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            dbS.dt = dt;
            dbS.da = da;
            return dbS;
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public DBServices getGuidesFromDB(string conString)
    {
        DBServices dbS = new DBServices(); // create a helper class
        SqlConnection con = null;
        try
        {
            con = dbS.connect(conString); // open the connection to the database/

            String selectStr = "SELECT guideID, guideName FROM FormaGuides WHERE [guideStatus] = 'פעיל'"; // create the select that will be used by the adapter to select data from the DB
            SqlDataAdapter da = new SqlDataAdapter(selectStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds);                        // Fill the datatable (in the dataset), using the Select command

            DataTable dt = ds.Tables[0];

            // add the datatable and the dataa adapter to the dbS helper class in order to be able to save it to a Session Object
            dbS.dt = dt;
            dbS.da = da;

            return dbS;
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public DBServices getClassesFromDB(string conString)
    {
        DBServices dbS = new DBServices(); // create a helper class
        SqlConnection con = null;
        try
        {
            con = dbS.connect(conString); // open the connection to the database/

            String selectStr = "SELECT ClassID ,ClassName FROM FormaActualClasses WHERE [classStatus] = 'פעיל'"; // create the select that will be used by the adapter to select data from the DB
            SqlDataAdapter da = new SqlDataAdapter(selectStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds);                        // Fill the datatable (in the dataset), using the Select command

            DataTable dt = ds.Tables[0];

            // add the datatable and the dataa adapter to the dbS helper class in order to be able to save it to a Session Object
            dbS.dt = dt;
            dbS.da = da;

            return dbS;
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string UpdateEventsAfterEditInDatabase(string conString, string tableName, string id, string Date, string endTime)
    {

        DBServices dbS = new DBServices(); // create a helper class
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString); // open the connection to the database
            

            String UpdateStr = "UPDATE " + tableName + " SET classDate='" + Date + "' , classEndTime = '" + endTime + " ' WHERE ClassID = '" + id + "'"; // create the select that will be used by the adapter to select data from the DB

            SqlDataAdapter da = new SqlDataAdapter(UpdateStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds); // Fill the datatable (in the dataset), using the Select command
            return "האירוע עודכן בהצלחה";
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }


    public string UpdateEventsAfterEditInDBbyDragging(string conString, string tableName, string id, string Date, string startTime, string endTime)
    {

        DBServices dbS = new DBServices(); // create a helper class
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString); // open the connection to the database


            String UpdateStr = "UPDATE " + tableName + " SET classDate='" + Date + "', classStartTime = '" + startTime + " ' , classEndTime = '" + endTime + " ' WHERE ClassID = '" + id + "'"; // create the select that will be used by the adapter to select data from the DB

            SqlDataAdapter da = new SqlDataAdapter(UpdateStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds); // Fill the datatable (in the dataset), using the Select command
            return "האירוע עודכן בהצלחה";
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string createNewEventInDB(string conString, string tableName, string classID, string NewClassDate, string classStartTime, string classEndTime, string MaximumUsersPerClass, string guideID, string isRecurring)
    {
        DBServices dbS = new DBServices();
        SqlConnection con = null;
        try
        {
            if (isRecurring == "Yes")
            {
                con = dbS.connect(conString);
                String SelectLastID = "SELECT TOP 1 [ClassID] FROM [FormaClasses] ORDER BY ClassID DESC";
                SqlDataAdapter da = new SqlDataAdapter(SelectLastID, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                string LastIDNumSTR = "";
                foreach (DataRow row in dt.Rows)
                {
                    LastIDNumSTR = row["ClassID"].ToString();
                }
                int lastIdPlus1 = Convert.ToInt32(LastIDNumSTR) + 1;
                string[] dateSplitByComma = NewClassDate.Split(',');
                for (int i = 0; i < (dateSplitByComma.Length - 1) ; i++)
                {
                    String UpdateStr = "INSERT INTO " + tableName + "([className], [classDate], [classStartTime], [classEndTime], [classLocation], [classStatus], [MaximumRegistered], [GuidesID], [isRecurring], [RecurringID]) VALUES ('" + classID + "'" + "," + "'" + dateSplitByComma[i] + "'" + "," + "'" + classStartTime + "'" + "," + "'" + classEndTime + "'" + "," + "'" + "חדר 2" + "'" + "," + "'" + "פעיל" + "'" + "," + "'" + MaximumUsersPerClass + "'" + "," + "'" + guideID + "'" + "," + "'" + "Yes" + "'" + "," + "'" + lastIdPlus1 + "'" + ")";
                    SqlDataAdapter daa = new SqlDataAdapter(UpdateStr, con);
                    DataSet dss = new DataSet();
                    daa.Fill(dss);
                }
            }
            else
            {
                con = dbS.connect(conString);
                String UpdateStr = "INSERT INTO " + tableName + "([className], [classDate], [classStartTime], [classEndTime], [classLocation], [classStatus], [MaximumRegistered], [GuidesID], [isRecurring]) VALUES ('" + classID + "'" + "," + "'" + NewClassDate + "'" + "," + "'" + classStartTime + "'" + "," + "'" + classEndTime + "'" + "," + "'" + "חדר 2" + "'" + "," + "'" + "פעיל" + "'" + "," + "'" + MaximumUsersPerClass + "'" + "," + "'" + guideID + "'" + "," + "'" + "No" + "'" + ")"; 
                SqlDataAdapter da = new SqlDataAdapter(UpdateStr, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
            }
            return "עודכן בהצלחה";
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string deleteEventFromDB(string conString, string tableName, string classID)
    {

        DBServices dbS = new DBServices();
        SqlConnection con = null;
        try
        {
            con = dbS.connect(conString);
            String DeleteStr = "UPDATE [FormaClasses] SET [classStatus] = 'לא פעיל' WHERE [ClassID] = '" + classID + "'";
            SqlDataAdapter da = new SqlDataAdapter(DeleteStr, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return "האירוע נמחק בהצלחה";
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string updateEventInDB(string conString, string classID, string className, string guideID, string classStartTime, string classEndTime, string newMaximumUsersPerClassEdit)
    {

        DBServices dbS = new DBServices(); // create a helper class
        SqlConnection con = null;
        try
        {
            con = dbS.connect(conString); // open the connection to the database/
            SqlCommand cmd = new SqlCommand("spUpdateFormaClassesFromEditModal", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ClassID", SqlDbType.SmallInt).Value = classID;
            cmd.Parameters.Add("@GuidesID", SqlDbType.SmallInt).Value = guideID;
            cmd.Parameters.Add("@className", SqlDbType.SmallInt).Value = className;
            cmd.Parameters.Add("@classStartTime", SqlDbType.Time, 7).Value = classStartTime;
            cmd.Parameters.Add("@classEndTime", SqlDbType.Time, 7).Value = classEndTime;
            cmd.Parameters.Add("@newMaximumUsersPerClassEdit", SqlDbType.Int).Value = newMaximumUsersPerClassEdit;
            SqlDataReader reader = cmd.ExecuteReader();
            return "האירוע עודכן בהצלחה";
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }


    public string createNewClassInDB(string conString, string tableName, string className)
    {

        DBServices dbS = new DBServices(); // create a helper class
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString); // open the connection to the database

            String InsertStr = "INSERT INTO FormaActualClasses (ClassName, classStatus) Values ('" + className + "'" + "," + "'" + "פעיל" + "'" + ")";
            // create the select that will be used by the adapter to select data from the DB

            SqlDataAdapter da = new SqlDataAdapter(InsertStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds); // Fill the datatable (in the dataset), using the Select command
            return "החוג נוסף בהצלחה";
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string DeleteClassFromDB(string conString, string tableName, string ClassToDeleteID)
    {
        DBServices dbS = new DBServices();
        SqlConnection con = null;
        try
        {
            con = dbS.connect(conString);
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("spCheckIfClassCanBeDeletedFromDB", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ClassIdForChecking", SqlDbType.NVarChar).Value = ClassToDeleteID;
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                con = dbS.connect(conString);
                String DeleteStr = "UPDATE " + tableName + " SET [classStatus] = 'לא פעיל' WHERE ClassID = '" + ClassToDeleteID + "'";
                SqlDataAdapter daa = new SqlDataAdapter(DeleteStr, con);
                DataSet dss = new DataSet();
                daa.Fill(dss);
                return "החוג נמחק בהצלחה";
            }
            else
            {
                string msg = "";
                foreach (DataRow row in dt.Rows)
                {
                    msg += "חוג " + row["ClassName"].ToString() + " בתאריך " + row["classDate"].ToString() + " בשעה " + row["classStartTime"].ToString() + " עם " + row["GuideName"].ToString() + " \n";
                }

                return "החוג אינו יכול להימחק מבסיס הנתונים מכיוון שהוא עתיד להתקיים בזמנים הבאים: " + "\n\n" + msg + "\n" + "אנא שבץ באירועים אלו חוג אחר או לחילופין מחק את החוגים לעיל" + "";
            }
        }
        catch (Exception ex)
        {
            if (ex is SqlException)
            {
                return "It's an SQL error ... :)";
            }
            //write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string createNewGuideInDB(string conString, string tableName, string guideName)
    {

        DBServices dbS = new DBServices(); // create a helper class
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString); // open the connection to the database

            String InsertStr = "INSERT INTO FormaGuides (guideName, guideStatus) Values ('" + guideName + "'" + "," + "'" + "פעיל" + "'" + ")";
            SqlDataAdapter da = new SqlDataAdapter(InsertStr, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return "המדריך נוסף בהצלחה";
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string DeleteGuideFromDB(string conString, string tableName, string guideToDeleteID)
    {

        DBServices dbS = new DBServices();
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString);
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("spCheckIfGuideCanBeDeletedFromDB", con);  // taken care of upcoming events ONLY
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@GuideIdForChecking", SqlDbType.SmallInt).Value = guideToDeleteID;
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                con = dbS.connect(conString);
                String DeleteStr = "UPDATE " + tableName + " SET [guideStatus] = 'לא פעיל' WHERE guideID = '" + guideToDeleteID + "'";
                SqlDataAdapter daa = new SqlDataAdapter(DeleteStr, con);
                DataSet dss = new DataSet();
                daa.Fill(dss);
                return "המדריך נמחק בהצלחה";      
            }
            else
            {
                string msg = "";
                foreach (DataRow row in dt.Rows)
                {
                    msg += "חוג " + row["ClassName"].ToString() + " בתאריך " + row["classDate"].ToString() + " בשעה " + row["classStartTime"].ToString() + " \n";
                }

                return "המדריך אינו יכול להימחק מבסיס הנתונים מכיוון שהוא משובץ לחוגים הבאים: " + "\n\n" + msg + "\n" + "אנא שבץ באירועים אלו מדריך מחליף בכדי למחוק את המדריך הקיים" + "";
            }
        }
        catch (Exception ex)
        {
            if (ex is SqlException)
            {
                return "It's an SQL error ... :)";
            }
             //write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public DBServices getNotificationsFromDB(string conString)
    {

        DBServices dbS = new DBServices(); 
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString);
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("spGetNotificationsAndUpdates", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.StoredProcedure;
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            dbS.dt = dt;
            dbS.da = da;

            return dbS;
        }
        catch (Exception ex)
        {
            
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string addNewMessageToDB(string conString, string tableName, string messageText, string finalDateAndTime)
    {

        DBServices dbS = new DBServices(); // create a helper class
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString); // open the connection to the database

            String InsertStr = "INSERT INTO " + tableName + " (content, isActive, Type, DateAndTimeOfPublish) Values ('" + messageText + "'" + "," + "'פעיל'" + "," + "'כללי'" + "," + "'" + finalDateAndTime + "'" + ")";

            SqlDataAdapter da = new SqlDataAdapter(InsertStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds); // Fill the datatable (in the dataset), using the Select command
            return "ההודעה נוספה בהצלחה";
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string DeleteNotificationsFromDB(string conString, string tableName, string values)
    {

        DBServices dbS = new DBServices(); // create a helper class
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString); // open the connection to the database

            String InsertStr = "UPDATE " + tableName + " SET isActive = 'לא פעיל' WHERE id IN (" + values + ")";
            //UPDATE FormaNotificationsAndUpdates SET isActive = 'לא פעיל' WHERE id IN (15,8,7)
            SqlDataAdapter da = new SqlDataAdapter(InsertStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds); // Fill the datatable (in the dataset), using the Select command

            return "נמחק בהצלחה";
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public DBServices getCurrentNumberOfNotificationFromDB(string conString)
    {
        DBServices dbS = new DBServices(); // create a helper class
        SqlConnection con = null;
        try
        {
            con = dbS.connect(conString); // open the connection to the database/

            String selectStr = "SELECT [NotificationsToShow] FROM [FormaNotificationsToShow]"; // create the select that will be used by the adapter to select data from the DB
            SqlDataAdapter da = new SqlDataAdapter(selectStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds);                        // Fill the datatable (in the dataset), using the Select command

            DataTable dt = ds.Tables[0];

            // add the datatable and the dataa adapter to the dbS helper class in order to be able to save it to a Session Object
            dbS.dt = dt;
            dbS.da = da;

            return dbS;
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string changeCurrentNumberOfNotificationOnDB(string conString, string tableName, string messageToShowValue)
    {

        DBServices dbS = new DBServices(); // create a helper class
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString); // open the connection to the database

            String InsertStr = "UPDATE [FormaNotificationsToShow] SET [NotificationsToShow] = '" + messageToShowValue + "'";
            //UPDATE FormaNotificationsAndUpdates SET isActive = 'לא פעיל' WHERE id IN (15,8,7)
            SqlDataAdapter da = new SqlDataAdapter(InsertStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds); // Fill the datatable (in the dataset), using the Select command

            return "מספר הודעות אחרונות השתנה בהצלחה";
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public DBServices getMotivationSentencesFromDB(string conString)
    {
        DBServices dbS = new DBServices();
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString);
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("spGetMotivationSentences", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.StoredProcedure;
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            dbS.dt = dt;
            dbS.da = da;

            return dbS;
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string addNewMotivationSentenceToDB(string conString, string tableName, string SentenceText, string finalDateAndTime)
    {
        DBServices dbS = new DBServices();
        SqlConnection con = null;
        try
        {
            con = dbS.connect(conString);
            String InsertStr = "INSERT INTO " + tableName + " (content, isActive, Type, DateAndTimeOfPublish) Values ('" + SentenceText + "'" + "," + "'פעיל'" + "," + "'מוטיבציה'" + "," + "'" + finalDateAndTime + "'" + ")";
            SqlDataAdapter da = new SqlDataAdapter(InsertStr, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return "המשפט נוסף בהצלחה";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string DeleteMotivationSentenceFromDB(string conString, string tableName, string values)
    {
        DBServices dbS = new DBServices();
        SqlConnection con = null;
        try
        {
            con = dbS.connect(conString);
            String InsertStr = "UPDATE " + tableName + " SET isActive = 'לא פעיל' WHERE id IN (" + values + ")";
            SqlDataAdapter da = new SqlDataAdapter(InsertStr, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return "נמחק בהצלחה";
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public DBServices getMaxUserPerClassFromDB(string conString, string classID)
    {
        DBServices dbS = new DBServices();
        SqlConnection con = null;
        try
        {
            con = dbS.connect(conString);
            String selectStr = "SELECT [MaximumRegistered] FROM [FormaClasses] WHERE [ClassID] = '" + classID + "'";
            SqlDataAdapter da = new SqlDataAdapter(selectStr, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            dbS.dt = dt;
            dbS.da = da;
            return dbS;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public DBServices getRegisteredUsersFromDB(string conString)
    {
        DBServices dbS = new DBServices();
        SqlConnection con = null;
        try
        {
            con = dbS.connect(conString);
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("spgetRegisteredUsersFromDB", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.StoredProcedure;
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            dbS.dt = dt;
            dbS.da = da;
            return dbS;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string EditNotificationMessageInDB(string conString, string tableName, string newCellTEXT, string rowID)
    {
        DBServices dbS = new DBServices();
        SqlConnection con = null;
        try
        {
            con = dbS.connect(conString);
            String InsertStr = "UPDATE " + tableName + " SET content = " +  "'" + newCellTEXT + "'" + " WHERE [id] = " + rowID;
            SqlDataAdapter da = new SqlDataAdapter(InsertStr, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return "עודכן בהצלחה";
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public DBServices whoIsRegisteredToThisClass(string conString, string id)
    {
        DBServices dbS = new DBServices();
        SqlConnection con = null;
        try
        {
            con = dbS.connect(conString);
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("spgetRegisteredUsersByClass", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ClassIdVAR", SqlDbType.SmallInt).Value = id;
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            dbS.dt = dt;
            dbS.da = da;
            return dbS;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public DBServices getCurrentUsersFromDB(string conString)
    {
        DBServices dbS = new DBServices();
        SqlConnection con = null;
        try
        {
            con = dbS.connect(conString);
            String selectStr = "SELECT [FirstName], [LastName], [UserName], [Password], [UserType], [Status], CONVERT(char(10), [DOB],126)AS DOB ,CONVERT(char(10), [DateOfStart],126) AS DateOfStart ,CONVERT(char(10), [DateOfFinish],126) AS DateOfFinish, [PhoneNumber], [EmailAaddress], [Sex], [mailNotification] FROM FormaUsers";
            SqlDataAdapter da = new SqlDataAdapter(selectStr, con);
            DataSet ds = new DataSet();  
            da.Fill(ds);                        
            DataTable dt = ds.Tables[0];
            dbS.dt = dt;
            dbS.da = da;
            return dbS;
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string addNewUserInDB(string conString, string tableName, string FirstName, string LastName, string userSex, string UserName, string Password, string UserType, string UserStatus, string DOB, string BeginDate, string EndDate, string Mobile, string Email, string EmailNotification)
    {

        DBServices dbS = new DBServices();
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString);
            String UpdateStr = "INSERT INTO " + tableName + "([FirstName], [LastName], [UserName], [Password], [UserType], [Status], [DOB], [DateOfStart], [DateOfFinish], [PhoneNumber], [EmailAaddress], [Sex], [mailNotification]) VALUES ('" + FirstName + "'" + "," + "'" + LastName + "'" + "," + "'" + UserName + "'" + "," + "'" + Password + "'" + "," + "'" + UserType + "'" + "," + "'" + UserStatus + "'" + "," + "'" + DOB + "'" + "," + "'" + BeginDate + "'" + "," + "'" + EndDate + "'" + "," + "'" + Mobile + "'" + "," + "'" + Email + "'" + "," + "'" + userSex + "'" + "," + "'" + EmailNotification + "')";
            SqlDataAdapter da = new SqlDataAdapter(UpdateStr, con); // create the data adapter
            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds); // Fill the datatable (in the dataset), using the Select command
            return "המשתמש נוסף בהצלחה";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string updateExistingUserInDB(string conString, string tableName, string newVal, string col, string id, string currentVal)
    {

        DBServices dbS = new DBServices(); // create a helper class
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString); // open the connection to the database


            String UpdateStr = "UPDATE " + tableName + " SET content = " + "'" + newVal + "'" + " WHERE [id] = " + id;
            // create the select that will be used by the adapter to select data from the DB
            SqlDataAdapter da = new SqlDataAdapter(UpdateStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds); // Fill the datatable (in the dataset), using the Select command
            return "האירוע עודכן בהצלחה";
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string DeleteUserFromDB(string conString, string tableName, string values)
    {

        DBServices dbS = new DBServices(); // create a helper class
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString); // open the connection to the database

            String InsertStr = "DELETE FROM " + tableName + " WHERE UserName = " + values + "";
            //DELETE FROM FormaUsers WHERE UserName = 1;
            SqlDataAdapter da = new SqlDataAdapter(InsertStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds); // Fill the datatable (in the dataset), using the Select command

            return "נמחק בהצלחה";
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }
}