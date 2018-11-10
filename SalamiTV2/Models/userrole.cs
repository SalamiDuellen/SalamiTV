namespace SalamiTV2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserRole")]
    public partial class UserRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int UserID { get; set; }

        public int RoleID { get; set; }

        public virtual Role Role { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}
