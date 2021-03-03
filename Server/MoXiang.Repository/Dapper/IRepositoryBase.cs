using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MoXiang.Repository.Dapper
{
    public interface IRepositoryBase
    {
        IDbConnection DbConnection<T>() where T : class; //开启数据库连接
        int Add<T>( string insertSql, object entity = null, IDbConnection conn = null, IDbTransaction transaction = null) where T : class; //添加并获取id
        Task<int> AddAsync<T>( string insertSql, object entity = null, IDbConnection conn = null, IDbTransaction transaction = null) where T : class;//异步添加并获取id
        Task<int> BatchAddAsync<T>(string insertSql, object entity = null, IDbConnection conn = null, IDbTransaction transaction = null) where T : class;
        Task<IDataReader> GetAddAsync<T>(string insertSql, object entity = null, IDbConnection conn = null, IDbTransaction transaction = null) where T : class;//异步添加返回查询信息
        int UpDate<T>( string updateSql, object entity = null, IDbConnection conn = null, IDbTransaction transaction = null) where T : class;//修改
        Task<int> UpDateAsync<T>( string updateSql, object entity = null, IDbConnection conn = null, IDbTransaction transaction = null) where T : class;//异步修改
        int Delete<T>(string deleteSql, object entity = null, IDbConnection conn = null, IDbTransaction transaction = null) where T : class;//删除
        Task<int> DeleteAsync<T>( string deleteSql, object entity = null, IDbConnection conn = null, IDbTransaction transaction = null) where T : class;//异步删除
        List<object> Find<T>( string selectSql, object entity = null) where T : class;//查询列表
        Task<List<object>> FindAsync<T>(string selectSql, object entity = null) where T : class;//异步查询列表
        //T Detail<T>(string selectSql) where T : class;//查询单个数据
        //Task<T> DetailAsync<T>(string selectSql) where T : class;//异步查询单个数据
        void Close(IDbConnection conn);
        Task CloseAsync(IDbConnection conn);
    }
}
