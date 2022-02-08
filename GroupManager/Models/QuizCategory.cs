namespace GroupManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuizCategory")]
    public partial class QuizCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuizCategory()
        {
            Quizs = new HashSet<Quiz>();
        }

        public int ID { get; set; }

        [StringLength(200)]
        public string Desc { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Created_Date { get; set; }

        public int? Created_By { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quiz> Quizs { get; set; }

        public virtual User User { get; set; }
    }
}
