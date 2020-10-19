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
        public CreateTeamForm()
        {
            InitializeComponent();
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
    }
}
