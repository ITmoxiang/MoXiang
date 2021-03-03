using AutoMapper;
using Dapper;
using MoXiang.Application.Request;
using MoXiang.Application.Response;
using MoXiang.Infrastructure.AutoMapper;
using MoXiang.Infrastructure.Redis;
using MoXiang.Infrastructure.Returned;
using MoXiang.Repository;
using MoXiang.Repository.Dapper;
using MoXiang.Repository.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoXiang.Application
{
    public class UserInfoApp
    {
        private readonly IRepositoryBase _repositorybase;
        public UserInfoApp(IRepositoryBase repositorybase)
        {
            _repositorybase = repositorybase;
        }
        /// <summary>
        /// dapper示例
        /// </summary>
        /// <returns></returns>
        public async Task Add(AddOrUpDataUserInfoReq req)
        {
            string userSql = "INSERT INTO  UserInfo(Account, Password, Phone, Name, Email, Slogan, Icon, Notice) VALUES(@Account,@Password,@Phone,@Name,@Email,@Slogan,@Icon,@Notice);";
            string socialsSql = "INSERT INTO  Socials(UserInfoId,Title,Icon,Color,Href) VALUES(@UserInfoId,@Title,@Icon,@Color,@Href)";
            //事务使用示例
            var dbContext = _repositorybase.DbConnection<UserInfo>();
            int num = 0;
            var user = req.MapTo<UserInfo>();
            using (var transaction = dbContext.BeginTransaction())
            {
                try
                {
                    var obj=await _repositorybase.AddAsync<UserInfo>(userSql, user, dbContext, transaction);
                    req.socials.ForEach(s => s.UserInfoId = obj);
                    await _repositorybase.BatchAddAsync<Socials>(socialsSql, req.socials, dbContext, transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }
        /// <summary>
        /// automapper示例
        /// </summary>
        /// <returns></returns>
        public async Task<TableData> Load()
        {
            var result = new TableData();
            string sql = "select * from UserInfo";
            var userinfos = _repositorybase.Find<UserInfo>(sql);
            var dto = userinfos.MapTo<AddOrUpDataUserInfoReq>();
            result.Data = dto;
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
        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <returns></returns>
        public async Task<TableData> GetUserDetails(int userid = 1)
        {
            var result = new TableData();
            string sql = "select * from UserInfo  where Id=@Id;";
            var Parameters = new DynamicParameters();
            Parameters.Add("Id", userid);
            var Details = _repositorybase.Find<UserInfo>(sql,Parameters).FirstOrDefault();
            var obj=Details.MapTo<AddOrUpDataUserInfoReq>();
            obj.socials = _repositorybase.Find<Socials>("select * from Socials where UserInfoId=@Id", Parameters).MapToList<AddOrUpDataSocialsReq>();
            result.Data = obj;
            return result;
        }


    }
}
