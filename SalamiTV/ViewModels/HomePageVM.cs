using SalamiTV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalamiTV.ViewModels
{
    public class HomePageVM
    {
        public List<TvChannel> TvChannels { get; set; }
        public List<string> Dates { get; set; }

        public HomePageVM()
        {
            ReturnDateStrings();
        }

        void ReturnDateStrings()
        {
            Dates = new List<string>();
            for (int i = 0; i < 6; i++)
            {
                var dates = DateTime.Today.Date.AddDays(i).ToShortDateString();
                Dates.Add(dates);

            }
        }
    }
}