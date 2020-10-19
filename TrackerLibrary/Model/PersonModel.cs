using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Model
{
    public class PersonModel
    {
        /// <summary>
        /// Unique identifier for the person
        /// </summary>
        public int Id { get; set; }

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
        public string CellphoneNumber { get; set; }
        
        public string FullName
        {
            get
            {
                return $"{ FirstName } {LastName }";
            }
        }
        public PersonModel()
        {

        }

        public PersonModel(string firstName, string lastName, string email, string cellPhone)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = email;
            CellphoneNumber = cellPhone;
        }
    }
}
