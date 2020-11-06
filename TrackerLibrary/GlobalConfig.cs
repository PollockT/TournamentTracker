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
        public const string PRIZEFILE = "PrizeModels.csv";
        public const string PERSONFILE = "PersonModels.csv";
        public const string TEAMFILE = "TeamModels.csv";
        public const string TOURNAMENTFILE = "TournamentModels.csv";
        public const string MATCHUPFILE = "MatchupModels.csv";
        public const string MATCHUPENTRYFILE = "MatchupEntryModels.csv";

        public static IDataConnection Connection { get; private set; }


        /// <summary>
        /// creates a value to input on program.cs to control database interaction
        /// </summary>
        /// <param name="db">The Database</param>
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

        /// <summary>
        /// Shortcut to creating connection string
        /// </summary>
        /// <param name="name">ConnectionString</param>
        /// <returns></returns>
        public static string CnnString(string name)
        {

            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}