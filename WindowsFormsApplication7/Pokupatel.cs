using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication7
{
    public partial class Pokupatel : Form
    {
        string orderid;
        string whouse;
        public Pokupatel(string k)
        {
            InitializeComponent();
            OleDbConnection DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source =apteka.mdb");
            DB.Open();
            
            OleDbDataAdapter DA = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip)", DB);
            
            DataTable DT = new DataTable();
            DA.Fill(DT);
            dataGridView1.DataSource = DT;
            DB.Close();
            OleDbConnection DB1 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
            DB1.Open();
            
            OleDbCommand command = new OleDbCommand("SELECT id_zakazch from zakazchik where login='"+k+"'", DB1);
            
            whouse = command.ExecuteScalar().ToString();
            

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (button3.Enabled)
            {
                if (MessageBox.Show("Восстановление данных будет невозможно!\r\n Продолжить?", "Подтверждение удаления", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    OleDbConnection DB1 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
                    DB1.Open();

                    OleDbCommand drop_command = new OleDbCommand("delete from order_sostav where id_order=" + orderid, DB1);
                    if (drop_command.ExecuteNonQuery() == 0)
                    {
                        MessageBox.Show("не вышло(");
                    }
                    else
                    {

                        button4.Enabled = false;
                    }
                    DB1.Close();
                    Log l = this.Owner as Log;
                    l.Show();
                    this.Close();

                }
            }
            else
            {
                
                Log l = this.Owner as Log;
                l.Show();
                this.Close();
            }
            
        }

        private void Pokupatel_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            OleDbConnection DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
            DB.Open();
            OleDbCommand command = new OleDbCommand("SELECT skladitem.id_skladitem, lekarstva.nazvanie from((lekarstva inner join skladitem on lekarstva.id_lek = skladitem.id_lek) left join order_sostav on skladitem.id_skladitem = order_sostav.id_skladitem) where lekarstva.nazvanie = '" + dataGridView1.SelectedCells[0].Value.ToString() + "' and order_sostav.id_skladitem is null AND skladitem.id_skladitem = (SELECT min(skladitem.id_skladitem) FROM((lekarstva inner join skladitem on lekarstva.id_lek = skladitem.id_lek) left join order_sostav on skladitem.id_skladitem = order_sostav.id_skladitem) WHERE order_sostav.id_skladitem IS NULL and lekarstva.nazvanie = '" + dataGridView1.SelectedCells[0].Value.ToString() + "')", DB);
            string item = command.ExecuteScalar().ToString();
            OleDbConnection DB1 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
            DB1.Open();
            OleDbCommand command1 = new OleDbCommand("select max(id_order)+1 from zakaz", DB1);
            orderid = command1.ExecuteScalar().ToString();      
            OleDbConnection DB2 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
            DB2.Open();          
            OleDbDataAdapter DA2 = new OleDbDataAdapter("insert into order_sostav(id_skladitem,id_order) values("+item+","+orderid+")", DB2);
            DataTable DT2 = new DataTable();
            DA2.Fill(DT2);
            DB2.Close();

            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = -1;
            OleDbConnection DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
            DB.Open();
            OleDbDataAdapter DA = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip)", DB);
            DataTable DT = new DataTable();
            DA.Fill(DT);
            dataGridView1.DataSource = DT;
            DB.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbConnection DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
            DB.Open();
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    try
                    {
                        OleDbDataAdapter DA1 = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from ((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip) where lekarstva.id_tip=1", DB);
                        DataTable DT1 = new DataTable();
                        DA1.Fill(DT1);
                        dataGridView1.DataSource = DT1;
                    }
                    catch
                    {
                        MessageBox.Show("По данному фильтру нет результатов");
                    }
                    break;
                case 1:
                    try
                    {
                        OleDbDataAdapter DA2 = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip) where lekarstva.id_tip=2", DB);
                        DataTable DT2 = new DataTable();
                        DA2.Fill(DT2);
                        dataGridView1.DataSource = DT2;
                    }
                    catch
                    {
                        MessageBox.Show("По данному фильтру нет результатов");
                    }
                    break;
                case 2:
                    try
                    {
                        OleDbDataAdapter DA3 = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip) where lekarstva.id_vid=1", DB);
                        DataTable DT3 = new DataTable();
                        DA3.Fill(DT3);
                        dataGridView1.DataSource = DT3;
                    }
                    catch
                    {
                        MessageBox.Show("По данному фильтру нет результатов");
                    }
                    break;
                case 3:
                    try
                    {
                        OleDbDataAdapter DA4 = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip) where lekarstva.id_vid=2", DB);
                        DataTable DT4 = new DataTable();
                        DA4.Fill(DT4);
                        dataGridView1.DataSource = DT4;
                    }
                    catch
                    {
                        MessageBox.Show("По данному фильтру нет результатов");
                    }
                    break;
                case 4:
                    try
                    {
                        OleDbDataAdapter DA5 = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip) where lekarstva.id_vid=3", DB);
                        DataTable DT5 = new DataTable();
                        DA5.Fill(DT5);
                        dataGridView1.DataSource = DT5;
                    }
                    catch
                    {
                        MessageBox.Show("По данному фильтру нет результатов");
                    }
                    break;
                case 5:
                    try
                    {
                        OleDbDataAdapter DA6 = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip) where lekarstva.id_vid=4", DB);
                        DataTable DT6 = new DataTable();
                        DA6.Fill(DT6);
                        dataGridView1.DataSource = DT6;
                    }
                    catch
                    {
                        MessageBox.Show("По данному фильтру нет результатов");
                    }
                    break;
                case 6:
                    try
                    {
                        OleDbDataAdapter DA7 = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip) where lekarstva.id_vid=5", DB);
                        DataTable DT7 = new DataTable();
                        DA7.Fill(DT7);
                        dataGridView1.DataSource = DT7;
                    }
                    catch
                    {
                        MessageBox.Show("По данному фильтру нет результатов");
                    }
                    break;
                case 7:
                    try
                    {
                        OleDbDataAdapter DA8 = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip) where lekarstva.id_vid=6", DB);
                        DataTable DT8 = new DataTable();
                        DA8.Fill(DT8);
                        dataGridView1.DataSource = DT8;
                    }
                    catch
                    {
                        MessageBox.Show("По данному фильтру нет результатов");
                    }
                    break;
                case 8:
                    try
                    {
                        OleDbDataAdapter DA9 = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip) where lekarstva.id_vid=1 and lekarstva.id_tip=1", DB);
                        DataTable DT9 = new DataTable();
                        DA9.Fill(DT9);
                        dataGridView1.DataSource = DT9;
                    }
                    catch
                    {
                        MessageBox.Show("По данному фильтру нет результатов");
                    }
                    break;
                case 9:
                    try
                    {
                        OleDbDataAdapter DA10 = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip) where lekarstva.id_vid=2 and lekarstva.id_tip=1", DB);
                        DataTable DT10 = new DataTable();
                        DA10.Fill(DT10);
                        dataGridView1.DataSource = DT10;
                    }
                    catch
                    {
                        MessageBox.Show("По данному фильтру нет результатов");
                    }
                    break;
                case 10:
                    try
                    {
                        OleDbDataAdapter DA11 = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip) where lekarstva.id_vid=3 and lekarstva.id_tip=1", DB);
                        DataTable DT11 = new DataTable();
                        DA11.Fill(DT11);
                        dataGridView1.DataSource = DT11;
                    }
                    catch
                    {
                        MessageBox.Show("По данному фильтру нет результатов");
                    }
                    break;
                case 11:
                    try
                    {
                        OleDbDataAdapter DA12 = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip) where lekarstva.id_vid=4 and lekarstva.id_tip=1", DB);
                        DataTable DT12 = new DataTable();
                        DA12.Fill(DT12);
                        dataGridView1.DataSource = DT12;
                    }
                    catch
                    {
                        MessageBox.Show("По данному фильтру нет результатов");
                    }
                    break;
                case 12:
                    try
                    {
                        OleDbDataAdapter DA13 = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip) where lekarstva.id_vid=5 and lekarstva.id_tip=1", DB);
                        DataTable DT13 = new DataTable();
                        DA13.Fill(DT13);
                        dataGridView1.DataSource = DT13;
                    }
                    catch
                    {
                        MessageBox.Show("По данному фильтру нет результатов");
                    }
                    break;
                case 13:
                    try
                    {
                        OleDbDataAdapter DA14 = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip) where lekarstva.id_vid=6 and lekarstva.id_tip=1", DB);
                        DataTable DT14 = new DataTable();
                        DA14.Fill(DT14);
                        dataGridView1.DataSource = DT14;
                    }
                    catch
                    {
                        MessageBox.Show("По данному фильтру нет результатов");
                    }
                    break;
                case 14:
                    try
                    {
                        OleDbDataAdapter DA15 = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip) where lekarstva.id_vid=1 and lekarstva.id_tip=2", DB);
                        DataTable DT15 = new DataTable();
                        DA15.Fill(DT15);
                        dataGridView1.DataSource = DT15;
                    }
                    catch
                    {
                        MessageBox.Show("По данному фильтру нет результатов");
                    }
                    break;
                case 15:
                    try
                    {
                        OleDbDataAdapter DA16 = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip) where lekarstva.id_vid=2 and lekarstva.id_tip=2", DB);
                        DataTable DT16 = new DataTable();
                        DA16.Fill(DT16);
                        dataGridView1.DataSource = DT16;
                    }
                    catch
                    {
                        MessageBox.Show("По данному фильтру нет результатов");
                    }
                    break;
                case 16:
                    try
                    {
                        OleDbDataAdapter DA17 = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip) where lekarstva.id_vid=3 and lekarstva.id_tip=2", DB);
                        DataTable DT17 = new DataTable();
                        DA17.Fill(DT17);
                        dataGridView1.DataSource = DT17;
                    }
                    catch
                    {
                        MessageBox.Show("По данному фильтру нет результатов");
                    }
                    break;
                case 17:
                    try
                    {
                        OleDbDataAdapter DA18 = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip) where lekarstva.id_vid=4 and lekarstva.id_tip=2", DB);
                        DataTable DT18 = new DataTable();
                        DA18.Fill(DT18);
                        dataGridView1.DataSource = DT18;
                    }
                    catch
                    {
                        MessageBox.Show("По данному фильтру нет результатов");
                    }
                    break;
                case 18:
                    try
                    {
                        OleDbDataAdapter DA19 = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip) where lekarstva.id_vid=5 and lekarstva.id_tip=2", DB);
                        DataTable DT19 = new DataTable();
                        DA19.Fill(DT19);
                        dataGridView1.DataSource = DT19;
                    }
                    catch
                    {
                        MessageBox.Show("По данному фильтру нет результатов");
                    }
                    break;
                case 19:
                    try
                    {
                        OleDbDataAdapter DA20 = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip) where lekarstva.id_vid=6 and lekarstva.id_tip=2", DB);
                        DataTable DT20 = new DataTable();
                        DA20.Fill(DT20);
                        dataGridView1.DataSource = DT20;
                    }
                    catch
                    {
                        MessageBox.Show("По данному фильтру нет результатов");
                    }
                    break;
            }
            
            DB.Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            OleDbConnection DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
            DB.Open();
            OleDbDataAdapter DA = new OleDbDataAdapter("select lekarstva.nazvanie, tip.nazvanie, vid.nazvanie, lekarstva.price from ((((lekarstva inner join vid on vid.id_vid=lekarstva.id_vid) inner join tip on tip.id_tip=lekarstva.id_tip) inner join skladitem on lekarstva.id_lek=skladitem.id_lek) inner join order_sostav on order_sostav.id_skladitem=skladitem.id_skladitem) where order_sostav.id_order="+orderid, DB);
            DataTable DT = new DataTable();
            DA.Fill(DT);
            dataGridView1.DataSource = DT;
            DB.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button1.Enabled = false;
            OleDbConnection DB1 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
            DB1.Open();

            OleDbCommand drop_command = new OleDbCommand("delete from order_sostav where id_order="+orderid, DB1);
            if (drop_command.ExecuteNonQuery() == 0)
            {
                MessageBox.Show("не вышло(");
            }
            else
            {
                MessageBox.Show("успешно!");
                button4.Enabled = false;
            }
            DB1.Close();

            OleDbConnection DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
            DB.Open();
            OleDbDataAdapter DA = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip)", DB);
            DataTable DT = new DataTable();
            DA.Fill(DT);
            dataGridView1.DataSource = DT;
            DB.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection DB1 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
            DB1.Open();

            OleDbCommand drop_command = new OleDbCommand("insert into zakaz (id_order,id_zakazch,data_zak,id_sost) values("+orderid+","+whouse+",now(),3)", DB1);
            if (drop_command.ExecuteNonQuery() == 0)
            {
                MessageBox.Show("не вышло(");
            }
            else
            {
                MessageBox.Show("успешно!");
                button4.Enabled = false;
                button1.Enabled = false;
                button3.Enabled = false;
            }
            DB1.Close();
            OleDbConnection DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
            DB.Open();
            OleDbDataAdapter DA = new OleDbDataAdapter("SELECT lekarstva.nazvanie, vid.nazvanie, tip.nazvanie, lekarstva.price from((lekarstva inner join vid on lekarstva.id_vid = vid.id_vid) inner join tip on lekarstva.id_tip = tip.id_tip)", DB);
            DataTable DT = new DataTable();
            DA.Fill(DT);
            dataGridView1.DataSource = DT;
            DB.Close();
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Width = 22;
            pictureBox2.Height = 22;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Width = 24;
            pictureBox2.Height = 24;
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Width = 24;
            pictureBox1.Height = 24;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Width = 26;
            pictureBox1.Height = 26;
        }

        private void btn_help_Click(object sender, EventArgs e)
        {
            HelpNavigator navigator = HelpNavigator.Index;
            Help.ShowHelp(this, "FAQ.chm", navigator, "заказ");
        }
    }
}
