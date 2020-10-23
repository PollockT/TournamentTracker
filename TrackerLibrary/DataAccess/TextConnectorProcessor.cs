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
                foreach(PrizeModel prize in models)
                {
                    lines.Add($"{prize.Id},{prize.PlaceName},{prize.PlaceNumber}," +
                        $"{prize.PrizeAmount},{prize.PrizePercentage}");
                }

                File.WriteAllLines(fileName.FullFilePath(), lines);
            }
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

            foreach(string line in lines)
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

            foreach(PersonModel person in models)
            {
                lines.Add($"{ person.Id },{ person.FirstName },{ person.LastName },{ person.EmailAddress }," +
                    $"{ person.CellphoneNumber }");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        //////////////////////TEAM MODEL FILE CSV///////////////////////////////////

        /// <summary>
        /// builds the file structure for data to be positioned in
        /// </summary>
        /// <param name="lines">full list of team model attributes</param>
        /// <param name="peopleFileName">the file name that will be modified to</param>
        /// <returns></returns>
        public static List<TeamModel> ConvertToTeamModels(this List<string> lines, string peopleFileName)
        {
            //id,team name, list of ids seperated by pipe
            //3,Team 1, 1|3|5

            List<TeamModel> output = new List<TeamModel>();
            List<PersonModel> people = peopleFileName.FullFilePath().LoadFile().ConvertToPersonModels();
            foreach(string line in lines)
            {
                string[] cols = line.Split(',');

                TeamModel t = new TeamModel();
                t.Id = int.Parse(cols[0]);
                t.TeamName = cols[1];

                string[] personIds = cols[2].Split('|');

                foreach(string id in personIds)
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
        /// Grabs people's saved Id's and creates them to a list to add to team
        /// </summary>
        /// <param name="people">PersonModel Id attribute</param>
        /// <returns>returns a list array of attributes found</returns>
        private static string ConvertPeopleListToString(List<PersonModel> people)
        {
            string output = "";

            if(people.Count() == 0){
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

        //////////////////////MATCHUP MODEL FILE CSV////////////////////////////////

        public static List<MatchupModel> ConvertToMatchupModels(this List<string> lines)
        {
            throw new NotImplementedException();
        }

        public static void SaveToMatchupsFile(this List<MatchupModel> models, string fileName)
        {
            throw new NotImplementedException();
        }
        
        //////////////////////MATCHUPENTRY MODEL FILE CSV///////////////////////////

        public static List<MatchupEntryModel> ConvertToMatchupEntriesModels(this List<string> lines)
        {
            throw new NotImplementedException();
        }

        public static void SaveToMatchupEntriesFile(this List<MatchupEntryModel> models, string fileName)
        {
            throw new NotImplementedException();
        }


        //////////////////////TOURNAMENT MODEL FILE CSV/////////////////////////////
        
        public static List<TournamentModel> ConvertToTournamentModels(this List<string> lines)
        {
            throw new NotImplementedException();
        }
        
        public static void SaveToTournamentsFile(this List<TournamentModel> models, string fileName)
        {
            throw new NotImplementedException();
        }
        
    }
}
