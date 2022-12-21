using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleaningg.Components.PartialClasses
{
    public partial class Order
    {
        public ObservableCollection<OrderService> OrdersService
        {
            get
            {
                return new ObservableCollection<OrderService>(OrdersService);
            }
        }
        public ObservableCollection<Services> Services
        {
            get
            {
                return new ObservableCollection<Services>(Services);
            }
        }
        public int? Quanity
        {
            get
            {
                return this.OrdersService.Sum(x => x.QuanityThings);
            }
        }
        public decimal? TotalCost
        {
            get
            {
                return this.OrdersService.Sum(x => x.QuanityThings * x.Services.Cost);
            }
        }
      
       
    }
}
