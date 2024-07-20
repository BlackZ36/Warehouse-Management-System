using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;

namespace WMS_DAL.Interface
{
    public interface IStockOutRepository
    {
        List<StockOut> GetStockOuts();
        StockOut GetStockOutById(int id);
        bool CreateStockOut(StockOut stockOut);
        bool UpdateStockOut(StockOut stockOut);
        bool DeleteStockOut(int id);
    }
}
