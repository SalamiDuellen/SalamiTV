namespace SalamiTV.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TvProgram")]
    public partial class TvProgram
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TvProgram()
        {
            TvProgramCategories = new HashSet<TvProgramCategory>();
        }

        DateTime _dateTime;
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Details { get; set; }

        public DateTime Broadcasting { get; set; }

        public DateTime EndTime
        {
            get => _dateTime;
            set
            {
                _dateTime = Broadcasting.AddMinutes(Duration);
            }
        }

        public int Duration { get; set; }

        public int TvChannelID { get; set; }
        public bool? IsInFocus { get; set; }
        public virtual TvChannel TvChannel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TvProgramCategory> TvProgramCategories { get; set; }

        public override string ToString()
        {
            return Broadcasting.ToString();
        }

    }
}
