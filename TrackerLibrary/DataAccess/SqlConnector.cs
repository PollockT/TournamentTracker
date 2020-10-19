using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAccess.TextHelpers;
using TrackerLibrary.Model;
using System.Data.SqlClient;

namespace TrackerLibrary.DataAccess
{
    class SqlConnector : IDataConnection
    {
        /*
        public MatchupModel CreateMatchup(MatchupModel matchup)
        {
            throw new NotImplementedException();
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {
            
            }
                
        }

        public MatchupEntryModel CreateMatchupEntry(MatchupEntryModel matchupEntry)
        {
            throw new NotImplementedException();
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {

            }    
        }
        */
        /// <summary>
        /// Saves to people table
        /// </summary>
        /// <param name="model">saves information to people records</param>
        /// <returns></returns>
        public PersonModel CreatePerson(PersonModel model)
        {           
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {
                var person = new DynamicParameters();
                person.Add("@FirstName", model.FirstName);
                person.Add("@LastName", model.LastName);
                person.Add("@EmailAddress", model.EmailAddress);
                person.Add("@CellphoneNumber", model.CellphoneNumber);
                person.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                                              
                connection.Execute("dbo.spPeople_Insert", person, commandType: CommandType.StoredProcedure);

                model.Id = person.Get<int>("@id");

                return model;
            }
        }

        /// <summary>
        /// Saves a new prize to the database
        /// </summary>
        /// <param name="model"> the prize's information</param>
        /// <returns>The prize information, including identifier</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {
                var prize = new DynamicParameters();
                prize.Add("@PlaceNumber", model.PlaceNumber);
                prize.Add("@PlaceName", model.PlaceName);
                prize.Add("@PrizeAmount", model.PrizeAmount);
                prize.Add("@PrizePercentage", model.PrizePercentage);
                prize.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPrizes_Insert", prize, commandType: CommandType.StoredProcedure);
                
                model.Id = prize.Get<int>("@id");

                return model;
            }

        }

        /*
        public TeamModel CreateTeam(TeamModel team)
        {
            throw new NotImplementedException();
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {

            }
        }

        public TournamentModel CreateTournament(TournamentModel tournament)
        {
            throw new NotImplementedException();
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {

            }    
        }*/
    }
}
