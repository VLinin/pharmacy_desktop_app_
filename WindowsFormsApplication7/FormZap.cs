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
    public partial class FormZap : Form
    {
        int id;
        string nameid;
        public int tekatr = 1;
        string tablename;
        int xx;
        public FormZap(string table,int x)
        {
            InitializeComponent();
            xx = x;
            tablename = table;
            //////////////////////////////////////////////////
            switch(x)
            {
                case 1:
                    OleDbConnection myConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
                    OleDbDataAdapter myAdapter = new OleDbDataAdapter("select * from " + table + "\"", myConnection);
                    DataSet dataSet = new DataSet();
                    myConnection.Open();
                    try
                    {
                        myAdapter.Fill(dataSet, "tabl");
                    }
                    catch
                    {
                        MessageBox.Show("Неверная SQL команда: либо Connection ");
                    }
                    finally { myConnection.Close(); }


                    label1.Text = dataSet.Tables[0].Columns[tekatr].ColumnName;
                    /////////////////////
                    nameid = dataSet.Tables[0].Columns[0].ColumnName;
                    break;
                case 2:
                    OleDbConnection myConnection2 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
                    OleDbDataAdapter myAdapter2 = new OleDbDataAdapter("select id_zakazch,Login,Password,who from Zakazchik", myConnection2);
                    DataSet dataSet2 = new DataSet();
                    myConnection2.Open();
                    try
                    {
                        myAdapter2.Fill(dataSet2, "tabl");
                    }
                    catch
                    {
                        MessageBox.Show("Неверная SQL команда: либо Connection ");
                    }
                    finally { myConnection2.Close(); }


                    label1.Text = dataSet2.Tables[0].Columns[tekatr].ColumnName;
                    /////////////////////
                    nameid = dataSet2.Tables[0].Columns[0].ColumnName;
                    break;
                case 3:
                    OleDbConnection myConnection3 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
                    OleDbDataAdapter myAdapter3 = new OleDbDataAdapter("select id_reg,Log,Pas from reg", myConnection3);
                    DataSet dataSet3 = new DataSet();
                    myConnection3.Open();
                    try
                    {
                        myAdapter3.Fill(dataSet3, "tabl");
                    }
                    catch
                    {
                        MessageBox.Show("Неверная SQL команда: либо Connection ");
                    }
                    finally { myConnection3.Close(); }


                    label1.Text = dataSet3.Tables[0].Columns[tekatr].ColumnName;
                    /////////////////////
                    nameid = dataSet3.Tables[0].Columns[0].ColumnName;
                    break;
                    
            }
            
            if(x==1)
            {
                OleDbConnection DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source =apteka.mdb");
                DB.Open();

                OleDbCommand command = new OleDbCommand("SELECT MAX(" + nameid + ") FROM " + table, DB);
                string prom = command.ExecuteScalar().ToString();
                try
                {
                    id = Convert.ToInt32(prom);
                    id++;
                    DB.Close();
                    OleDbConnection DB1 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
                    DB1.Open();
                    OleDbDataAdapter DA1 = new OleDbDataAdapter("insert into " + table + "(" + nameid + ") values(" + id.ToString() + ")", DB1);
                    DataTable DT1 = new DataTable();
                    DA1.Fill(DT1);
                    DB1.Close();



                    /////////////////////////////////////////////////////////////

                }
                catch
                {
                    id = 1;
                    OleDbConnection DB1 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
                    DB1.Open();
                    OleDbDataAdapter DA1 = new OleDbDataAdapter("insert into " + table + "(" + nameid + ") values(" + id.ToString() + ")", DB1);
                    DataTable DT1 = new DataTable();
                    DA1.Fill(DT1);
                    DB1.Close();
                }
            }
            
            
        }

        private void FormZap_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int prov = 0;
            switch(xx)
            {
                case 1:
                    OleDbConnection myConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
                    OleDbDataAdapter myAdapter = new OleDbDataAdapter("select * from " + tablename + "\"", myConnection);
                    DataSet dataSet = new DataSet();
                    myConnection.Open();
                    try
                    {
                        myAdapter.Fill(dataSet, "tabl");
                    }
                    catch
                    {
                        MessageBox.Show("Неверная SQL команда: либо Connection ");
                    }
                    finally { myConnection.Close(); }
                    if (textBox1.Text != "")
                    {
                        try
                        {
                            OleDbConnection DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
                            DB.Open();
                            OleDbDataAdapter DA = new OleDbDataAdapter("update " + tablename + " set " + dataSet.Tables[0].Columns[tekatr].ColumnName + "=\"" + textBox1.Text + "\" where " + nameid + "=" + id, DB);
                            DataTable DT = new DataTable();
                            DA.Fill(DT);
                            DB.Close();

                        }
                        catch
                        {
                            MessageBox.Show("Введите корректное значение!");
                            textBox1.Text = "";
                            prov = 1;
                        }
                    }
                    if (prov != 1)
                    {
                        tekatr++;
                        if (tekatr == dataSet.Tables[0].Columns.Count)
                        {
                            Admin f = this.Owner as Admin;
                            f.zakrZap(tablename);
                            this.Close();
                        }
                        if (tekatr < dataSet.Tables[0].Columns.Count)
                        {
                            label1.Text = dataSet.Tables[0].Columns[tekatr].ColumnName;

                        }
                        textBox1.Text = "";
                    }
                    break;
                case 2:
                    OleDbConnection myConnection2 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source =apteka.mdb");
                    OleDbDataAdapter myAdapter2 = new OleDbDataAdapter("select id_zakazch,Login,Password,who from Zakazchik", myConnection2);
                    DataSet dataSet2 = new DataSet();
                    myConnection2.Open();
                    try
                    {
                        myAdapter2.Fill(dataSet2, "tabl");
                    }
                    catch
                    {
                        MessageBox.Show("Неверная SQL команда: либо Connection ");
                    }
                    finally { myConnection2.Close(); }
                    if (textBox1.Text != "")
                    {
                        try
                        {
                            OleDbConnection DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
                            DB.Open();
                            OleDbDataAdapter DA = new OleDbDataAdapter("update " + tablename + " set " + dataSet2.Tables[0].Columns[tekatr].ColumnName + "=\"" + textBox1.Text + "\" where " + nameid + "=" + id, DB);
                            DataTable DT = new DataTable();
                            DA.Fill(DT);
                            DB.Close();

                        }
                        catch
                        {
                            MessageBox.Show("Введите корректное значение!");
                            textBox1.Text = "";
                            prov = 1;
                        }
                    }
                    if (prov != 1)
                    {
                        tekatr++;
                        if (tekatr == dataSet2.Tables[0].Columns.Count)
                        {
                            switch(xx)
                            {
                                case 1:
                                    Admin f = this.Owner as Admin;

                                    f.zakrZap(tablename);
                                    break;
                                case 2:
                                    users us = this.Owner as users;
                                    us.zakrZap1();
                                    break;
                                case 3:
                                    users us1 = this.Owner as users;
                                    us1.zakrZap2(tablename);
                                    break;
                            }
                            
                                
                            
                            
                            
                                
                            
                            this.Close();

                        }
                        if (tekatr < dataSet2.Tables[0].Columns.Count)
                        {
                            label1.Text = dataSet2.Tables[0].Columns[tekatr].ColumnName;

                        }




                        textBox1.Text = "";
                    }
                    break;
                case 3:
                    OleDbConnection myConnection3 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
                    OleDbDataAdapter myAdapter3 = new OleDbDataAdapter("select id_reg,Log,Pas from reg", myConnection3);
                    DataSet dataSet3 = new DataSet();
                    myConnection3.Open();
                    try
                    {
                        myAdapter3.Fill(dataSet3, "tabl");
                    }
                    catch
                    {
                        MessageBox.Show("Неверная SQL команда: либо Connection ");
                    }
                    finally { myConnection3.Close(); }
                    if (textBox1.Text != "")
                    {
                        try
                        {
                            OleDbConnection DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = Log.mdb");
                            DB.Open();
                            OleDbDataAdapter DA = new OleDbDataAdapter("update " + tablename + " set " + dataSet3.Tables[0].Columns[tekatr].ColumnName + "=\"" + textBox1.Text + "\" where " + nameid + "=" + id, DB);
                            DataTable DT = new DataTable();
                            DA.Fill(DT);
                            DB.Close();

                        }
                        catch
                        {
                            MessageBox.Show("Введите корректное значение!");
                            textBox1.Text = "";
                            prov = 1;
                        }
                    }
                    if (prov != 1)
                    {
                        tekatr++;
                        if (tekatr == dataSet3.Tables[0].Columns.Count)
                        {
                            Admin f = this.Owner as Admin;

                            f.zakrZap(tablename);
                            this.Close();

                        }
                        if (tekatr < dataSet3.Tables[0].Columns.Count)
                        {
                            label1.Text = dataSet3.Tables[0].Columns[tekatr].ColumnName;

                        }




                        textBox1.Text = "";
                    }
                    break;

            }
            
            
               
            



        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(xx==1)
            {
                OleDbConnection DB1 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = apteka.mdb");
                DB1.Open();

                OleDbCommand drop_command = new OleDbCommand("delete from " + tablename + " where " + nameid + "=" + id.ToString(), DB1);
                if (drop_command.ExecuteNonQuery() == 0)
                {
                    MessageBox.Show("Запись отменена!");
                }

                DB1.Close();
            }
            
            this.Close();
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
    }
}
