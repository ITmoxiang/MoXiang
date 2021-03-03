using MoXiang.Infrastructure.Returned;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MoXiang.Infrastructure.Thirdparty
{
    /// <summary>
    /// 第三方api帮助类
    /// </summary>
    public class ThirdpartyHelper
    {
        private  IHttpClientFactory _httpClient;
        public ThirdpartyHelper(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        /// <summary>
        /// 调用第三方Api Get
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public  async Task<string> GetApi(string url) 
        {
            //使用注入的httpclientfactory获取client
            var client = _httpClient.CreateClient();
            client.BaseAddress = new Uri(url);
            //设置请求体中的内容，并以post的方式请求
            var response = await client.GetAsync(url);
            //获取请求到数据，并转化为字符串
            var dataJson = response.Content.ReadAsStringAsync().Result;
            return dataJson;
        }

        /// <summary>
        /// 调用第三方Api Post
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public  async Task<TableData> PostApi<T>(string url,T condition) where T:class
        {
            //使用注入的httpclientfactory获取client
            var client = _httpClient.CreateClient();
            client.BaseAddress = new Uri(url);
            //设置请求体中的内容，并以post的方式请求
            var response = await client.PostAsync(url,new ThirdpartyJsonContent(condition));
            //获取请求到数据，并转化为字符串
            var dataJson = response.Content.ReadAsStringAsync().Result;
            var result = new TableData();
            result.Data = dataJson;
            return result;
        }
    }
}
