using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;

namespace WMS_DAL.DAO
{
    public class StockOutDetailDAO
    {
        private static StockOutDetailDAO instance = null;
        private static readonly object padlock = new object();
        private PRN221_WMSContext _context;

        private StockOutDetailDAO()
        {
            _context = new PRN221_WMSContext();
        }

        public static StockOutDetailDAO Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new StockOutDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public List<StockOutDetail> GetStockOuts()
        {
            try
            {
                return _context.StockOutDetails
                    .Include(sod => sod.StockOut)  // Eager load related StockOut entity
                    .Include(sod => sod.Product)   // Eager load related Product entity
                    .ToList();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error retrieving stock out details with related entities", ex);
            }
        }


        public StockOutDetail GetStockOutById(int stockOutDetailId)
        {
            try
            {
                return _context.StockOutDetails
                    .Include(sod => sod.StockOut)  // Eager load related StockOut entity
                    .Include(sod => sod.Product)   // Eager load related Product entity
                    .SingleOrDefault(sod => sod.StockOutDetailId == stockOutDetailId)
                    ?? throw new Exception($"StockOutDetail with ID {stockOutDetailId} not found.");
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error retrieving stock out detail with ID {stockOutDetailId}", ex);
            }
        }

        public bool Create(StockOutDetail stockOutDetail)
        {
            try
            {
                _context.StockOutDetails.Add(stockOutDetail);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error creating stock out detail", ex);
            }
        }


        public bool Update(StockOutDetail stockOutDetail)
        {
            try
            {
                using (var db = new PRN221_WMSContext())
                {
                    var existing = db.StockOutDetails.SingleOrDefault(sod => sod.StockOutDetailId == stockOutDetail.StockOutDetailId);
                    if (existing != null)
                    {
                        // Update only non-null properties
                        existing.StockOutId = stockOutDetail.StockOutId; // Assuming StockOutId is always provided
                        existing.ProductId = stockOutDetail.ProductId; // Assuming ProductId is always provided
                        existing.Quantity = stockOutDetail.Quantity ?? existing.Quantity;
                        existing.PackingType = stockOutDetail.PackingType ?? existing.PackingType;
                        existing.Status = stockOutDetail.Status ?? existing.Status;

                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("StockOutDetail not found for updating.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error updating stock out detail", ex);
            }
        }

        public bool Delete(int stockOutDetailId)
        {
            try
            {
                using (var db = new PRN221_WMSContext())
                {
                    var stockOutDetail = db.StockOutDetails.SingleOrDefault(sod => sod.StockOutDetailId == stockOutDetailId);
                    if (stockOutDetail != null)
                    {
                        db.StockOutDetails.Remove(stockOutDetail);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("StockOutDetail not found for deletion.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error deleting stock out detail with ID {stockOutDetailId}", ex);
            }
        }

    }
}
