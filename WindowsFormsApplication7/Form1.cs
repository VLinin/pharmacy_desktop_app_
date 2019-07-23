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
    public partial class Log : Form
    {
        public string user;
        public string username()
        {
            return user;
        }
        public Log()
        {
            InitializeComponent();
            textBox2.UseSystemPasswordChar = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            user = textBox1.Text;
            
            OleDbConnection DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
            DB.Open();
            OleDbDataAdapter DA = new OleDbDataAdapter("select who from zakazchik where Login='"+textBox1.Text+"' and pas='"+textBox2.Text+"'", DB);
            DataTable DT = new DataTable();
            DA.Fill(DT);
            try
            {
                if (Convert.ToInt32(DT.Rows[0][0]) != 0)
                {
                    int k = Convert.ToInt32(DT.Rows[0][0]);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    switch (k)
                    {
                        case 1:
                            Admin ad = new Admin();
                            ad.Owner = this;
                            ad.Show();
                            this.Hide();
                            break;
                        case 2:
                            
                            Pokupatel p = new Pokupatel(user);
                            p.Owner = this;
                            p.Show();
                            this.Hide();
                            break;
                        
                    }
                }
            }
            catch
            {
                
                textBox1.Text = "";
                textBox2.Text = "";
                label3.Visible = true;
            }
            
            DB.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            
            pictureBox1.Width = 23;
            pictureBox1.Height = 23;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Width = 24;
            pictureBox1.Height = 24;
        }

        private void Log_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            regorder ro = new regorder();
            ro.Owner = this;
            ro.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label3.Visible = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label3.Visible = false;
        }

        private void Log_KeyDown(object sender, KeyEventArgs e)
        {
          //  if (e.KeyCode == Keys.Enter) button
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;

            }
            else
            {
                textBox2.UseSystemPasswordChar = true;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "FAQ.chm");
        }
    }
}
