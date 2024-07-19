using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;
using WMS_DAL;
using WMS_DAL.Interface;

namespace WMS_DAL.Implement
{
    public class StorageAreaRepository : IStorageRepository
    {

        private readonly StorageAreaDAO _storageAreaDAO;

        public StorageAreaRepository()
        {
            _storageAreaDAO = new StorageAreaDAO();
        }

        public List<StorageArea> GetStorageAreas() => _storageAreaDAO.GetStorageAreas();

        public StorageArea GetStorageAreaByID(int id) => _storageAreaDAO.GetStorageAreaByID(id);

        public bool AddStorageArea(StorageArea storage) => _storageAreaDAO.AddStorageArea(storage);

        public bool UpdateStorageArea(StorageArea storage) => _storageAreaDAO.UpdateStorageArea(storage);

        public bool BanStorageAreaStatus(int areaId) => _storageAreaDAO.BanStorageAreaStatus(areaId);
        public List<StorageArea> LoadArea() => _storageAreaDAO.LoadArea();
    }
}
