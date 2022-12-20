using Cleaningg.Components;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для UserCabPage.xaml
    /// </summary>
    public partial class UserCabPage : Page
    {
        public User User { get; set; }
        public List<Role> Roles { get; set; }

        public UserCabPage(User _user = null)
        {
            DBConnect.db.User.Load();
            User = _user ?? new User();
            Roles = DBConnect.db.Role.Local.ToList();
            InitializeComponent();


        }



        private void SaveUserBtn_Click(object sender, object e)
        {
            if (User.Id == 0)
                DBConnect.db.User.Add(User);
            DBConnect.db.SaveChanges();
            MessageBox.Show("Сохранено");
            NavigationService.Navigate(new UsersListPage());
        }

        private void RolCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RolCb.SelectedItem == null)
                return;
            User.Role = RolCb.SelectedItem as Role;
        }
    }
}
