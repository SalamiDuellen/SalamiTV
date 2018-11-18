﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalamiTV.Models
{
    public class UserTablauAddViewModel
    {
        public int ID { get; set; }

        public int TvChannelID { get; set; }

        public string AspNetUsersId { get; set; }
        public string Name { get; set; }
        public IEnumerable<TvChannel> TvChannels { get; set; }
    }
}