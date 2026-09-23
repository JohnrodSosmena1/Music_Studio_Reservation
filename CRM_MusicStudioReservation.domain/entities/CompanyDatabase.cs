using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_MusicStudioReservation.domain.entities
{

    public class CompanyDatabase
    {
        public int CompanyDatabaseId { get; set; }

        public int CompanyId { get; set; }

        public string ServerName { get; set; } = string.Empty;

        public string DatabaseName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        // 👇 ADDED — used by the multi-tenant services to pick which credentials to load
        public string CredentialKey { get; set; } = string.Empty;

        public Company? Company { get; set; }

    }
}