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
    public class LotRepository : ILotRepository
    {
        public List<Lot> GetAllLots()
        {
            throw new NotImplementedException();
        }

        public List<Lot> GetListLotByAccountID(int acID)
        {
            throw new NotImplementedException();
        }

        public List<Lot> GetListLotByPartnerID(int acID)
        {
            throw new NotImplementedException();
        }

        public Lot GetLotById(int id)
        {
            throw new NotImplementedException();
        }

        public Lot GetLotByAccountId(int id)
        {
            throw new NotImplementedException();
        }

        public Lot GetLotByLotCode(string code)
        {
            throw new NotImplementedException();
        }

        public Lot GetLotByPartnerId(int id)
        {
            throw new NotImplementedException();
        }

        public void AddLot(Lot lot)
        {
            throw new NotImplementedException();
        }

        public void UpdateLot(Lot lot)
        {
            throw new NotImplementedException();
        }

        public void DeleteLotPermanently(Lot lot)
        {
            throw new NotImplementedException();
        }

        public void DeleteLotStatus(Lot lot)
        {
            throw new NotImplementedException();
        }

        public List<LotDetail> GetAllLotDetail()
        {
            throw new NotImplementedException();
        }

        public List<LotDetail> GetListLotDetailByProductID(int pID)
        {
            throw new NotImplementedException();
        }

        public List<LotDetail> GetListLotDetailByPartnerID(int parID)
        {
            throw new NotImplementedException();
        }

        public List<LotDetail> GetListLotDetailByLotID(int lotID)
        {
            throw new NotImplementedException();
        }

        public LotDetail GetLotDetailById(int id)
        {
            throw new NotImplementedException();
        }

        public LotDetail GetLotDetailByProductId(int pid)
        {
            throw new NotImplementedException();
        }

        public LotDetail GetLotDetailByPartnerId(int id)
        {
            throw new NotImplementedException();
        }

        public LotDetail GetLotDetailByLotId(int id)
        {
            throw new NotImplementedException();
        }

        public void AddLotDetail(LotDetail detail)
        {
            throw new NotImplementedException();
        }

        public void UpdateLotDetail(LotDetail detail)
        {
            throw new NotImplementedException();
        }

        public void DeleteLotDetailPermanently(LotDetail detail)
        {
            throw new NotImplementedException();
        }

        public void DeleteLotDetailStatus(LotDetail detail)
        {
            throw new NotImplementedException();
        }

        public List<LotDetail> GetListLotDetailById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
