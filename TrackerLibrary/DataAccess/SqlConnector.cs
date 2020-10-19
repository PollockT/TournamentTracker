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
using System.Xml;

namespace TrackerLibrary.DataAccess
{
    class SqlConnector : IDataConnection
    {

        private const string DB = "Tournaments";

        public MatchupModel CreateMatchup(MatchupModel matchup)
        {
            throw new NotImplementedException();
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(DB)))
            {
            
            }
                
        }

        public MatchupEntryModel CreateMatchupEntry(MatchupEntryModel matchupEntry)
        {
            throw new NotImplementedException();
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(DB)))
            {

            }    
        }
        
        /// <summary>
        /// Saves to people table
        /// </summary>
        /// <param name="model">saves information to people records</param>
        /// <returns></returns>
        public PersonModel CreatePerson(PersonModel model)
        {           
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(DB)))
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
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(DB)))
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

        /// <summary>
        /// Calls stored procedure in MSSQL dbo.spPeople_GetAll
        /// </summary>
        /// <returns>list of all records in dbo.People table</returns>
        public List<PersonModel> GetPerson_All()
        {
            List<PersonModel> output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(DB)))
            {
                output = connection.Query<PersonModel>("dbo.spPeople_GetAll").ToList();
            }

            return output;
        }

        
        public TeamModel CreateTeam(TeamModel team)
        {
            throw new NotImplementedException();
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(DB)))
            {

            }
        }

        public TournamentModel CreateTournament(TournamentModel tournament)
        {
            throw new NotImplementedException();
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(DB)))
            {

            }    
        }
    }
}
