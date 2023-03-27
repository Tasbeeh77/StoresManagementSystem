﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EFProject
{
    public partial class ReportsDialog : Form
    {
        #region Fields
        EFEntity Ent = new EFEntity();
        List<string> stores = new List<string>();
        List<showAllData> data = new List<showAllData>();
        List<report2> items= new List<report2>();
        IQueryable<Items_Movement> transfered;
        List<int> fromstores = new List<int>();
        List<int> tostores = new List<int>();
        List<int> productsId = new List<int>();
        List<Items_Movement> items_s = new List<Items_Movement>();
        int  productId;
        Item product;
        string measurement="";
        int storeid;
        #endregion

        #region Report 1
        private void button1_Click(object sender, EventArgs e)
        {
            data.Clear();
            dataGridView1.DataSource = null;
            if (checkBox1.Checked)
            {
                var storeid = (from store in Ent.Stores where store.Store_Name == comboBox1.Text select store).FirstOrDefault();
                var storeitems = from st in Ent.Store_Items where st.Store_Id == storeid.Store_Id select st; 
                foreach (var item in storeitems) 
                {
                    var itemdata = (from i in Ent.Items where i.Item_Code == item.Item_Code select i).FirstOrDefault();
					var permid = (from d in Ent.Permissions_Items where (d.Item_Code == itemdata.Item_Code) select d.Perm_Id).FirstOrDefault();
					var permdate = (from p in Ent.Permissions where (p.Perm_Type == "Import" && p.Store_Id == storeid.Store_Id && p.Perm_Id == permid) select p.Perm_Date).FirstOrDefault();
					showAllData allData = new showAllData();
                    allData.Store_Name= comboBox1.Text;
                    allData.Store_Id= storeid.Store_Id;
                    allData.Address = storeid.Address;
                    allData.Manager_Name= storeid.Manager_Name;
                    allData.Item_Name = itemdata.Item_Name;
                    allData.Item_Total_Count = item.Item_Total_Count;
                    allData.Prod_Date = itemdata.Prod_Date;
                    allData.Expire_Date = itemdata.Validity_Period;
					allData.Perm_Date = permdate;
					data.Add(allData);
                }
            }
            else
            {
                var dt1 = dateTimePicker1.Value;
                var dt2 = dateTimePicker2.Value;
                var storeid = (from store in Ent.Stores where store.Store_Name == comboBox1.Text select store).FirstOrDefault();
                var storeitems = from st in Ent.Store_Items where st.Store_Id == storeid.Store_Id select st;
                foreach (var item in storeitems)
                {
                    var itemdata = (from i in Ent.Items where i.Item_Code == item.Item_Code select i).FirstOrDefault();
                    var permid =(from d in Ent.Permissions_Items where (d.Item_Code == itemdata.Item_Code) select d.Perm_Id).FirstOrDefault();
                    var permdate = (from p in Ent.Permissions where (p.Perm_Type=="Import" && p.Store_Id == storeid.Store_Id && p.Perm_Id == permid)select p.Perm_Date).FirstOrDefault();
                    if(permdate > dt1 && permdate < dt2)
                    {
                        showAllData allData = new showAllData();
                        allData.Store_Name = comboBox1.Text;
                        allData.Store_Id = storeid.Store_Id;
                        allData.Address = storeid.Address;
                        allData.Manager_Name = storeid.Manager_Name;
                        allData.Item_Name = itemdata.Item_Name;
                        allData.Item_Total_Count = item.Item_Total_Count;
                        allData.Prod_Date = itemdata.Prod_Date;
                        allData.Expire_Date = itemdata.Validity_Period;
                        allData.Perm_Date = permdate;
                        data.Add(allData);
                    }
                }
            }
             dataGridView1.DataSource = data;
        }
		#endregion

		#region Report 2
		private void button2_Click(object sender, EventArgs e)
		{
            items.Clear();
            dataGridView2.DataSource = null;
            var productname =comboBox2.Text;
			if (checkBox2.Checked)
            {
                foreach (var item in stores)
                {
					var storeid = (from s in Ent.Stores where s.Store_Name == item select s.Store_Id).FirstOrDefault();
                    var measure = from m in Ent.Measurements where m.Item_Code == product.Item_Code select m;
					var total_count = (from s in Ent.Store_Items where (s.Item_Code == product.Item_Code && s.Store_Id == storeid) select s.Item_Total_Count).FirstOrDefault();
					foreach (var item1 in measure)
                    {
                        measurement += item1.measure_Name+" | ";
                    }
                    report2 report = new report2();
                    report.prodId = product.Item_Code;
                    report.prodName = productname;
                    report.Store_Name = item;
                    report.Store_Id = storeid;
                    report.Prod_Date = product.Prod_Date;
                    report.Expire_Date = product.Validity_Period;
                    report.Measurements = measurement;
                    report.Item_Total_Count = total_count;
                    items.Add(report);
				}
            }
            else
            {
				var dt1 = dateTimePicker3.Value;
				var dt2 = dateTimePicker4.Value;
				foreach (var item in stores)
				{
					var storeid = (from s in Ent.Stores where s.Store_Name == item select s.Store_Id).FirstOrDefault();
					var measure = from m in Ent.Measurements where m.Item_Code == product.Item_Code select m;
					var total_count = (from s in Ent.Store_Items where (s.Item_Code == product.Item_Code && s.Store_Id == storeid) select s.Item_Total_Count).FirstOrDefault();
					var permid = (from d in Ent.Permissions_Items where (d.Item_Code == product.Item_Code) select d.Perm_Id).FirstOrDefault();
					var permdate = (from p in Ent.Permissions where (p.Perm_Type == "Import" && p.Store_Id == storeid && p.Perm_Id == permid) select p.Perm_Date).FirstOrDefault();
					foreach (var item1 in measure)
					{
						measurement += item1.measure_Name + " | ";
					}
					if (permdate > dt1 && permdate < dt2)
					{
					    report2 report = new report2();
					    report.prodId = product.Item_Code;
					    report.prodName = productname;
					    report.Store_Name = item;
					    report.Store_Id = storeid;
					    report.Prod_Date = product.Prod_Date;
					    report.Expire_Date = product.Validity_Period;
					    report.Measurements = measurement;
					    report.Item_Total_Count = total_count;
					    items.Add(report);
					}
				}
			}
			dataGridView2.DataSource = items;
			stores.Clear();
        }

        #endregion

        #region Report 3
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            comboBox6.Items.Clear();
            productId = (from p in Ent.Items where p.Item_Name == comboBox5.Text select p.Item_Code).FirstOrDefault();
            transfered = from t in Ent.Items_Movement where t.Item_Code == productId select t;
            foreach (var item in transfered)
            {
                var fromstoreName = (from s in Ent.Stores where s.Store_Id == item.FromStore_Id select s.Store_Name).FirstOrDefault();
                comboBox6.Items.Add(fromstoreName);
                var tostoreName = (from s in Ent.Stores where s.Store_Id == item.ToStore_Id select s.Store_Name).FirstOrDefault();
                comboBox4.Items.Add(tostoreName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            items_s.Clear();
            dataGridView3.DataSource = null;
            if (checkBox3.Checked)
            {
                dataGridView3.DataSource = transfered.ToList();
            }
            else
            {
                for (int i = 0; i < fromstores.Count; i++)
                {
                    var frm = fromstores[i];
                    var to = tostores[i];
                    var x = (from s in Ent.Items_Movement
                             where (s.FromStore_Id == frm &&
                                    s.ToStore_Id == to &&
                                    s.Item_Code == productId)
                             select s).FirstOrDefault();
                    if (x.transfer_Date > dateTimePicker5.Value && x.transfer_Date < dateTimePicker6.Value)
                        items_s.Add(x);
                }
                dataGridView3.DataSource = items_s;
            }
        }
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            var fromstoreid = (from s in Ent.Stores where s.Store_Name == comboBox6.Text select s.Store_Id).FirstOrDefault();
            fromstores.Add(fromstoreid);
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tostoreid = (from s in Ent.Stores where s.Store_Name == comboBox4.Text select s.Store_Id).FirstOrDefault();
            tostores.Add(tostoreid);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            fromstores.Clear();
            tostores.Clear();
        }
        #endregion

        #region Report 4
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            storeid = (from s in Ent.Stores where s.Store_Name == comboBox7.Text select s.Store_Id).FirstOrDefault();
            var items = from i in Ent.Store_Items where i.Store_Id == storeid select i;
            foreach (var item in items)
            {
                var prodName = (from p in Ent.Items where p.Item_Code == item.Item_Code select p.Item_Name).FirstOrDefault();
                comboBox8.Items.Add(prodName);
            }
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Add(comboBox8.Text);
            int prodid = (from p in Ent.Items where p.Item_Name == comboBox8.Text select p.Item_Code).FirstOrDefault();
            productsId.Add(prodid);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            foreach (var item in productsId)
            {
                var permid = (from d in Ent.Permissions_Items where (d.Item_Code == item) select d.Perm_Id).FirstOrDefault();
                var permdate = (from p in Ent.Permissions where (p.Perm_Type == "Import" && p.Store_Id == storeid && p.Perm_Id == permid) select p.Perm_Date).FirstOrDefault();
                if(true)
                {

                }
            }
            productsId.Clear();
        }
        #endregion
        public ReportsDialog()
        {
            InitializeComponent();
        }
        private void ReportsDialog_Load(object sender, EventArgs e)
        {
            foreach (var item in Ent.Stores)
            {
                comboBox1.Items.Add(item.Store_Name);
                comboBox7.Items.Add(item.Store_Name);
            }

            foreach (var item in Ent.Items)
			{
				comboBox2.Items.Add(item.Item_Name);
            }
            var products = (from p in Ent.Items_Movement select p.Item_Code).ToHashSet();
            foreach (var item in products)
            {
                var Prodname = Ent.Items.Find(item).Item_Name;
                comboBox5.Items.Add(Prodname);
            }
        }
		private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
		{
            stores.Add(comboBox3.Text);
		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
            comboBox3.Items.Clear();    
            product = (from p in Ent.Items where p.Item_Name == comboBox2.Text select p).FirstOrDefault();
            var storesid = from s in Ent.Store_Items where s.Item_Code == product.Item_Code select s;
            foreach (var item in storesid)
            {
                var storeName = (from s in Ent.Stores where s.Store_Id == item.Store_Id select s.Store_Name).FirstOrDefault();
                comboBox3.Items.Add(storeName);
			}
		}

    }
}