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
        PrizeModel CreatePrize(PrizeModel model);
        PersonModel CreatePerson(PersonModel person);
        TeamModel CreateTeam(TeamModel team);
        MatchupModel CreateMatchup(MatchupModel matchup);
        MatchupEntryModel CreateMatchupEntry(MatchupEntryModel matchupEntry);
        TournamentModel CreateTournament(TournamentModel tournament);
    }
}
