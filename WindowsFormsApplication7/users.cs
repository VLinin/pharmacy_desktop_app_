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
    public partial class users : Form
    {
        public users()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Width = 22;
            pictureBox1.Height = 22;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Width = 24;
            pictureBox1.Height = 24;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox1.SelectedIndex)
            {
                case 0:
                    button1.Enabled = true;
                    button3.Enabled = false;
                    OleDbConnection DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
                    DB.Open();
                    OleDbDataAdapter DA = new OleDbDataAdapter("select id_zakazch,Login,Pas,who from Zakazchik", DB);
                    DataTable DT = new DataTable();
                    DA.Fill(DT);
                    dataGridView1.DataSource = DT;
                    DB.Close();
                    break;
                case 1:
                    button1.Enabled = false;
                    button3.Enabled = true;
                    OleDbConnection DB1 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = log.mdb");
                    DB1.Open();
                    OleDbDataAdapter DA1 = new OleDbDataAdapter("select id_reg,Log,Pas from reg", DB1);
                    DataTable DT1 = new DataTable();
                    DA1.Fill(DT1);
                    dataGridView1.DataSource = DT1;
                    DB1.Close();
                    break;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Admin ad = this.Owner as Admin;
            ad.Show();
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch(comboBox1.SelectedIndex)
            {
                case 0:
                    OleDbConnection DB1 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
                    DB1.Open();

                    OleDbCommand drop_command = new OleDbCommand("delete from Zakazchik where " + dataGridView1.Columns[0].HeaderText + "=" + dataGridView1.SelectedCells[0].Value.ToString(), DB1);
                    if (drop_command.ExecuteNonQuery() == 0)
                    {
                        MessageBox.Show("Ошибка в запросе!");
                    }

                    DB1.Close();
                    OleDbConnection DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
                    DB.Open();
                    OleDbDataAdapter DA = new OleDbDataAdapter("select id_zakazch,Login,Pas,who from Zakazchik", DB);
                    DataTable DT = new DataTable();
                    DA.Fill(DT);
                    dataGridView1.DataSource = DT;
                    DB.Close();
                    break;
                case 1:
                    OleDbConnection DB2 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = log.mdb");
                    DB2.Open();

                    OleDbCommand drop_command2 = new OleDbCommand("delete from Reg where " + dataGridView1.Columns[0].HeaderText + "=" + dataGridView1.SelectedCells[0].Value.ToString(), DB2);
                    if (drop_command2.ExecuteNonQuery() == 0)
                    {
                        MessageBox.Show("Ошибка в запросе!");
                    }

                    DB2.Close();
                    OleDbConnection DB3 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = log.mdb");
                    DB3.Open();
                    OleDbDataAdapter DA3 = new OleDbDataAdapter("select id_reg,Log,Pas from reg", DB3);
                    DataTable DT3 = new DataTable();
                    DA3.Fill(DT3);
                    dataGridView1.DataSource = DT3;
                    DB3.Close();
                    break;
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string login = dataGridView1.SelectedCells[1].Value.ToString();
            string password = dataGridView1.SelectedCells[2].Value.ToString();
            if (MessageBox.Show("Сделать пользователя администратором??", "Определение статуса", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                OleDbConnection DB1 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source =apteka.mdb");
                DB1.Open();

                OleDbCommand drop_command1 = new OleDbCommand("insert into zakazchik ( Login , pas , who ) values ('" + login + "','" + password + "',1)", DB1);
                if (drop_command1.ExecuteNonQuery() == 0)
                {
                    MessageBox.Show("Ошибка в запросе!");
                }
               
            }  
            else
            {
                OleDbConnection DB5 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
                DB5.Open();
                
                string zap = "insert into zakazchik ( Login , pas , who ) values ('" + login + "','" + password + "',2)";
               
                /* OleDbCommand command5 = new OleDbCommand(zap, DB5);
                 command5.ExecuteNonQuery();*/



                /*"insert into zakazchik ( Login , password , who ) values ('" + login + "','" + password + "', 2 )"
                OleDbConnection DB5 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = C:\Users\VL\Documents\Visual Studio 2015\Projects\apteka.mdb");
                DB5.Open(); */
                OleDbDataAdapter DA5 = new OleDbDataAdapter(zap, DB5);
              //  MessageBox.Show("insert into Zakazchik(login,password,who) values('" + dataGridView1.SelectedCells[1].Value.ToString() + "','" + dataGridView1.SelectedCells[2].Value.ToString() + "',2)");
                DataTable DT5 = new DataTable();
                DA5.Fill(DT5);
                DB5.Close();
            }
            OleDbConnection DB2 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = log.mdb");
            DB2.Open();

            OleDbCommand drop_command2 = new OleDbCommand("delete from reg where " + dataGridView1.Columns[0].HeaderText + "=" + dataGridView1.SelectedCells[0].Value.ToString(), DB2);
            if (drop_command2.ExecuteNonQuery() == 0)
            {
                MessageBox.Show("Ошибка в запросе!");
            }

            DB2.Close();
            OleDbConnection DB3 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = log.mdb");
            DB3.Open();
            OleDbDataAdapter DA3 = new OleDbDataAdapter("select * from reg", DB3);
            DataTable DT3 = new DataTable();
            DA3.Fill(DT3);
            dataGridView1.DataSource = DT3;
            DB3.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch(comboBox1.SelectedIndex)
            {
                case 0:
                    PassCh fz = new PassCh(dataGridView1.SelectedCells[0].Value.ToString());
                    fz.Owner = this;
                    fz.Show();
                    break;
                case 1:
                    FormZap fz1 = new FormZap(comboBox1.SelectedItem.ToString(), 3);
                    fz1.Owner = this;
                    fz1.Show();
                    break;
            }
            
        }

        private void users_Load(object sender, EventArgs e)
        {

        }
        public void zakrZap1()
        {
            comboBox1.SelectedIndex = 0;
            button3.Enabled = false;
            OleDbConnection DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
            DB.Open();
            OleDbDataAdapter DA = new OleDbDataAdapter("select id_zakazch,Login,Pas,who from Zakazchik", DB);
            DataTable DT = new DataTable();
            DA.Fill(DT);
            dataGridView1.DataSource = DT;
            DB.Close();
        }
        public void zakrZap2(string znach)
        {
            OleDbConnection DB1 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = log.mdb");
            DB1.Open();
            OleDbDataAdapter DA1 = new OleDbDataAdapter("select id_reg,Log,Pas from reg", DB1);
            DataTable DT1 = new DataTable();
            DA1.Fill(DT1);
            dataGridView1.DataSource = DT1;
            DB1.Close();
        }

        private void btn_help_Click(object sender, EventArgs e)
        {
            HelpNavigator navigator = HelpNavigator.TableOfContents;
            Help.ShowHelp(this, "FAQ.chm", navigator);
        }
    }
}
