using System;
using System.Collections.Generic;
//even when "using System.Collections; still get error
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using System.IO;


namespace test2project {
    public partial class Form1 : Form {

        public Form1(string loginst, string pass) {

            InitializeComponent();
            Player newplayer = new Player(loginst, pass);
        }//end Form1 constructor taking in username and password


        public class Player {
            string playerlogin;
            string password;

            public Player(string asdf, string gfasdg) {
                this.playerlogin = asdf;
                this.password = gfasdg;
            }//end Player constructor
            public void printplayer() {
                System.Diagnostics.Debug.Write(playerlogin);
            }
            public string getlogin() {
                return playerlogin;
            }
            public string getpass() {
                return password;
            }
        }//end Player class

        public class board { //board class

            //int[] myboard = new int[32];


                

            Int64[] myboard = new Int64[64];
           
            public board(Int64[] board) {
                //int[,] myboard = new int[4,4];

                for (int i = 0; i < 64; i++) {
                        myboard[i] = board[i];
                }//init board loop
                

            }//end board constructor

            public void printBoard() {
                for (int i = 0; i < 64; i++) {
                    System.Diagnostics.Debug.WriteLine(myboard[i] + " " + i + " is for index");

                    if (i % 8 == 7)
                        System.Diagnostics.Debug.WriteLine("");
                }//init board loop, end for loop


            }//end printBoard method
            
            public int getvalue(int val) {
                return (int)myboard[val];//conversion cast to use int, seems better
            }//end getValue method
        }//end board class


        private void Form1_Load(object sender, EventArgs e) {
            string urlstring = "http://52.24.237.185/login?user=test&pass=test123123";

            //urlstring +=
            

            WebClient client = new WebClient();
            Stream stream = client.OpenRead(urlstring);
            StreamReader reader = new StreamReader(stream);

            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(reader.ReadLine());
            

            // instead of WriteLine, 2 or 3 lines of code here using WebClient to download the file


            System.Diagnostics.Debug.WriteLine(jObject.Root["Board"]);


            stream.Close();
            //Int64[][] tempboard = new Int64[8][];


            Int64[] tempboard = new Int64[64];
            
            for (int i=0; i<tempboard.Length; i++) {
                
                    //this creates a problem as it invalid operation exception in Newtonsoft.Json.dll
                    tempboard[i] = (Int64)jObject.Root["Board"][i];
                
            }//end for loop


            board gameboard = new board(tempboard);
            //test printing
            gameboard.printBoard();

            Button[] buttons = this.Controls.OfType<Button>().ToArray();
            int tempbutton = 0;

            

            for (int i = 0; i < tempboard.Length; i++) { 
                    tempbutton = gameboard.getvalue(i);

                System.Console.WriteLine(tempbutton+" tempbutton " + i + " is the i");


                    buttons[i].Text = "" + tempbutton;

                    if (tempbutton == 1) {
                    

                    //it does not work while candies inside test2project, only on C drive


                    buttons[i].BackgroundImage = new Bitmap(@"C:\Users\User\Desktop\candies\candies1.bmp");//there seems to be issue with buttons as it is not working..

                }
                     else if (tempbutton == 2) {
                    buttons[i].BackgroundImage = new Bitmap(@"C:\Users\User\Desktop\candies\candies2.bmp");//there seems to be issue with buttons as it is not working..
                }
                                else if (tempbutton == 3)
                                  {
                    buttons[i].BackgroundImage = new Bitmap(@"C:\Users\User\Desktop\candies\candies3.bmp");//there seems to be issue with buttons as it is not working..
                }
                    else if (tempbutton == 4)
                {
                    buttons[i].BackgroundImage = new Bitmap(@"C:\Users\User\Desktop\candies\candies4.bmp");//there seems to be issue with buttons as it is not working..
                }            
 
            }//end for

        }//end form 1 load method
        
        //for Login button..
        private void button1_Click(object sender, EventArgs e) {

        }
    }
}
