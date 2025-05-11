using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;

namespace WMS_DAL.DAO
{
    public class PartnerDAO
    {
        private static PartnerDAO instance = null;
        private static readonly object padlock = new object();
        private PRN221_WMSContext _context;

        private PartnerDAO()
        {
            _context = new PRN221_WMSContext();
        }

        public static PartnerDAO Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new PartnerDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Partner> GetPartners()
        {
            try
            {
                return _context.Partners.ToList();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error retrieving partners", ex);
            }
        }

        public Partner GetPartnerById(int partnerId)
        {
            try
            {
                return _context.Partners.SingleOrDefault(p => p.PartnerId == partnerId);
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error retrieving partner with code {partnerId}", ex);
            }
        }

        public bool Add(Partner partner)
        {
            try
            {
                _context.Partners.Add(partner);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error adding partner", ex);
            }
        }

        public bool Update(Partner partner)
        {
            try
            {
                using (var db = new PRN221_WMSContext())
                {
                    var existing = db.Partners.SingleOrDefault(p => p.PartnerCode == partner.PartnerCode);
                    if (existing != null)
                    {
                        // Update only non-null properties
                        existing.PartnerType = partner.PartnerType;
                        existing.Name = partner.Name;
                        existing.Phone = partner.Phone;
                        existing.Email = partner.Email;
                        existing.UpdatedAt = DateTime.Now;
                        existing.Status = partner.Status;

                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("Partner not found for updating.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error updating partner", ex);
            }
        }


        public bool Delete(int partnerId)
        {
            try
            {
                var partner = _context.Partners.SingleOrDefault(p => p.PartnerId == partnerId);
                if (partner != null)
                {
                    _context.Partners.Remove(partner);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error deleting partner with code {partnerId}", ex);
            }
        }
    }
}
