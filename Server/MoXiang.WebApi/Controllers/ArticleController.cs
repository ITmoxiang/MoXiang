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
    public class ArticleController : ControllerBase
    {
        private readonly ArticleApp _app;
        public ArticleController(ArticleApp app) 
        {
            _app = app;
        }
        /// <summary>
        /// 查询文章列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<TableData> Load([FromQuery]QueryArticleReq req)
        {
            return await _app.Load(req);
        }
        /// <summary>
        /// 查询单个文章信息
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<TableData> GetDetails(int ArticleId)
        {
            return await _app.GetDetails(ArticleId);
        }
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> Add(AddOrUpDataArticleReq req)
        {
            Response response = new Response();
            await _app.Add(req);
            return response;
        }

        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> UpData(AddOrUpDataArticleReq req)
        {
            Response response = new Response();
            await _app.UpData(req);
            return response;
        }
    }
}
