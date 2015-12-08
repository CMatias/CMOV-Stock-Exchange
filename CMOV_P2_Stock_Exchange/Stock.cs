using System.Collections.Generic;
using System.Diagnostics;

namespace CMOV_P2_Stock_Exchange
{
    public class Stock
    {
        private string ticket, name;
        private float high, low, current;
        private bool active;


        public Stock(string t, string n)
        {
            ticket = t;
            name = n;
        }

        public Stock(string t, string n, float h, float l)
        {
            ticket = t;
            name = n;
            high = h;
            low = l;
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

        public float getCurrent()
        {
            return current;
        }

        public void setCurrent(float c)
        {
            current = c;
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

        public static List<Stock> toList(string s)
        {
            List<Stock> aux = new List<Stock>();
            string[] array = s.Split('/');

            for (int i = 0; i < array.Length; i++)
            {
                string t, n, h, l;

                //Debug.WriteLine(array[i]);

                t = array[i].Substring(array[i].IndexOf("ticket:") + 7);
                t = t.Remove(t.IndexOf(",name:"));

                n = array[i].Substring(array[i].IndexOf(",name:") + 6);
                n = n.Remove(n.IndexOf(",h:"));

                h = array[i].Substring(array[i].IndexOf(",h:") + 3);
                h = h.Remove(h.IndexOf(",l:"));

                l = array[i].Substring(array[i].IndexOf(",l:") + 3, array[i].Length - (array[i].IndexOf(",l:") + 3));

                //Debug.WriteLine(t);
                //Debug.WriteLine(n);
                //Debug.WriteLine(h);
                //Debug.WriteLine(l);
                aux.Add(new Stock(t, n, float.Parse(h), float.Parse(l)));

            }
            return aux;

        }
    }


}