using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoXiang.Application.Response
{
    public class ArticleListResp
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Banner { get; set; }
        public bool IsTop { get; set; }
        public bool IsHot { get; set; }
        public string Summary { get; set; }
        public int Hits { get; set; }
        public DateTime CreateTime { get; set; }
        public int commentsCount { get; set; }
    }
}
