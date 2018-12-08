using SalamiTV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalamiTV.ViewModels
{
    public class SearchProgramVM
    {
        public List<TvChannel> GetTvChannels { get; set; }
        public List<TvChannel> SetTvChannels { get; set; }
        public List<TvProgram> HighlightedProgram { get; set; }

        public List<string> Dates { get; set; }
        public int? Page { get; set; }
        public int? ID { get; set; }
        public TvChannel TvChannel { get; set; }


        public void RenderToUserTablau(List<TvChannel> tvChannels, List<UserTablau> userTablaus)
        {
            foreach (var channel in tvChannels)
            {
                foreach (var tablau in userTablaus)
                {
                    if (channel.ID == tablau.TvChannelID)
                    {
                        GetTvChannels.Add(channel);
                        break;
                    }
                }
            }

        }

        public SearchProgramVM()
        {
            ReturnDateStrings();
            GetTvChannels = new List<TvChannel>();
            SetTvChannels = new List<TvChannel>();
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