using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Model
{
    public class MatchupModel
    {
        public int Id { get; set; }

        /// <summary>
        /// List of matched up teams
        /// </summary>
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();

        /// <summary>
        /// Which team is the winner
        /// </summary>
        public TeamModel Winner { get; set; }

        /// <summary>
        /// Displays what round is currently being played
        /// </summary>
        public int MatchupRound { get; set; }
    }
}
