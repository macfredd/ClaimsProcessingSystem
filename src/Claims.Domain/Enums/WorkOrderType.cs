namespace Claims.Domain.Enums;

/// <summary>
/// Types of work orders that can be generated.
/// </summary>
public enum WorkOrderType
{
    IssueRefund,
    ShipReplacement,
    ScheduleInspection,
    ManualReview
}
