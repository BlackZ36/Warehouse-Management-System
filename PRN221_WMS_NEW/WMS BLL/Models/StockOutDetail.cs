﻿using System;
using System.Collections.Generic;

namespace WMS_BLL.Models
{
    public partial class StockOutDetail
    {
        public int StockOutDetailId { get; set; }
        public int StockOutId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public string? PackingType { get; set; }
        public int? Status { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual StockOut StockOut { get; set; } = null!;
    }
}
