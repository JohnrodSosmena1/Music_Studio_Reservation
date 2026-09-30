using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CRM.winforms.DTOs
{
    public class TermsListItemDto
    {
        [JsonPropertyName("tandCId")]
        public int TandCId { get; set; }

        [JsonPropertyName("tandCCode")]
        public string TandCCode { get; set; } = string.Empty;

        [JsonPropertyName("tandCType")]
        public string TandCType { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("authorName")]
        public string? AuthorName { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }

    public class TermsDetailDto
    {
        [JsonPropertyName("tandCId")]
        public int TandCId { get; set; }

        [JsonPropertyName("tandCCode")]
        public string TandCCode { get; set; } = string.Empty;

        [JsonPropertyName("tandCType")]
        public string TandCType { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("authorName")]
        public string? AuthorName { get; set; }

        [JsonPropertyName("approvedByName")]
        public string? ApprovedByName { get; set; }

        [JsonPropertyName("approvedAt")]
        public DateTime? ApprovedAt { get; set; }

        [JsonPropertyName("publishedAt")]
        public DateTime? PublishedAt { get; set; }

        [JsonPropertyName("requiresReAcceptance")]
        public bool RequiresReAcceptance { get; set; }

        [JsonPropertyName("changeNotes")]
        public string? ChangeNotes { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }

    public class TermsCreateRequest
    {
        [JsonPropertyName("tandCType")]
        public string TandCType { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("requiresReAcceptance")]
        public bool RequiresReAcceptance { get; set; }

        [JsonPropertyName("changeNotes")]
        public string? ChangeNotes { get; set; }
    }

    public class TermsUpdateRequest
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("content")]
        public string? Content { get; set; }

        [JsonPropertyName("requiresReAcceptance")]
        public bool? RequiresReAcceptance { get; set; }

        [JsonPropertyName("changeNotes")]
        public string? ChangeNotes { get; set; }
    }

    public class TermsPublishRequest
    {
        [JsonPropertyName("approverNote")]
        public string? ApproverNote { get; set; }
    }

    public class TermsRejectRequest
    {
        [JsonPropertyName("reason")]
        public string Reason { get; set; } = string.Empty;
    }

    public class TermsAcknowledgmentDto
    {
        [JsonPropertyName("acknowledgmentId")]
        public int AcknowledgmentId { get; set; }

        [JsonPropertyName("tandCId")]
        public int TandCId { get; set; }

        [JsonPropertyName("tandCCode")]
        public string TandCCode { get; set; } = string.Empty;

        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }

        [JsonPropertyName("tandCVersion")]
        public string TandCVersion { get; set; } = string.Empty;

        [JsonPropertyName("tandCType")]
        public string TandCType { get; set; } = string.Empty;

        [JsonPropertyName("acknowledgmentContext")]
        public string AcknowledgmentContext { get; set; } = string.Empty;

        [JsonPropertyName("acknowledgedAt")]
        public DateTime AcknowledgedAt { get; set; }

        [JsonPropertyName("ipAddress")]
        public string? IpAddress { get; set; }

        [JsonPropertyName("userAgent")]
        public string? UserAgent { get; set; }
    }
}
