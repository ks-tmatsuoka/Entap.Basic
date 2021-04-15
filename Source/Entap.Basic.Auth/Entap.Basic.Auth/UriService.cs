using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Entap.Basic.Auth
{
    public class UriService
    {
        /// <summary>
        /// 指定したURLにクエリを付加したURIを生成する
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryObject"></param>
        /// <returns></returns>
        public static Uri GetUri(string url, object queryObject)
        {
            var uriBuilder = new UriBuilder(url);

            var queryString = GetQueryString(queryObject);
            if (!string.IsNullOrEmpty(queryString))
                uriBuilder.Query = queryString;

            return new Uri(uriBuilder.ToString());
        }

        /// <summary>
        /// 任意のオブジェクトからQueryを生成する
        /// </summary>
        /// <param name="queryObject">オブジェクト</param>
        /// <returns>クエリ文字列</returns>
        public static string GetQueryString(object queryObject)
        {
            var requestJson = JsonConvert.SerializeObject(queryObject);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(requestJson);
            return string.Join("&",
                dictionary
                    .Where(arg => arg.Value != null)
                    .Select(arg => arg.Key + "=" + arg.Value));
        }

        /// <summary>
        /// クエリ文字列からオブジェクトを生成する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryString">クエリ文字列</param>
        /// <returns>オブジェクト</returns>
        public static T GetQueryObject<T>(string queryString)
        {
            var dictionary = GetQueryDictionary(queryString);
            return GetQueryObject<T>(dictionary);
        }

        /// <summary>
        /// Dictionaryからオブジェクトを生成する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dictionary">Dictionary</param>
        /// <returns>オブジェクト</returns>
        public static T GetQueryObject<T>(Dictionary<string, string> dictionary)
        {
            var json = JsonConvert.SerializeObject(dictionary);
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// クエリ文字列からDictionaryを生成する
        /// </summary>
        /// <param name="dictionary">Dictionary</param>
        /// <returns>クエリ文字列からDictionaryを生成する</returns>
        static Dictionary<string, string> GetQueryDictionary(string queryString)
        {
            var collection = HttpUtility.ParseQueryString(queryString);
            return collection.AllKeys.ToDictionary(k => k, k => collection[k]);
        }
    }
}
