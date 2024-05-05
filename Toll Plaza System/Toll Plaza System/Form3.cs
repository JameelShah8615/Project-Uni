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
    public partial class Form3 : Form
    {
        //Define objects for accessiing database
        OleDbConnection connection;
        OleDbCommand command;
        OleDbDataReader rd;
        OleDbDataAdapter ad;
        DataTable data;

        public Form3()
        {
            InitializeComponent();
        }
        // Create function for to show data in grid view
        void GetToll()
        {
            //Establish connection with the database file
            using (connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source= TollCollection.accdb"))
            {
                //Now access data table from database
                data = new DataTable();
                //Implement "Select" query
                using (OleDbDataAdapter ad = new OleDbDataAdapter("SELECT * FROM [TOLL]", connection))
                {
                    //Now open the connection
                    connection.Open();
                    //Now pass through 'ad' object
                    ad.Fill(data);
                    //Now store data in grid view
                    dataGridView1.DataSource = data;
                    //Now close the connection
                    connection.Close();
                }
            }

        }
        private void Form3_Load(object sender, EventArgs e)
        {
            //Now call the function
            GetToll();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Also Establish connection with the database file
            connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source= TollCollection.accdb");
            //Now Implement "Insert" Query
            string query = "INSERT INTO [TOLL] (VEHICLENUMBER, ENTERINGTIME, LEAVINGTIME, TOLLCOLLECTED)" +
                "VALUES( @plate, @ent, @leav, @fee) ";
            //Now insert the data
            command= new OleDbCommand(query, connection);
            command.Parameters.AddWithValue("@plate", Numtext.Text);
            command.Parameters.AddWithValue("@ent", dateTimePicker1.Value);
            command.Parameters.AddWithValue("@leav", dateTimePicker2.Value);
            command.Parameters.AddWithValue("@fee", AmountCombo.Text);
            //Now open the connection
            connection.Open();
            //Execute query
            command.ExecuteNonQuery();
            //Now close the connection
            connection.Close();
            //Print a message to the user that the data is inserted
            MessageBox.Show("Saved Successfully");
            //Now call the function
            GetToll();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source= TollCollection.accdb");
            //Impement "Delete" query
            string query = "DELETE FROM [TOLL] " +
                "WHERE VEHICLENUMBER= @plate";
            command = new OleDbCommand(query, connection);
            command.Parameters.AddWithValue("@plate", Numtext.Text);
            connection.Open();
            //Execute query
            command.ExecuteNonQuery();
            //Close the connection
            connection.Close();
            //Print a message to the user that the record is deleted
            MessageBox.Show("Deleted Successfully");
            //Now call the function
            GetToll();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Show the inserted data in gridview
            Numtext.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            dateTimePicker2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            AmountCombo.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnRec_Click(object sender, EventArgs e)
        {
            //First Generate a Reciept
            richTextBox1.Clear();
            //Craete a method for some "**" characters
            richTextBox1.Text += "**************************\n";
            //Now create a method for the title of the project
            richTextBox1.Text += "****  Toll Plaza System    ****\n";
            //Now create a method for some other "**" characters
            richTextBox1.Text += "**************************\n";
            //Now create a method for Vehicle Number
            richTextBox1.Text += "Vehicle NO: " + Numtext.Text + "\n\n";
            //Now create a method for Entering time 
            richTextBox1.Text += "Entering Time: " + dateTimePicker1.Text + "\n\n";
            //Now create a method for leaving time
            richTextBox1.Text += "Leaving Time: " + dateTimePicker2.Text + "\n\n";
            //Now create a method for the amount collected as toll tax
            richTextBox1.Text += "Toll Tax: " + AmountCombo.Text + "\n\n";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //Now clear all textboxes, datetimepickers and comboboxes
            Numtext.Text= "";
            dateTimePicker1.Text= "";
            dateTimePicker2.Text= "";
            AmountCombo.Text= "";
            richTextBox1.Text = "";
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Now write a code for printing the reciept
            e.Graphics.DrawString(richTextBox1.Text, new Font("Calibri", 18,FontStyle.Bold),Brushes.Black, new Point(10,10));

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //Now write code for print button
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }
    }
}
