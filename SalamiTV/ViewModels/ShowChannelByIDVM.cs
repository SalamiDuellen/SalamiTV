using SalamiTV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalamiTV.ViewModels
{
    public class ShowChannelByIDVM
    {
        public ShowChannelByIDVM()
        {
            GetDates = new GetDates();
        }

        public int Page { get; set; }
        public int TvChannelID { get; set; }
        public TvChannel TvChannel { get; set; }
        public string Name { get; set; }

        public GetDates GetDates { get; set; }

    }
}