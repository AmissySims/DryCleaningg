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
    /// Логика взаимодействия для AllSuppliesPage.xaml
    /// </summary>
    public partial class AllSuppliesPage : Page
    {
        public ObservableCollection<Supply> AllSupply
        {
            get { return (ObservableCollection<Supply>)GetValue(AllSupplyProperty); }
            set { SetValue(AllSupplyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllSupplyProperty =
            DependencyProperty.Register("AllSupply", typeof(ObservableCollection<Supply>), typeof(AllSuppliesPage));


        public ObservableCollection<Supplier> Suppliers { get; set; }
        public AllSuppliesPage()
        {
            DBConnect.db.Supply.Load();
            AllSupply = DBConnect.db.Supply.Local;
            DBConnect.db.Detergent.Load();
            DBConnect.db.Supplier.Load();
            InitializeComponent();
        }
    }
}
