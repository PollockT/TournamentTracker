using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary;
using TrackerLibrary.DataAccess;
using TrackerLibrary.Model;
using System.Configuration;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static List<IDataConnection> Connections { get; private set; } = new List<IDataConnection>();

        public static void InitializeConnections(bool database, bool textfiles)
        {
            if (database == true)
            {
                //TODO- Create SQL Connection properly
                SqlConnector sql = new SqlConnector();
                Connections.Add(sql);
            }

            if (textfiles == true)
            {
                //TODO- Create the Textfile Connection
                TextConnector text = new TextConnector();
                Connections.Add(text);
            }
        }

        public static string CnnString(string name)
        {

            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}