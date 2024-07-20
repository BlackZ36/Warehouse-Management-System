using System;
using System.Collections.Generic;

namespace WMS_BLL.Models
{
    public partial class Lot
    {
        public Lot()
        {
            Histories = new HashSet<History>();
            LotDetails = new HashSet<LotDetail>();
        }

        public int LotId { get; set; }
        public string LotCode { get; set; } = null!;
        public int PartnerId { get; set; }
        public int AccountId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Note { get; set; }
        public int? Status { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Partner Partner { get; set; } = null!;
        public virtual ICollection<History> Histories { get; set; }
        public virtual ICollection<LotDetail> LotDetails { get; set; }
    }
}
