using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;

namespace WMS_DAL.DAO
{
    public class StorageDAO
    {
        private static StorageDAO instance = null;

        public StorageDAO() { }

        public static StorageDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StorageDAO();
                }
                return instance;
            }
        }

        public List<Storage> GetStorages()
        {
            List<Storage> storages;
            try
            {
                var context = new PRN221_WMSContext();
                storages = context.Storages.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return storages;
        }

        public Storage GetStorageById(int id)
        {
            Storage storage = null;
            try
            {
                var context = new PRN221_WMSContext();
                storage = context.Storages.FirstOrDefault(s => s.StorageId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return storage;
        }

        public bool AddStorage(Storage storage)
        {
            try
            {
                bool storageCheck = GetStorages().Any(s => s.StorageId == storage.StorageId);
                if (storageCheck != true)
                {
                    using (var context = new PRN221_WMSContext())
                    {
                        context.Storages.Add(storage);
                        context.SaveChanges();
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateStorage(Storage storage)
        {
            try
            {
                using (var db = new PRN221_WMSContext())
                {
                    var existing = db.Storages.SingleOrDefault(x => x.StorageId == storage.StorageId);
                    if (existing != null)
                    {
                        existing.StorageCode = storage.StorageCode;
                        existing.Name = storage.Name;
                        existing.Province = storage.Province;
                        existing.Address = storage.Address;
                        existing.MaxCapacity = storage.MaxCapacity;
                        existing.CurrentCapacity = storage.CurrentCapacity;
                        existing.UpdatedAt = DateTime.Now;

                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Storage not found for updating.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Update Storage: {ex.Message}", ex);
                return false;
            }
        }
        public bool DeleteStorage(int id)
        {
            var storage = GetStorageById(id);
            if (storage != null && storage.CurrentCapacity == 0)
            {
                using (var context = new PRN221_WMSContext())
                {
                    context.Storages.Remove(storage);
                    context.SaveChanges();
                }
                return true;
            }
            return false;
        }

    }
}
