using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Common.Utility
{
        /// <summary>
        /// AutoMapper扩展帮助类
        /// </summary>
        public static class AutoMapperHelper
        {
            /// <summary>
            ///  类型映射
            /// </summary>
            public static T MapTo<T>(this object obj)
            {
                if (obj == null) return default(T);
                Mapper.CreateMap(obj.GetType(), typeof(T));
                return Mapper.Map<T>(obj);
            }

            /// <summary>
            /// 映射类型，不返回结果
            /// </summary>
            /// <typeparam name="TSource">源类型</typeparam>
            /// <typeparam name="TDestination">目标类型</typeparam>
            /// <param name="ignoreMembers">要排除的成员</param>
            public static void CreateMap<TSource, TDestination>(List<Expression<Func<TDestination, object>>> ignoreMembers = null)
            {
                var map = Mapper.CreateMap<TSource, TDestination>();

                if (ignoreMembers != null)
                {
                    foreach (var member in ignoreMembers)
                    {
                        map.ForMember(member, opt => opt.Ignore());
                    }
                }
            }

            /// <summary>
            /// 集合列表类型映射
            /// </summary>
            public static List<TDestination> MapToList<TDestination>(this IEnumerable source)
            {
                foreach (var first in source)
                {
                    var type = first.GetType();
                    CreateNestedMappers(type, typeof(TDestination));
                    break;
                }
                return Mapper.Map<List<TDestination>>(source);
            }
            /// <summary>
            /// 集合列表类型映射
            /// </summary>
            public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source)
            {
                //IEnumerable<T> 类型需要创建元素的映射
                //Mapper.CreateMap<TSource, TDestination>();
                CreateNestedMappers(typeof(TSource), typeof(TDestination));
                return Mapper.Map<List<TDestination>>(source);
            }
            /// <summary>
            /// 类型映射
            /// </summary>
            public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
                where TSource : class
                where TDestination : class
            {
                if (source == null) return destination;
                Mapper.CreateMap<TSource, TDestination>();
                return Mapper.Map(source, destination);
            }
            /// <summary>
            /// DataReader映射
            /// </summary>
            public static IEnumerable<T> DataReaderMapTo<T>(this IDataReader reader)
            {
                Mapper.Reset();
                Mapper.CreateMap<IDataReader, IEnumerable<T>>();
                return Mapper.Map<IDataReader, IEnumerable<T>>(reader);
            }

            /// <summary>
            /// 递归创建映射
            /// </summary>
            /// <param name="sourceType"></param>
            /// <param name="destinationType"></param>
            public static void CreateNestedMappers(Type sourceType, Type destinationType)
            {
                PropertyInfo[] sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo[] destinationProperties = destinationType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var destinationProperty in destinationProperties)
                {
                    Type destinationPropertyType = destinationProperty.PropertyType;
                    if (Filter(destinationPropertyType))
                        continue;

                    PropertyInfo sourceProperty = sourceProperties.FirstOrDefault(prop => NameMatches(prop.Name, destinationProperty.Name));
                    if (sourceProperty == null)
                        continue;

                    Type sourcePropertyType = sourceProperty.PropertyType;
                    if (destinationPropertyType.IsGenericType)
                    {
                        Type destinationGenericType = destinationPropertyType.GetGenericArguments()[0];
                        if (Filter(destinationGenericType))
                            continue;

                        Type sourceGenericType = sourcePropertyType.GetGenericArguments()[0];
                        CreateNestedMappers(sourceGenericType, destinationGenericType);
                    }
                    else
                    {
                        CreateNestedMappers(sourcePropertyType, destinationPropertyType);
                    }
                }

                Mapper.CreateMap(sourceType, destinationType);
            }

            /// <summary>
            /// 标准类型过滤 (Filter)
            /// </summary>
            /// <param name="type"></param>
            /// <returns></returns>
            static bool Filter(Type type)
            {
                return type.IsPrimitive || NoPrimitiveTypes.Contains(type.Name);
            }

            static readonly HashSet<string> NoPrimitiveTypes = new HashSet<string>() { "String", "DateTime", "Decimal","Guid" };

            private static bool NameMatches(string memberName, string nameToMatch)
            {
                return String.Compare(memberName, nameToMatch, StringComparison.OrdinalIgnoreCase) == 0;
            }
        }
    
}
