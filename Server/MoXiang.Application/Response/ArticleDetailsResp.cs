using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoXiang.Application.Response
{
    public class ArticleDetailsResp
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TypeName { get; set; }
        public int Hits { get; set; }
        public string Content { get; set; }
        public DateTime UpdateTime { get; set; }

        public List<CommentsListResp> CommentsList { get; set; }
    }

    public class CommentsListResp {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public int CommentsId { get; set; }
    }
}
