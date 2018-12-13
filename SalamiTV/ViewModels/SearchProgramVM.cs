using SalamiTV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalamiTV.ViewModels
{
    public class SearchProgramVM
    {
        bool _showAll;
        public List<TvChannel> TvChannels { get; set; }
        public List<TvProgram> InFocusPrograms { get; set; }

        //public List<string> Dates { get; set; }
        public int Page { get; set; }
        public string AspNetUserID { get; set; }
        public GetDates GetDates { get; set; }
        public bool ShowAll
        { get=>_showAll;
            set
            {
                _showAll = value;
            }
        }


        public SearchProgramVM()
        {
            GetDates = new GetDates();
            TvChannels = new List<TvChannel>();
        }

    }
}