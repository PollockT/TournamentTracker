using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Model;
using TrackerLibrary.DataAccess;
using System.Data.SqlClient;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();

        public CreateTeamForm()
        {
            InitializeComponent();

            //CreateSampleData();

            WireUpLists();
            
        }

        
        private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Tim", LastName = "Corey" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Sue", LastName = "Storm" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Theodore", LastName = "Pollock" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Steve", LastName = "Schmitty" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Walter", LastName = "White" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Jane", LastName = "Smith" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "Bill", LastName = "Jones" });
        }

        /// <summary>
        /// fills drop down box and fills list box 
        /// </summary>
        private void WireUpLists() //TODO - refreshing of databinding after dropdown box empty
        {
            selectTeamMemberDropDown.DataSource = null;

            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = null;

            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";

            
        }

        /// <summary>
        /// Creates a new person to create team form
        /// </summary>
        /// <param name="sender">person data object</param>
        /// <param name="e">event of clicking on button</param>
        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PersonModel person = new PersonModel(
                    firstNameValue.Text,
                    lastNameValue.Text,
                    emailValue.Text,
                    cellPhoneValue.Text);


                GlobalConfig.Connection.CreatePerson(person);

                selectedTeamMembers.Add(person);
                WireUpLists();

                //reset Create Person Form Box
                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                cellPhoneValue.Text = "";
            }
            else
            {
                MessageBox.Show("Add New Member of Person Creation failed.");
            }
        }

        private bool ValidateForm()
        {

            if(firstNameValue.Text.Length == 0)
            {
                MessageBox.Show("First Name Field is empty.");
                return false;
            }

            if (lastNameValue.Text.Length == 0)
            {
                MessageBox.Show("Last Name Name Field is empty.");
                return false;
            }

            if (emailValue.Text.Length == 0)
            {
                MessageBox.Show("Email Address Field is empty.");
                return false;
            }

            if (cellPhoneValue.Text.Length == 0)
            {
                MessageBox.Show("Cell Phone Field is empty.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Button event to take dropdown box Member and add them to teamMembersListBox
        /// </summary>
        /// <param name="sender">selected person to move</param>
        /// <param name="e">event of clicking</param>
        private void addMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel personModel = (PersonModel)selectTeamMemberDropDown.SelectedItem;
            
            if(personModel != null)
            {
                availableTeamMembers.Remove(personModel);
                selectedTeamMembers.Add(personModel);

                WireUpLists();
            }          
        }

        private void removeSelectedButton_Click(object sender, EventArgs e)
        {
            PersonModel personModel = (PersonModel)teamMembersListBox.SelectedItem;

            if (personModel != null)
            {
                selectedTeamMembers.Remove(personModel);
                availableTeamMembers.Add(personModel);

                WireUpLists();
            }
        }
    }
}
