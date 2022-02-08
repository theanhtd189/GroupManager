namespace GroupManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_Group
    {
        public int ID { get; set; }

        public int? User_ID { get; set; }

        public int? Group_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Created_Date { get; set; }

        public virtual Group Group { get; set; }

        public virtual User User { get; set; }
    }
}
