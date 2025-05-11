using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;

namespace WMS_DAL.DAO
{
    public class HistoryDAO
    {
        private static HistoryDAO instance = null;
        private static readonly object padlock = new object();
        private PRN221_WMSContext _context;

        private HistoryDAO()
        {
            _context = new PRN221_WMSContext();
        }

        public static HistoryDAO Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new HistoryDAO();
                    }
                    return instance;
                }
            }
        }


        public List<History> GetHistories()
        {
            try
            {
                return _context.Histories.ToList();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error retrieving history records", ex);
            }
        }

        public History GetHistoryById(int historyId)
        {
            try
            {
                return _context.Histories.SingleOrDefault(h => h.HistoryId == historyId);
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error retrieving history record with ID {historyId}", ex);
            }
        }

        public bool Create(History history)
        {
            try
            {
                _context.Histories.Add(history);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error creating history record", ex);
            }
        }

        public bool Delete(int historyId)
        {
            try
            {
                using (var db = new PRN221_WMSContext())
                {
                    var history = db.Histories.SingleOrDefault(h => h.HistoryId == historyId);
                    if (history != null)
                    {
                        db.Histories.Remove(history);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("History record not found for deletion.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error deleting history record with ID {historyId}", ex);
            }
        }




    }
}
