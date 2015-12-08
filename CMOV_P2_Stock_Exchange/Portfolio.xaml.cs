using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CMOV_P2_Stock_Exchange
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Portfolio : Page
    {
        private User user;

        public Portfolio()
        {
            this.InitializeComponent();

        }

        public Portfolio(User u)
        {
            this.InitializeComponent();

            user = u;   

            Thickness margin;
            margin.Top = 0;

            foreach (Stock s in user.getStockList())
            {

                Debug.WriteLine("Making request for Stock");
                
                //stocksPanel.Children.Add(new TextBlock { Text = s.display(), Foreground = new SolidColorBrush(Colors.White), Margin = margin });
                margin.Top += 20;
            }
        }




        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            user = e.Parameter as User;
            
            string url = "http://finance.yahoo.com/d/quotes?f=sl1d1t1v&s=";

            foreach (Stock s in user.getStockList())
            {
                url += s.getTicket() + ",";
            }

            updateStocks(url);

        }



        public async void updateStocks(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
            Stream resStream = response.GetResponseStream();

            StringBuilder sb = new StringBuilder();

            StreamReader read = new StreamReader(resStream);
            string tmpString = null;
            int count = (int)response.ContentLength;
            int offset = 0;
            Byte[] buf = new byte[count];
            do
            {
                int n = resStream.Read(buf, offset, count);
                if (n == 0) break;
                count -= n;
                offset += n;
                tmpString = Encoding.ASCII.GetString(buf, 0, buf.Length);
                sb.Append(tmpString);
            } while (count > 0);

            Debug.WriteLine(tmpString);


            read.Dispose();

            Thickness margin;
            margin.Top = 0;

            string[] a = tmpString.Split('\n');
            int pos = 0;

            foreach (Stock s in user.getStockList())
            {
                int valueLength = a[pos].IndexOf(",\"") - a[pos].IndexOf("\",") - 2;
                int startValuePos = a[pos].IndexOf("\",") + 2;

                s.setCurrent(float.Parse(a[pos].Substring(startValuePos,valueLength)));

                //stocksPanel.Children.Add(new TextBlock { Text = s.getCurrent().ToString(), Foreground = new SolidColorBrush(Colors.White), Margin = margin });



                margin.Top += 20;

                pos++;
            }

            wvChart.Source = new Uri("http://chart.finance.yahoo.com/z?s=GOOG&t=1m&q=l&z=m");

            response.Dispose();
        }
    }
}
