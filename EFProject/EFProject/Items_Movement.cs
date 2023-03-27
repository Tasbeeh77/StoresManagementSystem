namespace EFProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Items_Movement
    {
        public int Id { get; set; }

        public int FromStore_Id { get; set; }

        public int ToStore_Id { get; set; }

        public int Item_Code { get; set; }

        public int Item_Count { get; set; }

        public int Sup_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Prod_Date { get; set; }

        [Column(TypeName = "date")]
        public DateTime Validity_Period { get; set; }
        [Column(TypeName = "date")]
        public DateTime transfer_Date { get; set; }

        public virtual Item Item { get; set; }

        public virtual Store Store { get; set; }

        public virtual Store Store1 { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
