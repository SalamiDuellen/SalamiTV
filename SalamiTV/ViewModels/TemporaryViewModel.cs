using SalamiTV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalamiTV.ViewModels
{
    public class TemporaryViewModel
    {
        public int Page { get; set; } = 0;
        public IEnumerable<TvChannel> TvChannels { get; set; }
        public IEnumerable<TvProgram> InFocusPrograms { get; set; }
    }
}