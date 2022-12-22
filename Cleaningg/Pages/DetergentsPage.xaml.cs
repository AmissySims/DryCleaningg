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
    /// Логика взаимодействия для DetergentsPage.xaml
    /// </summary>
    public partial class DetergentsPage : Page
    {
        int actPage = 0;
        public ObservableCollection<Detergent> Detergents
        {
            get { return (ObservableCollection<Detergent>)GetValue(DetergentsProperty); }
            set { SetValue(DetergentsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DetergentsProperty =
            DependencyProperty.Register("Detergents", typeof(ObservableCollection<Detergent>), typeof(DetergentsPage));
        public DetergentsPage()
        {
            DBConnect.db.Detergent.Load();
            Detergents = DBConnect.db.Detergent.Local;
            InitializeComponent();
            if (Navigation.AuthUser.RoleId == 3)
            {

                AddDetBtn.Visibility = Visibility.Collapsed;
            }
            
            GeneralCount.Text = DBConnect.db.Detergent.Local.Count.ToString();
        }

        private void CreateDetBtn_Click(object sender, RoutedEventArgs e)
        {
            var seldetergent = (sender as Button).DataContext as Detergent;
            NavigationService.Navigate(new AddEditDetergentPage(seldetergent));
        }
        public void Sort()
        {
            ObservableCollection<Detergent> detergentList = Detergents;

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
                        detergentList = DBConnect.db.Detergent.Local;
                        break;
                    case "2":
                        detergentList = new ObservableCollection<Detergent>(Detergents.OrderBy(x => x.Title));
                        break;
                    case "3":
                        detergentList = new ObservableCollection<Detergent>(Detergents.OrderByDescending(x => x.Title));
                        break;
                    default:
                        break;
                }
            }
            Detergents = detergentList;
            if (FilterCb.SelectedItem != null)
            {
                switch ((FilterCb.SelectedItem as ComboBoxItem).Tag)
                {
                    case "1":
                        detergentList = DBConnect.db.Detergent.Local;
                        break;
                    case "2":
                        detergentList = new ObservableCollection<Detergent>(Detergents.Where(x => x.Cost > 500));
                        break;
                    case "3":
                        detergentList = new ObservableCollection<Detergent>(Detergents.Where(x => x.Cost > 1000));
                        break;

                    default:
                        break;
                }
            }
            Detergents = detergentList;
            if (FindTb.Text.Length > 0 && FindTb != null)
            {
                detergentList = new ObservableCollection<Detergent>(Detergents.Where(x => x.Title.ToLower().StartsWith(FindTb.Text.ToLower().ToLower()) || x.Description.ToLower().StartsWith(FindTb.Text.ToLower().ToLower())));
            }
            Detergents = detergentList;

            if (CountCb.SelectedIndex > -1 && detergentList.Count() > 0)
            {
                int selCount;
                if ((CountCb.SelectedItem as ComboBoxItem).Content.ToString() == "Все")
                    detergentList = DBConnect.db.Detergent.Local;
                else
                {
                    selCount = Convert.ToInt32((CountCb.SelectedItem as ComboBoxItem).Content);
                    detergentList = new ObservableCollection<Detergent>(detergentList.Skip(selCount * actPage).Take(selCount));
                    if (detergentList.Count() == 0)
                    {
                        actPage--;
                        Sort();
                    }
                }


            }
            Detergents = detergentList;
            FoundCount.Text = detergentList.Count().ToString() + " из ";

        }

        private void CountCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            actPage = 0;
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

        private void AddDetBtn_Click(object sender, RoutedEventArgs e)
        {
            var seldetergent = (sender as Button).DataContext as Detergent;
            NavigationService.Navigate(new AddEditDetergentPage(seldetergent));
        }
    }
}
