﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFProject
{
    public class Itemreports
    {
        public int prodId { get; set; }
        public string prodName { get; set; }
        public int? Store_Id { get; set; }
        public string Store_Name { get; set; }
        public int Item_Total_Count { get; set; }
        public DateTime Prod_Date { get; set; }
        public DateTime Expire_Date { get; set; }
        public DateTime? Permission_Date { get; set; }

    }
}
