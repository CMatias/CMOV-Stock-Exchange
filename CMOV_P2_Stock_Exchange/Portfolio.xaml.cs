using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
                stocksPanel.Children.Add(new TextBlock { Text = s.display(), Foreground = new SolidColorBrush(Colors.White), Margin = margin });
                margin.Top += 20;
            }
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            user = e.Parameter as User;
            Thickness margin;
            margin.Top = 0;

            foreach (Stock s in user.getStockList()) { 
                stocksPanel.Children.Add(new TextBlock {Text = s.display(), Foreground = new SolidColorBrush(Colors.White), Margin = margin });
                margin.Top += 20;
            }

            //Debug.WriteLine(user.getStockList()[0].display());
        }
    }
}
