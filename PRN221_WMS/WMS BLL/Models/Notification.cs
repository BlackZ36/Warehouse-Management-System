using System;
using System.Collections.Generic;

namespace WMS_BLL.Models
{
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public string? Message { get; set; }
        public string Slug { get; set; } = null!;
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
