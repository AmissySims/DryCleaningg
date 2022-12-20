using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cleaningg.Components.PartialClasses
{
    public partial class Detergent
    {
        public Visibility BtnVisible
        {
            get
            {
                if (Navigation.AuthUser.RoleId == 3)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }
    }
}
