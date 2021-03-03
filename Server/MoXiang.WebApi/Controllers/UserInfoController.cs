using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using MoXiang.Application;
using MoXiang.Application.Response;
using MoXiang.Infrastructure.Returned;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoXiang.WebApi.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    [Route("api/[controller]/[action]")]
    public class UserInfoController : ControllerBase
    {
        private readonly UserInfoApp _userinfoApp;
        public UserInfoController(UserInfoApp userinfoApp)
        {
            _userinfoApp = userinfoApp;
        }

        [HttpPost]
        public async Task<Response> Add(AddOrUpDataUserInfoReq req)
        {
            var result = new Response();
            try
            {
                await _userinfoApp.Add(req);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[ApiExplorerSettings(IgnoreApi = true)] //隐藏此接口
        public async Task<TableData> Load()
        {
            var result = new TableData();
            try
            {
                return await _userinfoApp.Load();
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message= ex.Message;
            }
            return result;
        }

        /// <summary>
        /// redis示例
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string>  Get()
        {
            try
            {
                return await _userinfoApp.Get();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<TableData> GetUserDetails()
        {
            return await _userinfoApp.GetUserDetails();
        }

    }
}
