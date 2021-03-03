using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoXiang.Application.Request
{
    public class QueryArticleReq: PageReq
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
    }
}
