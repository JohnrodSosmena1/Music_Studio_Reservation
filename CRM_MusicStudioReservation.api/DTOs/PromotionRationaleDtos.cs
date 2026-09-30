using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM_MusicStudioReservation.api.DTOs
{
    public class PromotionRationaleCreateDto
    {
        [Required]
        [StringLength(100)]
        public string PurposeType { get; set; } = null!;

        [StringLength(200)]
        public string? TargetAudience { get; set; }

        [StringLength(500)]
        public string? TriggerCondition { get; set; }

        [StringLength(500)]
        public string? ExpectedKpi { get; set; }

        public decimal? Budget { get; set; }

        public int? MaxRedemptions { get; set; }

        [StringLength(2000)]
        public string? RationaleNotes { get; set; }

        public List<string> Segments { get; set; } = new List<string>();
    }

    public class PromotionRationaleUpdateDto
    {
        [StringLength(100)]
        public string? PurposeType { get; set; }

        [StringLength(200)]
        public string? TargetAudience { get; set; }

        [StringLength(500)]
        public string? TriggerCondition { get; set; }

        [StringLength(500)]
        public string? ExpectedKpi { get; set; }

        public decimal? Budget { get; set; }

        public int? MaxRedemptions { get; set; }

        [StringLength(2000)]
        public string? RationaleNotes { get; set; }

        public List<string>? Segments { get; set; }
    }

    public class PromotionRationaleApproveDto
    {
        [StringLength(500)]
        public string? ApproverNote { get; set; }
    }

    public class PromotionRationaleRejectDto
    {
        [Required]
        [StringLength(1000)]
        public string Reason { get; set; } = null!;
    }

    public class PromotionRationaleRoiDto
    {
        [Required]
        public decimal ActualRevenueDelta { get; set; }

        [Required]
        [StringLength(2000)]
        public string RoiSummary { get; set; } = null!;
    }

    public class PromotionRationaleResponseDto
    {
        public int PromotionRationaleId { get; set; }
        public int PromotionId { get; set; }
        public string PurposeType { get; set; } = null!;
        public string? TargetAudience { get; set; }
        public string? TriggerCondition { get; set; }
        public string? ExpectedKpi { get; set; }
        public decimal? Budget { get; set; }
        public int? MaxRedemptions { get; set; }
        public int ActualRedemptions { get; set; }
        public string WorkflowStatus { get; set; } = null!;
        public string? ApprovedByName { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string? RejectionReason { get; set; }
        public string? RationaleNotes { get; set; }
        public decimal? ActualRevenueDelta { get; set; }
        public string? RoiSummary { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<string> Segments { get; set; } = new List<string>();
    }
}
