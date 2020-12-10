using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abouca.Application.Dto;
using Abouca.Application.Services;
using Abouca.Domain.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Abouca.RestApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
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
            return Ok(userService.GetAll().Result);
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
