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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cleaningg.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }
        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTb.Text.Trim();
            string password = PasswordTb.Text.Trim();
            string fname = FNameTb.Text.Trim();
            string lname = LNameTb.Text.Trim();
            string patronymic = PatronTb.Text.Trim();
            string phone = PhoneTb.Text.Trim();
            string adress = AdressTb.Text.Trim();
            char[] chars = { '!', '@', '#', '$', '%', '^' };
            var check = DBConnect.db.User.Where(x => x.Login == login && x.Phone == phone).FirstOrDefault();
            if (login.Length == 0 || password.Length == 0 ||
                fname.Length == 0 || lname.Length == 0 ||
                patronymic.Length == 0 || adress.Length == 0 ||
                phone.Length == 0 || GenderCb.SelectedItem == null)
            {
                MessageBox.Show("Заполните поля");
            }

            else
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
                            GenderId = GenderCb.SelectedIndex + 1,
                            RoleId = 2

                        });

                        MessageBox.Show("Успешно");
                        DBConnect.db.SaveChanges();
                        Navigation.NextPage(new Nav(new AuthPage()));
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
        }


        private void EntrBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav(new AuthPage()));
        }
    }
}
