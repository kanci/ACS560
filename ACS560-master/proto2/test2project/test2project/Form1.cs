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


//this is the 2nd update of candy object in board..

namespace test2project {
    public partial class Form1 : Form {

        public Player newPlayer;

        public Form1(string loginst, string pass, string indo) {    
            this.newPlayer = new Player(loginst, pass, indo);
            
            InitializeComponent();
        }//end Form1 constructor taking in username and password
        
        public class Player {
            string playerlogin;
            string password;
            string operation;

            public Player(string newLogin, string newPassword, string indo) {
                this.playerlogin = newLogin;
                this.password = newPassword;
                this.operation = indo;
            }//end Player constructor

            public void printplayer() {
                System.Diagnostics.Debug.Write(playerlogin);
            }

            public string getOperation() {
                return this.operation;
            }

            public string getlogin() {
                return this.playerlogin;
            }

            public string getpass() {
                return this.password;
            }
        }//end Player class

        public class board { //board class
            Boolean clickStatus = new Boolean();

            Int64[] myboard = new Int64[64];
            Boolean[] isclicked = new Boolean[64];

            public board(Int64[] board) {
                //int[,] myboard = new int[4,4];

                for (int i = 0; i < 64; i++) {
                    myboard[i] = board[i];
                    isclicked[i] = false;
                }//init board loop
            }//end board constructor

            public void printBoard() {
                for (int i = 0; i < 64; i++) {
                    System.Diagnostics.Debug.WriteLine(myboard[i] + " " + i + " is for index");

                    if (i % 8 == 7)
                        System.Diagnostics.Debug.WriteLine("");
                }//init board loop, end for loop


            }//end printBoard method
            
            public void setClickStatus() {
                clickStatus = true;
            }

            public void setUnClickStatus() {
                clickStatus = false;
            }

            public Boolean getClickStatus() {
                if (clickStatus)
                    return true;
                else
                    return false;
            }

            public Boolean isClicked(int val)
            {
                if (isclicked[val])
                {
                    return true;
                }

                return false;
            }

            public int getvalue(int val) {
                return (int)myboard[val];//conversion cast to use int, seems better
            }//end getValue method
        }//end board class


        private void Form1_Load(object sender, EventArgs e) {

            //some issues on how click new button.. should need to create new user and password

            string urlstring = "http://52.24.237.185/"+newPlayer.getOperation().ToString()+"?user=" + newPlayer.getlogin() + "&pass=" + newPlayer.getpass();
            
            

            WebClient client = new WebClient();
            Stream stream = client.OpenRead(urlstring);
            StreamReader reader = new StreamReader(stream);

            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(reader.ReadLine());
            

            // instead of WriteLine, 2 or 3 lines of code here using WebClient to download the file


            System.Diagnostics.Debug.WriteLine(jObject.First.ToString().Substring(1,5));


            stream.Close();

            if (jObject.First.ToString().Substring(1, 5) != "Error".ToString())
            {
                System.Diagnostics.Debug.WriteLine("Close form");


                Int64[] tempboard = new Int64[64];

                for (int i = 0; i < tempboard.Length; i++)
                {

                    //this creates a problem as it invalid operation exception in Newtonsoft.Json.dll
                    tempboard[i] = (Int64)jObject.Root["Board"][i];

                }//end for loop


                board gameboard = new board(tempboard);
                //test printing
                //gameboard.printBoard();

                Button[] buttons = Controls.OfType<Button>().OrderBy(b => b.TabIndex).ToArray();
                int tempbutton = 0;

                //buttons[1].Click += (sender1, ex) => System.Console.WriteLine("button " + 1 + " pressed");

                setOnClick(buttons);

                for (int i = 0; i < tempboard.Length; i++)
                {
                    tempbutton = gameboard.getvalue(i);

                    //cheat to create the click handlers for setOnClick()
                    //System.Console.WriteLine("button[" + i + "].Click += (sender1, ex) => System.Console.WriteLine(\"button \" + " + i + " + \" pressed\");");

                    buttons[i].Text = "" + tempbutton;




                    if (tempbutton == 1)
                    {


                        //it does not work while candies inside test2project, only on C drive


                        //   buttons[i].BackgroundImage = (Bitmap)Image.FromFile(@"C:\tmp\candy1.bmp", true);//there seems to be issue with buttons as it is not working..
                        buttons[i].BackgroundImage = (Bitmap)Image.FromFile(@"C:\Users\User\Desktop\candies\candies1.bmp", true);

                    }
                    else if (tempbutton == 2)
                    {
                        //    buttons[i].BackgroundImage = (Bitmap)Image.FromFile(@"C:\tmp\candy2.bmp", true);//there seems to be issue with buttons as it is not working..
                        buttons[i].BackgroundImage = (Bitmap)Image.FromFile(@"C:\Users\User\Desktop\candies\candies2.bmp", true);
                    }
                    else if (tempbutton == 3)
                    {
                        //     buttons[i].BackgroundImage = (Bitmap)Image.FromFile(@"C:\tmp\candy3.bmp", true);//there seems to be issue with buttons as it is not working..
                        buttons[i].BackgroundImage = (Bitmap)Image.FromFile(@"C:\Users\User\Desktop\candies\candies3.bmp", true);
                    }
                    else if (tempbutton == 4)
                    {
                        //      buttons[i].BackgroundImage = (Bitmap)Image.FromFile(@"C:\tmp\candy4.bmp", true);//there seems to be issue with buttons as it is not working..
                        buttons[i].BackgroundImage = (Bitmap)Image.FromFile(@"C:\Users\User\Desktop\candies\candies4.bmp", true);
                    }

                }//end for
            }//end json error block

            //added the else statement to make clear
            else
            {
                this.Close();

                MessageBox.Show("Bad user name or password");
            }
        }//end form 1 load method
        
