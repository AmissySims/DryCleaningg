using Cleaningg.Components;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
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
    /// Логика взаимодействия для AddEditDetergentPage.xaml
    /// </summary>
    public partial class AddEditDetergentPage : Page
    {
        public Detergent Detergent { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        public AddEditDetergentPage(Detergent _detergent = null)
        {
            DBConnect.db.Supplier.Load();
            Detergent = _detergent ?? new Detergent();
            InitializeComponent();
        }

        private void SaveDetBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!DBConnect.db.Detergent.Local.Any(detergent => detergent.Id == Detergent.Id))
                DBConnect.db.Detergent.Local.Add(Detergent);
            DBConnect.db.SaveChanges();
            MessageBox.Show("Сохранено");
            NavigationService.Navigate(new DetergentsPage());
        }

        private void AddDetImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpg|*.jpg|*.jpeg|*.jpeg",
            };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                Detergent.Photo = File.ReadAllBytes(openFile.FileName);
                DetImage.Source = new BitmapImage(new Uri(openFile.FileName));
            }
        }
    }
}
