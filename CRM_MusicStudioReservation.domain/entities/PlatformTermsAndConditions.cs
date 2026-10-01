using System;
using System.Collections.Generic;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class PlatformTermsAndConditions
    {
        public int PlatformTandCId { get; set; }

        public string TandCCode { get; set; } = string.Empty; // e.g. PTC-00001

        public string TandCType { get; set; } = "PlatformTerms"; // PlatformTerms, PrivacyPolicy, DataProcessingAgreement, TenantAgreement

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string Version { get; set; } = "v1.0";

        public int MajorVersion { get; set; } = 1;

        public int MinorVersion { get; set; } = 0;

        public string Status { get; set; } = "Draft"; // Draft, Published, Archived

        public DateTime? PublishedAt { get; set; }

        public string? AuthorName { get; set; } = "Super Admin";

        public string? ChangeNotes { get; set; }

        public bool RequiresReAcceptance { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<PlatformTandCAcknowledgment> Acknowledgments { get; set; } = new List<PlatformTandCAcknowledgment>();
    }

    public class PlatformTandCAcknowledgment
    {
        public int AcknowledgmentId { get; set; }

        public int PlatformTandCId { get; set; }

        public int CompanyId { get; set; }

        public string AcknowledgedByEmail { get; set; } = string.Empty;

        public string AcknowledgedByName { get; set; } = string.Empty;

        public string Version { get; set; } = string.Empty;

        public string? IpAddress { get; set; }

        public DateTime AcknowledgedAt { get; set; } = DateTime.UtcNow;

        public virtual PlatformTermsAndConditions? PlatformTermsAndConditions { get; set; }

        public virtual Company? Company { get; set; }
    }
}
