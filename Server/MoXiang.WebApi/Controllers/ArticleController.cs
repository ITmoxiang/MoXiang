using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoXiang.Application;
using MoXiang.Application.Request;
using MoXiang.Infrastructure.Returned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        /// 修改文章
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

        /// <summary>
        /// 取得HTML中所有图片的 URL。
        /// </summary>
        /// <param name="sHtmlText">HTML代码</param>
        /// <returns>图片的URL列表</returns>
        [HttpPost]
        public async Task<string[]> GetHtmlImageUrlList(string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            // 搜索匹配的字符串
            MatchCollection matches = regImg.Matches(sHtmlText);
            int i = 0;
            string[] sUrlList = new string[matches.Count];
            // 取得匹配项列表
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;
            return sUrlList;
        }
    }
}
