using System;
using System.ComponentModel.DataAnnotations;

namespace HCAMiniEHR.Models
{
    public class AuditLog
    {
        [Key]
        public int AuditId { get; set; }

        public string TableName { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string Details { get; set; } = string.Empty;
    }
}
    