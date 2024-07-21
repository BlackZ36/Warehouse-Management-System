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
        List<Storage> GetStorages();
        Storage GetStorageById(int id);
        bool AddStorage(Storage storage);
        bool UpdateStorage(Storage storage);
        bool DeleteStorage(int id);
    }
}
