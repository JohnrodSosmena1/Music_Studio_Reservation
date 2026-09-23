using System;
using System.Text.Json.Serialization;

namespace CRM.winforms.DTOs
{
    // ==================== RESPONSE ====================

    public class CustomerReviewDto
    {
        [JsonPropertyName("customerReviewId")]
        public int CustomerReviewId { get; set; }

        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }

        [JsonPropertyName("studioId")]
        public int? StudioId { get; set; }

        [JsonPropertyName("bookingId")]
        public int? BookingId { get; set; }

        [JsonPropertyName("reviewType")]
        public string ReviewType { get; set; } = "General";

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; } = string.Empty;

        [JsonPropertyName("moderationStatus")]
        public string ModerationStatus { get; set; } = "Pending";

        [JsonPropertyName("isVerified")]
        public bool IsVerified { get; set; }

        [JsonPropertyName("adminReply")]
        public string? AdminReply { get; set; }

        [JsonPropertyName("repliedAt")]
        public DateTime? RepliedAt { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        // -------- Helpers (not serialized) --------
        [JsonIgnore]
        public string Stars => new string('★', Rating) + new string('☆', 5 - Rating);

        [JsonIgnore]
        public string DisplayTitle =>
            string.IsNullOrWhiteSpace(Title) ? "(no title)" : Title!;

        [JsonIgnore]
        public string CommentsPreview
        {
            get
            {
                var c = (Comment ?? "").Trim();
                return c.Length > 100 ? c.Substring(0, 100) + "…" : c;
            }
        }
    }

    // ==================== CREATE ====================

    public class CustomerReviewCreateRequest
    {
        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }

        [JsonPropertyName("studioId")]
        public int? StudioId { get; set; }

        [JsonPropertyName("bookingId")]
        public int? BookingId { get; set; }

        [JsonPropertyName("reviewType")]
        public string ReviewType { get; set; } = "General";

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; } = string.Empty;
    }

    // ==================== UPDATE ====================

    public class CustomerReviewUpdateRequest
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("rating")]
        public int? Rating { get; set; }

        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        [JsonPropertyName("reviewType")]
        public string? ReviewType { get; set; }
    }

    // ==================== MODERATE ====================

    public class CustomerReviewModerateRequest
    {
        [JsonPropertyName("moderationStatus")]
        public string ModerationStatus { get; set; } = "Pending";
    }

    // ==================== REPLY ====================

    public class CustomerReviewReplyRequest
    {
        [JsonPropertyName("adminReply")]
        public string AdminReply { get; set; } = string.Empty;
    }
}