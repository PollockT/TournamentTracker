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
        PrizeModel CreatePrize(PrizeModel personModel);
        PersonModel CreatePerson(PersonModel prizeModel);
        TeamModel CreateTeam(TeamModel teamModel);
        List<PersonModel> GetPerson_All();

        
    }
}
