using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MoXiang.Infrastructure.AutoMapper;
using MoXiang.Infrastructure.Thirdparty;
using MoXiang.Repository.Dapper;
using MoXiang.WebApi.Nlog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace MoXiang.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddControllersAsServices();
            //注册automapper暂未实现
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //注册HttpClient
            services.AddHttpClient();
            //注册swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MoXiangAPI",
                    Description = "接口文档-通用",
                    Contact = new OpenApiContact
                    {
                        Name = "zlg",
                        Email = "ITmoxiang@163.com",
                    }
                });

                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "MoXiangAPI",
                    Description = "接口文档-业务"
                });

                // 获取xml文件名
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // 获取xml文件路径
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // 添加控制器层注释，true表示显示控制器注释
                c.IncludeXmlComments(xmlPath, true);
            }).AddScoped<SwaggerGenerator>();//注入SwaggerGenerator,后面可以直接使用这个方法
            //数据库连接
            //services.AddSingleton(s => new ConnectionFactory(DatabaseType.MySql, Configuration.GetConnectionString("MysqlDbContext")));
            //redis缓存
            var csredis = new CSRedis.CSRedisClient(Configuration.GetSection("Redis:Default").Value);
            RedisHelper.Initialization(csredis);
            //services.AddScoped(typeof(ICacheContext), typeof(RedisCacheContext));
            //services.AddSingleton(s => new ConnectionFactory(DatabaseType.SqlServer, Configuration.GetConnectionString("SqlserverDbContext")));
            //注入全局异常处理
            services.AddMvc(option =>
            {
                option.Filters.Add(typeof(GlobalExceptionFilter));
            });
            //设置跨域

            services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(_ => true) // =AllowAnyOrigin()
                    .AllowCredentials();
            }));

        }
        /// <summary>
        /// AutoFac依赖注入
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //注册操作数据库
            builder.RegisterType(typeof(RepositoryBase)).As(typeof(IRepositoryBase));
            //注册操作第三方Api
            builder.RegisterType(typeof(ThirdpartyHelper));
            //注册Application层
            var asm = Assembly.Load("MoXiang.Application");//指定dll名称的程序集集
            //var defulatAsm = Assembly.GetExecutingAssembly();//默认执行的dll
            builder.RegisterAssemblyTypes(asm) //asm, 
                .PublicOnly();//仅注册public的方法
                              //.Except()排除某个类
                              //.Where(t => t.Name.EndsWith("Service") || t.Name == "ClassA")//可以在这里写一些过滤类名规则
                              //.AsImplementedInterfaces();
                              //AutofacExt.InitAutofac(builder);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //swagger如发布后希望不继续使用，移入if即可
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "接口文档-通用");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "接口文档-业务");
            }
            );
            // 允许所有跨域，CorsPolicy是在ConfigureServices方法中配置的跨域策略名称
            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //AutoMapperHelper扩展方法注册
            AutoMapperHelper.UseStateAutoMapper(app);
        }
    }
}
