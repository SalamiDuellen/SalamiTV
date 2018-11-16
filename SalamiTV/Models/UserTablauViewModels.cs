using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalamiTV.Models
{
    public class UserTablauViewModel
    {
        public virtual ICollection<TvProgram> TvPrograms { get; set; }
        public virtual ICollection<TvChannel> TvChannels { get; set; }

        public int ID { get; set; }

        public int TvChannelID { get; set; }
        public string Name { get; set; }

        public string AspNetUsersId { get; set; }
        public string UserName { get; set; }

    }
}