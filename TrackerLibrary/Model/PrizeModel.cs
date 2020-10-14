using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Model
{
    public class PrizeModel
    {
        /// <summary>
        /// prize model record ID number
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Place number of the team
        /// </summary>
        public int PlaceNumber { get; set; }

        /// <summary>
        /// Place title holder
        /// </summary>
        public string PlaceName { get; set; }

        /// <summary>
        /// How much money for the prize title
        /// </summary>
        public decimal PrizeAmount { get; set; }

        /// <summary>
        /// What prize percentage goes to each place
        /// </summary>
        public double PrizePercentage { get; set; }

        public PrizeModel()
        {

        }

        public PrizeModel(string placeNumber, string placeName, string prizeAmount, string prizePercentage)
        {
            

            int placenumberValue = 0;
            int.TryParse(placeNumber, out placenumberValue);
            PlaceNumber = placenumberValue;

            PlaceName = placeName;

            decimal prizeAmountValue = 0;
            decimal.TryParse(prizeAmount, out prizeAmountValue);
            PrizeAmount = prizeAmountValue;

            double prizePercentageValue = 0;
            double.TryParse(prizePercentage, out prizePercentageValue);
            PrizePercentage = prizePercentageValue;
        }
    }
}
