using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Model;

namespace TrackerLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
    {
        // *load text file

        // *convert to list<PrizeModel>

        // find the max Id

        // add the new record with the new Id(max + 1)

        // convert the prizes to list<string>

        // save the list<string> to the text file

        /// <summary>
        /// Directs application to the location of where the file should be
        /// </summary>
        /// <param name="fileName"> will be fed in the file name</param>
        /// <returns></returns>
        public static string FullFilePath(this string fileName) //PrizeModels.csv
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

        /// <summary>
        /// seperates every line with a comma as the delimitor and puts the array of items into
        /// a string called cols. Builds the array of PrizeModel
        /// </summary>
        /// <param name="lines">array of cols split by a comma</param>
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
                prize.PlaceNumber = int.Parse(cols[2]); // may need to flip placeNumber and Name
                prize.PrizeAmount = decimal.Parse(cols[3]);
                prize.PrizePercentage = double.Parse(cols[4]);
                output.Add(prize);
            }
            return output;
        }

        public static void SaveToPrizeFile(this List<PrizeModel> models, string fileName)
        {

        }
    }
}
