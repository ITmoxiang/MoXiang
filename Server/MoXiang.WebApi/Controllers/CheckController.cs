using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoXiang.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoXiang.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "v2")]
    [ApiController]
    public class CheckController : ControllerBase
    {
        private readonly CheckApp _checkApp;
        public CheckController(CheckApp checkApp)
        {
            _checkApp = checkApp;
        }
        [HttpGet]
        public async Task Cipher() 
        {
            await _checkApp.Cipher();
        }
    }
}
