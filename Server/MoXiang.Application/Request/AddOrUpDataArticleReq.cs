using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoXiang.Application.Request
{
    public class AddOrUpDataArticleReq
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 正文
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 文章标签
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 文章头图片
        /// </summary>
        public string Banner { get; set; }
        /// <summary>
        /// 文章简介
        /// </summary>
        public string Summary { get; set; }
    }
}
