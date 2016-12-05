using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace candy3
{
    public class Candy
    {
        int cValue;
        bool isClicked;
        int location;
        public Candy(int cValue, bool isClicked, int location)
        {
            this.cValue = cValue;
            this.isClicked = isClicked;
            this.location = location;
        }

        //adding getters and setters
        public Candy getCandy()
        {
            return this;
        }

        public bool isClick()
        {
            return this.isClicked;
        }

        public void setClicked()
        {
            this.isClicked = true;
        }

        public void setNotClicked()
        {
            this.isClicked = false;
        }

        public int getLocation()
        {
            return this.location;
        }

        public void setLocation(int loc)
        {
            this.location = loc;
        }

        public void setValue(int val)
        {
            this.cValue = val;
        }

        public int getValue()
        {
            return this.cValue;
        }
    } //class candy
}
