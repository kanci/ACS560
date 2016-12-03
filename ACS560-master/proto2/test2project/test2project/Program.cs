using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


//this is the 2nd update of candy object in board..

namespace test2project {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            string user = "";
            string pass = "";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new login());

          //  Application.Run(new Form1(user, pass));
        }
    }
}
