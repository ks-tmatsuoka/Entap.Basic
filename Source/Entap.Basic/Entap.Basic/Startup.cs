using System;
using Microsoft.Extensions.DependencyInjection;

namespace Entap.Basic
{
    public static class Startup
    {
        public static ServiceCollection ServiceCollection { get; set; }
        public static IServiceProvider ServiceProvider => ServiceCollection.BuildServiceProvider();

        static Startup()
        {
            ServiceCollection = new ServiceCollection();
        }
    }
}
