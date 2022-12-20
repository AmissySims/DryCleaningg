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
    /// Логика взаимодействия для UsersListPage.xaml
    /// </summary>
    public partial class UsersListPage : Page
    {
        int actPage = 0;

        public ObservableCollection<User> Userss
        {
            get { return (ObservableCollection<User>)GetValue(UserssProperty); }
            set { SetValue(UserssProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserssProperty =
            DependencyProperty.Register("Userss", typeof(ObservableCollection<User>), typeof(UsersListPage));


        public UsersListPage()
        {
            DBConnect.db.User.Load();
            Userss = DBConnect.db.User.Local;
            InitializeComponent();
            GeneralCount.Text = DBConnect.db.User.Count().ToString();
        }

        public void Sort()
        {
            ObservableCollection<User> users = Userss;
            if (CountCb == null)
                return;
            if (FilterCb == null)
                return;
            if (FilterCb.SelectedItem != null)
            {
                switch ((FilterCb.SelectedItem as ComboBoxItem).Tag)
                {
                    case "1":
                        users = DBConnect.db.User.Local;
                        break;
                    case "2":
                        users = new ObservableCollection<User>(Userss.Where(x => x.RoleId == 1));
                        break;
                    case "3":
                        users = new ObservableCollection<User>(Userss.Where(x => x.RoleId == 2));
                        break;
                    case "4":
                        users = new ObservableCollection<User>(Userss.Where(x => x.RoleId == 3));
                        break;
                }
            }
            Userss = users;
            if (CountCb.SelectedIndex > -1 && users.Count() > 0)
            {
                int selCount;
                if ((CountCb.SelectedItem as ComboBoxItem).Content.ToString() == "Все")
                    users = DBConnect.db.User.Local;
                else
                {
                    selCount = Convert.ToInt32((CountCb.SelectedItem as ComboBoxItem).Content);
                    users = new ObservableCollection<User>(users.Skip(selCount * actPage).Take(selCount));
                    if (users.Count() == 0)
                    {
                        actPage--;
                        Sort();
                    }
                }
            }
            Userss = users;

            FoundCount.Text = users.Count().ToString() + " из ";
        }
        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            var seluser = (sender as Button).DataContext as User;
            NavigationService.Navigate(new UserCabPage(seluser));
        }

        private void RightBtn_Click(object sender, RoutedEventArgs e)
        {
            actPage++;
            Sort();
        }

        private void LeftBtn_Click(object sender, RoutedEventArgs e)
        {
            actPage--;
            if (actPage < 0)
                return;
        }

        private void UserRedBtn_Click(object sender, RoutedEventArgs e)
        {
            var seluser = (sender as Button).DataContext as User;
            NavigationService.Navigate(new UserCabPage(seluser));
        }

        private void CountCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            actPage = 0;
            Sort();
        }

        private void FilterCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
        }
    }
}
