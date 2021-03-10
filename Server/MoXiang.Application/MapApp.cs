using MoXiang.Infrastructure.Returned;
using MoXiang.Infrastructure.Thirdparty;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MoXiang.Infrastructure.Json;

namespace MoXiang.Application
{
    public class MapApp
    {
        private IHttpClientFactory _httpClient;//暂时不用
        private ThirdpartyHelper _thirdpartyHelper;
        public MapApp( IHttpClientFactory httpClient, ThirdpartyHelper thirdpartyHelper)
        {
            _httpClient = httpClient;
            _thirdpartyHelper = thirdpartyHelper;
        }
        /// <summary>
        ///  使用第三方api示例
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public async Task<TableData> GetGeocoding(string Address)
        {
            var url = $"http://api.map.baidu.com/geocoding/v3/?address={Address}&output=json&ak=uGyEag9q02RPI81dcfk7h7vT8tUovWfG&callback=showLocation";
            var dataJson =await _thirdpartyHelper.GetApi(url);
            int index = dataJson.IndexOf("(");
            dataJson = dataJson.Substring(index + 1,(dataJson.Length- index- 2)).ToString();
            var result = new TableData();
            result.Data = dataJson;
            return result;
        }


        /// <summary>
        /// redis示例
        /// </summary>
        /// <returns></returns>
        public async Task<string> Get()
        {
            // 往Redis里面存入数据
            //await _redis.HashSetAsync("Token",new HashEntry[] { new HashEntry(new RedisValue("a1"), new RedisValue("tom")) {  }, new HashEntry(new RedisValue("a2"), new RedisValue("zlg")) { } });
            await RedisHelper.SetAsync("Token", "123546");
            // 从Redis里面取数据
            var name = await RedisHelper.GetAsync("Token");
            return name;
        }

    }
}
