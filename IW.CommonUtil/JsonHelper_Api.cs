using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Common.Utility
{
    public class MyDateTimeFormat : IsoDateTimeConverter
    {
        public MyDateTimeFormat()
        {
            base.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        }

    }

    /// <summary>
    /// Json序列化和反序列化辅助类 
    /// </summary>
    public static class JsonHelper_Api
    {
        public static string ObjectToJson(object List)
        {
            return JsonConvert.SerializeObject(List);
        }

        public static string ListToJson<T>(T List)
        {
            return JsonConvert.SerializeObject(List);
        }

        public static T JsonToList<T>(string List)
        {
            return JsonConvert.DeserializeObject<T>(List);
        }

        public static string ObjectToJsonExtDateTime(object List)
        {

            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(List, Newtonsoft.Json.Formatting.Indented, timeFormat);
        }

        /// <summary>
        /// Newtonsoft.Json序列化（忽略循环引用）
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        /// <summary>
        /// Newtonsoft.Json反序列化（忽略循环引用）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(this string data)
        {
            return JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

    }
}
