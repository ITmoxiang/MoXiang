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
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class SwaggerController : ControllerBase
    {
        private readonly SwaggeApp _swaggeApp;
        public SwaggerController(SwaggeApp swaggeApp) 
        {
            _swaggeApp = swaggeApp;
        }
        /// <summary>
        /// 导出接口离线文档
        /// </summary>
        /// <param name="type">文件类型</param>
        /// <param name="version">版本号V1</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<FileResult> ExportDocument(string type,string version)
        {
            var data = await _swaggeApp.ExportSwagger(type,version);
            return File(data, "application/pdf");
        }
    }
}
