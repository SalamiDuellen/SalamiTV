using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalamiTV.Models
{
    public class TablauViewModels
    {
        [Required]
        [StringLength(255)]
        public string TvChannelName { get; set; }

        [Required]
        [StringLength(255)]
        public string ProgramTitle { get; set; }

        [Required]
        [StringLength(255)]
        public string ProgramDetails { get; set; }
    }
}