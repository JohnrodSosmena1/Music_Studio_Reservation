using System;
using System.ComponentModel.DataAnnotations;

namespace CRM_MusicStudioReservation.api.DTOs
{
    public class TandCCreateDto
    {
        [Required]
        [StringLength(50)]
        public string TandCType { get; set; } = null!;

        [Required]
        [StringLength(300)]
        public string Title { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;

        public bool RequiresReAcceptance { get; set; } = false;

        [StringLength(2000)]
        public string? ChangeNotes { get; set; }
    }

    public class TandCUpdateDto
    {
        [StringLength(300)]
        public string? Title { get; set; }

        public string? Content { get; set; }

        public bool? RequiresReAcceptance { get; set; }

        [StringLength(2000)]
        public string? ChangeNotes { get; set; }
    }

    public class TandCPublishDto
    {
        [StringLength(500)]
        public string? ApproverNote { get; set; }
    }

    public class TandCRejectDto
    {
        [Required]
        [StringLength(1000)]
        public string Reason { get; set; } = null!;
    }

    public class TandCAcknowledgeDto
    {
        [Required]
        public int TandCId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public string Context { get; set; } = null!;
    }

    public class TandCResponseDto
    {
        public int TandCId { get; set; }
        public string TandCCode { get; set; } = null!;
        public string TandCType { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Version { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string? AuthorName { get; set; }
        public string? ApprovedByName { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public bool RequiresReAcceptance { get; set; }
        public string? ChangeNotes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class TandCListItemDto
    {
        public int TandCId { get; set; }
        public string TandCCode { get; set; } = null!;
        public string TandCType { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Version { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string? AuthorName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class TandCAcknowledgmentResponseDto
    {
        public int AcknowledgmentId { get; set; }
        public int TandCId { get; set; }
        public string TandCCode { get; set; } = null!;
        public string TandCVersion { get; set; } = null!;
        public string TandCType { get; set; } = null!;
        public int CustomerId { get; set; }
        public string Context { get; set; } = null!;
        public DateTime AcknowledgedAt { get; set; }
    }
}
