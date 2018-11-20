using SalamiTV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalamiTV.ViewModels
{
    public class UserTablauViewModel
    {
        public string AspNetUsersID { get; set; }
        public string TvChannelName { get; set; }
        public SelectList TvChannels { get; set; }
        public SelectList TvPrograms { get; set; }
        public string TvProgramName { get; set; }
        //public string Category { get; set; }
        public UserTablau UserTablau { get; set; }
        public string Title { get; set; }

        public string Details { get; set; }
        public DateTime Broadcasting { get; set; }

        public int Duration { get; set; }


    }
}