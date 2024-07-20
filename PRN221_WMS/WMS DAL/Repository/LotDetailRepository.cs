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
    public class LotDetailRepository : ILotDetailRepository
    {
        public List<LotDetail> GetLotDetails() => LotDetailDAO.Instance.GetLotDetails();

        public LotDetail GetLotDetailById(int id) => LotDetailDAO.Instance.GetLotDetailById(id);

        public bool CreateLotDetail(LotDetail lotDetail) => LotDetailDAO.Instance.Create(lotDetail);

        public bool UpdateLotDetail(LotDetail lotDetail) => LotDetailDAO.Instance.Update(lotDetail);

        public bool DeleteLotDetail(int id) => LotDetailDAO.Instance.Delete(id);
    }
}
