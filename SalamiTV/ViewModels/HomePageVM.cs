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
        public List<TvProgram> HighlightedProgram { get; set; }

        public List<string> Dates { get; set; }

        public void RenderToUserTablau(List<TvChannel> tvChannels, List<UserTablau> userTablaus)
        {
            foreach (var channel in tvChannels)
            {
                foreach (var tablau in userTablaus)
                {
                    if (channel.ID == tablau.TvChannelID)
                    {
                        TvChannels.Add(channel);
                        break;
                    }
                }
            }

        }

        private void SetSearchDates(int? page)
        {
            //Sätter pagenumber till 0 om värdet är null
            int pageNumber = (page ?? 0);
            var searchDate = DateTime.Now.AddDays(pageNumber);
            if (pageNumber != 0)
            {
                searchDate = DateTime.Now.AddDays(pageNumber).Date;
            }
            var tomorrow = searchDate.AddDays(1).Date;
        }

        public HomePageVM()
        {
            ReturnDateStrings();
            TvChannels = new List<TvChannel>();
        }

        void ReturnDateStrings()
        {
            Dates = new List<string>();
            for (int i = 0; i < 7; i++)
            {
                var dates = DateTime.Today.Date.AddDays(i).ToShortDateString();
                Dates.Add(dates);

            }
        }
    }
}