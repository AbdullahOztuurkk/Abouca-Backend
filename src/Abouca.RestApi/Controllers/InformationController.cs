using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abouca.Application.Dto;
using Abouca.Application.Services;
using Abouca.Domain.Information;
using Abouca.Domain.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Abouca.RestApi.Controllers
{
    [Route("api/informations")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly InformationService informationService;
        private IDistributedCache distributedCache;

        public InformationController(InformationService informationService, IDistributedCache distributedCache)
        {
            this.informationService = informationService;
            this.distributedCache = distributedCache;
        }

        [HttpPost]
        public async Task<IActionResult> AddInformation([FromBody] Information information)
        {
            try
            {
                informationService.Create(information);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(information);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetInformation(int id)
        {
            return Ok(informationService.GetOne(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetInformations()
        {
            var informations = distributedCache.GetString("Informations");
            if (String.IsNullOrEmpty(informations))
            {
                var option = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                };
                distributedCache.SetString("Informations", informationService.GetAll().Result.ToString(), option);
                return Ok(informationService.GetAll().Result);
            }

            return Ok(distributedCache.GetString("Informations"));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInformation([FromBody] Information information)
        {
            try
            {
                await informationService.Update(information);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteInformation(int id)
        {
            Information currentInformation = null;
            try
            {
                currentInformation = await informationService.Delete(new InformationDeleteDto(){UserId = id});

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(currentInformation);
        }
    }
}
