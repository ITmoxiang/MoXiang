using System;
using System.Collections.Generic;
using System.Text;

namespace MoXiang.Repository.Dapper
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class ConnectionStringAttribute : Attribute
    {
        public ConnectionStringAttribute(string ConnectionStringName, DatabaseType DbType)
        {
            this.ConnectionStringName = ConnectionStringName;
            this.DbType = DbType;
        }

        public string ConnectionStringName { get; set; }
        public string ConnectionString { get; set; }

        public DatabaseType DbType { get; set; }
    }
}
