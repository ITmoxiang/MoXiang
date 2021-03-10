using MoXiang.Repository.Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoXiang.Repository.Entities
{
    [ConnectionString("MysqlConnection", DatabaseType.MySql)]
    public class Message
    {
        public Message()
        {
        }

        [Description("Id")]
        [Key]
        public int id { get; set; }

        [Description("邮箱")]
        public int EMail { get; set; }

        [Description("留言内容")]
        public string Content { get; set; }

        [Description("创建时间")]
        public DateTime CreateTime { get; set; }
    }
}
