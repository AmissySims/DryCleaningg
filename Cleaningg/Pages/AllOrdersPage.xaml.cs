using Cleaningg.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cleaningg.Pages
{
    /// <summary>
    /// Логика взаимодействия для AllOrdersPage.xaml
    /// </summary>
    public partial class AllOrdersPage : Page
    {
        public ObservableCollection<Order> AllOrders
        {
            get { return (ObservableCollection<Order>)GetValue(AllOrdersProperty); }
            set { SetValue(AllOrdersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllOrdersProperty =
            DependencyProperty.Register("AllOrders", typeof(ObservableCollection<Order>), typeof(AllOrdersPage));
        public AllOrdersPage()
        {
            DBConnect.db.Order.Load();
            AllOrders = DBConnect.db.Order.Local;
            
            InitializeComponent();
        }
        private void CreateOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            var selorder = (sender as Button).DataContext as Order;
            NavigationService.Navigate(new OrderPage(selorder));
        }
        public void Refresh()
        {
            ObservableCollection<Order> orders = AllOrders;
            if (OrderCb == null)
                return;
            if (OrderCb.SelectedItem != null)
            {
                switch ((OrderCb.SelectedItem as ComboBoxItem).Tag)
                {
                    case "1":
                        orders = DBConnect.db.Order.Local;
                        break;
                    case "2":
                        orders = new ObservableCollection<Order>(AllOrders.Where(x => x.StatusId == 1));
                        break;
                    case "3":
                        orders = new ObservableCollection<Order>(AllOrders.Where(x => x.StatusId == 2));
                        break;
                    case "4":
                        orders = new ObservableCollection<Order>(AllOrders.Where(x => x.StatusId == 3));
                        break;
                    case "5":
                        orders = new ObservableCollection<Order>(AllOrders.Where(x => x.StatusId == 4));
                        break;
                    case "6":
                        orders = new ObservableCollection<Order>(AllOrders.Where(x => x.StatusId == 5));
                        break;
                    case "7":
                        orders = new ObservableCollection<Order>(AllOrders.Where(x => x.StatusId == 6));
                        break;
                    case "8":
                        orders = new ObservableCollection<Order>(AllOrders.Where(x => x.StatusId == 7));
                        break;
                    default:
                        break;
                }

            }
            AllOrders = orders;
            //OrdersList.ItemsSource = orders.ToList();
        }
        private void OrderCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        

        private void AddOrderBtn_Click_1(object sender, RoutedEventArgs e)
        {
            var selorder = (sender as Button).DataContext as Order;
            NavigationService.Navigate(new OrderPage(selorder));
        }
    }
}
