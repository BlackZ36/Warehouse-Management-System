using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;

namespace WMS_DAL.Interface
{
    public interface IStorageRepository
    {
        List<StorageArea> GetStorageAreas();
        StorageArea GetStorageAreaByID(int id);
        bool AddStorageArea(StorageArea storage);
        bool UpdateStorageArea(StorageArea storage);
        bool BanStorageAreaStatus(int areaId);
        List<StorageArea> LoadArea();
    }
}
