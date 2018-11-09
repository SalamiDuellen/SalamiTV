namespace SalamiTV2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TvChannelProgram")]
    public partial class TvChannelProgram
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int ProgramID { get; set; }

        public int TvProgramID { get; set; }

        public virtual TvChannel TvChannel { get; set; }

        public virtual TvProgram TvProgram { get; set; }
    }
}
