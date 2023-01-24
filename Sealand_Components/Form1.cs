using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Sealand_Components
{
    public partial class Form1 : Form
    {

        private SQLiteConnection sqlConn;
        private SQLiteCommand sqlCmd;
        private DataTable sqlDT = new DataTable();
        private DataSet DS = new DataSet();
        private SQLiteDataAdapter DB;
         
        public Form1()
        {
            InitializeComponent();
            uploadData();

            Random rnd = new Random();
            int iRef = rnd.Next(2975, 32576);
            textBox1.Text = "Comp" + Convert.ToString(iRef);
        }

        private void SetConnectDB()
        {
            sqlConn = new SQLiteConnection("Data Source = C:\\Users\\edward.stewart\\OneDrive - Sealand Projects Ltd\\Desktop\\Sealand_Components_Solution\\Sealand_Components\\bin\\Debug\\details.db");

            //var datasource = "C:\\Users\\edward.stewart\\OneDrive - Sealand Projects Ltd\\Desktop\\Sealand_Components_Solution\\Sealand_Components\\bin\\Debug\\details.db";//your server
            //var database = "details"; //your database name
            //var username = ""; //username of server to connect
            //var password = ""; //password

            ////your connection string 
            //string connString = @"Data Source=" + datasource + ";Initial Catalog="
            //            + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

            ////create instanace of database connection
            //sqlConn = new SQLiteConnection(connString);


            //try
            //{
            //    Console.WriteLine("Openning Connection ...");

            //    //open connection
            //    sqlConn.Open();
                
            //    Console.WriteLine("Connection successful!");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Error: " + e.Message);
            //}

            //Console.Read();
        
        }



        private void ExecuteQqery(string QueryData)
        {
            SetConnectDB();
            sqlConn.Open();
            sqlCmd = sqlConn.CreateCommand();
            sqlCmd.CommandText = QueryData;
            sqlCmd.ExecuteNonQuery();
            sqlCmd.Dispose();
            sqlConn.Close();
        }

        private void RefinedSearch(string QueryData)
        {
            SetConnectDB();
            sqlConn.Open();
            sqlCmd = sqlConn.CreateCommand();
            sqlCmd.CommandText = QueryData;
            sqlCmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SQLiteDataAdapter da = new SQLiteDataAdapter(sqlCmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlCmd.Dispose();
            sqlConn.Close();
        }

        private void uploadData()
        {
            SetConnectDB();
            sqlConn.Open();
            sqlCmd = sqlConn.CreateCommand();
            string CommandText = "Select * from details";
            DB = new SQLiteDataAdapter(CommandText, sqlConn);
            DS.Reset();
            DB.Fill(DS);
            sqlDT = DS.Tables[0];
            dataGridView1.DataSource = sqlDT;
            sqlConn.Close();
        }

       //Exit button

        private void button6_Click(object sender, EventArgs e) 
        {
            try
            {
                if (MessageBox.Show("Confirm if you want to exit", "Data Entry System",MessageBoxButtons.YesNo,MessageBoxIcon.Question)
                    ==DialogResult.Yes )
                {
                    Application.Exit();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Data Entry System", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control txt in panel3.Controls)
                {
                    if (txt is TextBox)
                        ((TextBox)txt).Clear();
                }

                foreach (Control cbo in panel3.Controls)
                {
                    if (cbo is ComboBox)
                        ((ComboBox)cbo).Text = "";
                }

                dateTimePicker1.ResetText();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Data Entry System", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Random rnd = new Random();
            int iRef = rnd.Next(2975, 32576);
            textBox1.Text = "Comp" + Convert.ToString(iRef);
        }

        Bitmap bitmap;
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                int height = dataGridView1.Height;
                dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height * 2;
                bitmap = new Bitmap(dataGridView1.Width, dataGridView1.Height);
                dataGridView1.DrawToBitmap(bitmap, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
                printPreviewDialog1.PrintPreviewControl.Zoom = 1;
                printPreviewDialog1.ShowDialog();
                dataGridView1.Height = height;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Data Entry System", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (sqlConn.State == ConnectionState.Closed)
                    sqlConn.Open();
                using (SQLiteDataAdapter DB = new SQLiteDataAdapter("select *from details", sqlConn))
                {
                    DataTable sqlDT = new DataTable("Firstname");
                    DB.Fill(sqlDT);
                    dataGridView1.DataSource = sqlDT;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Data Entry System", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //details is the database name

            string QueryData ="insert into details(CompRef, CompName, Price, Quantity, HireTime, Source, Date)  values  ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text
                + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + dateTimePicker1 + "')";

            ExecuteQqery(QueryData);
            uploadData();
        }

       
        private void button4_Click(object sender, EventArgs e)
        {
            string QueryData = "delete from details where CompRef = '" + textBox1.Text + "'";

                ExecuteQqery(QueryData);
            uploadData();
        }

       
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Data Entry System", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataView dv = sqlDT.DefaultView;
            dv.RowFilter = string.Format("Component like'%{0}%'", textBox7.Text);
            dataGridView1.DataSource = dv.ToTable();
        }
        //This method is fired by the KeyUp event handler on the textbox.
        //The purpose of this method is to take the text from the search
        //box, split it up into words, and then create and assign a filter
        //statement that will do a LIKE comparison on each of the selected
        //search fields. Each word's filter statement is AND'ed together
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            string outputInfo = "";
            string[] keyWords = textBox7.Text.Split(' ');

            foreach (string word in keyWords)
            {
                if (outputInfo.Length == 0)
                {
                    outputInfo = "(compName LIKE '%" + word + "%' OR Price LIKE '%" + word + "%' OR Source LIKE '%" + word + "%')";
                }
                else
                {
                    outputInfo += " AND (compName LIKE '%" + word + "%' OR Price LIKE '%" + word + "%' OR Source LIKE '%" + word + "%')";
                }
            }
            //Applies the filter to the DataView
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = outputInfo;

        }
                       
        
    }
}
