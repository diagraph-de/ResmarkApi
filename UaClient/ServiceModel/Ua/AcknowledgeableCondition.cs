// Copyright (c) Converter Systems LLC. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Workstation.ServiceModel.Ua;

/// <summary>
///     Represents an acknowledgeable condition.
/// </summary>
public class AcknowledgeableCondition : Condition
{
    [EventField(ObjectTypeIds.AcknowledgeableConditionType, "AckedState/Id")]
    public bool? AckedState { get; set; }

    [EventField(ObjectTypeIds.AcknowledgeableConditionType, "ConfirmedState/Id")]
    public bool? ConfirmedState { get; set; }
}