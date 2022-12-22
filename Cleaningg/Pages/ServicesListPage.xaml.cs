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
    /// Логика взаимодействия для ServicesListPage.xaml
    /// </summary>
    public partial class ServicesListPage : Page
    {
        int actPage = 0;

        public ObservableCollection<Services> ServicesList
        {
            get { return (ObservableCollection<Services>)GetValue(ServicesListProperty); }
            set { SetValue(ServicesListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ServicesListProperty =
            DependencyProperty.Register("ServicesList", typeof(ObservableCollection<Services>), typeof(ServicesListPage));


        public ServicesListPage()
        {

            DBConnect.db.Services.Load();
            ServicesList = DBConnect.db.Services.Local;
            InitializeComponent();

            if (Navigation.AuthUser.RoleId == 2)
            {
                AddServBtn.Visibility = Visibility.Collapsed;
            }
          

            if (Navigation.AuthUser.RoleId == 3)
            {
                AddServBtn.Visibility = Visibility.Collapsed;
            }
            GeneralCount.Text = DBConnect.db.Services.Local.Count().ToString();
        }

        private void CreateServBtn_Click(object sender, RoutedEventArgs e)
        {
            var selservice = (sender as Button).DataContext as Services;
            NavigationService.Navigate(new AddEditServPage(selservice));
        }

        private void AddOrderBtn_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new OrderPage(ServiceList.SelectedItems.Cast<Services>()));
        }

        public void Sort()
        {
            ObservableCollection<Services> serviceList = ServicesList;

            if (CountCb == null)
                return;
            if (SortCb == null) return;
            if (FilterCb == null) return;
            if (FindTb == null) return;
            if (SortCb.SelectedItem != null)
            {
                switch ((SortCb.SelectedItem as ComboBoxItem).Tag)
                {
                    case "1":
                        serviceList = DBConnect.db.Services.Local;
                        break;
                    case "2":
                        serviceList = new ObservableCollection<Services>(ServicesList.OrderByDescending(x => x.Deadline));
                        break;
                    case "3":
                        serviceList = new ObservableCollection<Services>(ServicesList.OrderBy(x => x.Deadline));
                        break;
                    default:
                        break;
                }
            }
            ServicesList = serviceList;
            if (FilterCb.SelectedItem != null)
            {
                switch ((FilterCb.SelectedItem as ComboBoxItem).Tag)
                {
                    case "1":
                        serviceList = DBConnect.db.Services.Local;
                        break;
                    case "2":
                        serviceList = new ObservableCollection<Services>(ServicesList.Where(x => x.Cost > 500));
                        break;
                    case "3":
                        serviceList = new ObservableCollection<Services>(ServicesList.Where(x => x.Cost > 1000));
                        break;
                    case "4":
                        serviceList = new ObservableCollection<Services>(ServicesList.Where(x => x.Cost > 2000));
                        break;
                    default:
                        break;
                }
            }
            ServicesList = serviceList;
            if (FindTb.Text.Length > 0 && FindTb != null)
            {
                serviceList = new ObservableCollection<Services>(ServicesList.Where(x => x.Title.ToLower().StartsWith(FindTb.Text.ToLower().ToLower())));
            }
            ServicesList = serviceList;

            if (CountCb.SelectedIndex > -1 && serviceList.Count() > 0)
            {
                int selCount;
                if ((CountCb.SelectedItem as ComboBoxItem).Content.ToString() == "Все")
                    serviceList = DBConnect.db.Services.Local;
                else
                {
                    selCount = Convert.ToInt32((CountCb.SelectedItem as ComboBoxItem).Content);
                    serviceList = new ObservableCollection<Services>(serviceList.Skip(selCount * actPage).Take(selCount));
                    if (serviceList.Count() == 0)
                    {
                        actPage--;
                        Sort();
                    }
                }


            }
            ServicesList = serviceList;

            FoundCount.Text = serviceList.Count().ToString() + " из ";

        }
        private void CountCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            actPage = 0;
            Sort();
        }

        private void LeftBtn_Click(object sender, RoutedEventArgs e)
        {
            actPage--;
            if (actPage < 0)
                actPage = 0;
            Sort();
        }

        private void RightBtn_Click(object sender, RoutedEventArgs e)
        {
            actPage++;
            Sort();
        }

        private void SortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
        }

        private void FilterCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
        }

        private void FindTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Sort();
        }

        private void AddServBtn_Click(object sender, RoutedEventArgs e)
        {
            var selserv = (sender as Button).DataContext as Services;
            NavigationService.Navigate(new AddEditServPage(selserv));
        }
    }
}
