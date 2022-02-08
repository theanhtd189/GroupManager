namespace GroupManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GroupCategory")]
    public partial class GroupCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GroupCategory()
        {
            Groups = new HashSet<Group>();
        }

        public int ID { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Created_Date { get; set; }

        public int? Created_By { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Group> Groups { get; set; }

        public virtual User User { get; set; }
    }
}
