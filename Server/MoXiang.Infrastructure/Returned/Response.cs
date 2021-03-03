using System;
using System.Collections.Generic;
using System.Text;

namespace MoXiang.Infrastructure.Returned
{
    /// <summary>
    /// 通用返回类
    /// </summary>
    public class Response
    {
        public Response() 
        {
            Code = 200;
            Message = "操作成功";
        }

        public int Code { get; set; }

        public string Message { get; set; }
    }
}
