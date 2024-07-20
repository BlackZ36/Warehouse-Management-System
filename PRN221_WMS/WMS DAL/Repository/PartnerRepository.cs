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
    public class PartnerRepository : IPartnerRepository
    {
        public List<Partner> GetPartners() => PartnerDAO.Instance.GetPartners();

        public Partner GetPartnerById(int id) => PartnerDAO.Instance.GetPartnerById(id);

        public bool CreatePartner(Partner partner) => PartnerDAO.Instance.Add(partner);

        public bool UpdatePartner(Partner partner) => PartnerDAO.Instance.Update(partner);

        public bool DeletePartner(int id) => PartnerDAO.Instance.Delete(id);
    }
}
