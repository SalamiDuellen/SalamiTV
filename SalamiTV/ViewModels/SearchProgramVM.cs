using SalamiTV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalamiTV.ViewModels
{
    public class SearchProgramVM
    {
        public List<TvChannel> TvChannels { get; set; }
        public List<TvProgram> HighlightedProgram { get; set; }

        public List<string> Dates { get; set; }


        public List<TvChannel> RenderToUserTablau(List<TvChannel> tvChannels, List<UserTablau> userTablaus)
        {
            List<TvChannel> channels = new List<TvChannel>();
            foreach (var channel in tvChannels)
            {
                foreach (var tablau in userTablaus)
                {
                    if (channel.ID == tablau.TvChannelID)
                    {
                        channels.Add(channel);
                        break;
                    }
                }
            }
            return channels;

        }

        public SearchProgramVM()
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