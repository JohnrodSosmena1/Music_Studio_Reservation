using System;
using System.Text.Json.Serialization;

namespace CRM.winforms.DTOs
{
    // ==================== RESPONSE ====================

    public class CustomerInquiryDto
    {
        [JsonPropertyName("customerInquiryId")]
        public int CustomerInquiryId { get; set; }

        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; } = string.Empty;

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = "Open";

        [JsonPropertyName("priority")]
        public string Priority { get; set; } = "Normal";

        [JsonPropertyName("response")]
        public string? Response { get; set; }

        [JsonPropertyName("respondedAt")]
        public DateTime? RespondedAt { get; set; }

        [JsonPropertyName("respondedBy")]
        public string? RespondedBy { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        // -------- Helpers (not serialized) --------
        [JsonIgnore]
        public bool HasResponse => !string.IsNullOrWhiteSpace(Response);

        [JsonIgnore]
        public string ShortMessage
        {
            get
            {
                var m = (Message ?? "").Trim();
                return m.Length > 80 ? m.Substring(0, 80) + "…" : m;
            }
        }

        [JsonIgnore]
        public string StatusDisplay => Status switch
        {
            "Open" => "Open",
            "InProgress" => "In Progress",
            "Resolved" => "Resolved",
            "Closed" => "Closed",
            _ => Status
        };
    }

    // ==================== CREATE ====================

    public class CustomerInquiryCreateRequest
    {
        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; } = string.Empty;

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("priority")]
        public string Priority { get; set; } = "Normal";
    }

    // ==================== UPDATE ====================

    public class CustomerInquiryUpdateRequest
    {
        [JsonPropertyName("subject")]
        public string? Subject { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("priority")]
        public string? Priority { get; set; }
    }

    // ==================== RESPOND ====================

    public class CustomerInquiryRespondRequest
    {
        [JsonPropertyName("response")]
        public string Response { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }

    // ==================== STATUS CHANGE ====================

    public class CustomerInquiryStatusRequest
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = "Open";
    }
}