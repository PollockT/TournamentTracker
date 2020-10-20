using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Model;
using TrackerLibrary.DataAccess.TextHelpers;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        private const string PRIZEFILE = "PrizeModels.csv";
        private const string PERSONFILE = "PersonModels.csv";
        private const string TEAMFILE = "TeamModels.csv";
        private const string MATCHUPFILE = "MatchupModels.csv";
        private const string MATCHUPENTRYFILE = "MatchupEntryModels.csv";
        private const string TOURNAMENTFILE = "TournamentModels.csv";

        /// <summary>
        /// Creates Person model to be written to PresonModel.csv, both creating and updating
        /// </summary>
        /// <param name="personModel">Person model information</param>
        /// <returns></returns>
        public PersonModel CreatePerson(PersonModel personModel)
        {
            List<PersonModel> person = PERSONFILE.FullFilePath().LoadFile().ConvertToPersonModels();

            int currentId = 1;

            if(person.Count > 0)
            {
                currentId = person.OrderByDescending(x => x.Id).First().Id + 1;
            }
            personModel.Id = currentId;
            person.Add(personModel);

            person.SaveToPersonsFile(PERSONFILE);

            return personModel;
        }

        /// <summary>
        /// Creates Prize model to be written to PrizeModel.csv, both creating and updating
        /// </summary>
        /// <param name="prizeModel">Prize Model information</param>
        /// <returns></returns>
        public PrizeModel CreatePrize(PrizeModel prizeModel)
        {
            List<PrizeModel> prizes = PRIZEFILE.FullFilePath().LoadFile().ConvertToPrizeModels();

            ///Finds the id of the highest Id, and then adds 1 to make the new current Id
            int currentId = 1;

            if(prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }
            prizeModel.Id = currentId;
            prizes.Add(prizeModel);

            ///convert the prizes to list<string> save the list<string> to the text file
            prizes.SaveToPrizeFile(PRIZEFILE);

            return prizeModel;
        }

        public TeamModel CreateTeam(TeamModel teamModel)
        {
            throw new NotImplementedException();
        }

        public List<PersonModel> GetPerson_All()
        {
            return PERSONFILE.FullFilePath().LoadFile().ConvertToPersonModels();
        }
    }
}
