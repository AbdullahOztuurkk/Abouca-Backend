using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abouca.Application.Dto;
using Abouca.Application.Services;
using Abouca.Domain.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Abouca.RestApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;
        private IDistributedCache distributedCache;

        public UserController(UserService userService, IDistributedCache distributedCache)
        {
            this.userService = userService;
            this.distributedCache = distributedCache;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserRegisterDto user)
        {
            try
            {
                userService.Create(user);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(user);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok(userService.GetOne(id).Result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = distributedCache.GetString("Users");
            if (String.IsNullOrEmpty(users))
            {
                var option = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                };
                distributedCache.SetString("Users", userService.GetAll().Result.ToString(), option);
                return Ok(userService.GetAll().Result);
            }

            return Ok(distributedCache.GetString("Users"));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody]User user)
        {
            try
            {
                await userService.Update(user);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            User currentUser = null;
            try
            {
                currentUser = await userService.GetOne(id);
                await userService.Delete(currentUser);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(currentUser);
        }
    }
}
