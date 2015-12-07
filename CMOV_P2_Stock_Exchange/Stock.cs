using System.Diagnostics;

namespace CMOV_P2_Stock_Exchange
{
    public class Stock
    {
        private string ticket, name;
        private float high, low;
        private bool active;


        public Stock(string t, string n)
        {
            ticket = t;
            name = n;
        }

        
        public Stock(string full)
        {
            // example : Alphabet Inc. (GOOG)

            int nameLen = 0, ticketLen = 0;

            //Debug.WriteLine("Full:" + full);

            nameLen = full.IndexOf("(")-1;
            //Debug.WriteLine("nameLen:" + nameLen + " // " + full.Length + " // " + full.IndexOf("("));

            ticketLen = full.Length - nameLen - 3;
            //Debug.WriteLine("ticketLen:" + ticketLen);

            name = full.Substring(0, nameLen);
            ticket = full.Substring(full.IndexOf("(") + 1, ticketLen);
        }

        public string getTicket()
        {
            return ticket;
        }

        public string getName()
        {
            return name;
        }

        public string display()
        {
            return name + " (" + ticket + ")";
        }

        public float getHigh()
        {
            return high;
        }

        public float getLow()
        {
            return low;
        }

        public void setHigh(float h)
        {
            high = h;
        }

        public void setLow(float l)
        {
            low = l;
        }

        public bool isActive()
        {
            return active;
        }

        public void setActive(bool b)
        {
            active = b;
        }

    }


}