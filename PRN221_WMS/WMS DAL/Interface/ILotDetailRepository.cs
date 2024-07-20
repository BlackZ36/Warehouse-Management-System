using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;

namespace WMS_DAL.Interface
{
    public interface ILotDetailRepository
    {
        List<LotDetail> GetLotDetails();
        LotDetail GetLotDetailById(int id);
        bool CreateLotDetail(LotDetail lotDetail);
        bool UpdateLotDetail(LotDetail lotDetail);
        bool DeleteLotDetail(int id);
    }
}
