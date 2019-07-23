using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication7
{
    public partial class PassCh : Form
    {
        
        string idzap;
        public PassCh(string id)
        {
            InitializeComponent();
            
            idzap = id;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="")
            {
                
                        OleDbConnection DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
                        DB.Open();
                //MessageBox.Show("update Zakazchik set password=\"" + textBox1.Text + "\" where id_zakazch=" + idzap);
                        OleDbDataAdapter DA = new OleDbDataAdapter("update Zakazchik set pas=\"" + textBox1.Text + "\" where id_zakazch=" + idzap, DB);
                        DataTable DT = new DataTable();
                        DA.Fill(DT);
                        DB.Close();
                        users us = this.Owner as users;
                        us.zakrZap1();
                        this.Close();
                
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {
                textBox1.UseSystemPasswordChar = false;

            }
            else
            {
                textBox1.UseSystemPasswordChar = true;

            }
        }
        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Width = 22;
            pictureBox2.Height = 22;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Width = 24;
            pictureBox2.Height = 24;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
