using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;

namespace WMS_DAL.Interface
{
    public interface IStockOutDetailRepository
    {
        List<StockOutDetail> GetStockOutDetails();
        StockOutDetail GetStockOutDetailById(int id);
        bool CreateStockOutDetail(StockOutDetail stockOutDetail);
        bool UpdateStockOutDetail(StockOutDetail stockOutDetail);
        bool DeleteStockOutDetail(int id);
    }
}
