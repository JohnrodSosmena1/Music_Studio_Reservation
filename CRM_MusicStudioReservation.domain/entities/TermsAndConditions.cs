using System;
using System.Collections.Generic;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class TermsAndConditions
    {
        public int TandCId { get; set; }
        public string TandCCode { get; set; } = null!;
        public string TandCType { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Version { get; set; } = null!;
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public string Status { get; set; } = "Draft";
        public string? AuthorUserId { get; set; }
        public string? AuthorName { get; set; }
        public string? ApprovedByUserId { get; set; }
        public string? ApprovedByName { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public bool RequiresReAcceptance { get; set; } = false;
        public string? ChangeNotes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public virtual ICollection<UserTandCAcknowledgment> Acknowledgments { get; set; } = new List<UserTandCAcknowledgment>();
    }
}
