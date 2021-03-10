using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoXiang.Application;
using MoXiang.Infrastructure.Returned;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoXiang.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly MapApp _mapApp; 
        private readonly ILogger<MapController> _logger;

        public MapController(MapApp mapApp, ILogger<MapController> logger)
        {
            _mapApp = mapApp;
            _logger = logger;
        }
        /// <summary>
        /// 根据地址获取经纬度
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<TableData> GetGeocoding(string Address)
        {
            var result = new TableData();
            try
            {
                return await _mapApp.GetGeocoding(Address);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// NLog示例
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task aaaa()
        {
            _logger.LogWarning("警告你一次");
            _logger.LogError("报错一次");

            throw new Exception("报错啦");
        }

        /// <summary>
        /// redis示例
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> Get()
        {
            try
            {
                return await _mapApp.Get();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
