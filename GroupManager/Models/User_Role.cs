namespace GroupManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_Role
    {
        public int ID { get; set; }

        public int? User_ID { get; set; }

        public int? Role_ID { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }
}
