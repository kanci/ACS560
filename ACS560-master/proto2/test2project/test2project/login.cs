using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//this is the 2nd update of candy object in board..

namespace test2project {
    public partial class login : Form {
        public Form1 form1;

        public login() {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e) {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create a new instance of the Form2 class
            Form1 settingsForm = new Form1(textBox1.Text, textBox2.Text, "login");

            // Show the settings form
            settingsForm.Show();
            
        }

        private void button2_Click(object sender, EventArgs e) {
            // Create a new instance of the Form2 class
            Form1 settingsForm = new Form1(textBox1.Text, textBox2.Text, "new");

            // Show the settings form
            settingsForm.Show();
        }
        private void textBox1_TextChanged(object sender, EventArgs e) {

        }
    }
}
