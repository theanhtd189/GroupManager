namespace GroupManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LessonComment")]
    public partial class LessonComment
    {
        public int ID { get; set; }

        public int? Lesson_ID { get; set; }

        public string Content { get; set; }

        public int? Created_By { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Created_Date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Modified_Date { get; set; }

        public int? Parrent_ID { get; set; }

        public bool? Stt { get; set; }

        public virtual User User { get; set; }
    }
}
