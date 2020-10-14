using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Model
{
    public class TeamModel
    {
        /// <summary>
        /// List of persons who make up team
        /// </summary>
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();

        /// <summary>
        /// Team Name
        /// </summary>
        public string TeamName { get; set; }
    }
}
