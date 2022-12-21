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
using System.Windows.Threading;

namespace Cleaningg.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        public AuthPage()
        {
            InitializeComponent();
            if (Properties.Settings.Default.Login != null)
                LoginTb.Text = Properties.Settings.Default.Login;
            if (Properties.Settings.Default.Password != null)
                PasswordTb.Password = Properties.Settings.Default.Password;


        }

        private void EntrBtn_Click(object sender, RoutedEventArgs e)
        {
            int TimeAuth = Properties.Settings.Default.TimeAuth;
            string login = LoginTb.Text.Trim();
            string password = PasswordTb.Password.Trim();
            if (TimeAuth < 3)
            {
                if (login.Length == 0 || password.Length == 0)
                {
                    MessageBox.Show("Заполните поля");
                    TimeAuth += 1;
                    Properties.Settings.Default.TimeAuth = TimeAuth;
                }

                else
                {
                    Navigation.AuthUser = DBConnect.db.User.Local.ToList().Find(x => x.Login == login && x.Password == password);
                    if (Navigation.AuthUser == null)
                    {
                        MessageBox.Show("Такого пользователя нет");
                        TimeAuth += 1;
                        Properties.Settings.Default.TimeAuth = TimeAuth;
                    }
                    else
                    {
                        if (SaveCb.IsChecked == true)
                        {
                            Properties.Settings.Default.Login = LoginTb.Text;
                            Properties.Settings.Default.Password = PasswordTb.Password;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            Properties.Settings.Default.Login = null;
                            Properties.Settings.Default.Password = null;
                            Properties.Settings.Default.Save();
                        }
                        TimeAuth = 0;
                        Navigation.isAuth = true;
                        Navigation.NextPage(new Nav(new MainPage()));


                    }
                }
            }
            else
            {
                MessageBox.Show("Данные неверны. Доступ заблокирован на 1 минуту!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                TimeAuth = 0;
                EntrBtn.IsEnabled = false;
                RegBtn.IsEnabled = false;
                timer.Interval = new TimeSpan(0, 0, 60);
                timer.Tick += new EventHandler(IsVisibleBtn);
                timer.Start();
            }


        }
        private void IsVisibleBtn(object sender, EventArgs e)
        {
            EntrBtn.IsEnabled = true;
            RegBtn.IsEnabled = true;
            timer.Stop();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav(new RegPage()));
        }


    }
}

