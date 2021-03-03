using MoXiang.Repository.Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoXiang.Repository.Entities
{
    [ConnectionString("MysqlConnection", DatabaseType.MySql)]
    public partial class Socials
    {
        public Socials()
        {
        }

        [Description("Id")]
        [Browsable(false)]
        [Key]
        public int id { get; set; }

        [Description("用户Id")]
        [Browsable(false)]
        public int UserInfoId { get; set; }

        [Description("标题")]
        public string Title { get; set; }

        [Description("图标")]
        public string Icon { get; set; }

        [Description("颜色")]
        public string Color { get; set; }

        [Description("地址")]
        public string Href { get; set; }
    }
}
