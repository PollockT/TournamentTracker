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
using System.Linq;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();
        private int ListBoxCount = 0;
        public CreateTeamForm()
        {
            InitializeComponent();

            //CreateSampleData();

            WireUpLists();

        }

        /// <summary>
        /// Seed data
        /// </summary>
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

                ResetMemberForm();
            }
            else
            {
                MessageBox.Show("Add New Member of Person Creation failed.");
            }
        }

        private void ResetMemberForm()
        {
            firstNameValue.Text = "";
            lastNameValue.Text = "";
            emailValue.Text = "";
            cellPhoneValue.Text = "";
        }

        /// <summary>
        /// Creates Team member to put into person model
        /// </summary>
        /// <returns>Person data</returns>
        private bool ValidateForm()
        {

            if (firstNameValue.Text.Length == 0)
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

            if (personModel != null)
            {
                availableTeamMembers.Remove(personModel);
                selectedTeamMembers.Add(personModel);

                ListBoxCount = ListBoxCount + 1;
                WireUpLists();
            }
        }

        /// <summary>
        /// Removes person model from the list box, only way to remove person
        /// </summary>
        /// <param name="sender">Person being removed</param>
        /// <param name="e">mouse click event</param>
        private void removeSelectedButton_Click(object sender, EventArgs e)
        {
            PersonModel personModel = (PersonModel)teamMembersListBox.SelectedItem;

            if (personModel != null)
            {
                selectedTeamMembers.Remove(personModel);
                availableTeamMembers.Add(personModel);
                ListBoxCount = ListBoxCount - 1;
                WireUpLists();
            }
        }
        /// <summary>
        /// Creates the team with all of the team members selected
        /// </summary>
        /// <param name="sender">team model creation</param>
        /// <param name="e">mouse click event</param>
        private void createTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel team = new TeamModel();
            PersonModel personModel = (PersonModel)teamMembersListBox.SelectedItem;

            team.TeamName = teamNameValue.Text;
            team.TeamMembers = selectedTeamMembers;

            GlobalConfig.Connection.CreateTeam(team);

            //TODO - clean up form if it's not closed, reset

            ResetMemberForm();
            teamNameValue.Text = "";

            for (int i = 0; i <= ListBoxCount; i++)
            {
                teamMembersListBox.SetSelected(0, true);
                ResetingBox();
                teamMembersListBox.SetSelected(0, true);
            }
            teamMembersListBox.SetSelected(0, true);
            ResetingBox();
        }
        private void ResetingBox()
        {
            removeSelectedButton_Click(new object(), new EventArgs());
        }
    }
}

