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
        bool AddStockOut(StockOut stockOut);
        bool AddOneStockOutDetail(StockOutDetail detail);
        bool AddStockOutDetail(int stockOutId, List<StockOutDetail> stockOutDetails);
        List<StockOutDetail> GetStockOutsDetail();
        List<StockOutDetail> GetStockOutDetailById(int id);
        void UpdateStockOuts(StockOut stockOut);
        void UpdateStockOutsDetail(int stockOutDetailsId, int Quantity);
        void DeleteStockOutPermanently(StockOut stockOut);
        void DeleteStockOutDetailsPermanently(StockOutDetail stockOutDetail);
    }
}
