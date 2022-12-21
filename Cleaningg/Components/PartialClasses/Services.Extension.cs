
using System.Windows;

namespace Cleaningg.Components.PartialClasses
{
    public partial class Services
    {
        public Visibility BtnVisible
        {
            get
            {
                if (Navigation.AuthUser.RoleId == 2)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }
        
    }
}
