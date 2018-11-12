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

        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Details { get; set; }

        public DateTime Broadcasting { get; set; }

        public int Duration { get; set; }

        public int TvChannelID { get; set; }

        public virtual TvChannel TvChannel { get; set; }

        //public string StartTime
        //{
        //    get => Broadcasting.TimeOfDay.ToString().Remove(5);
        //    set
        //    {
        //        Broadcasting.TimeOfDay.ToString().Remove(5);
        //    }
        //}
        //public string EndTime
        //{
        //    get { return Broadcasting.AddMinutes(Duration).TimeOfDay.ToString().Remove(5); }
        //    set
        //    {
        //        Broadcasting.AddMinutes(Duration).TimeOfDay.ToString().Remove(5);
        //    }
        //}

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TvProgramCategory> TvProgramCategories { get; set; }
    }
}
