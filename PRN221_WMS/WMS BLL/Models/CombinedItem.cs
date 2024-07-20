using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS_BLL.Models
{
    public class CombinedItem
    {
        public DateTime? Date { get; set; }
        public int Id { get; set; }
        public int Type { get; set; }
        public string Code { get; set; }
        public string AccountName { get; set; }
        public string PartnerName { get; set; }
        public string Description { get; set; }
        public string TimeAgo { get; set; }
    }
}
