using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;

namespace WMS_DAL.DAO
{
    public class LotDetailDAO
    {
        private static LotDetailDAO instance = null;
        private static readonly object padlock = new object();
        private PRN221_WMSContext _context;

        private LotDetailDAO()
        {
            _context = new PRN221_WMSContext();
        }

        public static LotDetailDAO Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new LotDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public List<LotDetail> GetLotDetails()
        {
            try
            {
                return _context.LotDetails
                    .Include(ld => ld.Lot)       // Eager load related Lot entity
                    .Include(ld => ld.Product)   // Eager load related Product entity
                    .ToList();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error retrieving lot details with related entities", ex);
            }
        }

        public LotDetail GetLotDetailById(int lotDetailId)
        {
            try
            {
                return _context.LotDetails
                    .Include(ld => ld.Lot)       // Eager load related Lot entity
                    .Include(ld => ld.Product)   // Eager load related Product entity
                    .SingleOrDefault(ld => ld.LotDetailId == lotDetailId)
                    ?? throw new Exception($"LotDetail with ID {lotDetailId} not found.");
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error retrieving lot detail with ID {lotDetailId}", ex);
            }
        }

        public bool Create(LotDetail lotDetail)
        {
            try
            {
                _context.LotDetails.Add(lotDetail);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error creating lot detail", ex);
            }
        }

        public bool Update(LotDetail lotDetail)
        {
            try
            {
                using (var db = new PRN221_WMSContext())
                {
                    var existing = db.LotDetails.SingleOrDefault(ld => ld.LotDetailId == lotDetail.LotDetailId);
                    if (existing != null)
                    {
                        // Update only non-null properties
                        existing.LotId = lotDetail.LotId; // Assuming LotId is always provided
                        existing.ProductId = lotDetail.ProductId; // Assuming ProductId is always provided
                        existing.Quantity = lotDetail.Quantity ?? existing.Quantity;
                        existing.PackingType = lotDetail.PackingType ?? existing.PackingType;
                        existing.Status = lotDetail.Status ?? existing.Status;

                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("LotDetail not found for updating.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error updating lot detail", ex);
            }
        }

        public bool Delete(int lotDetailId)
        {
            try
            {
                using (var db = new PRN221_WMSContext())
                {
                    var lotDetail = db.LotDetails.SingleOrDefault(ld => ld.LotDetailId == lotDetailId);
                    if (lotDetail != null)
                    {
                        db.LotDetails.Remove(lotDetail);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("LotDetail not found for deletion.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error deleting lot detail with ID {lotDetailId}", ex);
            }
        }

    }
}
