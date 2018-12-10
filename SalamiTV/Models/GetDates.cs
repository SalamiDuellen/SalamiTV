using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalamiTV.Models
{
    public class GetDates
    {
        public GetDates()
        {
            ReturnDateStrings();
        }
        public List<string> Dates { get; set; }


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