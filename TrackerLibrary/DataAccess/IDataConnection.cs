using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Model;

namespace TrackerLibrary.DataAccess
{
    public interface IDataConnection
    {
        /// <summary>
        /// Creates the prize model and combines all the data
        /// </summary>
        /// <param name="prizeModel">The person record loaded from table</param>
        /// <returns></returns>
        PrizeModel CreatePrize(PrizeModel prizeModel);

        /// <summary>
        /// Creates the person model and combines all the data
        /// </summary>
        /// <param name="personModel">The prize record loaded from table</param>
        /// <returns></returns>
        PersonModel CreatePerson(PersonModel personModel);

        /// <summary>
        /// Creates the person model and combines all the data
        /// </summary>
        /// <param name="teamModel">The team record loaded from table</param>
        /// <returns></returns>
        TeamModel CreateTeam(TeamModel teamModel);

        /// <summary>
        /// Creates the tournament model and passes data
        /// </summary>
        /// <param name="tournamentModel">The tournament record loaded from table</param>
        /// <returns></returns>
        void CreateTournament(TournamentModel tournamentModel);

        /// <summary>
        /// Calls Stored Procedure of [dbo].[spPeople_GetAll]
        /// </summary>
        /// <returns>All people from [dbo].[Person]</returns>
        List<PersonModel> GetPerson_All();

        /// <summary>
        /// Calls Stored Procedure of [dbo].[spTeam_GetAll]
        /// </summary>
        /// <returns>"@TeamId", team.Id</returns>
        List<TeamModel> GetTeam_All();

    }
}