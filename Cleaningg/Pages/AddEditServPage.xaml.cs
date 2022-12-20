using Cleaningg.Components;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
    /// Логика взаимодействия для AddEditServPage.xaml
    /// </summary>
    public partial class AddEditServPage : Page
    {

        public AddEditServPage()
        {
            InitializeComponent();
        }
        public Services Services { get; set; }
        public List<Detergent> Detergents { get; set; }
        public AddEditServPage(Services _services)
        {
            DBConnect.db.Detergent.Load();
            Services = _services ?? new Services();
            Detergents = DBConnect.db.Detergent.Local.ToList();
            InitializeComponent();
        }

        private void SaveServBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Services.Id == 0)
            {
                DBConnect.db.Services.Add(Services);
            }
            DBConnect.db.SaveChanges();
            MessageBox.Show("Сохранено");
            NavigationService.Navigate(new ServicesListPage());

        }

        private void DetergentServ_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DetergentServ.SelectedItem == null)
                return;
            Services.Detergent = DetergentServ.SelectedItem as Detergent;
        }

        private void AddImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpg|*.jpg|*.jpeg|*.jpeg",
            };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                Services.Photo = File.ReadAllBytes(openFile.FileName);
                ServImage.Source = new BitmapImage(new Uri(openFile.FileName));
            }



        }
    }
}
