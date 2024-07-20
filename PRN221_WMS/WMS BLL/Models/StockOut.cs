using System;
using System.Collections.Generic;

namespace WMS_BLL.Models
{
    public partial class StockOut
    {
        public StockOut()
        {
            Histories = new HashSet<History>();
            StockOutDetails = new HashSet<StockOutDetail>();
        }

        public int StockOutId { get; set; }
        public string? StockOutCode { get; set; }
        public int AccountId { get; set; }
        public int PartnerId { get; set; }
        public string? Note { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? Status { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Partner Partner { get; set; } = null!;
        public virtual ICollection<History> Histories { get; set; }
        public virtual ICollection<StockOutDetail> StockOutDetails { get; set; }
    }
}
