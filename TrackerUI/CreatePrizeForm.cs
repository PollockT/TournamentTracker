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
    public partial class CreatePrizeForm : Form
    {
        IPrizeRequester callingForm;

        public CreatePrizeForm(IPrizeRequester caller)
        {
            InitializeComponent();
            callingForm = caller;
        }
        
       
        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PrizeModel prizeModel = new PrizeModel(                    
                    placeNumberValue.Text,
                    placeNameValue.Text,
                    prizeAmountValue.Text,
                    prizePercentageValue.Text);


                prizeModel = GlobalConfig.Connection.CreatePrize(prizeModel);

                callingForm.PrizeComplete(prizeModel);

                this.Close();
                // prizeModelReset();
            }
            else
            {
                MessageBox.Show("This form has invalid information." +
                    " Please check the values entered!");
            }
        }

        private void prizeModelReset()
        {
            placeNumberValue.Text = "";
            placeNameValue.Text = "";            
            prizeAmountValue.Text = "0";
            prizePercentageValue.Text = "0";
        }

        private bool ValidateForm()
        {
            bool output = true;
            int placeNumber = 0;
            bool placeNumberValidNumber = int.TryParse(placeNumberValue.Text, out placeNumber);

            decimal prizeAmount = 0;
            bool prizeAmountValid = decimal.TryParse(prizeAmountValue.Text, out prizeAmount);

            double prizePercentage = 0;
            bool prizePercentageValid = double.TryParse(prizePercentageValue.Text, out prizePercentage);

            if (placeNumberValidNumber == false)
            {
                MessageBox.Show("Place Number is not true!" +
                    " Please check the values entered!");
                output = false;
            }

            if (placeNumber < 1)
            {
                MessageBox.Show("Place Number is not valid!" +
                     " Please check the values entered!");
                output = false;
            }

            if (placeNameValue.Text.Length == 0)
            {
                MessageBox.Show("Place Name is not valid!" +
                    " Please check the values entered!");
                output = false;
            }

            if (prizeAmountValid == false || prizePercentageValid == false)
            {
                MessageBox.Show("Prize Amount or Prize % is not valid!" +
                    " Please check the values entered!");
                output = false;
            }

            if (prizeAmount < 0 && prizePercentage < 0)
            {
                MessageBox.Show("Prize Amount and Prize % is not valid!" +
                    " Please check the values entered!");
                output = false;
            }

            if (prizePercentage < 0 && prizePercentage < 100)
            {
                MessageBox.Show("Prize Percentage is less than 0 or greater than 100!" +
                    " Please check the values entered!");
                output = false;
            }


            return output;
        }
    }
}