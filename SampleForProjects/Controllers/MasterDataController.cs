using BusinesLayer.Services.MasterData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SampleForProjects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        public IMasterDataService _masterDataService { get; }
        public MasterDataController(IMasterDataService masterDataService)
        {
            _masterDataService = masterDataService;
        }

      
        [HttpPost("Country")]
        public async Task<IActionResult> Country()
        {
            var data=await _masterDataService.lstCountry();
            return Ok(data);
        }
    }
}
