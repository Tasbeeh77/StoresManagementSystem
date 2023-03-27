using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFProject
{
    public class showAllData
    {
        public int Store_Id { get; set; }
        public string Store_Name { get; set; }
        public string Address { get; set; }
        public string Manager_Name { get; set; }
        public string Item_Name { get; set; }
        public int Item_Total_Count { get; set; }
        public DateTime Prod_Date { get; set; }
        public DateTime Expire_Date { get; set; }
		public DateTime Perm_Date { get; set; }

	}
}
