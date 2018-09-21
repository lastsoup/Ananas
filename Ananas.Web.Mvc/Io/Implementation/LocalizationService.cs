using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ananas.Web.Mvc.Base;
using System.Threading;
using System.Globalization;
using System.Web;
using Ananas.Web.Mvc.Extensions;
using NLog;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Ananas.Web.Mvc.Io.Implementation
{
    /// <summary>
    /// 本地资源服务
    /// </summary>
    internal class LocalizationService : ILocalizationService
    {
        private static readonly IDictionary<string, ResourceBase> cache = new Dictionary<string, ResourceBase>(StringComparer.OrdinalIgnoreCase);
        private static readonly ReaderWriterLockSlim syncLock = new ReaderWriterLockSlim();
        private readonly ResourceBase resource;    
        public LocalizationService(string resourceName, CultureInfo culture)
        {
            resource = DetectResource("~/App_GlobalResources", resourceName, culture);
        }

        //进入写入模式锁定状态,,如果缓存中已存在该键则退出写入模式
        private static ResourceBase DetectResource(string resourceLocation, string resourceName, CultureInfo culture)
        {
            string cacheKey = resourceName + ":" + culture;
            ResourceBase resource;
            // using (syncLock.ReadAndWrite())
            // {
            //     if (!cache.TryGetValue(cacheKey, out resource))
            //     {
            //         using (syncLock.Write())
            //         {
            //             if (!cache.TryGetValue(cacheKey, out resource))
            //             {
            //                 resource = CreateResource(resourceName, culture, resourceLocation);
            //                 cache.Add(cacheKey, resource);
            //             }
            //         }
            //     }
            // }
             syncLock.EnterReadLock();
             if (!cache.TryGetValue(cacheKey, out resource)){
                 syncLock.EnterWriteLock();
                 if (!cache.TryGetValue(cacheKey, out resource)){
                     resource = CreateResource(resourceName, culture, resourceLocation);
                      cache.Add(cacheKey, resource);
                 }
                 syncLock.ExitWriteLock();
             }
             syncLock.ExitReadLock();
            return resource;
        }

        private static ResourceBase CreateResource(string resourceName, CultureInfo culture, string resourceLocation)
        {
            Func<string, string> fixResourceName = c => resourceName + ((c != null) ? "." + c : string.Empty) + ".resx";
            //默认查找的资源文件格式：EditorLocalization.zh-CN.resx
            string fullResourcePath = Path.Combine(resourceLocation, fixResourceName(culture.ToString()));
            bool exists =  File.Exists(fullResourcePath);
            //如果不存在格式：EditorLocalization.zh.resx
            if (!exists)
            {
                fullResourcePath = Path.Combine(resourceLocation, fixResourceName(culture.TwoLetterISOLanguageName));
                exists = File.Exists(fullResourcePath);
            }
            //如果不存在格式：EditorLocalization.resx
            if (!exists)
            {
                fullResourcePath = Path.Combine(resourceLocation, fixResourceName(null));
                exists = File.Exists(fullResourcePath);
            }
            //获取资源
            ResourceBase resource = exists ?new ResXResource(fullResourcePath) :new EmbeddedResource(resourceName, culture) as ResourceBase;
            return resource;
        }

        public string One(string key)
        {
            return resource.GetByKey(key);
        }

        public bool IsDefault
        {
            get
            {
                return resource is EmbeddedResource;
            }
        }

        public IDictionary<string, string> All()
        {
            return resource.GetAll();
        }
    }
}
