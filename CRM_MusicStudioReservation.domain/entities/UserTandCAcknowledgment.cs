using System;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class UserTandCAcknowledgment
    {
        public int AcknowledgmentId { get; set; }
        public int TandCId { get; set; }
        public int CustomerId { get; set; }
        public string TandCVersion { get; set; } = null!;
        public string TandCType { get; set; } = null!;
        public string AcknowledgmentContext { get; set; } = null!;
        public DateTime AcknowledgedAt { get; set; } = DateTime.UtcNow;
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public virtual TermsAndConditions? TermsAndConditions { get; set; }
        public virtual Customer? Customer { get; set; }
    }
}
