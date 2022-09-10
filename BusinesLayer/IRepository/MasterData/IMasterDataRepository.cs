using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.IRepository.MasterData
{
    public interface IMasterDataRepository:IRepositoryGeneric<Domains.Domains.MasterData.Country>
    {
        Task<IEnumerable<Domains.Domains.MasterData.Country>> lstCountry();
    }
}
