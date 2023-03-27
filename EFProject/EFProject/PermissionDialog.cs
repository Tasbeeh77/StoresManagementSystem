using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EFProject
{
    public partial class PermissionDialog : Form
    {
        EFEntity Ent = new EFEntity();
        Item Products;
        string permType;
        List<Item> items1= new List<Item>();
        public PermissionDialog()
        {
            InitializeComponent();
        }

        private void PermissionDialog_Load(object sender, EventArgs e)
        {
            foreach (var item in Ent.Items)
            {
                comboBox1.Items.Add(item.Item_Name);
            }
            foreach (var item in Ent.Stores)
            {
                comboBox2.Items.Add(item.Store_Name);
            }
            foreach (var item in Ent.Suppliers)
            {
                comboBox3.Items.Add(item.Sup_Name);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Products = (from pd in Ent.Items where pd.Item_Name == comboBox1.Text select pd).FirstOrDefault();
            textBox1.Text = Products?.Item_Code.ToString();
            textBox2.Text = Products?.Item_Name;
            dateTimePicker2.Value = Products.Prod_Date;
            dateTimePicker3.Value = Products.Validity_Period;
        }

        void setpermissionType()
        {
            if (radioButton1.Checked == true)
            {
                permType = radioButton1.Text;
            }
            else
            {
                permType = radioButton2.Text;
            }
        }

        #region Choose items
        private void button3_Click(object sender, EventArgs e)
        {            
            listBox1.Items.Add("\t"+textBox2.Text + "\t" + textBox5.Text+"\t"+Products.total_Count.ToString());
            Item item = new Item();
            item.Prod_Date = dateTimePicker2.Value;
            item.Validity_Period = dateTimePicker3.Value;
            item.total_Count = int.Parse(textBox5.Text);
            item.Item_Code = int.Parse(textBox1.Text);
            item.Item_Name = textBox2.Text;
            items1.Add(item);
        }
        #endregion

        #region Add Permission
        private void button1_Click(object sender, EventArgs e, DateTimePicker dateTimePicker1)
        {
            setpermissionType();
            //adding to permission table
            Permission perm = new Permission();
            perm.Perm_Id = int.Parse(textBox6.Text);
            perm.Perm_Type = permType;
            var storeid = (from s in Ent.Stores where s.Store_Name == comboBox2.Text select s).FirstOrDefault();
            perm.Store_Id = storeid.Store_Id;
            var supid = (from s in Ent.Suppliers where s.Sup_Name == comboBox3.Text select s).FirstOrDefault();
            perm.Sup_ID= supid.Sup_Id;
            perm.Perm_Date = dateTimePicker1.Value;
            Ent.Permissions.Add(perm);
            //adding to items, permission_items and store_items tables
            foreach (var item in items1)
            {
                var checkItemExist = (from i in Ent.Items where i.Item_Code == item.Item_Code select i).FirstOrDefault();
                var checkStoreItemExist = (from i in Ent.Store_Items where (i.Item_Code == item.Item_Code && i.Store_Id == storeid.Store_Id) select i).FirstOrDefault();
                if (permType == "Import")
                {
                    if (checkItemExist == null)
                    {
                        IEnumerable<EFEntity> add = Ent.Database.SqlQuery<EFEntity>($"exec Insert_Item {item.Item_Code}, '{item.Item_Name}', '{item.Prod_Date}', '{item.Validity_Period}',{item.total_Count}").ToList();
                    }
                    else
                    {
                        IEnumerable<EFEntity> edit = Ent.Database.SqlQuery<EFEntity>($"exec Update_Item {item.Item_Code}, '{item.Item_Name}', '{checkItemExist.Prod_Date}', '{checkItemExist.Validity_Period}',{item.total_Count}").ToList();
                    }
                    //store_items
                    if (checkStoreItemExist == null)
                    {
                        Store_Items items = new Store_Items();
                        items.Store_Id = storeid.Store_Id;
                        items.Item_Code = item.Item_Code;
                        items.Item_Total_Count = item.total_Count;
                        Ent.Store_Items.Add(items); 
                    }
                    else
                    {
                        checkStoreItemExist.Item_Total_Count += item.total_Count;
                    }
                }
                //export
                else
                {
                    if(checkStoreItemExist.Item_Total_Count < item.total_Count)
                    {
                        MessageBox.Show("Sorry this Store has only "+ checkStoreItemExist.Item_Total_Count+ " item");
                    }
                    else if (checkStoreItemExist.Item_Total_Count == item.total_Count)
                    {
                        IEnumerable<EFEntity> edit = Ent.Database.SqlQuery<EFEntity>($"exec Update_Item {item.Item_Code}, '{textBox2.Text}', '{checkItemExist.Prod_Date}', '{checkItemExist.Validity_Period}', {item.total_Count},'Export'").ToList();
                        Ent.Store_Items.Remove(checkStoreItemExist);
                    }
                    else
                    {
                        IEnumerable<EFEntity> edit = Ent.Database.SqlQuery<EFEntity>($"exec Update_Item {item.Item_Code}, '{textBox2.Text}', '{checkItemExist.Prod_Date}', '{checkItemExist.Validity_Period}', {item.total_Count},'Export'").ToList();
                        checkStoreItemExist.Item_Total_Count -= item.total_Count;
                    }
                }
                //add permission items
                Permissions_Items permissionsItem = new Permissions_Items();
                permissionsItem.Perm_Id = int.Parse(textBox6.Text);
                permissionsItem.Item_Code = item.Item_Code;
                permissionsItem.Item_Count = item.total_Count;
                Ent.Permissions_Items.Add(permissionsItem);
            }
            Ent.SaveChanges();
            MessageBox.Show("Inserted Successfully");
            textBox1.Text=string.Empty;
            textBox2.Text=string.Empty;
            textBox5.Text=string.Empty;
            textBox6.Text=string.Empty;
            listBox1.Items.Clear();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e, dateTimePicker1);
        }
        #endregion

        #region Update Permission
        private void button2_Click(object sender, EventArgs e)
        {
            permissionUpdateDialog dialog = new permissionUpdateDialog();
            DialogResult = dialog.ShowDialog();
            if(DialogResult== DialogResult.OK) 
            {
                var perm = (from p in Ent.Permissions where p.Perm_Id == dialog.permid select p).FirstOrDefault();
                if(perm != null)
                {
                    perm.Sup_ID = (from s in Ent.Suppliers where s.Sup_Name == dialog.supName select s.Sup_Id).FirstOrDefault();
                    perm.Perm_Date = dialog.permDate;
                }
                else
                {
                    MessageBox.Show("Invalid Id");
                }
                Ent.SaveChanges();
                MessageBox.Show("Updated Successfully");
            }
        }
        #endregion
    }
}
