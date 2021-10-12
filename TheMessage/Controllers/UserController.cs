using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMessage.Exceptions;
using TheMessage.Hubs;
using TheMessage.Interfaces.Servives;
using TheMessage.Resources;

namespace TheMessage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IHubContext<ChatHub> _hubContext;

        public UserController(IUserService userService, IHubContext<ChatHub> hubContext)
        {
            _userService = userService;
            _hubContext = hubContext;
        }

        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> signUp(SignUpResource sign)
        {
            if (sign == null)
            {
                throw new BadRequestException("object is null");
            }
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Object is invalid");
            }

            var result = await _userService.signUp(sign);

            if (!result.Status)
            {
                throw new BadRequestException(result.Message);
            }
            return Ok(result);
        }


        [Route("signin")]
        [HttpPost]
        public async Task<IActionResult> signIp(SignInResource sign)
        {
            if (sign == null)
            {
                throw new BadRequestException("object is null");
            }
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Object is invalid");
            }

            var result = await _userService.signIn(sign);

            if (!result.Status)
            {
                throw new BadRequestException(result.Message);
            }

            await _hubContext.Clients.All.SendAsync("online",result.User);

            return Ok(result);
        }

        [Route("logout/{Id}")]
        [HttpGet]
        public async Task<IActionResult> signUp(string Id)
        {
            if (Id == null)
            {
                throw new BadRequestException("object is null");
            }
            

            var result = await _userService.logout(Id);

            if (!result.Status)
            {
                throw new BadRequestException(result.Message);
            }

            await _hubContext.Clients.All.SendAsync("offline",Id);

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles ="USER")]
        public async Task<IActionResult> onlineUsers()
        {
           

            var result = await _userService.getOnlineUsers();

            if (!result.Status)
            {
                throw new BadRequestException(result.Message);
            }
            return Ok(result);
        }
    }
}
