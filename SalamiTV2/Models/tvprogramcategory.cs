namespace SalamiTV2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tvprogramcategory")]
    public partial class tvprogramcategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int tvprogramid { get; set; }

        public int categoryid { get; set; }

        public virtual category category { get; set; }

        public virtual tvprogram tvprogram { get; set; }
    }
}
