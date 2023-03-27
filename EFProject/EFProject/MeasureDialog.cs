using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFProject
{
    public partial class MeasureDialog : Form
    {
        public List<string> measureList = new List<string>();
        public int quantity; 
        public MeasureDialog()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Itemsmeasurement();
            quantity = int.Parse(textBox1.Text);
            DialogResult= DialogResult.OK;
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult= DialogResult.Cancel;
            this.Close();
        }
        void Itemsmeasurement()
        {
            if(checkBox1.Checked) 
            {
                measureList.Add(checkBox1.Text);
            }
            if (checkBox2.Checked)
            {
                measureList.Add(checkBox2.Text);
            }
            if (checkBox3.Checked)
            {
                measureList.Add(checkBox3.Text);
            }
            if (checkBox4.Checked)
            {
                measureList.Add(checkBox4.Text);
            }
            if (checkBox5.Checked)
            {
                measureList.Add(checkBox5.Text);
            }
            else
            {
                measureList.Add(checkBox6.Text);
            }
        }
    }
}
