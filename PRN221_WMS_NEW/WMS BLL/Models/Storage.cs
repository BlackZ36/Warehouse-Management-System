using System;
using System.Collections.Generic;

namespace WMS_BLL.Models
{
    public partial class Storage
    {
        public Storage()
        {
            Products = new HashSet<Product>();
        }

        public int StorageId { get; set; }
        public string StorageCode { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Province { get; set; }
        public string? Address { get; set; }
        public int? MaxCapacity { get; set; }
        public int? CurrentCapacity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
