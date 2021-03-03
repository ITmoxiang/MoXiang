using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoXiang.Application.Request
{
    public class PageReq
    {
        public int page { get; set; }
        public int limit { get; set; }

        public string key { get; set; }

        public PageReq()
        {
            page = 0;
            limit = 10;
        }
    }
}
