using System;
using System.Collections.Generic;
using System.Text;

namespace MoXiang.Infrastructure.Returned
{
    /// <summary>
    /// 通用返回类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TableData<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 操作消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 总记录条数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 数据内容
        /// </summary>
        public T Data { get; set; }

        public TableData()
        {
            Code = 200;
            Message = "加载成功";
        }
    }
    /// <summary>
    /// table的返回数据
    /// </summary>
    public class TableData : TableData<dynamic>
    {
    }
}
