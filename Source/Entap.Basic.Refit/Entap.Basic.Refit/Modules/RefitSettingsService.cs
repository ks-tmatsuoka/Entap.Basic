using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;

namespace Entap.Basic.Refit
{
    public static class RefitSettingsService
    {
        #region ContentSerializer
        /// <summary>
        /// SnakeCase用のシリアライザを取得する
        /// </summary>
        public static NewtonsoftJsonContentSerializer SnakeCaseSerializer
            => new NewtonsoftJsonContentSerializer(
                new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    }
                });

        /// <summary>
        /// CamelCase用のシリアライザを取得する
        /// </summary>
        public static NewtonsoftJsonContentSerializer CamelCaseSerializer
            => new NewtonsoftJsonContentSerializer(
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        #endregion
    }
}
