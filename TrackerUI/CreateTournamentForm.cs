﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TrackerLibrary.Model;
using TrackerLibrary;

namespace TrackerUI
{
    public partial class CreateTournamentForm : 
                            Form, 
                            IPrizeRequester,
                            ITeamRequester

    {
        List<TeamModel> availableTeams = GlobalConfig.Connection.GetTeam_All();
        List<TeamModel> selectedTeams = new List<TeamModel>();
        List<PrizeModel> selectedPrizes = new List<PrizeModel>();
        List<PrizeModel> availablePrizeModels = new List<PrizeModel>();

        /// <summary>
        /// Creates the Tournament form method
        /// </summary>
        public CreateTournamentForm()
        {
            InitializeComponent();
            WireUpLists();
        }

        /// <summary>
        /// Loads all the teams into the selectTeamDropDown element
        /// </summary>
        private void WireUpLists()
        {

            selectTeamDropDown.DataSource = null;
            selectTeamDropDown.DataSource = availableTeams;
            selectTeamDropDown.DisplayMember = "TeamName";

            tournamentTeamsListBox.DataSource = null;
            tournamentTeamsListBox.DataSource = selectedTeams;
            tournamentTeamsListBox.DisplayMember = "TeamName";

            prizesListBox.DataSource = null;
            prizesListBox.DataSource = selectedPrizes;
            prizesListBox.DisplayMember = "PlaceName";
        }

        /// <summary>
        /// Moves selected team to listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel team = (TeamModel)selectTeamDropDown.SelectedItem;

            if(team != null)
            {
                availableTeams.Remove(team);
                selectedTeams.Add(team);
                WireUpLists();
            }
        }

        /// <summary>
        /// Brings up prize form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            
            CreatePrizeForm prizeForm = new CreatePrizeForm(this);
            prizeForm.Show();

        }

        /// <summary>
        /// Wires up tournament form with prize
        /// </summary>
        /// <param name="prizeModel"></param>
        public void PrizeComplete(PrizeModel prizeModel)
        {
            selectedPrizes.Add(prizeModel);
            WireUpLists();

        }

        /// <summary>
        /// Wires up tournament form with team
        /// </summary>
        /// <param name="teamModel"></param>
        public void TeamComplete(TeamModel teamModel)
        {
            selectedTeams.Add(teamModel);
            WireUpLists();
        }

        /// <summary>
        /// Link to create new CreateTeamForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createNewTeamLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm teamForm = new CreateTeamForm(this);
            teamForm.Show();
        }

        /// <summary>
        /// removes the SELECTED index'd team from list box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeSelectedTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel team = (TeamModel)tournamentTeamsListBox.SelectedItem;
            if(team != null)
            {
                selectedTeams.Remove(team);
                availableTeams.Add(team);
                WireUpLists();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removePrizeButton_Click(object sender, EventArgs e)
        {
            PrizeModel prize = (PrizeModel)prizesListBox.SelectedItem;
            if(prize != null)
            {
                selectedPrizes.Remove(prize);
                //availablePrizeModels.Add(prize);
                WireUpLists();
            }
        }

        /// <summary>
        /// Button for creating tournament models
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            decimal entryFee;

            bool feeAcceptable = decimal.TryParse(entryFeeValue.Text, out entryFee);
            if (!feeAcceptable)
            {
                MessageBox.Show("Please enter a valid Fee.", "Invalid Fee",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }
            
            // Creates tournamentModel
            TournamentModel tournamentModel = new TournamentModel
            {
                TournamentName = tournamentNameValue.Text,
                EntryFee = entryFee,
                Prizes = selectedPrizes,
                EnteredTeams = selectedTeams
            };
            // TODO - Wireup

            TournamentLogic.CreateRounds(tournamentModel);


            GlobalConfig.Connection.CreateTournament(tournamentModel);

        }
    }
}
