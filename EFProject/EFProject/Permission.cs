namespace EFProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Permission
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Permission()
        {
            Permissions_Items = new HashSet<Permissions_Items>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Perm_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Perm_Type { get; set; }

        public int Store_Id { get; set; }

        public int Sup_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Perm_Date { get; set; }

        public virtual Store Store { get; set; }

        public virtual Supplier Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Permissions_Items> Permissions_Items { get; set; }
    }
}
