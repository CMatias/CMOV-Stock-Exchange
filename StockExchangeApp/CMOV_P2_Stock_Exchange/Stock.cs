using System.Collections.Generic;
using System.Diagnostics;

namespace CMOV_P2_Stock_Exchange
{
    public class Stock
    {
        private string ticket, name;
        private float high, low, current;
        private bool active, displaying;


        public Stock(string t, string n)
        {
            ticket = t;
            name = n;
        }

        public Stock(string t, string n, float h, float l, bool a)
        {
            ticket = t;
            name = n;
            high = h;
            low = l;
            active = a;
        }


        public Stock(string full)
        {

            int nameLen = 0, ticketLen = 0;

            nameLen = full.IndexOf("(")-1;
            ticketLen = full.Length - nameLen - 3;

            name = full.Substring(0, nameLen);
            ticket = full.Substring(full.IndexOf("(") + 1, ticketLen);
            high = 1;
            low = 0;
            active = false;
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

        public bool isDisplaying()
        {
            return displaying;
        }

        public void setDisplaying(bool d)
        {
            displaying = d;
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
                string t, n, h, l, a;

                Debug.WriteLine(array[0]);

                t = array[i].Substring(array[i].IndexOf("ticket:") + 7);
                t = t.Remove(t.IndexOf(",name:"));

                n = array[i].Substring(array[i].IndexOf(",name:") + 6);
                n = n.Remove(n.IndexOf(",h:"));

                h = array[i].Substring(array[i].IndexOf(",h:") + 3);
                h = h.Remove(h.IndexOf(",l:"));

                l = array[i].Substring(array[i].IndexOf(",l:") + 3);
                l = l.Remove(l.IndexOf(",active:"));

                a = array[i].Substring(array[i].IndexOf(",active:") + 8, array[i].Length - (array[i].IndexOf(",active:") + 8));

                

                aux.Add(new Stock(t, n, float.Parse(h), float.Parse(l), bool.Parse(a)));

            }
            return aux;
        }

        public string saveMode()
        {
            return "ticket:" + ticket + ",name:" + name + ",h:" + high.ToString() + ",l:" + low.ToString() + ",active:" +
                   active.ToString() + "/";
        }


    }
}