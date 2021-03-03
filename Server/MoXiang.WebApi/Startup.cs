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
            //ע��automapper��δʵ��
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //ע��HttpClient
            services.AddHttpClient();
            //ע��swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MoXiangAPI",
                    Description = "�ӿ��ĵ�-ͨ��",
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
                    Description = "�ӿ��ĵ�-ҵ��"
                });

                // ��ȡxml�ļ���
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // ��ȡxml�ļ�·��
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // ��ӿ�������ע�ͣ�true��ʾ��ʾ������ע��
                c.IncludeXmlComments(xmlPath, true);
            }).AddScoped<SwaggerGenerator>();//ע��SwaggerGenerator,�������ֱ��ʹ���������
            //���ݿ�����
            //services.AddSingleton(s => new ConnectionFactory(DatabaseType.MySql, Configuration.GetConnectionString("MysqlDbContext")));
            //redis����
            var csredis = new CSRedis.CSRedisClient(Configuration.GetSection("Redis:Default").Value);
            RedisHelper.Initialization(csredis);
            //services.AddScoped(typeof(ICacheContext), typeof(RedisCacheContext));
            //services.AddSingleton(s => new ConnectionFactory(DatabaseType.SqlServer, Configuration.GetConnectionString("SqlserverDbContext")));
            //ע��ȫ���쳣����
            services.AddMvc(option =>
            {
                option.Filters.Add(typeof(GlobalExceptionFilter));
            });
            //���ÿ���

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
        /// AutoFac����ע��
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //ע��������ݿ�
            builder.RegisterType(typeof(RepositoryBase)).As(typeof(IRepositoryBase));
            //ע�����������Api
            builder.RegisterType(typeof(ThirdpartyHelper));
            //ע��Application��
            var asm = Assembly.Load("MoXiang.Application");//ָ��dll���Ƶĳ��򼯼�
            //var defulatAsm = Assembly.GetExecutingAssembly();//Ĭ��ִ�е�dll
            builder.RegisterAssemblyTypes(asm) //asm, 
                .PublicOnly();//��ע��public�ķ���
                              //.Except()�ų�ĳ����
                              //.Where(t => t.Name.EndsWith("Service") || t.Name == "ClassA")//����������дһЩ������������
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
            //swagger�緢����ϣ��������ʹ�ã�����if����
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "�ӿ��ĵ�-ͨ��");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "�ӿ��ĵ�-ҵ��");
            }
            );
            // �������п���CorsPolicy����ConfigureServices���������õĿ����������
            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //AutoMapperHelper��չ����ע��
            AutoMapperHelper.UseStateAutoMapper(app);
        }
    }
}
