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
            int byes = NumberOfByes(rounds, randomizedTeams.Count);

            model.Rounds.Add(CreateFirstRound(byes, randomizedTeams));

            CreateOtherRounds(model, rounds);
            
        }

        /// <summary>
        /// Creates the first round
        /// </summary>
        /// <param name="byes">auto win</param>
        /// <param name="teams">teams particpating in first round</param>
        /// <returns></returns>
        private static List<MatchupModel> CreateFirstRound(int byes, List<TeamModel> teams)
        {
            List<MatchupModel> output = new List<MatchupModel>();
            MatchupModel currentMatchupModel = new MatchupModel();

            foreach (TeamModel team in teams)
            {
                currentMatchupModel.Entries.Add(new MatchupEntryModel { TeamCompeting = team });
                if (byes > 0 || currentMatchupModel.Entries.Count > 1)
                {
                    currentMatchupModel.MatchupRound = 1; //hardcoded because first round
                    output.Add(currentMatchupModel);
                    currentMatchupModel = new MatchupModel();

                    if (byes > 0)
                    {
                        //byes = byes - 1
                        byes -= 1;
                    }
                }
            }
            return output;
        }


        /// <summary>
        /// Loops through every possible round except first round
        /// </summary>
        /// <param name="model"></param>
        /// <param name="rounds"></param>
        private static void CreateOtherRounds(TournamentModel model, int rounds)
        {
            int round = 2;
            List<MatchupModel> previousRound = model.Rounds[0];
            List<MatchupModel> currentRound = new List<MatchupModel>();
            MatchupModel currentMatchup = new MatchupModel();

            while(round <= rounds)
            {
                foreach (MatchupModel matchup in previousRound)
                {
                    currentMatchup.Entries.Add(new MatchupEntryModel { ParentMatchup = matchup });

                    if (currentMatchup.Entries.Count > 1)
                    {
                        currentMatchup.MatchupRound = round;
                        currentRound.Add(currentMatchup);
                        currentMatchup = new MatchupModel();
                    }
                }

                model.Rounds.Add(currentRound);
                previousRound = currentRound;
                currentRound = new List<MatchupModel>();

                //round = round + 1;
                round += 1;
            }
        }

        private static int NumberOfByes(int rounds, int numberOfTeams)
        {
            int output = 0;
            int totalTeams = 1;

            for (int i = 1; i <= rounds; i++)
            {
                //totalTeams = totalTeams * 2;
                totalTeams *= 2;
            }

            output = totalTeams - numberOfTeams;
            return output;
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
