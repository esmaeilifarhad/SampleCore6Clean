using BusinesLayer.IRepository.MasterData;
using Domains.Domains.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.Services.MasterData
{
    public class MasterDataService : IMasterDataService
    {
        public IMasterDataRepository _masterDataRepository { get; }
        public MasterDataService(IMasterDataRepository masterDataRepository)
        {
            _masterDataRepository = masterDataRepository;
        }

      

        public async Task<List<Country>> lstCountry()
        {
           var data=await _masterDataRepository.GetAll();
            return data.ToList();
        }
    }
}
