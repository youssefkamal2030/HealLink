using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealLink.Infrastructure.Notifications.Models;

namespace HealLink.Infrastructure.Notifications.Interfaces
{
    public interface INotificationClient
    {
        Task ReceiveNotification(NotificationMessage message);
    }
}
