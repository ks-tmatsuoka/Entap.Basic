using System;
using System.Net;

namespace Entap.Basic.Api
{
    /// <summary>
    /// System.Net.HttpStatusCodeに定義のないコードを定義する
    /// https://docs.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=netframework-2.0
    /// </summary>
    public struct ExHttpStatusCode
    {
        public const HttpStatusCode UnprocessableEntity = (HttpStatusCode)422;
    }
}
