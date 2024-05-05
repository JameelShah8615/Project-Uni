using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toll_Plaza_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnVehicle_Click(object sender, EventArgs e)
        {
            //Access Vehicle Registration Form
            Form2 f2= new Form2();
            f2.Show();
        }

        private void btnToll_Click(object sender, EventArgs e)
        {
            //Access Toll Collection Form
            Form f3= new Form3();
            f3.Show();
        }

        private void btnRecords_Click(object sender, EventArgs e)
        {
            //Access Records Form
            Form f4 = new Form4();
            f4.Show();
        }
    }
}
