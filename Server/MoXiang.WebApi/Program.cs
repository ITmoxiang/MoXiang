using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace MoXiang.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ���ö�ȡָ��λ�õ�nlog.config�ļ�
            NLogBuilder.ConfigureNLog("Nlog/Nlog.config");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())//AutoFac
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseUrls("http://*:9636");
                }).UseNLog();
    }
}
