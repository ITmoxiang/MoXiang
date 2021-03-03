using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MoXiang.Infrastructure.Returned;
using Newtonsoft.Json;
using System;

namespace MoXiang.WebApi.Nlog
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> _logger)
        {
            this._logger = _logger;
        }

        public void OnException(ExceptionContext context)
        {
            // 如果异常没有被处理则进行处理
            if (context.ExceptionHandled == false)
            {
                //日志入库
                Exception ex = context.Exception;
                //这里给系统分配标识，监控异常肯定不止一个系统。
                int sysId = 1;
                //这里获取服务器ip时，需要考虑如果是使用nginx做了负载，这里要兼容负载后的ip，
                //监控了ip方便定位到底是那台服务器出故障了
                string ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
                //写入日志
                _logger.LogError($"系统编号：{sysId},主机IP:{ip},异常描述：{ex.Message},堆栈信息：{ex.StackTrace}");

                //定义返回信息
                Response res = new Response();
                res.Code = 500;
                res.Message = "网络不稳定，请稍后重试";
                context.Result = new ContentResult
                {
                    // 返回状态码设置为200，表示成功
                    StatusCode = StatusCodes.Status500InternalServerError,
                    // 设置返回格式
                    ContentType = "application/json;charset=utf-8",
                    Content = JsonConvert.SerializeObject(res)
                };
            }
            // 设置为true，表示异常已经被处理了
            context.ExceptionHandled = true;

        }
    }
}
