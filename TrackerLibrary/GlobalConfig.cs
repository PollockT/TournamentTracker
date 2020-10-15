using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary;
using TrackerLibrary.DataAccess;
using TrackerLibrary.Model;
using System.Configuration;
using System.Runtime.InteropServices;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static IDataConnection Connection { get; private set; }



        public static void InitializeConnections(DatabaseType db)
        {
            
            if (db == DatabaseType.Sql)
            {
                //TODO- Create SQL Connection properly
                SqlConnector sql = new SqlConnector();
                Connection = sql;
            }

            else if (db == DatabaseType.TextFile)
            {
                //TODO- Create the Textfile Connection
                TextConnector text = new TextConnector();
                Connection = text;
            }
        }

        public static string CnnString(string name)
        {

            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}