using System;
using System.Collections.Generic;

namespace WMS_BLL.Models
{
    public partial class History
    {
        public int HistoryId { get; set; }
        public string? HistoryType { get; set; }
        public int? StockoutId { get; set; }
        public int? LotId { get; set; }
        public int AccountId { get; set; }
        public string? Action { get; set; }
        public DateTime? Timestamp { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Lot? Lot { get; set; }
        public virtual StockOut? Stockout { get; set; }
    }
}
