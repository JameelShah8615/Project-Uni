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

namespace Toll_Plaza_System
{
    public partial class Form4 : Form
    {
        //Create Objects For accessing database
        OleDbConnection conn;
        OleDbCommand cmd;
        OleDbDataReader reader;
        OleDbDataAdapter adapter;
        DataTable dt;
        public Form4()
        {
            InitializeComponent();
        }
        //Create a function to show data in database
        void getVehicles()
        {
            //Establish connection with the database file
            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Vehicle.accdb");
            //Access data from database
            dt = new DataTable();
            //Implement Select query
            adapter= new OleDbDataAdapter("SELECT * FROM Vehicles", conn);
            //Now open the connection
            conn.Open();
            //Passs the dt object through adapter object
            adapter.Fill(dt);
            //Store data in the gridview
            dataGridView1.DataSource = dt;
            //Close the connection
            conn.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            //Now call the function
            getVehicles();
            //Subscribe to the databinding event  
            dataGridView1.DataBindingComplete += dataGridView1_DataBindingComplete;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //First initialize the values of desired coloumns to Zero
            int totalVehicles = 0;
            int vehicleAmount= 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //Check the indexes of the columns
                if (row.Cells[1].Value != null && row.Cells[1].Value.ToString() != string.Empty)
                {
                    totalVehicles++;
                }

                if (row.Cells[3].Value != null && row.Cells[3].Value.ToString() != string.Empty)
                {
                    vehicleAmount += Convert.ToInt32(row.Cells[3].Value);
                }
            }
            //Display the desired results in the textboxes
            TotalCars.Text = totalVehicles.ToString();
            TotalAmount.Text = vehicleAmount.ToString(); 
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            TotalCars.Text = "";
            TotalAmount.Text = "";
        }
    }
}
