using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;

namespace WMS_DAL.DAO
{
    public class StockOutDAO
    {
        private static StockOutDAO instance = null;
        private static readonly object padlock = new object();
        private PRN221_WMSContext _context;

        private StockOutDAO()
        {
            _context = new PRN221_WMSContext();
        }

        public static StockOutDAO Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new StockOutDAO();
                    }
                    return instance;
                }
            }
        }

        public List<StockOut> GetList()
        {
            try
            {
                return _context.StockOuts
                    .Include(so => so.Account)  // Eager load related Account entity
                    .Include(so => so.Partner)  // Eager load related Partner entity
                    .ToList();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error retrieving stock outs with related entities", ex);
            }
        }

        public StockOut GetById(int stockOutId)
        {
            try
            {
                return _context.StockOuts
                    .Include(so => so.Account)  // Eager load related Account entity
                    .Include(so => so.Partner)  // Eager load related Partner entity
                    .SingleOrDefault(so => so.StockOutId == stockOutId)
                    ?? throw new Exception($"StockOut with ID {stockOutId} not found.");
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error retrieving stock out with ID {stockOutId}", ex);
            }
        }

        public bool Create(StockOut stockOut)
        {
            try
            {
                _context.StockOuts.Add(stockOut);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error creating stock out", ex);
            }
        }

        public bool Update(StockOut stockOut)
        {
            try
            {
                using (var db = new PRN221_WMSContext())
                {
                    var existing = db.StockOuts.SingleOrDefault(so => so.StockOutId == stockOut.StockOutId);
                    if (existing != null)
                    {
                        // Update only non-null properties
                        existing.StockOutCode = stockOut.StockOutCode ?? existing.StockOutCode;
                        existing.AccountId = stockOut.AccountId; // Assuming AccountId is always provided
                        existing.PartnerId = stockOut.PartnerId; // Assuming PartnerId is always provided
                        existing.Note = stockOut.Note ?? existing.Note;
                        existing.CreatedAt = stockOut.CreatedAt ?? existing.CreatedAt;
                        existing.UpdatedAt = stockOut.UpdatedAt ?? existing.UpdatedAt;
                        existing.Status = stockOut.Status ?? existing.Status;

                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("StockOut not found for updating.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error updating stock out", ex);
            }
        }
        public bool Delete(int stockOutId)
        {
            try
            {
                using (var db = new PRN221_WMSContext())
                {
                    var stockOut = db.StockOuts.SingleOrDefault(so => so.StockOutId == stockOutId);
                    if (stockOut != null)
                    {
                        db.StockOuts.Remove(stockOut);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("StockOut not found for deletion.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error deleting stock out with ID {stockOutId}", ex);
            }
        }

    }
}
