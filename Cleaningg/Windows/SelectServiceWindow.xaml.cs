using Cleaningg.Components;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Cleaningg.Windows
{
    /// <summary>
    /// Логика взаимодействия для SelectServiceWindow.xaml
    /// </summary>
    public partial class SelectServiceWindow : Window
    {
        public IEnumerable<Services> Services { get; set; }
        public IEnumerable<Services> SelectedService => ServiceList.SelectedItems.Cast<Services>();
        public SelectServiceWindow(IEnumerable<Services> services)
        {
            Services = DBConnect.db.Services.Local.Except(services);
            InitializeComponent();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) =>
            DialogResult = true;
    }
}
