using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;

namespace WMS_DAL.Interface
{
    public interface ILotRepository
    {
        List<Lot> GetAllLots();
        List<Lot> GetListLotByAccountID(int acID);
        List<Lot> GetListLotByPartnerID(int acID);
        Lot GetLotById(int id);
        Lot GetLotByAccountId(int id);
        Lot GetLotByLotCode(string code);
        Lot GetLotByPartnerId(int id);
        void AddLot(Lot lot);
        void UpdateLot(Lot lot);
        void DeleteLotPermanently(Lot lot);
        void DeleteLotStatus(Lot lot);

        //Detail
        List<LotDetail> GetAllLotDetail();
        List<LotDetail> GetListLotDetailByProductID(int pID);
        List<LotDetail> GetListLotDetailByPartnerID(int parID);
        List<LotDetail> GetListLotDetailByLotID(int lotID);
        LotDetail GetLotDetailById(int id);
        LotDetail GetLotDetailByProductId(int pid);
        LotDetail GetLotDetailByPartnerId(int id);
        LotDetail GetLotDetailByLotId(int id);
        void AddLotDetail(LotDetail detail);
        void UpdateLotDetail(LotDetail detail);
        void DeleteLotDetailPermanently(LotDetail detail);
        void DeleteLotDetailStatus(LotDetail detail);
        List<LotDetail> GetListLotDetailById(int id);
    }
}
