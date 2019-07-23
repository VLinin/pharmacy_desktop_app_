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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();

            OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
            connection.Open();
            DataTable dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            foreach (DataRow item in dt.Rows)
            {
                comboBox1.Items.Add((string)item["TABLE_NAME"]);
            }
            connection.Close();
            /////////////////////////////
            comboBox1.SelectedIndex = 0;

            OleDbConnection DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
            DB.Open();
            OleDbDataAdapter DA = new OleDbDataAdapter("select * from " + comboBox1.SelectedItem, DB);
            DataTable DT = new DataTable();
            DA.Fill(DT);
            dataGridView1.DataSource = DT;
            DB.Close();
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Log l = this.Owner as Log;
            l.Show();
            this.Close();


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Log l=  this.Owner as Log;
            l.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OleDbConnection DB1 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
            DB1.Open();
            
            OleDbCommand drop_command = new OleDbCommand("delete from "+comboBox1.SelectedItem+" where "+ dataGridView1.Columns[0].HeaderText+"="+dataGridView1.SelectedCells[0].Value.ToString(), DB1);
            if (drop_command.ExecuteNonQuery() == 0)
            {
                MessageBox.Show("Ошибка в запросе!");
            }

            DB1.Close();
            OleDbConnection DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
            DB.Open();
            OleDbDataAdapter DA = new OleDbDataAdapter("select * from " + comboBox1.SelectedItem, DB);
            DataTable DT = new DataTable();
            DA.Fill(DT);
            dataGridView1.DataSource = DT;
            DB.Close();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Width = 24;
            pictureBox1.Height = 24;
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Width = 22;
            pictureBox1.Height = 22;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection DB1 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
                DB1.Open();

                OleDbCommand drop_command = new OleDbCommand(textBox1.Text, DB1);
                if (drop_command.ExecuteNonQuery() == 0)
                {
                    MessageBox.Show("Ошибка в запросе!");
                }

                DB1.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка в запросе!");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Восстановление данных будет невозможно!\r\n Удалить ТАБЛИЦУ??", "Подтверждение удаления", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                OleDbConnection DB1 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
                DB1.Open();

                OleDbCommand drop_command = new OleDbCommand("drop table "+comboBox1.SelectedItem.ToString(), DB1);
                if (drop_command.ExecuteNonQuery() == 0)
                {
                    comboBox1.Items.Remove(comboBox1.SelectedItem.ToString());
                    comboBox1.SelectedItem = 1;
                    
                }
                DB1.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbConnection DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
            DB.Open();
            OleDbDataAdapter DA = new OleDbDataAdapter("select * from " + comboBox1.SelectedItem.ToString(), DB);
            DataTable DT = new DataTable();
            DA.Fill(DT);
            dataGridView1.DataSource = DT;
            DB.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormZap fz = new FormZap(comboBox1.SelectedItem.ToString(),1);
            fz.Owner = this;
            fz.Show();
        }
        public void zakrZap(string znach)
        {
            comboBox1.SelectedItem = znach;
            OleDbConnection DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
            DB.Open();
            OleDbDataAdapter DA = new OleDbDataAdapter("select * from " + comboBox1.SelectedItem + "\"", DB);
            DataTable DT = new DataTable();
            DA.Fill(DT);
            dataGridView1.DataSource = DT;
            DB.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            users us = new users();
            us.Owner = this;
            us.Show();
            this.Hide();
        }

        private void btn_help_Click(object sender, EventArgs e)
        {
            Help.ShowHelpIndex(this, "FAQ.chm");
        }
    }
}
