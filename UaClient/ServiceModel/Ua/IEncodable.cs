﻿// Copyright (c) Converter Systems LLC. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Workstation.ServiceModel.Ua;

public interface IEncodable
{
    void Encode(IEncoder encoder);

    void Decode(IDecoder decoder);
}