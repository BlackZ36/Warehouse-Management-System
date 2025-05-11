using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;

namespace WMS_DAL.DAO
{
    public class NotificationDAO
    {
        private static NotificationDAO instance = null;
        private static readonly object padlock = new object();
        private PRN221_WMSContext _context;

        private NotificationDAO()
        {
            _context = new PRN221_WMSContext();
        }

        public static NotificationDAO Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new NotificationDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Notification> GetList()
        {
            try
            {
                return _context.Notifications
                    .ToList();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error retrieving notifications", ex);
            }
        }

        public Notification GetById(int notificationId)
        {
            try
            {
                return _context.Notifications
                    .SingleOrDefault(n => n.NotificationId == notificationId)
                    ?? throw new Exception($"Notification with ID {notificationId} not found.");
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error retrieving notification with ID {notificationId}", ex);
            }
        }

        public bool Create(Notification notification)
        {
            try
            {
                _context.Notifications.Add(notification);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error creating notification", ex);
            }
        }

        public bool Delete(int notificationId)
        {
            try
            {
                using (var db = new PRN221_WMSContext())
                {
                    var notification = db.Notifications.SingleOrDefault(n => n.NotificationId == notificationId);
                    if (notification != null)
                    {
                        db.Notifications.Remove(notification);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("Notification not found for deletion.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error deleting notification with ID {notificationId}", ex);
            }
        }


    }
}
