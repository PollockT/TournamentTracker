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

        private const string DB = "Tournaments"; // MAGIC STRING

        //////////////////////////CREATION MODELS FOR INSERTING INTO SQL TOP///////////////////////
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
        /// creates and saves teams, then grabs people dbo and inserts into team
        /// </summary>
        /// <param name="teamModel"></param>
        /// <returns></returns>
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

        /// <summary>
        ///  Pulls from three methods to create tournament
        /// </summary>
        /// <param name="tournamentModel">stores all tournament data</param>
        /// <returns></returns>
        public void CreateTournament(TournamentModel tournamentModel)
        {            
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(DB)))
            {
                SaveTournament(connection, tournamentModel);
                SaveTournamentEntries(connection, tournamentModel);
                SaveTournamentPrizes(connection, tournamentModel);
                SaveTournamentRounds(connection, tournamentModel);
                
            }            
        }

        //////////////////////////CREATION MODELS FOR INSERTING INTO SQL BOTTOM////////////////////

        /////////////////////////SAVING METHODS FOR INSERTING INTO MODELS TOP//////////////////////


        /// <summary>
        ///  CONNECTION TO SAVE TOURNAMENT METHOD
        /// </summary>
        /// <param name="connection">interface to sql connection</param>
        /// <param name="tournamentModel">grabs tournament data</param>
        private void SaveTournament(IDbConnection connection, TournamentModel tournamentModel)
        {
            var tournament = new DynamicParameters();
            tournament.Add("@TournamentName", tournamentModel.TournamentName);
            tournament.Add("@EntryFee", tournamentModel.EntryFee);
            tournament.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
            connection.Execute("dbo.spTournament_Insert", tournament, commandType: CommandType.StoredProcedure);
            tournamentModel.Id = tournament.Get<int>("@id");
        }

        /// <summary>
        /// CONNECTION TO SAVE PRIZES FOR TOURNAMENT
        /// </summary>
        /// <param name="connection">interface to sql connection</param>
        /// <param name="tournamentModel">grabs prize data to feed to tournament model</param>
        private void SaveTournamentPrizes(IDbConnection connection, TournamentModel tournamentModel)
        {
            foreach (PrizeModel prizeModel in tournamentModel.Prizes)
            {
                var tournament = new DynamicParameters();
                tournament.Add("@TournamentId", tournamentModel.Id);
                tournament.Add("@PrizeId", prizeModel.Id);
                tournament.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spTournamentPrizes_Insert", tournament, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// CONNECTION TO SAVE ENTRIES FOR TOURNAMENT
        /// </summary>
        /// <param name="connection">interface to sql connection</param>
        /// <param name="tournamentModel">grabs team entries and feed to tournament model</param>
        private void SaveTournamentEntries(IDbConnection connection, TournamentModel tournamentModel)
        {
            foreach (TeamModel teamModel in tournamentModel.EnteredTeams)
            {
                var tournament = new DynamicParameters();
                tournament.Add("@TournamentId", tournamentModel.Id);
                tournament.Add("@TeamId", teamModel.Id);
                connection.Execute("dbo.spTournamentEntries_Insert", tournament, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// CONNECTION TO SAVE ROUNDS FOR TOURNAMENT
        /// </summary>
        /// <param name="connection">interface to sql connection</param>
        /// <param name="tournamentModel">grabs rounds to feed into tournament model</param>
        private void SaveTournamentRounds(IDbConnection connection, TournamentModel tournamentModel)
        {
            foreach (List<MatchupModel> round in tournamentModel.Rounds)
            {
                foreach (MatchupModel matchup in round)
                {
                    var tournament = new DynamicParameters();
                    tournament.Add("@TournamentId", tournamentModel.Id);
                    tournament.Add("@MatchupRound", matchup.MatchupRound);
                    tournament.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Execute("dbo.spMatchups_Insert", tournament, commandType: CommandType.StoredProcedure);

                    matchup.Id = tournament.Get<int>("@id");

                    foreach (MatchupEntryModel entry in matchup.Entries)
                    {
                        tournament = new DynamicParameters();

                        tournament.Add("@MatchupId", matchup.Id);
                        if(entry.ParentMatchup == null)
                        {
                            tournament.Add("@ParentMatchupId", null);
                        }
                        else
                        {
                            tournament.Add("@ParentMatchupId", entry.ParentMatchup.Id);
                        }


                        if(entry.TeamCompeting == null)
                        {
                            tournament.Add("@TeamCompetingId", null);
                        }
                        else
                        {
                            tournament.Add("@TeamCompetingId", entry.TeamCompeting.Id);
                        }                        
                        tournament.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Execute("dbo.spMatchupEntries_Insert", tournament, commandType: CommandType.StoredProcedure);
                    }
                }
            }
        }

        /////////////////////////SAVING METHODS FOR INSERTING INTO MODELS BOTTOM///////////////////

        ////////////////////////CALLING STORED PROCEDURES FOR FUNCTIONALITY TOP////////////////////


        /// <summary>
        /// For each Team, the foreach query is run until it selects all people per team, then moves on to next team
        /// </summary>
        /// <returns>All Teams with connected team members inside</returns>
        public List<TeamModel> GetTeam_All()
        {
            List<TeamModel> output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(DB)))
            {
                output = connection.Query<TeamModel>("dbo.spTeam_GetAll").ToList();           
                foreach(TeamModel team in output)
                {
                    var person = new DynamicParameters();
                    person.Add("@TeamId", team.Id);
                    team.TeamMembers = connection.Query<PersonModel>("dbo.spTeamMembers_GetByTeam", person, 
                        commandType:CommandType.StoredProcedure).ToList();
                }
            }
            return output;
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

        ////////////////////////CALLING STORED PROCEDURES FOR FUNCTIONALITY BOTTOM/////////////////
    }
}
