using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;
using WMS_DAL.DAO;
using WMS_DAL.Interface;

namespace WMS_DAL.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        public List<Notification> GetNotifications() => NotificationDAO.Instance.GetList();

        public Notification GetNotificationById(int id) => NotificationDAO.Instance.GetById(id);

        public bool CreatNotification(Notification notification) => NotificationDAO.Instance.Create(notification);

        public bool DeleteNotification(int id) => NotificationDAO.Instance.Delete(id);
    }
}
