using System;
using System.Collections.Generic;

namespace WMS_BLL.Models
{
    public partial class Product
    {
        public Product()
        {
            LotDetails = new HashSet<LotDetail>();
            StockOutDetails = new HashSet<StockOutDetail>();
        }

        public int ProductId { get; set; }
        public string ProductCode { get; set; } = null!;
        public int CategoryId { get; set; }
        public int StorageId { get; set; }
        public string Name { get; set; } = null!;
        public int? Quantity { get; set; }
        public string? Unit { get; set; }
        public int? Status { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Storage Storage { get; set; } = null!;
        public virtual ICollection<LotDetail> LotDetails { get; set; }
        public virtual ICollection<StockOutDetail> StockOutDetails { get; set; }
    }
}
