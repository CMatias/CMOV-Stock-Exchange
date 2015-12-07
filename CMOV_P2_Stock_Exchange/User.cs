using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMOV_P2_Stock_Exchange
{
    class User
    {
        private string app_id;
        List<Stock> mystocks;

        public User(List<Stock> s)
        {
            mystocks = s;
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


    }
}
