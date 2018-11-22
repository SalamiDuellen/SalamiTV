namespace SalamiTV
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

        public int? TvChannelID { get; set; }

        [StringLength(128)]
        public string AspNetUsersID { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
