using System;
using Microsoft.Extensions.DependencyInjection;

namespace Entap.Basic.Core
{
    public class BasicStartup
    {
        public ServiceCollection ServiceCollection;
        public IServiceProvider ServiceProvider => ServiceCollection.BuildServiceProvider();

        static readonly Lazy<BasicStartup> _instance = new Lazy<BasicStartup>(() => new BasicStartup());
        public static BasicStartup Current => _instance.Value;

        public BasicStartup()
        {
            ServiceCollection = new ServiceCollection();
        }

        /// <summary>
        /// DIサービスを取得する
        /// </summary>
        public T GetService<T>() => ServiceProvider.GetService<T>();
    }
}


