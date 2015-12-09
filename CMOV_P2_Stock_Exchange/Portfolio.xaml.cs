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
using Windows.UI.Popups;
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
        private static User user;
        public static Windows.Storage.ApplicationDataContainer mySettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public Portfolio()
        {
            this.InitializeComponent();
        }

        public Portfolio(User u)
        {
            this.InitializeComponent();

            user = u;

            string url = "http://finance.yahoo.com/d/quotes?f=sl1d1t1v&s=";

            foreach (Stock s in user.getStockList())
            {
                url += s.getTicket() + ",";
                Debug.WriteLine(s.getTicket());
            }

            updateStocks(url);

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



        private void stockPress(object sender, RoutedEventArgs e)
        {

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

            read.Dispose();   

            string[] a = tmpString.Split('\n');
            int pos = 0;

            foreach (Stock s in user.getStockList())
            {
                bLow.Content = "[↓] " + s.getLow();
                bHigh.Content = s.getHigh() + " [↑]";

                int valueLength = a[pos].IndexOf(",\"") - a[pos].IndexOf("\",") - 2;
                int startValuePos = a[pos].IndexOf("\",") + 2;

                s.setCurrent(float.Parse(a[pos].Substring(startValuePos,valueLength)));

                StackPanel sPanel;

                if(s.isActive())
                    sPanel = new StackPanel { Margin = new Thickness(8, 8, 8, 8), Width = 100 , Height = 100 , Background = new SolidColorBrush(Colors.SteelBlue) };
                else
                    sPanel = new StackPanel { Margin = new Thickness(8, 8, 8, 8), Width = 100 , Height = 100 , Background = new SolidColorBrush(Colors.DarkCyan) };
                sPanel.Tapped += stockClick;
                sPanel.Holding += stockHold;
                sPanel.RightTapped += stockRightClick;
                sPanel.Children.Add(new TextBlock { Margin= new Thickness( 10,25,10,0 ), Foreground = new SolidColorBrush(Colors.White),FontSize = 12, Text = s.getTicket()});
                sPanel.Children.Add(new TextBlock { Margin = new Thickness(10, 0, 10, 0), Foreground = new SolidColorBrush(Colors.White), FontSize = 20, Text = s.getCurrent().ToString() });

                

                Grid.SetRow(sPanel, int.Parse((pos / 3).ToString()));
                Grid.SetColumn(sPanel, int.Parse(( pos % 3).ToString()));
                
             
                stocksPanel.Children.Add(sPanel);

                pos++;
            }

            changeChart(user.getActiveStock().getTicket());

            response.Dispose();
        }


        private void stockClick(object sender, TappedRoutedEventArgs e)
        {
            StackPanel sp = (StackPanel)sender;
            TextBlock tb = (TextBlock)sp.Children[0];

            changeChart(tb.Text);

            bLow.Content = "[↓] " + user.getStockByTicket(tb.Text).getLow();
            bHigh.Content = user.getStockByTicket(tb.Text).getHigh() + " [↑]";

    }


        private void stockRightClick(object sender, RightTappedRoutedEventArgs e)
        {
            foreach (UIElement sp in stocksPanel.Children)
            {
                if(sp is StackPanel)
                    if ((sp as StackPanel).Background.ToString() == new SolidColorBrush(Colors.SteelBlue).ToString())
                        (sp as StackPanel).Background = new SolidColorBrush(Colors.DarkCyan);
            }

            (sender as StackPanel).Background = new SolidColorBrush(Colors.SteelBlue);

            TextBlock tb = (TextBlock)(sender as StackPanel).Children[0];

            user.setActiveStockByTicket(tb.Text);
            stockClick(sender, new TappedRoutedEventArgs());

        }



        private void stockHold(object sender, HoldingRoutedEventArgs e)
        {
            foreach (UIElement sp in stocksPanel.Children)
            {
                if (sp is StackPanel)
                    if ((sp as StackPanel).Background.ToString() == new SolidColorBrush(Colors.SteelBlue).ToString())
                        (sp as StackPanel).Background = new SolidColorBrush(Colors.DarkCyan);
            }

            (sender as StackPanel).Background = new SolidColorBrush(Colors.SteelBlue);

            TextBlock tb = (TextBlock) (sender as StackPanel).Children[0];

            user.setActiveStockByTicket(tb.Text);
            stockClick(sender, new TappedRoutedEventArgs());

        }

        public void changeChart(string ticket)
        {
            foreach (Stock s in user.getStockList())
                if (s.getTicket() == ticket)
                    s.setDisplaying(true);
                else s.setDisplaying(false);

            wvChart.NavigateToString("<style>div{text-align: center;}img{max-width:90vw;height:90vh}body{overflow:hidden}</style><div><img src='http://chart.finance.yahoo.com/z?s=" + ticket + "&t=1m&q=l&z=l'></div>");
        }

        public static Task saveData()
        {
            string str = "";
            foreach (Stock s in user.getStockList())
                str += s.saveMode();

            str = str.Remove(str.Length - 1, 1);

            mySettings.Values["stocks"] =  str;
            Debug.WriteLine(str);


            return Task.Run(() =>
            {
                Debug.WriteLine("Settings saved.");
            });
           

        }

        private  void bLowClick(object sender, RoutedEventArgs e)
        {
            if (user.getDisplayingStock().getHigh()!=0)
                lowSlider.Maximum = user.getDisplayingStock().getHigh();

            Canvas.SetZIndex(lowSlider, 50);

            bLow.Opacity = 0.5;

        }

        private void spinnerLow(object sender, PointerRoutedEventArgs e)
        {
            if (bLow.Opacity < 1)
            {
                Canvas.SetZIndex(lowSlider, 0);
                user.getDisplayingStock().setLow(float.Parse((lowSlider as Slider).Value.ToString()));
                (bLow as Button).Content = "[↓] " + user.getDisplayingStock().getLow();

                if (user.getDisplayingStock().getLow() >= user.getDisplayingStock().getHigh())
                {
                    user.getDisplayingStock().setHigh(user.getDisplayingStock().getLow() + 1);
                    (bHigh as Button).Content = user.getDisplayingStock().getHigh() + " [↑]";
                }
            }
            bLow.Opacity = 1;

        }

        private void bHighClick(object sender, RoutedEventArgs e)
        {
            if (user.getDisplayingStock().getLow() != 0)
                highSlider.Minimum = user.getDisplayingStock().getLow();

            highSlider.Maximum = user.getDisplayingStock().getCurrent()*1.5;

            Canvas.SetZIndex(highSlider, 50);

            bHigh.Opacity = 0.5;

        }

        private void spinnerHigh(object sender, PointerRoutedEventArgs e)
        {
            if (bHigh.Opacity < 1) { 
            Canvas.SetZIndex(highSlider, 0);
            user.getDisplayingStock().setHigh(float.Parse((highSlider as Slider).Value.ToString()));
            (bHigh as Button).Content = user.getDisplayingStock().getHigh() + " [↑]";

            if (user.getDisplayingStock().getHigh() <= user.getDisplayingStock().getLow())
            {
                user.getDisplayingStock().setLow(user.getDisplayingStock().getHigh() - 1);
                (bLow as Button).Content = "[↓] " + user.getDisplayingStock().getLow();
            }
            }
            bHigh.Opacity = 1;

        }


    }

  
}
