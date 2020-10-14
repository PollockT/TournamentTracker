using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Model
{
    public class PersonModel
    {
        /// <summary>
        /// First Name of team member
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name of Team Member
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// EmailAddress of Team Member
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Cellphone number of team member
        /// </summary>
        public string CellPhoneNumber { get; set; }
    }
}
