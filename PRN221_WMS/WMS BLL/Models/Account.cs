using System;
using System.Collections.Generic;

namespace WMS_BLL.Models
{
    public partial class Account
    {
        public Account()
        {
            Histories = new HashSet<History>();
            Lots = new HashSet<Lot>();
            StockOuts = new HashSet<StockOut>();
        }

        public int AccountId { get; set; }
        public string AccountCode { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }

        public virtual ICollection<History> Histories { get; set; }
        public virtual ICollection<Lot> Lots { get; set; }
        public virtual ICollection<StockOut> StockOuts { get; set; }
    }
}
