using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalamiTV.Models
{
	public class TablauViewModel
	{
        public string Title { get; set; }

        public string Details { get; set; }

        public DateTime Broadcasting { get; set; }

        public int Duration { get; set; }

        public string StartTime
        {
            get => Broadcasting.TimeOfDay.ToString().Remove(5);
            set
            {
                Broadcasting.TimeOfDay.ToString().Remove(5);
            }
        }
        public string EndTime
        {
            get { return Broadcasting.AddMinutes(Duration).TimeOfDay.ToString().Remove(5); }
            set
            {
                Broadcasting.AddMinutes(Duration).TimeOfDay.ToString().Remove(5);
            }
        }

        public string Name { get; set; }

    }
}