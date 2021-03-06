﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMOV_P2_Stock_Exchange
{
    public class User
    {
        private string app_uri;
        List<Stock> mystocks;

        public User(List<Stock> s)
        {
            mystocks = s;


            if (getActiveStock() == null)
                mystocks[0].setActive(true);


        }

        public Stock getActiveStock()
        {

            foreach (Stock s in mystocks)
                if (s.isActive())
                    return s;
                
            return null;

        }

        public void addStock(Stock s)
        {
            mystocks.Add(s);
        }

        public Stock getStockByTicket(string t)
        {
            foreach (Stock s in mystocks)
                if (s.getTicket().Equals(t))
                    return s;
            return null;
        }

        public void removeStock(Stock s)
        {
            mystocks.Remove(s);
        }

        public List<Stock> getStockList()
        {
            return mystocks;
        }


        public string getAppUri()
        {
            return app_uri;
        }

        public void setAppUri(string uri)
        {
            app_uri = uri;
        }

        public void setActiveStockByTicket(string t)
        {
            foreach(Stock s in mystocks)
                if (s.getTicket() == t)
                    s.setActive(true);
                else s.setActive(false);
        }

        public Stock getDisplayingStock()
        {

            foreach (Stock s in mystocks)
                if (s.isDisplaying())
                    return s;

            return null;

        }
    }
}
