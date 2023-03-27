using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EFProject
{
    public partial class Form1 : Form
    {
        EFEntity Ent = new EFEntity();
        string displayType;
        List<TextBox> textBoxList = new List<TextBox>();
        List<Label> labelList = new List<Label>();

        #region Constructor
        public Form1()
        {
            InitializeComponent();
            textBoxList.Add(textBox1);
            textBoxList.Add(textBox2);
            textBoxList.Add(textBox3);
            textBoxList.Add(textBox4);
            textBoxList.Add(textBox5);
            textBoxList.Add(textBox6);
            labelList.Add(label1);
            labelList.Add(label2);
            labelList.Add(label3);
            labelList.Add(label4);
            labelList.Add(label5);
            labelList.Add(label6);
        }
        #endregion

        #region Clear TextBoxes
        void clearTextBoxes()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
        }
        #endregion

        #region Fill ComboBox
        void FillComboBox()
        {
            comboBox1.Items.Clear();
            comboBox1.Text = string.Empty;
            if(displayType == "Stores")
            {
                foreach (var item in Ent.Stores)
                {
                    comboBox1.Items.Add(item.Store_Name);
                }
            }
            else if (displayType == "Products")
            {
                foreach (var item in Ent.Items)
                {
                    comboBox1.Items.Add(item.Item_Name);
                }
            }
            else if (displayType == "Suppliers")
            {
                foreach (var item in Ent.Suppliers)
                {
                    comboBox1.Items.Add(item.Sup_Name);
                }
            }
            else
            {
                foreach (var item in Ent.Customers)
                {
                    comboBox1.Items.Add(item.Cust_Name);
                }
            }
        }
        #endregion

        #region Change Controls Visibility
        void ControlsVisibility()
        {
            if (displayType == "Stores")
            {
                textBox1.Enabled = true;
                for (int i = 0; i < 4; i++)
                {
                    textBoxList[i].Visible = true;
                    labelList[i].Visible = true;
                }
                textBox5.Visible = false;
                textBox6.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                label1.Text = "ID";
                label2.Text = "Name";
                label3.Text = "Address";
                label4.Text = "Manager";
            }
            else if (displayType == "Products")
            {
                textBox1.Enabled = true;
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
                textBox3.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                textBox6.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = true;
                label8.Visible = true;
                label1.Text = "Code";
                label2.Text = "Name";
            }
            else if (displayType == "Suppliers")
            {
                textBox1.Enabled = true;
                for (int i = 0; i < 6; i++)
                {
                    textBoxList[i].Visible = true;
                    labelList[i].Visible = true;
                }
                label7.Visible = false;
                label8.Visible = false;
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                label1.Text = "ID";
                label2.Text = "Name";
                label3.Text = "Phone";
                label4.Text = "Fax";
                label5.Text = "Email";
                label6.Text = "Website";
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    textBoxList[i].Visible = true;
                    labelList[i].Visible = true;
                }
                textBox6.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                textBox1.Enabled = false;
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                label1.Text = "ID";
                label2.Text = "Name";
                label3.Text = "Phone";
                label4.Text = "Fax";
                label5.Text = "Email";
            }
        }
        #endregion

        #region display data in dataGridView
        void DisplayDataGridView()
        {
            if (displayType == "Stores")
            {
                var stores = from st in Ent.Stores select st;
                dataGridView1.DataSource = stores.ToList();
            }
            else if (displayType == "Products")
            {
                var Products = from pd in Ent.Items select pd;
                dataGridView1.DataSource = Products.ToList();
            }
            else if (displayType == "Suppliers")
            {
                var suppliers = from sp in Ent.Suppliers select sp;
                dataGridView1.DataSource = suppliers.ToList();
            }
            else
            {
                var customers = from ct in Ent.Customers select ct;
                dataGridView1.DataSource = customers.ToList();
            }
        }
        #endregion
         
        #region Stores
        private void button1_Click(object sender, EventArgs e)
        {
            displayType = "Stores";
            label9.Text = button1.Text;
            clearTextBoxes();
            ControlsVisibility();
            FillComboBox();
            DisplayDataGridView();
        }
        #endregion

        #region Products
        private void button4_Click(object sender, EventArgs e)
        {
            displayType = "Products";
            label9.Text = button4.Text;
            clearTextBoxes();
            ControlsVisibility();
            FillComboBox();
            DisplayDataGridView();
        }
        #endregion

        #region Customers
        private void button3_Click(object sender, EventArgs e)
        {
            displayType = "Customers";
            label9.Text = button3.Text;
            clearTextBoxes();
            ControlsVisibility();
            FillComboBox();
            DisplayDataGridView();
        }
        #endregion

        #region Suppliers
        private void button2_Click(object sender, EventArgs e)
        {
            displayType = "Suppliers";
            label9.Text = button2.Text;
            clearTextBoxes();
            ControlsVisibility();
            FillComboBox();
            DisplayDataGridView();
        }
     	#endregion   

        #region Permissions
        private void button6_Click(object sender, EventArgs e)
        {
            PermissionDialog dlg = new PermissionDialog();
            dlg.Show();
        }
        #endregion

        #region ADD operation
        private void button10_Click(object sender, EventArgs e)
        {
            IEnumerable<EFEntity> add = null;
            if (displayType == "Stores")
            {
                int id = int.Parse(textBox1.Text);
                add = Ent.Database.SqlQuery<EFEntity>($"exec Insert_Store {id}, '{textBox2.Text}', '{textBox3.Text}', '{textBox4.Text}'").ToList();
            }
            else if (displayType == "Products")
            {
                MeasureDialog measureDialog = new MeasureDialog();
                DialogResult dialogResult = measureDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    int id = int.Parse(textBox1.Text);
                    var x = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    var y = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                    add = Ent.Database.SqlQuery<EFEntity>($"exec Insert_Item {id}, '{textBox2.Text}', '{x}', '{y}',{measureDialog.quantity}").ToList();
                    Ent.SaveChanges();
                    Measurement measurement = new Measurement();
                    foreach (var item in measureDialog.measureList)
                    {
                        measurement.Item_Code = id;
                        measurement.measure_Name = item;
                        Ent.Measurements.Add(measurement);
                        Ent.SaveChanges();
                    }
                }
                else
                {
                    MessageBox.Show("Choose item Measurement First");
                }
            }
            else if (displayType == "Suppliers")
            {
                int id = int.Parse(textBox1.Text);
                add = Ent.Database.SqlQuery<EFEntity>($"exec Insert_Supplier {id}, '{textBox2.Text}', '{textBox3.Text}', '{textBox4.Text}', '{textBox5.Text}', '{textBox6.Text}'").ToList();
            }
            else
            {
                add = Ent.Database.SqlQuery<EFEntity>($"exec Insert_Customer '{textBox2.Text}', '{textBox3.Text}', '{textBox4.Text}', '{textBox5.Text}'").ToList();
            }
            if (add.Any())
            {
                MessageBox.Show("Already Exist");
            }
            else
            {
                MessageBox.Show("Inserted Successfully");
                Ent.SaveChanges();
                clearTextBoxes();
                button11_Click(this, null);
                FillComboBox();
            }
        }
        #endregion

        #region Delete operation
        private void button9_Click(object sender, EventArgs e)
        {
            IEnumerable<EFEntity> delete;
            if (displayType == "Stores")
            {
                int id = int.Parse(textBox1.Text);
                delete = Ent.Database.SqlQuery<EFEntity>($"exec Delete_Store {id}").ToList();
                Ent.SaveChanges();
                FillComboBox();
            }
            else if (displayType == "Products")
            {
                int id = int.Parse(textBox1.Text);
                delete = Ent.Database.SqlQuery<EFEntity>($"exec Delete_Item {id}").ToList();
                Ent.SaveChanges();
                FillComboBox();
            }
            else if (displayType == "Suppliers")
            {
                int id = int.Parse(textBox1.Text);
                delete = Ent.Database.SqlQuery<EFEntity>($"exec Delete_Supplier {id}").ToList();
                Ent.SaveChanges();
                FillComboBox();
            }
            else
            {
                delete = Ent.Database.SqlQuery<EFEntity>($"exec Delete_Customer '{textBox2.Text}'").ToList();
                Ent.SaveChanges();
                FillComboBox();
            }
            if (delete.Any())
            {
                MessageBox.Show("Doesn't Exist");
            }
            else
            {
                MessageBox.Show("Deleted Successfully");
                clearTextBoxes();
                button11_Click(this, null);
            }
        }
        #endregion

        #region Update operation
        private void button8_Click(object sender, EventArgs e)
        {
            IEnumerable<EFEntity> edit;
            if (displayType == "Stores")
            {
                int id = int.Parse(textBox1.Text);
                edit = Ent.Database.SqlQuery<EFEntity>($"exec Update_Store {id}, '{textBox2.Text}', '{textBox3.Text}', '{textBox4.Text}'").ToList();
                Ent.SaveChanges();
                FillComboBox();
            }
            else if (displayType == "Products")
            {
                int id = int.Parse(textBox1.Text);
                var x = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                var y = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                edit = Ent.Database.SqlQuery<EFEntity>($"exec Update_Item {id}, '{textBox2.Text}', '{x}', '{y}'").ToList();
                Ent.SaveChanges();
                FillComboBox();
            }
            else if (displayType == "Suppliers")
            {
                int id = int.Parse(textBox1.Text);
                edit = Ent.Database.SqlQuery<EFEntity>($"exec Update_Supplier {id}, '{textBox2.Text}', '{textBox3.Text}', '{textBox4.Text}', '{textBox5.Text}', '{textBox6.Text}'").ToList();
                Ent.SaveChanges();
                FillComboBox();
            }
            else
            {
                edit = Ent.Database.SqlQuery<EFEntity>($"exec Update_Customer '{textBox2.Text}', '{textBox3.Text}', '{textBox4.Text}', '{textBox5.Text}'").ToList();
                Ent.SaveChanges();
                FillComboBox();
            }
            if (edit.Any())
            {
                MessageBox.Show("Doesn't Exist");
            }
            else
            {
                MessageBox.Show("Updated Successfully");
                clearTextBoxes();
                DisplayDataGridView();
            }
        }
        #endregion

        #region OnSelect item in comboBox 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (displayType == "Stores")
            {
                var stores = (from st in Ent.Stores where st.Store_Name == comboBox1.Text select st).ToList();
                textBox1.Text = stores[0]?.Store_Id.ToString();
                textBox2.Text = stores[0]?.Store_Name;
                textBox3.Text = stores[0]?.Address;
                textBox4.Text = stores[0]?.Manager_Name;
                dataGridView1.DataSource = stores;
            }
            else if (displayType == "Products")
            {
                var Products = (from pd in Ent.Items where pd.Item_Name == comboBox1.Text select pd).ToList();
                textBox1.Text = Products[0]?.Item_Code.ToString();
                textBox2.Text = Products[0]?.Item_Name;
                dateTimePicker1.Text = Products[0]?.Prod_Date.ToString();
                dateTimePicker2.Text = Products[0]?.Validity_Period.ToString();
                dataGridView1.DataSource = Products;
            }
            else if (displayType == "Suppliers")
            {
                var suppliers = (from sp in Ent.Suppliers where sp.Sup_Name == comboBox1.Text select sp).ToList();
                textBox1.Text = suppliers[0]?.Sup_Id.ToString();
                textBox2.Text = suppliers[0]?.Sup_Name;
                textBox3.Text = suppliers[0]?.Phone;
                textBox4.Text = suppliers[0]?.Fax;
                textBox5.Text = suppliers[0]?.EMail;
                textBox6.Text = suppliers[0]?.Website;
                dataGridView1.DataSource = suppliers;
            }
            else
            {
                var customers = (from ct in Ent.Customers where ct.Cust_Name == comboBox1.Text select ct).ToList();
                textBox1.Text = customers[0]?.Cust_Id.ToString();
                textBox2.Text = customers[0]?.Cust_Name;
                textBox3.Text = customers[0]?.Phone;
                textBox4.Text = customers[0]?.Fax;
                textBox5.Text = customers[0]?.EMail;
                dataGridView1.DataSource = customers;
            }
        }
        #endregion

        #region Display All Rows
        private void button11_Click(object sender, EventArgs e)
        {
            DisplayDataGridView();
        }
        #endregion

        private void button5_Click(object sender, EventArgs e)
        {
            TransferItems transfer = new TransferItems();
            transfer.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ReportsDialog reports = new ReportsDialog();
            reports.Show();
        }
    }
}
