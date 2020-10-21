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

        public MatchupModel CreateMatchup(MatchupModel matchupModel)
        {
            throw new NotImplementedException();
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(DB)))
            {
            
            }
                
        }

        public MatchupEntryModel CreateMatchupEntry(MatchupEntryModel matchupEntryModel)
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
        public PersonModel CreatePerson(PersonModel personModel)
        {           
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(DB)))
            {
                var person = new DynamicParameters();
                person.Add("@FirstName", personModel.FirstName);
                person.Add("@LastName", personModel.LastName);
                person.Add("@EmailAddress", personModel.EmailAddress);
                person.Add("@CellphoneNumber", personModel.CellphoneNumber);
                person.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                                              
                connection.Execute("dbo.spPeople_Insert", person, commandType: CommandType.StoredProcedure);

                personModel.Id = person.Get<int>("@id");

                return personModel;
            }
        }

        /// <summary>
        /// Saves a new prize to the database
        /// </summary>
        /// <param name="model"> the prize's information</param>
        /// <returns>The prize information, including identifier</returns>
        public PrizeModel CreatePrize(PrizeModel prizeModel)
        {
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(DB)))
            {
                var prize = new DynamicParameters();
                prize.Add("@PlaceNumber", prizeModel.PlaceNumber);
                prize.Add("@PlaceName", prizeModel.PlaceName);
                prize.Add("@PrizeAmount", prizeModel.PrizeAmount);
                prize.Add("@PrizePercentage", prizeModel.PrizePercentage);
                prize.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPrizes_Insert", prize, commandType: CommandType.StoredProcedure);

                prizeModel.Id = prize.Get<int>("@id");

                return prizeModel;
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

        
        public TeamModel CreateTeam(TeamModel teamModel)
        {
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(DB)))
            {
                var team = new DynamicParameters();
                team.Add("@TeamName", teamModel.TeamName);
                team.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spTeam_Insert", team, commandType: CommandType.StoredProcedure);
                
                teamModel.Id = team.Get<int>("@id");

                foreach(PersonModel personModel in teamModel.TeamMembers)
                {
                    team = new DynamicParameters();
                    team.Add("@TeamId", teamModel.Id);
                    team.Add("@PersonId", personModel.Id);

                    connection.Execute("dbo.spTeamMembers_Insert", team, commandType: CommandType.StoredProcedure);
                }
                return teamModel;
            }
        }

        public TournamentModel CreateTournament(TournamentModel tournamentModel)
        {
            throw new NotImplementedException();
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(DB)))
            {

            }    
        }
    }
}
