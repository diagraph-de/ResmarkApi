﻿// Copyright (c) Converter Systems LLC. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Workstation.ServiceModel.Ua;

public class IssuedIdentity : IUserIdentity
{
    public IssuedIdentity(byte[] tokenData)
    {
        TokenData = tokenData;
    }

    public byte[] TokenData { get; }
}