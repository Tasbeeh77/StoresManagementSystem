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
    public partial class permissionUpdateDialog : Form
    {
        public string supName;
        public DateTime permDate;
        public int permid;
        EFEntity Ent = new EFEntity();
        public permissionUpdateDialog()
        {
            InitializeComponent();
        }

        private void permissionUpdateDialog_Load(object sender, EventArgs e)
        {
            foreach (var item in Ent.Suppliers)
            {
                comboBox1.Items.Add(item.Sup_Name);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            supName = comboBox1.Text;
            permDate = dateTimePicker1.Value;
            permid = int.Parse(textBox1.Text);
            DialogResult= DialogResult.OK;
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult= DialogResult.Cancel;
            this.Close();
        }
    }
}
