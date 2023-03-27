namespace EFProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Permissions_Items
    {
        public int Id { get; set; }

        public int Perm_Id { get; set; }

        public int Item_Code { get; set; }

        public int Item_Count { get; set; }

        public virtual Item Item { get; set; }

        public virtual Permission Permission { get; set; }
    }
}
