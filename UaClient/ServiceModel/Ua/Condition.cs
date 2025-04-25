// Copyright (c) Converter Systems LLC. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Workstation.ServiceModel.Ua;

/// <summary>
///     Represents a condition.
/// </summary>
public class Condition : BaseEvent
{
    [EventField(ObjectTypeIds.ConditionType, attributeId: AttributeIds.NodeId)]
    public NodeId? ConditionId { get; set; }

    [EventField(ObjectTypeIds.ConditionType, "ConditionName")]
    public string? ConditionName { get; set; }

    [EventField(ObjectTypeIds.ConditionType, "BranchId")]
    public NodeId? BranchId { get; set; }

    [EventField(ObjectTypeIds.ConditionType, "Retain")]
    public bool? Retain { get; set; }
}