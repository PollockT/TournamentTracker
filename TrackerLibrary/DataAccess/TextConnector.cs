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
        private const string PrizesFile = "PrizeModels.csv";
        private const string PersonsFile = "PersonModels.csv";
        private const string TeamsFile = "TeamModels.csv";
        private const string MatchupsFile = "MatchupModels.csv";
        private const string MatchupEntriesFile = "MatchupEntryModels.csv";
        private const string TournamentsFiles = "TournamentModels.csv";

        public MatchupModel CreateMatchup(MatchupModel matchup)
        {
            throw new NotImplementedException();
        }

        public MatchupEntryModel CreateMatchupEntry(MatchupEntryModel matchupEntry)
        {
            throw new NotImplementedException();
        }

        public PersonModel CreatePerson(PersonModel person)
        {
            throw new NotImplementedException();
        }

        
        public PrizeModel CreatePrize(PrizeModel model)
        {
            ///Load text file and convert the text to List<PrizeModel>
            ///<param name="model"/> translated over from one line of prizes </param>
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            ///Finds the id of the highest Id, and then adds 1 to make the new current Id
            int currentId = 1;

            if(prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;
            prizes.Add(model);

            ///convert the prizes to list<string> save the list<string> to the text file
            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }

        public TeamModel CreateTeam(TeamModel team)
        {
            throw new NotImplementedException();
        }

        public TournamentModel CreateTournament(TournamentModel tournament)
        {
            throw new NotImplementedException();
        }
    }
}
