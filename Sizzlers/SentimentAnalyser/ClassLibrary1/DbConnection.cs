using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;


namespace CoreDataLayer
{
    public class DbConnection
    {
        public string ConnString;
		public  SqlConnection conn;

        public DbConnection()
		{
            
            ConnString = ConfigurationManager.ConnectionStrings["InsightConnectionString"].ConnectionString;
		}
        

		public SqlConnection GetConnection()
		{	
			conn = new SqlConnection( ConnString);
			return conn;
		}

		
	
    }
}
