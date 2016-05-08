using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;


/// <summary>
/// Summary description for Classes
/// </summary>
public class Classes
{
	public Classes()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable getClassesFromDB()
    {
        DBServices dbs = new DBServices();
        dbs = dbs.getClassesFromDB("FormaFitConnectionString");
        // save the dataset in a session object
        if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["UserDataSet"] != null)
        {
            HttpContext.Current.Session["UserDataSet"] = dbs;
        }
        return dbs.dt;
    }
}