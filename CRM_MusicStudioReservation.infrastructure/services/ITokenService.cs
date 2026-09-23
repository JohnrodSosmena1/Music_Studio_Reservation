using System;
using System.Collections.Generic;
using System.Text;
using CRM_MusicStudioReservation.domain.entities;

namespace CRM_MusicStudioSystem.infrastructure.services
{
    public interface ITokenService
    {
        string GenerateToken(AppUser user);
    }
}
