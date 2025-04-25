﻿// Copyright (c) Converter Systems LLC. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Workstation.ServiceModel.Ua;

[DataTypeId(DataTypeIds.DiagnosticInfo)]
public sealed class DiagnosticInfo
{
    public DiagnosticInfo(int namespaceUri = -1, int symbolicId = -1, int locale = -1, int localizedText = -1,
        string? additionalInfo = null, StatusCode innerStatusCode = default, DiagnosticInfo? innerDiagnosticInfo = null)
    {
        NamespaceUri = namespaceUri;
        SymbolicId = symbolicId;
        Locale = locale;
        LocalizedText = localizedText;
        AdditionalInfo = additionalInfo;
        InnerStatusCode = innerStatusCode;
        InnerDiagnosticInfo = innerDiagnosticInfo;
    }

    public int NamespaceUri { get; }

    public int SymbolicId { get; }

    public int Locale { get; }

    public int LocalizedText { get; }

    public string? AdditionalInfo { get; }

    public StatusCode InnerStatusCode { get; }

    public DiagnosticInfo? InnerDiagnosticInfo { get; }
}