        //for Login button..
        private void button1_Click(object sender, EventArgs e) {
            //System.Console.WriteLine("button1 pressed");
           // System.Console.WriteLine(sender.ToString());
            
        }
        private Button[] setOnClick(Button[] button)
        {

            button[0].Click += (sender1, ex) => System.Console.WriteLine("button " + 0 + " pressed");
            button[1].Click += (sender1, ex) => System.Console.WriteLine("button " + 1 + " pressed");
            button[2].Click += (sender1, ex) => System.Console.WriteLine("button " + 2 + " pressed");
            button[3].Click += (sender1, ex) => System.Console.WriteLine("button " + 3 + " pressed");
            button[4].Click += (sender1, ex) => System.Console.WriteLine("button " + 4 + " pressed");
            button[5].Click += (sender1, ex) => System.Console.WriteLine("button " + 5 + " pressed");
            button[6].Click += (sender1, ex) => System.Console.WriteLine("button " + 6 + " pressed");
            button[7].Click += (sender1, ex) => System.Console.WriteLine("button " + 7 + " pressed");
            button[8].Click += (sender1, ex) => System.Console.WriteLine("button " + 8 + " pressed");
            button[9].Click += (sender1, ex) => System.Console.WriteLine("button " + 9 + " pressed");
            button[10].Click += (sender1, ex) => System.Console.WriteLine("button " + 10 + " pressed");
            button[11].Click += (sender1, ex) => System.Console.WriteLine("button " + 11 + " pressed");
            button[12].Click += (sender1, ex) => System.Console.WriteLine("button " + 12 + " pressed");
            button[13].Click += (sender1, ex) => System.Console.WriteLine("button " + 13 + " pressed");
            button[14].Click += (sender1, ex) => System.Console.WriteLine("button " + 14 + " pressed");
            button[15].Click += (sender1, ex) => System.Console.WriteLine("button " + 15 + " pressed");
            button[16].Click += (sender1, ex) => System.Console.WriteLine("button " + 16 + " pressed");
            button[17].Click += (sender1, ex) => System.Console.WriteLine("button " + 17 + " pressed");
            button[18].Click += (sender1, ex) => System.Console.WriteLine("button " + 18 + " pressed");
            button[19].Click += (sender1, ex) => System.Console.WriteLine("button " + 19 + " pressed");
            button[20].Click += (sender1, ex) => System.Console.WriteLine("button " + 20 + " pressed");
            button[21].Click += (sender1, ex) => System.Console.WriteLine("button " + 21 + " pressed");
            button[22].Click += (sender1, ex) => System.Console.WriteLine("button " + 22 + " pressed");
            button[23].Click += (sender1, ex) => System.Console.WriteLine("button " + 23 + " pressed");
            button[24].Click += (sender1, ex) => System.Console.WriteLine("button " + 24 + " pressed");
            button[25].Click += (sender1, ex) => System.Console.WriteLine("button " + 25 + " pressed");
            button[26].Click += (sender1, ex) => System.Console.WriteLine("button " + 26 + " pressed");
            button[27].Click += (sender1, ex) => System.Console.WriteLine("button " + 27 + " pressed");
            button[28].Click += (sender1, ex) => System.Console.WriteLine("button " + 28 + " pressed");
            button[29].Click += (sender1, ex) => System.Console.WriteLine("button " + 29 + " pressed");
            button[30].Click += (sender1, ex) => System.Console.WriteLine("button " + 30 + " pressed");
            button[31].Click += (sender1, ex) => System.Console.WriteLine("button " + 31 + " pressed");
            button[32].Click += (sender1, ex) => System.Console.WriteLine("button " + 32 + " pressed");
            button[33].Click += (sender1, ex) => System.Console.WriteLine("button " + 33 + " pressed");
            button[34].Click += (sender1, ex) => System.Console.WriteLine("button " + 34 + " pressed");
            button[35].Click += (sender1, ex) => System.Console.WriteLine("button " + 35 + " pressed");
            button[36].Click += (sender1, ex) => System.Console.WriteLine("button " + 36 + " pressed");
            button[37].Click += (sender1, ex) => System.Console.WriteLine("button " + 37 + " pressed");
            button[38].Click += (sender1, ex) => System.Console.WriteLine("button " + 38 + " pressed");
            button[39].Click += (sender1, ex) => System.Console.WriteLine("button " + 39 + " pressed");
            button[40].Click += (sender1, ex) => System.Console.WriteLine("button " + 40 + " pressed");
            button[41].Click += (sender1, ex) => System.Console.WriteLine("button " + 41 + " pressed");
            button[42].Click += (sender1, ex) => System.Console.WriteLine("button " + 42 + " pressed");
            button[43].Click += (sender1, ex) => System.Console.WriteLine("button " + 43 + " pressed");
            button[44].Click += (sender1, ex) => System.Console.WriteLine("button " + 44 + " pressed");
            button[45].Click += (sender1, ex) => System.Console.WriteLine("button " + 45 + " pressed");
            button[46].Click += (sender1, ex) => System.Console.WriteLine("button " + 46 + " pressed");
            button[47].Click += (sender1, ex) => System.Console.WriteLine("button " + 47 + " pressed");
            button[48].Click += (sender1, ex) => System.Console.WriteLine("button " + 48 + " pressed");
            button[49].Click += (sender1, ex) => System.Console.WriteLine("button " + 49 + " pressed");
            button[50].Click += (sender1, ex) => System.Console.WriteLine("button " + 50 + " pressed");
            button[51].Click += (sender1, ex) => System.Console.WriteLine("button " + 51 + " pressed");
            button[52].Click += (sender1, ex) => System.Console.WriteLine("button " + 52 + " pressed");
            button[53].Click += (sender1, ex) => System.Console.WriteLine("button " + 53 + " pressed");
            button[54].Click += (sender1, ex) => System.Console.WriteLine("button " + 54 + " pressed");
            button[55].Click += (sender1, ex) => System.Console.WriteLine("button " + 55 + " pressed");
            button[56].Click += (sender1, ex) => System.Console.WriteLine("button " + 56 + " pressed");
            button[57].Click += (sender1, ex) => System.Console.WriteLine("button " + 57 + " pressed");
            button[58].Click += (sender1, ex) => System.Console.WriteLine("button " + 58 + " pressed");
            button[59].Click += (sender1, ex) => System.Console.WriteLine("button " + 59 + " pressed");
            button[60].Click += (sender1, ex) => System.Console.WriteLine("button " + 60 + " pressed");
            button[61].Click += (sender1, ex) => System.Console.WriteLine("button " + 61 + " pressed");
            button[62].Click += (sender1, ex) => System.Console.WriteLine("button " + 62 + " pressed");
            button[63].Click += (sender1, ex) => System.Console.WriteLine("button " + 63 + " pressed");
            return button;
        }
    }
}
