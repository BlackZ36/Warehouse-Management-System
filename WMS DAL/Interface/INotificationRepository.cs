using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;

namespace WMS_DAL.Interface
{
    public interface INotificationRepository
    {
        List<Notification> GetNotifications();
        Notification GetNotificationById(int id);
        bool CreatNotification(Notification notification);
        bool DeleteNotification(int id);
    }
}
