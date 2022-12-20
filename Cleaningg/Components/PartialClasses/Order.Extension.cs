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
        //public decimal? TotalSum
        //{
        //    get
        //    {
        //        return this.OrderServices.Sum(x => x.QuanityThings * x.Services.Cost);
        //    }
        //}
    }
}
