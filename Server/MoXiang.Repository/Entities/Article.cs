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
    public partial class Article
    {
        public Article()
        {
            this.Hits = 0;
            this.IsHot = false;
            this.IsTop = false;
            this.CreateTime = DateTime.Now;
            this.CreateUser = "墨香";
            this.CreateUserId = 1;
        }

        [Description("Id")]
        [Browsable(false)]
        [Key]
        public int Id { get; set; }
        [Description("标题")]
        public string Title { get; set; }
        [Description("正文")]
        public string Content { get; set; }
        [Description("阅读次数")]
        public int Hits { get; set; }
        [Description("文章分类")]
        public string TypeId { get; set; }
        [Description("文章标签")]
        public string Label { get; set; }
        [Description("文章头图片")]
        public string Banner { get; set; }
        [Description("是否置顶")]
        public bool IsTop { get; set; }
        [Description("是否火热")]
        public bool IsHot { get; set; }
        [Description("文章简介")]
        public string Summary { get; set; }
        [Description("修改时间")]
        public DateTime UpdateTime { get; set; }
        [Description("创建时间")]
        public DateTime CreateTime { get; set; }
        [Description("创建人")]
        public string CreateUser { get; set; }
        [Description("创建人Id")]
        public int CreateUserId { get; set; }
    }
}
