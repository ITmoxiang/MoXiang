using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MoXiang.Infrastructure.AutoMapper
{
    /// <summary>
    /// AutoMapper帮助类
    /// </summary>
    public static class AutoMapperHelper
    {
        private static IServiceProvider ServiceProvider;

        public static void UseStateAutoMapper(this IApplicationBuilder applicationBuilder)
        {
            ServiceProvider = applicationBuilder.ApplicationServices;
        }

        /// <summary>
        ///  单个对象映射
        /// </summary>
        public static T MapTo<T>(this object obj)
        {
            try
            {
                var mapper = ServiceProvider.GetRequiredService<IMapper>();
                return mapper.Map<T>(obj);
            }
            catch (Exception)
            {

                var config = new MapperConfiguration(cfg => cfg.CreateMap(obj.GetType(), typeof(T)));
                var mapper = config.CreateMapper();
                return mapper.Map<T>(obj);
            }
            
        }

        /// <summary>
        /// 集合列表类型映射
        /// </summary>
        public static List<TDestination> MapToList<TDestination>(this IEnumerable source)
        {
            try
            {
                var mapper = ServiceProvider.GetRequiredService<IMapper>();
                return mapper.Map<List<TDestination>>(source);
            }
            catch (Exception ex)
            {
                var ms = ex.Message;
                Type sourceType = source.GetType().GetGenericArguments()[0];  //获取枚举的成员类型
                var config = new MapperConfiguration(cfg => cfg.CreateMap(sourceType, typeof(TDestination)));
                var mapper = config.CreateMapper();
                return mapper.Map<List<TDestination>>(source);
            }
        }
        /// <summary>
        /// 类型映射
        /// </summary>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
            where TSource : class
            where TDestination : class
        {
            try
            {
                var mapper = ServiceProvider.GetRequiredService<IMapper>();
                return mapper.Map<TSource, TDestination>(source);
            }
            catch (Exception)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap(typeof(TSource), typeof(TDestination)));
                var mapper = config.CreateMapper();
                return mapper.Map<TDestination>(source);
            }
        }
    }
}
