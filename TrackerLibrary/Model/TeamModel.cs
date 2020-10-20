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
        /// Refernce Id number
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Team Name
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// List of persons who make up team
        /// </summary>
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();
    }
}
