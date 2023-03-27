namespace EFProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer
    {
        [Key]
        public int Cust_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Cust_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string Fax { get; set; }

        [Required]
        [StringLength(50)]
        public string EMail { get; set; }
    }
}
