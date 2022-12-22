using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleaningg.Components
{
    public partial class Order
    {
        public int? Quanity
        {
            get
            {
                return this.OrderService.Sum(x => x.QuanityThings);
            }
        }
        public decimal? TotalCost
        {
            get
            {
                return this.OrderService.Sum(x => x.QuanityThings * x.Services.Cost);
            }
        }
      
       
    }
}
