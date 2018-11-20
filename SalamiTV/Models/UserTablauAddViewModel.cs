using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalamiTV.Models
{
    public class UserTablauAddViewModel
    {
        public int TvChannelID { get; set; }

        public string AspNetUsersId { get; set; }
        public string TvChannelName { get; set; }
        public SelectList AvalibleTvChannels { get; set; }
        //public UserTablau UserTablau { get; set; }

    }
}