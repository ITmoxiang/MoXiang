using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoXiang.Repository.Dapper
{
    public class RepositoryBase : IRepositoryBase
    {
        private readonly IConfiguration _configuration;
        public RepositoryBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection DbConnection<T>() where T : class
        {
            var ConstructorArguments = typeof(T).CustomAttributes.Select(c => c.ConstructorArguments).ToList();
            var values = ConstructorArguments?[0]?.Select(c => c.Value)?.ToList();
            var connstring = "";
            if (values != null)
            {
                connstring = _configuration.GetConnectionString(values[0].ToString());
                var databaseType = DatabaseType.MySql;
                switch (values[1])
                {
                    case 0:
                        databaseType = DatabaseType.SqlServer;
                        break;
                    case 1:
                        databaseType = DatabaseType.MySql;
                        break;
                    case 2:
                        databaseType = DatabaseType.Npgsql;
                        break;
                    case 3:
                        databaseType = DatabaseType.Oracle;
                        break;
                    case 4:
                        databaseType = DatabaseType.Sqlite;
                        break;
                    default:
                        break;
                }
                var conn = new ConnectionFactory(databaseType, connstring).CreateConnection();
                return conn;
            }
            throw new Exception("当前实体未指向数据库");
        }
        public int Add<T>(string insertSql, object entity = null, IDbConnection conn = null, IDbTransaction transaction = null) where T : class
        {
            conn = conn == null ? DbConnection<T>() : conn;
            insertSql += "SELECT LAST_INSERT_ID()";
            int id = conn.ExecuteScalar<int>(insertSql, entity, transaction);
            if (transaction == null) Close(conn);
            return id;
        }
        public async Task<int> AddAsync<T>(string insertSql, object entity = null, IDbConnection conn = null, IDbTransaction transaction = null) where T : class
        {
            conn = conn == null ? DbConnection<T>() : conn;
            insertSql += "SELECT LAST_INSERT_ID()";
            int id = await conn.ExecuteScalarAsync<int>(insertSql, entity, transaction);
            if (transaction == null) await CloseAsync(conn);
            return id;
        }
        public async Task<int> BatchAddAsync<T>(string insertSql, object entity = null, IDbConnection conn = null, IDbTransaction transaction = null) where T : class
        {
            conn = conn == null ? DbConnection<T>() : conn;
            int r = await conn.ExecuteAsync(insertSql, entity, transaction);
            if (transaction == null) await CloseAsync(conn);
            return r;
        }
        public async Task<IDataReader> GetAddAsync<T>( string insertSql, object entity = null, IDbConnection conn = null, IDbTransaction transaction =null) where T : class
        {
            conn=conn == null ? DbConnection<T>() : conn;
            var obj= await conn.ExecuteReaderAsync(insertSql, entity,transaction);
            if (transaction == null)await CloseAsync(conn);
            obj.Read();
            return obj;
        }

        public int Delete<T>(string deleteSql, object entity = null, IDbConnection conn = null, IDbTransaction transaction = null) where T : class
        {
            conn = conn == null ? DbConnection<T>() : conn;
            int r =  conn.Execute(deleteSql,null, transaction);
            if (transaction == null) Close(conn);
            return r;
        }

        public async Task<int> DeleteAsync<T>(string deleteSql, object entity = null,IDbConnection conn = null, IDbTransaction transaction = null) where T : class
        {
            conn = conn == null ? DbConnection<T>() : conn;
            int r = await conn.ExecuteAsync(deleteSql, null, transaction);
            if (transaction == null) await CloseAsync(conn);
            return r;
        }

        public object Detail<T>(string selectSql, object entity = null, IDbConnection conn = null, IDbTransaction transaction = null) where T : class
        {

            conn = conn == null ? DbConnection<T>() : conn;
            var result = conn.QueryFirstOrDefault(selectSql,entity);
            if (transaction == null) Close(conn);
            return result;
        }

        public async Task<object> DetailAsync<T>(string selectSql, object entity = null, IDbConnection conn = null, IDbTransaction transaction = null) where T : class
        {
            conn = conn == null ? DbConnection<T>() : conn;
            var result = conn.QueryFirstOrDefaultAsync(selectSql,entity);
            if (transaction == null) await CloseAsync(conn);
            return result;
        }

        public List<object> Find<T>(string selectSql, object entity = null, IDbConnection conn = null, IDbTransaction transaction = null) where T : class
        {
            conn = conn == null ? DbConnection<T>() : conn;
            var result = conn.Query(selectSql, entity).ToList();
            if (transaction == null) Close(conn);
            return result;
        }

        public async Task<List<object>> FindAsync<T>( string selectSql, object entity = null, IDbConnection conn = null, IDbTransaction transaction = null) where T : class
        {
            conn = conn == null ? DbConnection<T>() : conn;
            var result = await Task.Run(() => conn.Query(selectSql, entity).ToList());
            if (transaction == null)  await CloseAsync(conn);
            return result;
        }

        public int UpDate<T>( string updateSql, object entity =null, IDbConnection conn = null, IDbTransaction transaction = null) where T : class
        {
            conn = conn == null ? DbConnection<T>() : conn;
            int r =  conn.Execute(updateSql, entity, transaction);
            if (transaction == null) Close(conn);
            return r;
        }

        public async Task<int> UpDateAsync<T>(string updateSql, object entity = null, IDbConnection conn = null, IDbTransaction transaction = null) where T : class
        {
            conn = conn == null ? DbConnection<T>() : conn;
            int r = await conn.ExecuteAsync(updateSql, entity, transaction);
            if (transaction == null) await CloseAsync(conn);
            return r;
        }
       

        public void Close(IDbConnection conn)
        {
            conn.Close();
            conn.Dispose();
        }

        public Task CloseAsync(IDbConnection conn)
        {
            conn.Close();
            conn.Dispose();
            return Task.CompletedTask;
        }

    }
}
