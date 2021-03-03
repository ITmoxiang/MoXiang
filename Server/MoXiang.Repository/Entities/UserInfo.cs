using Dapper.Contrib.Extensions;
using MoXiang.Repository.Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MoXiang.Repository.Entities
{
    [ConnectionString("MysqlConnection", DatabaseType.MySql)]
    public partial class UserInfo
    {
        public UserInfo()
        {
        }

        [Description("用户Id")]
        [Browsable(false)]
        [Key]
        public int id { get; set; }

        [Description("用户账户")]
        [Browsable(false)]
        public string Account { get; set; }

        [Description("用户密码")]
        [Browsable(false)]
        public string Password { get; set; }

        [Description("用户名称")]
        public string Name { get; set; }

        [Description("用户邮箱")]
        public string EMail { get; set; }

        [Description("用户手机号")]
        public string Phone { get; set; }

        [Description("口号")]
        public string Slogan { get; set; }

        [Description("头像")]
        public string Icon { get; set; }

        [Description("通知")]
        public string Notice { get; set; }

    }
}
