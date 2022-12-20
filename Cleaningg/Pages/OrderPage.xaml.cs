using Cleaningg.Components;
using Cleaningg.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public Order Order { get; set; }
        public ObservableCollection<OrderStatus> OrderStatus { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Services> Services { get; set; }
        public IEnumerable<User> Customers = DBConnect.db.User.Local.Where(user => user.RoleId == 2);
        public IEnumerable<User> Executors = DBConnect.db.User.Local.Where(user => user.RoleId == 3);
        public IEnumerable<OrderService> OrderServices => Order.OrderService;


        public OrderPage(Order order)
        {

            InitializeOrderPage();
            InitializeOrder(order);
            InitializeComponent();

        }

        private void InitializeOrderPage()
        {
            LoadDBTables();
            Users = DBConnect.db.User.Local;
            OrderStatus = DBConnect.db.OrderStatus.Local;
            Services = DBConnect.db.Services.Local;
        }

        private void InitializeOrder(Order order = null)
        {
            Order = order ?? new Order()
            {
                User = Navigation.AuthUser.RoleId == 2 ? Navigation.AuthUser : null,
                User1 = Navigation.AuthUser.RoleId == 3 ? Navigation.AuthUser : null,
                CompletionDate = DateTime.Now,
                StatusId = 1
            };
        }


        private static void LoadDBTables()
        {
            DBConnect.db.OrderStatus.Load();
            DBConnect.db.User.Load();
            DBConnect.db.Services.Load();
            DBConnect.db.OrderService.Load();
        }

        public OrderPage(IEnumerable<Services> addedServices)
        {
            InitializeOrderPage();
            InitializeOrder();
            foreach (var service in addedServices)
                DBConnect.db.OrderService.Local.Add(new OrderService()
                {
                    Order = Order,
                    Services = service,
                    QuanityThings = 1,

                });
            InitializeComponent();
        }



        private void SaveOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            DBConnect.db.Order.Local.Add(Order);
            DBConnect.db.SaveChanges();
            MessageBox.Show("save");
            NavigationService.Navigate(new AllOrdersPage());


        }

        private void AddProductInOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectServiceWindow selectService = new SelectServiceWindow(OrderServices.Select(c => c.Services));
            selectService.ShowDialog();
            if (selectService.DialogResult == true)
            {
                foreach (var service in selectService.SelectedService)
                {
                    DBConnect.db.OrderService.Local.Add(new OrderService()
                    {
                        Order = Order,
                        Services = service,
                        QuanityThings = 1,

                    });
                    OnPropertyChanged(nameof(OrderService));
                }
            }
        }

        private void DeleteProductInOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<OrderService> orderServices = ServiceList.SelectedItems.Cast<OrderService>();
            if (orderServices == null)
                return;
            foreach (var orderService in orderServices)
                DBConnect.db.OrderService.Local.Remove(orderService);
            DBConnect.db.SaveChanges();
            OnPropertyChanged(nameof(OrderService));
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

       
    }
}
