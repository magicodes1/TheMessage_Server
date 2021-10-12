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
    [Authorize(Roles ="USER")]
    public class MessageController : ControllerBase
    {

        private readonly IMessageService _messageService;
        private readonly IHubContext<ChatHub> _hubContext;
        public MessageController(IMessageService messageService, IHubContext<ChatHub> hubContext)
        {
            _messageService = messageService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            var result = await _messageService.get();
           
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> post([FromForm]AddMessageRequestResource addMessageResource)
        {
            if (addMessageResource == null)
            {
                throw new BadRequestException("Object is null.");
            }
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Object is invalid");
            }

            var result = await _messageService.add(addMessageResource);

            if (!result.Status)
            {
                throw new BadRequestException(result.Message);
            }
            await _hubContext.Clients.All.SendAsync("message", result.MessageContent);
            return Ok(result);
        }
    }
}
