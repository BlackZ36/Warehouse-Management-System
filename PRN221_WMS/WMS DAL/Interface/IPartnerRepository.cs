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
        Partner GetPartnerById(int id);
        bool CreatePartner(Partner partner);
        bool UpdatePartner(Partner partner);
        bool DeletePartner(int id);
    }
}
