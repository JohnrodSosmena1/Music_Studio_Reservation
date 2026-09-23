using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using CRM_MusicStudioReservation.domain.entities;
using CRM_MusicStudioReservation.domain.enums;

namespace CRM_MusicStudioReservation.api.DTOs
{
    public class StudioCreateDto
    {
        [Required]
        [StringLength(50)]
        public string StudioCode { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string StudioName { get; set; } = null!;

        [Required]
        public StudioType StudioType { get; set; }

        [Range(0, 100000)]
        public decimal HourlyRate { get; set; }

        [Range(1, 1000)]
        public int Capacity { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }
    }

    public class StudioUpdateDto
    {
        [StringLength(50)]
        public string? StudioCode { get; set; }

        [StringLength(100)]
        public string? StudioName { get; set; }

        public StudioType? StudioType { get; set; }

        [Range(0, 100000)]
        public decimal? HourlyRate { get; set; }

        [Range(1, 1000)]
        public int? Capacity { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public bool? IsActive { get; set; }
    }

    public class StudioResponseDto
    {
        public int StudioId { get; set; }
        public string StudioCode { get; set; } = null!;
        public string StudioName { get; set; } = null!;
        public StudioType StudioType { get; set; }
        public decimal HourlyRate { get; set; }
        public int Capacity { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
