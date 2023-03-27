namespace EFProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Measurement")]
    public partial class Measurement
    {
        public int Id { get; set; }

        public int Item_Code { get; set; }

        [Required]
        [StringLength(50)]
        public string measure_Name { get; set; }

        public virtual Item Item { get; set; }
    }
}
