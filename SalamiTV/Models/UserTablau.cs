namespace SalamiTV.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserTablau")]
    public partial class UserTablau
    {
        public int ID { get; set; }

        public int TvChannelID { get; set; }

        public string AspNetUsersId { get; set; }
        public virtual TvChannel TvChannel { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}
