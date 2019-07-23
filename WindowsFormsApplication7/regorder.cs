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
    public partial class regorder : Form
    {
        public regorder()
        {
            InitializeComponent();
            textBox2.UseSystemPasswordChar = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Log l = this.Owner as Log;
            l.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="" && textBox2.Text!="")
            {
                OleDbConnection DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
                DB.Open();
                OleDbDataAdapter DA = new OleDbDataAdapter("select Login from zakazchik where Login='" + textBox1.Text + "'", DB);
                DataTable DT = new DataTable();
                DA.Fill(DT);
                OleDbConnection DB1 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = log.mdb");
                DB1.Open();
                OleDbDataAdapter DA1 = new OleDbDataAdapter("select log from reg where Log='" + textBox1.Text + "'", DB1);
                DataTable DT1 = new DataTable();
                DA1.Fill(DT1);
                if (DT1.Rows.Count>0 || DT.Rows.Count > 0)
                {
                    MessageBox.Show("Этот логин уже занят");
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
                else
                {
                    MessageBox.Show("Ваша заявка принята!");
                    OleDbConnection DB2 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = log.mdb");
                    DB2.Open();
                    OleDbDataAdapter DA2 = new OleDbDataAdapter("insert into reg(log,pas) values('" + textBox1.Text + "', '" + textBox2.Text + "')", DB2);
                    DataTable DT2 = new DataTable();
                    DA2.Fill(DT2);
                    DB2.Close();
                    Log l = this.Owner as Log;
                    l.Show();
                    this.Close();
                }             
            }
            else
            {
                MessageBox.Show("Ошибка при вводе!");
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Width = 24;
            pictureBox1.Height = 24;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Width = 22;
            pictureBox1.Height = 22;
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
    }
}
