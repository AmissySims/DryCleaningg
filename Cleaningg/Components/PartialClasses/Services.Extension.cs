
using System.Windows;

namespace Cleaningg.Components
{
    public partial class Services
    {
        public Visibility BtnVisible
        {
            get
            {
                if (Navigation.AuthUser.RoleId == 1)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
               
            }
        }
        
    }
}
