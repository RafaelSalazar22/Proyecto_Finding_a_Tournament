
using System;
using Finding_a_Tournament.Domain.Entities;

namespace Finding_a_Tournament.Application.Services
{
    public class ClubsServices
    {      
        public static bool ValidateUpdate(Club Club)
            {
                if(Club.IdClub <= 0)
                return false;
            
                if(string.IsNullOrEmpty(Club.NombreClub))
                    return false;

                return true;
            }
    }
}