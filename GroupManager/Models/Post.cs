namespace GroupManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        public int ID { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Created_Date { get; set; }

        public int? Created_By { get; set; }

        public int? Category_ID { get; set; }

        public int? Group_ID { get; set; }

        public bool? Stt { get; set; }

        public virtual Group Group { get; set; }

        public virtual PostCategory PostCategory { get; set; }

        public virtual User User { get; set; }
    }
}
