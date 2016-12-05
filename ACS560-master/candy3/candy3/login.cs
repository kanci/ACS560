using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace candy3
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create a new instance of the Form2 class
            game settingsForm = new game(textBox1.Text, textBox2.Text, "login");

            // Show the settings form
            settingsForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
                // Create a new instance of the Form2 class
                game settingsForm = new game(textBox1.Text, textBox2.Text, "new");

                // Show the settings form
                settingsForm.Show();
            
        }
    }
}

