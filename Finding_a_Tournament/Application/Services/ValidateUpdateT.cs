using System;
using Finding_a_Tournament.Domain.Entities;
namespace Finding_a_Tournament.Application.Services
{
    public class TorneoServices
    {
         public static bool ValidateUpdatet(Torneo torneo)
            {
                if(torneo.IdTorneo <= 0)
                return false;
            
                if(string.IsNullOrEmpty(torneo.NombreTorneo))
                    return false;

                return true;
            }
    }
}