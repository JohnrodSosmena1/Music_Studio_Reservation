using System;
using System.Collections.Generic;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class PromotionRationale
    {
        public int PromotionRationaleId { get; set; }
        public int PromotionId { get; set; }
        public string PurposeType { get; set; } = null!;
        public string? TargetAudience { get; set; }
        public string? TriggerCondition { get; set; }
        public string? ExpectedKpi { get; set; }
        public decimal? Budget { get; set; }
        public int? MaxRedemptions { get; set; }
        public int ActualRedemptions { get; set; } = 0;
        public string WorkflowStatus { get; set; } = "Draft";
        public string? ApprovedByUserId { get; set; }
        public string? ApprovedByName { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string? RejectionReason { get; set; }
        public string? RationaleNotes { get; set; }
        public decimal? ActualRevenueDelta { get; set; }
        public string? RoiSummary { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public virtual Promotion? Promotion { get; set; }
        public virtual ICollection<PromotionSegment> Segments { get; set; } = new List<PromotionSegment>();
    }

    public class PromotionSegment
    {
        public int PromotionSegmentId { get; set; }
        public int PromotionRationaleId { get; set; }
        public string SegmentName { get; set; } = null!;
        public virtual PromotionRationale? PromotionRationale { get; set; }
    }
}
