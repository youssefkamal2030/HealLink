using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Notifications.Models
{
    public class NotificationMessage
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid ConnectionRequestId { get; set; }
        public Guid PatientId { get; set; }
    }
}
