using Cleaningg.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Cleaningg.Components
{
   
    internal class Navigation
    {
        public static User AuthUser = null;
        public static bool isAuth = false;
        public static MainWindow main;
        public static MainPage main1;
        public static List<Nav> navs = new List<Nav>();
        public static void NextPage(Nav nav)
        {
            navs.Add(nav);
            Update(nav);
        }

        private static void Update(Nav nav)
        {
            main.MainFrame.Navigate(nav.Page);

        }
    }
    class Nav
    {

        public Page Page { get; set; }


        public Nav(Page Page)
        {

            this.Page = Page;
        }
    }
}

