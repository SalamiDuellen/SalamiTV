using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalamiTV.Models
{
    public class Contact
    {

        [Display(Name = "Namn")]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-post")]
        [Required(ErrorMessage = "{0} är obligatoriskt")]
        [EmailAddress(ErrorMessage = "Ogiltig e-post")]
        public string Email { get; set; }
        [Display(Name = "Ämne")]
        public string Subject { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Meddelande")]
        [Required(ErrorMessage = "Meddelande är obligatoriskt")]
        public string Message { get; set; }
    }
}