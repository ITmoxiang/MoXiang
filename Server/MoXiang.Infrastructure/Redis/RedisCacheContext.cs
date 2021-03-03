using System;
using System.Collections.Generic;
using System.Text;

namespace MoXiang.Infrastructure.Redis
{
    /// <summary>
    /// 实现缓存接口（暂时不用）
    /// </summary>
    public class RedisCacheContext : ICacheContext
    {
        public override T Get<T>(string key)
        {
            return RedisHelper.Get<T>(key);
        }

        public override bool Remove(string key)
        {
            return RedisHelper.Del(key) > 0;
        }

        public override bool Set<T>(string key, T t, DateTime expire)
        {
            var obj = Get<T>(key);
            if (obj != null)
            {
                Remove(key);
            }
            return RedisHelper.Set(key, t, Convert.ToInt32((expire - DateTime.Now).TotalSeconds));
        }
    }
}
