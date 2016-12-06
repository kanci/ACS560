using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace candy3 {
    public class Board { //board class

        //added clickCount
        public static int clickCount;
        public int getClickCount()
        {
            return clickCount;
        }

        //number of count clicks
        public void setClickCount(int count)
        {
            Board.clickCount = count;
        }

        public static int candySize;

        //removed static in getCandySize
        public int getCandySize() {
            return candySize;
        }
        public void setCandySize(int size) {
            Board.candySize = size;
        }

        public static Candy[] newCandies;

        public bool ismatch(Candy candy1, Candy candy2)
        {
            if (candy1.getValue() == candy2.getValue())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Candy[] swapCandy(int firstCandy, int secondCandy) {
            Candy tempCandy = newCandies[firstCandy].getCandy();

            newCandies[firstCandy] = newCandies[secondCandy].getCandy();
            newCandies[secondCandy] = tempCandy.getCandy();
            return newCandies;
        }

        public bool isAdjacent(Candy candy1, Candy candy2) {
            if (candy1.getLocation() - 1 == candy2.getLocation()) {
                return true;
            }
            if (candy1.getLocation() + 1 == candy2.getLocation()) {
                return true;
            }
            if ((candy1.getLocation() % 8 == candy2.getLocation() % 8) && Math.Abs(candy1.getLocation() - candy2.getLocation()) == 8) {
                return true;
            }
            return false;
        }

        public Candy getCandy(int i)
        {
            return newCandies[i].getCandy();
        }

        public static void printBoard() {
            for (int i = 0; i < candySize; i++) {
                System.Diagnostics.Debug.Write(newCandies[i].getValue());

                if (i % 8 == 7)
                    System.Diagnostics.Debug.WriteLine("");
            }//init board loop, end for loop
        }//end printBoard method

        public Candy[] clearClicks(Candy[] clearBoard) {
            for (int i = 0; i < candySize; i++) {
                newCandies[i].getCandy().setNotClicked();
            }
            return clearBoard;
        }

        public Board(Candy[] board) {
            setCandySize(64);
            newCandies = new Candy[getCandySize()];

            for (int i = 0; i < candySize; i++) {
                newCandies[i] = new Candy((int)board[i].getCandy().getValue(), false, i, false);
            }
        }//end board constructor
 
    }//end board class
}