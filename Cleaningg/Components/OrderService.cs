//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cleaningg.Components
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderService
    {
        public int OrderId { get; set; }
        public int ServicesId { get; set; }
        public Nullable<int> QuanityThings { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Services Services { get; set; }
    }
}