using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;

/// <summary>
/// Summary description for Goal
/// </summary>
public class Goal
{
	public Goal()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable getGoalsFromDB()
    {
        DBServices dbs = new DBServices();
        dbs = dbs.getGoalsFromDB("FormaFitConnectionString");
        // save the dataset in a session object
        if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["UserDataSet"] != null)
        {
            HttpContext.Current.Session["UserDataSet"] = dbs;
        }
        return dbs.dt;
    }

    public string DeleteGoalsFromDB(string GoalID)
    {
        DBServices dbs = new DBServices();
        string answer = dbs.DeleteGoalsFromDB("FormaFitConnectionString", "FormaActualGoals", GoalID);
        return answer;
    }

    public string updateExistingGoalInDB(string _GoalID, string newGoalName, string newGoalStatus, string newUnitType)
    {
        DBServices dbs = new DBServices();
        string answer = dbs.updateExistingGoalInDB("FormaFitConnectionString", "FormaActualGoals", _GoalID, newGoalName, newGoalStatus, newUnitType);
        return answer;
    }

    public string addNewGoalInDB(string newGoalName, string newGoalStatus, string newGoalunitType)
    {
        DBServices dbs = new DBServices();
        string answer = dbs.addNewGoalInDB("FormaFitConnectionString", "FormaActualGoals", newGoalName, newGoalStatus, newGoalunitType);
        return answer;
    }
}