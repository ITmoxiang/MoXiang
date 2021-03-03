using System;
using System.Collections.Generic;
using System.Text;

namespace MoXiang.Repository.Dapper
{
    public enum DatabaseType
    {
        SqlServer,  //SQLServer数据库
        MySql,      //Mysql数据库
        Npgsql,     //PostgreSQL数据库
        Oracle,     //Oracle数据库
        Sqlite     //SQLite数据库
    }
}
