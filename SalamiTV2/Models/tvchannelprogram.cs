namespace SalamiTV2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tvchannelprogram")]
    public partial class tvchannelprogram
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int tvchannelid { get; set; }

        public int tvprogramid { get; set; }

        public virtual tvchannel tvchannel { get; set; }

        public virtual tvprogram tvprogram { get; set; }
    }
}
