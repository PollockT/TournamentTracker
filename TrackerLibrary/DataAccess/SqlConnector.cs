﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAccess.TextHelpers;
using TrackerLibrary.Model;


namespace TrackerLibrary.DataAccess
{
    class SqlConnector : IDataConnection
    {
        public MatchupModel CreateMatchup(MatchupModel matchup)
        {
            throw new NotImplementedException();
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
                
        }

        public MatchupEntryModel CreateMatchupEntry(MatchupEntryModel matchupEntry)
        {
            throw new NotImplementedException();
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
                
        }

        public PersonModel CreatePerson(PersonModel person)
        {
            throw new NotImplementedException();
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
           
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

        public TeamModel CreateTeam(TeamModel team)
        {
            throw new NotImplementedException();
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
        
        }

        public TournamentModel CreateTournament(TournamentModel tournament)
        {
            throw new NotImplementedException();
            ///using statement for complete garabage collection after method is run
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
                
        }
    }
}
