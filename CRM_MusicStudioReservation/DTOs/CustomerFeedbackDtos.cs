using System;
using System.Text.Json.Serialization;

namespace CRM.winforms.DTOs
{
    // ==================== RESPONSE ====================

    public class CustomerFeedbackDto
    {
        [JsonPropertyName("feedbackId")]
        public int FeedbackId { get; set; }

        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }

        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        [JsonPropertyName("comments")]
        public string? Comments { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        // -------- Helpers (not serialized) --------
        [JsonIgnore]
        public string Stars => new string('★', Rating) + new string('☆', 5 - Rating);

        [JsonIgnore]
        public string RatingLabel => Rating switch
        {
            5 => "Excellent",
            4 => "Good",
            3 => "Average",
            2 => "Poor",
            1 => "Very Bad",
            _ => "—"
        };
    }

    // ==================== CREATE / UPDATE ====================

    public class CustomerFeedbackCreateRequest
    {
        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }

        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        [JsonPropertyName("comments")]
        public string? Comments { get; set; }
    }
}