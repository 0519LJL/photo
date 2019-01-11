using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace MyPhotoManagement.Common
{
    public static class JsonHelper 
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

        /// <summary>
        /// JavaScriptSerializer序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToString<T>(T obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(obj);
        }

    }
}