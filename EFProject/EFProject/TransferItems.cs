using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EFProject
{
    public partial class TransferItems : Form
    {
        EFEntity Ent = new EFEntity();
        int storeId;
        List<Item> items1 = new List<Item>();
        Item Products;
        public TransferItems()
        {
            InitializeComponent();
        }
        private void TransferItems_Load(object sender, EventArgs e)
        {
            foreach (var item in Ent.Stores)
            {
                comboBox1.Items.Add(item.Store_Name);
                comboBox2.Items.Add(item.Store_Name);
            }
            foreach (var item in Ent.Suppliers)
            {
                comboBox4.Items.Add(item.Sup_Name);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            storeId = (from s in Ent.Stores where s.Store_Name == comboBox1.Text select s.Store_Id).FirstOrDefault();
            var itemsIds = from i in Ent.Store_Items where i.Store_Id == storeId select i;
            foreach (var item in itemsIds)
            {
                string itemsNames = (from i in Ent.Items where i.Item_Code == item.Item_Code select i.Item_Name).FirstOrDefault();
                comboBox3.Items.Add(itemsNames);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Products = (from pd in Ent.Items where pd.Item_Name == comboBox3.Text select pd).FirstOrDefault();
            textBox1.Text = Products?.Item_Name;
            var count = (from c in Ent.Store_Items where (c.Store_Id == storeId && c.Item_Code == Products.Item_Code) select c).FirstOrDefault();
            textBox2.Text = count.Item_Total_Count.ToString();
        }

        #region Transfer Items
        private void button1_Click(object sender, EventArgs e)
        {
            Items_Movement transfer = new Items_Movement();
            int tCount = int.Parse(textBox3.Text);
            transfer.FromStore_Id = storeId;
            transfer.ToStore_Id = (from t in Ent.Stores where t.Store_Name == comboBox2.Text select t.Store_Id).FirstOrDefault();
            transfer.Item_Code = Products.Item_Code;
            transfer.Item_Count = tCount;
            transfer.Sup_Id = (from s in Ent.Suppliers where s.Sup_Name == comboBox4.Text select s.Sup_Id).FirstOrDefault();
            transfer.Prod_Date = Products.Prod_Date;
            transfer.Validity_Period = Products.Validity_Period;
            transfer.transfer_Date = DateTime.Now;
            var checkTotalCount  = (from ch in Ent.Store_Items where (ch.Store_Id == storeId && ch.Item_Code == Products.Item_Code) select ch).FirstOrDefault();
            var checkItemExist = (from ch in Ent.Store_Items where(ch.Store_Id == transfer.ToStore_Id && ch.Item_Code == Products.Item_Code ) select ch).FirstOrDefault();
            if(checkTotalCount.Item_Total_Count < tCount)
            {
                MessageBox.Show("The amount is large please enter smaller quantity");
            }
            else
            {
                if(checkItemExist!= null)
                {
                    if (checkTotalCount.Item_Total_Count == tCount)
                    {
                        checkItemExist.Item_Total_Count += tCount;
                        Ent.Store_Items.Remove(checkTotalCount);
                    }
                    else
                    {
                        checkItemExist.Item_Total_Count += tCount;
                        checkTotalCount.Item_Total_Count -= tCount;
                    }
                }
                else
                {
                    if (checkTotalCount.Item_Total_Count == tCount)
                    {
                        Store_Items _Items = new Store_Items();
                        _Items.Store_Id = transfer.ToStore_Id;
                        _Items.Item_Code = Products.Item_Code;
                        _Items.Item_Total_Count = tCount;
                        Ent.Store_Items.Add(_Items); 
                        Ent.Store_Items.Remove(checkTotalCount);
                    }
                    else
                    {
                        checkTotalCount.Item_Total_Count -= tCount;
                        Store_Items _Items = new Store_Items();
                        _Items.Store_Id = transfer.ToStore_Id;
                        _Items.Item_Code = Products.Item_Code;
                        _Items.Item_Total_Count = tCount;
                        Ent.Store_Items.Add( _Items );
                    }
                }
                Ent.Items_Movement.Add(transfer);
            }
            Ent.SaveChanges();
            MessageBox.Show("transferred successfully");
        }
        #endregion
    }
}

