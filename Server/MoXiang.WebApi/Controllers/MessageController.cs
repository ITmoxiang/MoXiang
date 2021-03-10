using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoXiang.Application;
using MoXiang.Application.Request;
using MoXiang.Infrastructure.Returned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoXiang.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "v2")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageApp _messageApp;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MessageController(MessageApp messageApp, IHttpContextAccessor httpContextAccessor) 
        {
            _messageApp = messageApp;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost]
        public async Task<Response> Add(AddMessageReq req) 
        {
            //string ipaddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();//获取客户端ip地址
            var result = new Response();
            await _messageApp.Add(req);
            return result;
        }
    }
}
