using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class Device
    {
        public int DeviceId { get; set; }
        public string DeviceCode { get; set; } = string.Empty;
        public string DeviceName { get; set; } = string.Empty;

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}