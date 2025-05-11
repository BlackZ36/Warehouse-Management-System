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
    public class StorageRepository : IStorageRepository
    {
        public List<Storage> GetStorages() => StorageDAO.Instance.GetStorages();

        public Storage GetStorageById(int id) => StorageDAO.Instance.GetStorageById(id);

        public bool AddStorage(Storage storage) => StorageDAO.Instance.AddStorage(storage);

        public bool UpdateStorage(Storage storage) => StorageDAO.Instance.UpdateStorage(storage);

        public bool DeleteStorage(int id) => StorageDAO.Instance.DeleteStorage(id);
    }
}
