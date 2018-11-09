namespace SalamiTV2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("userrole")]
    public partial class userrole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int userid { get; set; }

        public int roleid { get; set; }

        public virtual role role { get; set; }

        public virtual user user { get; set; }
    }
}
