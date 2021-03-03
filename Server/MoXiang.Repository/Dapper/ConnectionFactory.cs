using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MoXiang.Repository.Dapper
{
    public class ConnectionFactory
    {
        private  DatabaseType dbType;
        private  string strConn;

        public ConnectionFactory(DatabaseType dbtype, string strconn)
        {
            dbType = dbtype;
            strConn = strconn;
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="dbtype">数据库类型</param>
        /// <param name="conStr">数据库连接字符串</param>
        /// <returns>数据库连接</returns>
        public  IDbConnection CreateConnection(string dbtype, string strConn)
        {
            if (string.IsNullOrWhiteSpace(dbtype))
                throw new ArgumentNullException("没有数据库类型");
            if (string.IsNullOrWhiteSpace(strConn))
                throw new ArgumentNullException("没有数据库连接字符");
            var dbType = GetDataBaseType(dbtype);
            return CreateConnection(dbType.ToString(), strConn);
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="conStr">数据库连接字符串</param>
        /// <returns>数据库连接</returns>
        public  IDbConnection CreateConnection()
        {
            IDbConnection connection = null;
            if (string.IsNullOrWhiteSpace(strConn))
                throw new ArgumentNullException("没有数据库连接字符");

            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    connection = new SqlConnection(strConn);
                    break;
                case DatabaseType.MySql:
                    connection = new MySqlConnection(strConn);
                    break;
                //case DatabaseType:
                //    connection = new NpgsqlConnection(strConn);
                //    break;
                default:
                    throw new ArgumentNullException($"不支持的{dbType.ToString()}数据库类型");

            }
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }

        /// <summary>
        /// 转换数据库类型
        /// </summary>
        /// <param name="dbtype">数据库类型字符串</param>
        /// <returns>数据库类型</returns>
        public  DatabaseType GetDataBaseType(string dbtype)
        {
            if (string.IsNullOrWhiteSpace(dbtype))
                throw new ArgumentNullException("没有数据库类型");
            DatabaseType returnValue = DatabaseType.SqlServer;
            foreach (DatabaseType dbType in Enum.GetValues(typeof(DatabaseType)))
            {
                if (dbType.ToString().Equals(dbtype, StringComparison.OrdinalIgnoreCase))
                {
                    returnValue = dbType;
                    break;
                }
            }
            return returnValue;
        }

    }
}
