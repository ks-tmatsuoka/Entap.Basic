﻿using System;
using Entap.Basic.Auth.Abstractions;

namespace Entap.Basic.Auth.Abstractions
{
    public interface IAuthManager
    {
        bool IsPasswordAuthSupported { get; }
        IPasswordAuthService PasswordAuthService { get; }
    }
}
