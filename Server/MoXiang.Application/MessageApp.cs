using Microsoft.AspNetCore.Http;
using MoXiang.Application.Request;
using MoXiang.Infrastructure.Cipher;
using MoXiang.Repository.Dapper;
using MoXiang.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoXiang.Application
{
    public class MessageApp
    {
        private readonly IRepositoryBase _repositorybase;
        public MessageApp(IRepositoryBase repositorybase)
        {
            _repositorybase = repositorybase;
        }

        public async Task Add(AddMessageReq req) 
        {
            // 从Redis里面取数据
            var number = await RedisHelper.GetAsync("number");
            if (number == null)
            {
                DateTime dt1 = DateTime.Parse(DateTime.Now.ToShortDateString() + " 23:59:59");
                DateTime dt2 = DateTime.Now;
                TimeSpan ts = new TimeSpan();
                ts = dt1 - dt2;
                await RedisHelper.SetAsync("number",0,Convert.ToInt32(ts.TotalSeconds));
                number = "0";
            }

            if (int.Parse(number) > 20)
            {
                throw new Exception("今日留言已达上线，请明日重试。");
            }
            else 
            {
                string sql = $"insert into Message(Content,EMail) values (@Content,@EMail);";
                var num = await _repositorybase.AddAsync<Message>(sql, req);
                if (num <= 0)
                {
                    throw new Exception("留言失败，请重试。");
                }
                await RedisHelper.IncrByAsync("number",1);
            }
        }
    }
}
