﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Model;

namespace TrackerUI
{
    public interface ITeamRequester
    {
        void TeamComplete(TeamModel teamModel);
    }
}
