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
using System.Linq.Expressions;

namespace Toll_Plaza_System
{
    public partial class Form2 : Form
    {
        //Define Objects for Accessing Data From Database
        OleDbConnection conn;
        OleDbCommand cmd;
        OleDbDataAdapter dataAdapter;
        OleDbDataReader reader;
        DataTable dt;
        public Form2()
        {
            InitializeComponent();
        }
        // Create a function for showing data in gridview
        void GetVehicle()
        {
            // Build connection by connecting your project with database file 
            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Vehicle.accdb");
            // Access table from database
            dt = new DataTable();
            // Perform Select query
            dataAdapter = new OleDbDataAdapter("SELECT *FROM Vehicles", conn);
            // Open the Connection
            conn.Open();
            // Pass through dataAdapter object
            dataAdapter.Fill(dt);
            // Now store data in grid view
            dataGridView1.DataSource = dt;
            // Close the connection
            conn.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Now call the function
            GetVehicle();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Now perform Insert query
            string query = "INSERT INTO Vehicles(VEHICLENUMBER, VEHICLENAME, VEHICLETYPE, TOLLAMOUNT)" +
                "VALUES (@num, @name, @type, @amount )";
            // Now take the new enetered data into gridview
            cmd= new OleDbCommand(query, conn);
            cmd.Parameters.AddWithValue("@num", NumberBox.Text);
            cmd.Parameters.AddWithValue("@name", VehicleCombo.Text);
            // Check if the user click on LTV or HTV Button
            if(radioButton1.Checked ) 
            {
                cmd.Parameters.AddWithValue("@type", radioButton1.Text);
            } else
            {
                cmd.Parameters.AddWithValue("@type", radioButton2.Text);
            }
            cmd.Parameters.AddWithValue("@amount", ComboAmount.Text);
            // Open the connection
            conn.Open();
            // Execute query
            cmd.ExecuteNonQuery();
            // Close the connection
            conn.Close();
            // Now show the user that the date is entered into the gridview
            MessageBox.Show("Registered Successfully");
            // Also Call the function
            GetVehicle();

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Show the inserted data in textboxes, comboboxes and radiobuttons 
            NumberBox.Text= dataGridView1.CurrentRow.Cells[0].Value.ToString();
            VehicleCombo.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            ComboAmount.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Now Update the inserted data by performing Update query
            string query = "UPDATE Vehicles " +
                "SET TOLLAMOUNT = @amount " +
                "WHERE VEHICLENUMBER= @num";
            // Now Reuse the code from "Insert Query" Module
            cmd = new OleDbCommand(query, conn);
            cmd.Parameters.AddWithValue("@amount",ComboAmount.Text);
            cmd.Parameters.AddWithValue("@num", NumberBox.Text);
            //Open the connection
            conn.Open();
            //Execute query
            cmd.ExecuteNonQuery();
            //Close the connection
            conn.Close();
            //Now show the user that the date is entered into the gridview
            MessageBox.Show("Toll Amount is Updated");
            //Also Call the function
            GetVehicle();
        }


        private void btnDel_Click(object sender, EventArgs e)
        {
            // Now Delete functionality has to be performed
            string query = "DELETE FROM Vehicles " +
                "WHERE VEHICLENUMBER = @num";
            cmd= new OleDbCommand(query, conn);
            cmd.Parameters.AddWithValue("@num", NumberBox.Text);
            conn.Open();
            // Execute query
            cmd.ExecuteNonQuery();
            // Close the connection
            conn.Close();
            // Now show the user that the date is entered into the gridview
            MessageBox.Show("Record is Deleted");
            // Also Call the function
            GetVehicle();

        }
    }
}
