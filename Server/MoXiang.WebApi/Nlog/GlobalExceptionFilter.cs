using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MoXiang.Infrastructure.Returned;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

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

                //报错地址
                string url = context.HttpContext.Request.Host+"/"+context.HttpContext.Request.Path;
                //报错参数
                string parameter = context.HttpContext.Request.QueryString.ToString();
                //报错请求方式
                string method = context.HttpContext.Request.Method.ToString();

                //写入日志
                _logger.LogError($"报错地址:{url},请求方式：{method},参数:{parameter},异常描述：{ex.Message},堆栈信息：{ex.StackTrace}");

                //定义返回信息
                Response res = new Response();
                if (new Regex("^[\u4E00-\u9FA5]{0,}$").IsMatch(ex.Message))
                {
                    res.Message = ex.Message;
                }
                else 
                {
                    res.Message = "网络不稳定，请稍后重试";
                }
                res.Code = 500;
               
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
