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

        //TODO- Wire up CreatePrize for text files
        public PrizeModel CreatePrize(PrizeModel model)
        {
            ///Load text file and convert the text to List<PrizeModel>
            ///<param name="model"/> translated over from one line of prizes </param>
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            ///Finds the id of the highest Id, and then adds 1 to make the new current Id
            int currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            model.Id = currentId;
            prizes.Add(model);

            

            

            // convert the prizes to list<string>

            // save the list<string> to the text file
        }
    }
}
