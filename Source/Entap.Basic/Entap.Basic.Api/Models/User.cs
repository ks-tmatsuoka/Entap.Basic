using System;
using Newtonsoft.Json;

namespace Entap.Basic.Api
{
    public class User
    {
        public User()
        {
        }

        /// <summary>
        /// 名前
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
