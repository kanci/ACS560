using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace test2project
{
    public partial class Form1 : Form {
        public Form1(string loginst, string pass) {
            InitializeComponent();
            Player newplayer = new Player(loginst, pass);
        }


        public class Player {
            string playerlogin;
            string password;

            public Player(string asdf, string gfasdg) {
                this.playerlogin = asdf;
                this.password = gfasdg;
            }
            public void printplayer() {
                System.Diagnostics.Debug.Write(playerlogin);
            }
            public string getlogin() {
                return playerlogin;
            }
            public string getpass() {
                return password;
            }
        }

        public class board {
            int[] myboard = new int[32];
            public board(int[] board) {
                //int[,] myboard = new int[4,4];

                for (int i = 0; i < 32; i++) {
                        myboard[i] = board[i];
                }//init board loop
            }//end board constructor

            public void printBoard() {
                for (int i = 0; i < 32; i++) {
                    System.Diagnostics.Debug.Write(myboard[i]);

                    if (i % 8 == 7)
                        System.Diagnostics.Debug.WriteLine("");
                }//init board loop, end for loop
            }

            public int getvalue(int val) {
                return myboard[val];
            }
        }


        private void Form1_Load(object sender, EventArgs e) {
            string urlstring = "http://52.24.237.185/login?user=";

            //urlstring +=
            

            WebClient client = new WebClient();
            Stream stream = client.OpenRead("http://52.24.237.185/login?user=test&pass=test123123");
            StreamReader reader = new StreamReader(stream);

            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(reader.ReadLine());

            // instead of WriteLine, 2 or 3 lines of code here using WebClient to download the file


            System.Diagnostics.Debug.WriteLine(jObject.Root["Board"]);


            stream.Close();
            int[] tempboard = new int[32];
            

            for (int i=0; i<32; i++) {
                tempboard[i] = (int)jObject.Root["Board"][i];

            }


            board gameboard = new board(tempboard);
            //test printing
            //gameboard.printBoard();

            Button[] buttons = this.Controls.OfType<Button>().ToArray();
            int tempbutton = 0;

            for (int i = 0; i < 32; i++) {
                tempbutton = gameboard.getvalue(i);
                buttons[i].Text = ""+tempbutton;

                if (tempbutton==1)
                    buttons[i].BackgroundImage = new Bitmap("candies/candies1.bmp");
                if (tempbutton == 2)
                    buttons[i].BackgroundImage = new Bitmap("candies/candies2.bmp");
                if (tempbutton == 3)
                    buttons[i].BackgroundImage = new Bitmap("candies/candies3.bmp");
                if (tempbutton == 4)
                    buttons[i].BackgroundImage = new Bitmap("candies/candies4.bmp");






                //maybe possible "C:\\Users\\User\\Downloads\\candies1.bmp" but i deleted it

            }

        }

        //for Login button..
        private void button1_Click(object sender, EventArgs e) {

        }
    }
}
