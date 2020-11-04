using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Model;

namespace TrackerLibrary
{
    public static class TournamentLogic
    {
        /// <summary>
        /// Creates Rounds for teams to compete against each other
        /// </summary>
        /// <param name="model">seperate tourny</param>
        public static void CreateRounds(TournamentModel model)
        {
            List<TeamModel> randomizedTeams = RandomizeTeamOrder(model.EnteredTeams);
            int rounds = FindNumberOfRounds(randomizedTeams.Count);
        }

        private static int FindNumberOfRounds(int teamCount)
        {
            int output = 1;
            int val = 2;

            while (val < teamCount)
            {
                //output = output + 1;
                output += 1;
                //val = val * 2;
                val *= 2;
            }

            return output;
        }


        /// <summary>
        /// Randomizes teams
        /// </summary>
        /// <param name="teams">Team Models</param>
        /// <returns>returns random guid</returns>
        private static List<TeamModel> RandomizeTeamOrder(List<TeamModel> teams)
        {
            return teams.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}
