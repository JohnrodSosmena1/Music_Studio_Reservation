using System;
using System.ComponentModel.DataAnnotations;
using CRM_MusicStudioReservation.domain.entities;

namespace CRM_MusicStudioReservation.api.DTOs
{
    public class StudioServiceCreateDto
    {
        [Required]
        [StringLength(50)]
        public string ServiceCode { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string ServiceName { get; set; } = null!;

        [Range(0, 100000)]
        public decimal Price { get; set; }
    }

    public class StudioServiceUpdateDto
    {
        [StringLength(50)]
        public string? ServiceCode { get; set; }

        [StringLength(100)]
        public string? ServiceName { get; set; }

        [Range(0, 100000)]
        public decimal? Price { get; set; }

        public bool? IsActive { get; set; }
    }

    public class StudioServiceResponseDto
    {
        public int StudioServiceId { get; set; }
        public string ServiceCode { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
