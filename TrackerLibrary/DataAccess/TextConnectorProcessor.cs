using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Model;

namespace TrackerLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
    {

        /// <summary>
        /// Directs application to the location of where the file should be
        /// </summary>
        /// <param name="fileName"> will be fed in the file name</param>
        /// <returns></returns>
        public static string FullFilePath(this string fileName) //method to directory
        {
            //C:\Users\pollo\source\repos\TournamentTracker\TrackerLibrary\DataAccess\Data
            return $"{ ConfigurationManager.AppSettings["filePath"] }\\{ fileName} ";
        }

        /// <summary>
        /// Checks for created file or not, if it exists then it reads all lines and returns it
        /// to a list, if not, then it creates a new list to prepare input
        /// </summary>
        /// <param name="file">the file.csv that holds the data</param>
        /// <returns></returns>
        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        //////////////////////PRIZE MODEL FILE CSV//////////////////////////////////
        /// <summary>
        /// Converts attributes to text form so it can be written into an array and then 
        /// columns are seperated with a ','!!!
        /// </summary>
        /// <param name="lines">Each array or record of Prize</param>
        /// <returns></returns>
        public static List<PrizeModel> ConvertToPrizeModels(this List<string> lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PrizeModel prize = new PrizeModel();
                prize.Id = int.Parse(cols[0]);
                prize.PlaceName = (cols[1]);
                prize.PlaceNumber = int.Parse(cols[2]);
                prize.PrizeAmount = decimal.Parse(cols[3]);
                prize.PrizePercentage = double.Parse(cols[4]);

                output.Add(prize);
            }
            return output;
        }

        /// <summary>
        /// Array of the PrizeModel is written to the PrizeModel.csv file.
        /// </summary>
        /// <param name="models">The prize information stored in memory(Id, PlaceName, PlaceNumber,
        /// PrizeAmount, PrizePercentage)</param>
        /// <param name="fileName">The file name the model's data are saved to</param>
        public static void SaveToPrizeFile(this List<PrizeModel> models, string fileName)
        {
            List<string> lines = new List<string>();
            {
                foreach (PrizeModel prize in models)
                {
                    lines.Add($"{prize.Id},{prize.PlaceName},{prize.PlaceNumber}," +
                        $"{prize.PrizeAmount},{prize.PrizePercentage}");
                }

                File.WriteAllLines(fileName.FullFilePath(), lines);
            }
        }

        /// <summary>
        /// Converts prize into readable string, splet with '|'
        /// </summary>
        /// <param name="prizes">Prize Model</param>
        /// <returns></returns>
        private static string ConvertPrizeListToString(List<PrizeModel> prizes)
        {
            string output = "";

            if (prizes.Count() == 0)
            {
                return "";
            }

            foreach (PrizeModel prize in prizes)
            {
                output += $"{ prize.Id }|";
            }

            // keeps | from showing up on last record
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        //////////////////////PERSON MODEL FILE CSV/////////////////////////////////


        /// <summary>
        /// Converts Id to a text, and then the rest of the list for Person attributes are
        /// left as text in the array as PersonModel
        /// </summary>
        /// <param name="lines">Each array or record of a person</param>
        /// <returns></returns>
        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PersonModel person = new PersonModel();
                person.Id = int.Parse(cols[0]);
                person.FirstName = cols[1];
                person.LastName = cols[2];
                person.EmailAddress = cols[3];
                person.CellphoneNumber = cols[4];

                output.Add(person);
            }
            return output;
        }

        /// <summary>
        /// Array of PersonModel.csv
        /// </summary>
        /// <param name="models">Person model information columns in list form</param>
        /// <param name="fileName">PersonModel.csv that contains PersonModel</param>
        public static void SaveToPersonsFile(this List<PersonModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PersonModel person in models)
            {
                lines.Add($"{ person.Id },{ person.FirstName },{ person.LastName },{ person.EmailAddress }," +
                    $"{ person.CellphoneNumber }");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        /// <summary>
        /// Grabs people's saved Id's and creates them to a list to add to team
        /// </summary>
        /// <param name="people">PersonModel Id attribute</param>
        /// <returns>returns a list array of attributes found</returns>
        private static string ConvertPeopleListToString(List<PersonModel> people)
        {
            string output = "";

            if (people.Count() == 0)
            {
                return "";
            }

            foreach (PersonModel person in people)
            {
                output += $"{ person.Id }|";
            }

            // keeps | from showing up on last record
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        //////////////////////TEAM MODEL FILE CSV///////////////////////////////////

        /// <summary>
        /// builds the file structure for data to be positioned in
        /// </summary>
        /// <param name="lines">full list of team model attributes</param>
        /// <param name="PERSONFILE">the file name that will be modified to</param>
        /// <returns></returns>
        public static List<TeamModel> ConvertToTeamModels(this List<string> lines, string PERSONFILE)
        {            
            List<TeamModel> output = new List<TeamModel>();
            List<PersonModel> people = PERSONFILE.FullFilePath().LoadFile().ConvertToPersonModels();
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                TeamModel t = new TeamModel();
                t.Id = int.Parse(cols[0]);
                t.TeamName = cols[1];

                string[] personIds = cols[2].Split('|');

                foreach (string id in personIds)
                {
                    t.TeamMembers.Add(people.Where(x => x.Id == int.Parse(id)).First());
                }
            }
            return output;
        }

        /// <summary>
        /// creates array of team members to be saved to file TeamModel.csv
        /// </summary>
        /// <param name="models">Team model id, name, and ids of peopel in team</param>
        /// <param name="fileName">TeamModel.csv created and added to</param>
        public static void SaveToTeamsFile(this List<TeamModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (TeamModel team in models)
            {
                lines.Add($" {team.Id },{ team.TeamName }," +
                    $"{ ConvertPeopleListToString(team.TeamMembers) } ");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        /// <summary>
        /// Converts team into readable string
        /// </summary>
        /// <param name="teams">Team Model</param>
        /// <returns></returns>
        private static string ConvertTeamListToString(List<TeamModel> teams)
        {
            string output = "";

            if (teams.Count() == 0)
            {
                return "";
            }

            foreach (TeamModel team in teams)
            {
                output += $"{ team.Id }|";
            }

            // keeps | from showing up on last record
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        //////////////////////MATCHUP MODEL FILE CSV////////////////////////////////


        /// <summary>
        /// Not Implemented Exception
        /// </summary>
        /// <param name="models"></param>
        /// <param name="fileName"></param>
        public static void SaveToMatchupsFile(this List<MatchupModel> models, string fileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the matchup into a readable and parsable string
        /// </summary>
        /// <param name="matchups">The selected matchup</param>
        /// <returns></returns>
        private static string ConvertMatchupListToString(List<MatchupModel> matchups)
        {
            string output = "";

            if (matchups.Count() == 0)
            {
                return "";
            }

            foreach (MatchupModel matchup in matchups)
            {
                output += $"{ matchup.Id }^";
            }

            // keeps | from showing up on last record
            output = output.Substring(0, output.Length - 1);

            return output;
        }
        
        /// <summary>
        /// Converts selected round into a parsable string
        /// </summary>
        /// <param name="rounds">selected round</param>
        /// <returns></returns>
        private static string ConvertRoundListToString(List<List<MatchupModel>> rounds)
        {
            string output = "";

            if (rounds.Count() == 0)
            {
                return "";
            }

            foreach (List<MatchupModel> round in rounds)
            {
                output += $"{ ConvertMatchupListToString(round) }|";
            }


            output = output.Substring(0, output.Length - 1);

            return output;
        }

        /// <summary>
        /// Saves rounds to a file using matchups and entries
        /// </summary>
        /// <param name="model"></param>
        /// <param name="MATCHUPFILE"></param>
        /// <param name="MATCHUPENTRYFILE"></param>
        public static void SaveRoundsToFile(
            this TournamentModel model, 
            string MATCHUPFILE,
            string MATCHUPENTRYFILE)
        {
            //loop through each round
            // loop through each matchup, get id, save the matchup
            //  loop through each entry, get id, save it

            foreach (List<MatchupModel> round in model.Rounds)
            {
                foreach (MatchupModel matchup in round)
                {
                    //load all matchups from file
                    //get top id and add 1
                    //store id
                    //save matchup record'

                    matchup.SaveMatchupToFile(MATCHUPFILE, MATCHUPENTRYFILE);
                }
            }
        }

        /// <summary>
        /// Turns MatchupEntryModel object into string
        /// </summary>
        /// <param name="input">MatchupEntryModel object</param>
        /// <returns></returns>
        private static List<MatchupEntryModel> ConvertStringToMatchupEntryModels(string input)
        {
            string[] ids = input.Split('|');
            List<MatchupEntryModel> output = new List<MatchupEntryModel>();
            List<MatchupEntryModel> entries = GlobalConfig.MATCHUPENTRYFILE.FullFilePath().LoadFile().ConvertToMatchupEntryModels();

            foreach (string id in ids)
            {
                output.Add((MatchupEntryModel)entries.Where(x => x.Id == int.Parse(id).ToString().First()));
            }
            return output;
        }

        /// <summary>
        /// Converts MatchupEntryModels into objects
        /// </summary>
        /// <param name="lines">records that hold each matchup entry</param>
        /// <returns></returns>
        public static List<MatchupEntryModel> ConvertToMatchupEntryModels(this List<string> lines)
        {
            List<MatchupEntryModel> output = new List<MatchupEntryModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                MatchupEntryModel model = new MatchupEntryModel();
                model.Id = int.Parse(cols[0]);
                model.TeamCompeting = LookupTeamById(int.Parse(cols[1]));
                model.Score = double.Parse(cols[2]);

                int parentId = 0;
                if(int.TryParse(cols[3], out parentId))
                {
                    model.ParentMatchup = LookupMatchupById(parentId);
                }
                else
                {
                    model.ParentMatchup = null;
                }
                output.Add(model);
            }
            return output;
        }

        /// <summary>
        /// looks up each matchup by their Id's
        /// </summary>
        /// <param name="id">MatchupModel parameter Id</param>
        /// <returns></returns>
        private static MatchupModel LookupMatchupById(int id)
        {
            List<MatchupModel> matchups = GlobalConfig.MATCHUPFILE.FullFilePath().LoadFile().ConvertToMatchupModels();

            return matchups.Where(x => x.Id == id).First();
        }

        /// <summary>
        /// method to SaveRoundsToFile(), looks up team through recorded Id Team
        /// </summary>
        /// <param name="id">Team Model's Id</param>
        /// <returns></returns>
        private static TeamModel LookupTeamById(int id)

        {
            List<TeamModel> teams = GlobalConfig.TEAMFILE.FullFilePath().LoadFile().ConvertToTeamModels(GlobalConfig.PERSONFILE);
            
            return teams.Where(x => x.Id == id).First();
            
        }

        /// <summary>
        /// Converts matchup to object
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static List<MatchupModel> ConvertToMatchupModels(this List<string> lines)
        {            
            List<MatchupModel> output = new List<MatchupModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                MatchupModel match = new MatchupModel();
                match.Id = int.Parse(cols[0]);
                match.Entries = ConvertStringToMatchupEntryModels(cols[1]);
                match.Winner = LookupTeamById(int.Parse(cols[2]));
                match.MatchupRound = int.Parse(cols[3]);

                output.Add(match);
            }
            return output;
        }

        /// <summary>
        /// extension method to SaveRoundsToFile()
        /// </summary>
        /// <param name="matchup"></param>
        /// <param name="MATCHUPFILE"></param>
        /// <param name="MATCHUPENTRYFILE"></param>
        public static void SaveMatchupToFile(
            this MatchupModel matchup, 
            string MATCHUPFILE, 
            string MATCHUPENTRYFILE)
        {
            List<MatchupModel> matchups = GlobalConfig.MATCHUPFILE.FullFilePath().LoadFile().ConvertToMatchupModels();

            int currentId = 1;

            if(matchups.Count > 0)
            {
                currentId = matchups.OrderByDescending(x => x.Id).First().Id + 1;
            }

            matchup.Id = currentId;

            foreach (MatchupEntryModel entry in matchup.Entries)
            {
                entry.SaveEntryToFile(MATCHUPENTRYFILE);
            }
            
            List<string> lines = new List<string>();
            foreach (MatchupModel m in matchups )
            {
                string winner = "";
                if(m.Winner != null)
                {
                    winner = m.Winner.Id.ToString();
                }
                lines.Add($"{ m.Id },{ ConvertMatchupEntryListToString(m.Entries) }, { winner }, { m.MatchupRound }");
            }
            File.WriteAllLines(GlobalConfig.MATCHUPFILE.FullFilePath(), lines);
        }

        /// <summary>
        /// Converts Entrylist object to a string
        /// </summary>
        /// <param name="entryModels">object Matchup Entry Model</param>
        /// <returns></returns>
        private static string ConvertMatchupEntryListToString(List<MatchupEntryModel> entryModels)
        {
            string output = "";

            if (entryModels.Count() == 0)
            {
                return "";
            }

            foreach (MatchupEntryModel entryModel in entryModels)
            {
                output += $"{ entryModel.Id }|";
            }

            // keeps | from showing up on last record
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        /// <summary>
        /// entension method to SaveRoundsToFile()
        /// </summary>
        /// <param name="entry">entry model of ids and score</param>
        /// <param name="MATCHUPENTRYFILE">MatchupEntryModels.csv</param>
        public static void SaveEntryToFile(this MatchupEntryModel entry, string MATCHUPENTRYFILE)
        {
            List<MatchupEntryModel> entries = GlobalConfig.MATCHUPENTRYFILE.FullFilePath().LoadFile().ConvertToMatchupEntryModels();

            int currentId = 1;

            if(entries.Count > 0)
            {
                currentId = entries.OrderByDescending(x => x.Id).First().Id + 1;
            }

            entry.Id = currentId;
            entries.Add(entry);

            List<string> lines = new List<string>();

            foreach(MatchupEntryModel e in entries)
            {
                string parent = "";
                if( e.ParentMatchup != null )
                {
                    parent = e.ParentMatchup.Id.ToString(); //empty string if first round
                }
                lines.Add($"{ e.Id }, { e.TeamCompeting.Id }, { e.Score }, { parent }");
                
            }

            File.WriteAllLines(GlobalConfig.MATCHUPENTRYFILE.FullFilePath(), lines);
        }

        //////////////////////MATCHUPENTRY MODEL FILE CSV///////////////////////////

        /// <summary>
        /// Not Implemented Exception
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static List<MatchupEntryModel> ConvertToMatchupEntriesModels(this List<string> lines)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not Implemented Exception
        /// </summary>
        /// <param name="models"></param>
        /// <param name="fileName"></param>
        public static void SaveToMatchupEntriesFile(this List<MatchupEntryModel> models, string fileName)
        {
            throw new NotImplementedException();
        }

        //////////////////////TOURNAMENT MODEL FILE CSV/////////////////////////////

        /// <summary>
        /// builds Tournament model file
        /// </summary>
        /// <param name="lines">line record holder</param>
        /// <param name="TEAMFILE">TeamModels.csv</param>
        /// <param name="PERSONFILE">PersonModels.csv</param>
        /// <param name="PRIZEFILE">PrizeModels.csv</param>
        /// <returns></returns>
        public static List<TournamentModel> ConvertToTournamentModels(
            this List<string> lines,
            string TEAMFILE,
            string PERSONFILE,
            string PRIZEFILE)
        {
            //Id, TournamentName, EntryFee, (id|id|id - EnteredTeams), (id|id|id - Prizes), (Rounds - id^id^id | id^id^id | id^id^id | 
            List<TournamentModel> output = new List<TournamentModel>();
            List<TeamModel> teams = GlobalConfig.TEAMFILE.FullFilePath().LoadFile().ConvertToTeamModels(PERSONFILE);
            List<PrizeModel> prizes = GlobalConfig.PRIZEFILE.FullFilePath().LoadFile().ConvertToPrizeModels();
            List<MatchupModel> matchups = GlobalConfig.MATCHUPFILE.FullFilePath().LoadFile().ConvertToMatchupModels();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                TournamentModel tournamentModel = new TournamentModel();
                tournamentModel.Id = int.Parse(cols[0]);
                tournamentModel.TournamentName = cols[1];
                tournamentModel.EntryFee = decimal.Parse(cols[2]);
                string[] teamIds = cols[3].Split('|');
                foreach (string id in teamIds)
                {
                    tournamentModel.EnteredTeams.Add(teams.Where(x => x.Id == int.Parse(id)).First());
                }
                string[] prizeIds = cols[4].Split('|');
                foreach (string id in prizeIds)
                {
                    tournamentModel.Prizes.Add(prizes.Where(x => x.Id == int.Parse(id)).First());
                }

                string[] rounds = cols[5].Split('|');
                List<MatchupModel> ms = new List<MatchupModel>();

                foreach (string round in rounds)
                {
                    string[] msText = round.Split('^');

                    foreach (string matchupModelTextId in msText)
                    {
                        ms.Add(matchups.Where(x => x.Id == int.Parse(matchupModelTextId)).First());
                    }
                    tournamentModel.Rounds.Add(ms);
                }

                output.Add(tournamentModel);
            }
            return output;

        }

        /// <summary>
        /// Saves data into TournamentFile
        /// </summary>
        /// <param name="models">full tournament model</param>
        /// <param name="fileName">TournamentModels.csv</param>
        public static void SaveToTournamentsFile(this List<TournamentModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (TournamentModel tournamentModel in models)
            {
                lines.Add($@"{ tournamentModel.Id },
                    { tournamentModel.TournamentName },
                    { tournamentModel.EntryFee },
                    { ConvertTeamListToString(tournamentModel.EnteredTeams) },
                    { ConvertPrizeListToString(tournamentModel.Prizes) },
                    { ConvertRoundListToString(tournamentModel.Rounds) } ");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }              
    }
}
