namespace SalamiTV
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TvProgramCategory")]
    public partial class TvProgramCategory
    {
        public int ID { get; set; }

        public int TvProgramID { get; set; }

        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }

        public virtual TvProgram TvProgram { get; set; }
    }
}
