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
        public List<Gender> Genders { get; set; }
        public UserCabPage(User _user = null)
        {
            DBConnect.db.User.Load();
            User = _user ?? new User();
            Roles = DBConnect.db.Role.Local.ToList();
            Genders = DBConnect.db.Gender.Local.ToList();
            InitializeComponent();


        }



        private void SaveUserBtn_Click(object sender, object e)
        {
            if (User.Id == 0)
            {

                string login = LoginTb.Text.Trim();
                string password = PasswordTb.Text.Trim();
                string fname = FNameTb.Text.Trim();
                string lname = LNameTb.Text.Trim();
                string patronymic = PatronTb.Text.Trim();
                string phone = PhoneTb.Text.Trim();
                string adress = AdressTb.Text.Trim();
                char[] chars = { '!', '@', '#', '$', '%', '^' };
                var check = DBConnect.db.User.Where(x => x.Login == login).FirstOrDefault();
                if (login.Length > 0 && password.Length > 0 && fname.Length > 0
               && lname.Length > 0
               && patronymic.Length > 0 && adress.Length > 0 &&
               phone.Length > 0 && GenderCb.SelectedItem != null && RolCb.SelectedItem != null)
                {
                    if (check == null)
                    {
                        if (password.Length > 5
                        && password.Any(ch => Char.IsUpper(ch))
                        && password.Any(ch => Char.IsDigit(ch))
                        && password.Any(ch => chars.Contains(ch)))
                        {


                            DBConnect.db.User.Add(new User
                            {
                                Login = login,
                                Password = password,
                                FirstName = fname,
                                LastName = lname,
                                Patronymic = patronymic,
                                Adress = adress,
                                Phone = phone,
                                GenderId = GenderCb.SelectedIndex +1,
                                RoleId = RolCb.SelectedIndex +1

                            });

                            MessageBox.Show("Успешно");
                            DBConnect.db.SaveChanges();
                            NavigationService.Navigate(new UsersListPage());
                        }
                        else
                        {
                            MessageBox.Show("Проверьте на правильность заполнения.\n" +
                            "Пароль должен содержать 6 символов, хотя бы 1 прописную букву," +
                            " хотя бы 1 цифру и хотя бы 1 из этих символов ! @ # $ % ^");
                        }
                    }
                    else
                        MessageBox.Show("Такой пользователь уже существует");
                }
                else
                    MessageBox.Show("Заполните поля");


            }
            else
            {
                MessageBox.Show("Успешно");
                DBConnect.db.SaveChanges();
                NavigationService.Navigate(new UsersListPage());
            }
        }

        private void RolCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (RolCb.SelectedItem == null)
            //    return;
            //User.Role = RolCb.SelectedItem as Role;
        }
    }
}
