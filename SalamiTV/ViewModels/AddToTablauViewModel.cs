using SalamiTV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalamiTV.ViewModels
{
    public class AddToTablauViewModel
    {
        public string AspNetUsersID { get; set; }
        public int? TvChannelID { get; set; }
        public SelectList MyChannels { get; set; }
        public SelectList AvailibleCannels { get; set; }
        //public string Category { get; set; }
        //public UserTablau UserTablau { get; set; }

    }
}