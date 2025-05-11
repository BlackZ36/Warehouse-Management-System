﻿using System;
using System.Collections.Generic;

namespace WMS_BLL.Models
{
    public partial class LotDetail
    {
        public int LotDetailId { get; set; }
        public int LotId { get; set; }
        public int PartnerId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public string? PackingType { get; set; }
        public int? Status { get; set; }

        public virtual Lot Lot { get; set; } = null!;
        public virtual Partner Partner { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
