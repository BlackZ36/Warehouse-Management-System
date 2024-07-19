using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;

namespace WMS_DAL.Interface
{
    public interface IPartnerRepository
    {
        List<Partner> GetPartners();
        Partner GetPartnerByID(int id);
        void AddPartner(Partner partner);
        bool UpdatePartner(Partner partner);
        bool BanPartner(int id);
    }
}
