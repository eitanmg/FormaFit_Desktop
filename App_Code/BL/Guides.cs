using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;

/// <summary>
/// Summary description for Guides
/// </summary>
public class Guides
{
	public Guides()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable getGuidesFromDB()
    {
        DBServices dbs = new DBServices();
        dbs = dbs.getGuidesFromDB("FormaFitConnectionString");
        // save the dataset in a session object
        if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["UserDataSet"] != null)
        {
            HttpContext.Current.Session["UserDataSet"] = dbs;
        }
        return dbs.dt;
    }
}