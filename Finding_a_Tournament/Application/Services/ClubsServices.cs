
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
                 if(string.IsNullOrEmpty(Club.Direccion))
                    return false;
                if(string.IsNullOrEmpty(Club.TelefonoContacto))
                    return false;
                if(string.IsNullOrEmpty(Club.Logotipo))
                    return false;
                if(string.IsNullOrEmpty(Club.ClubServicio.Diciplina))
                    return false;
                if(Club.ClubServicio.CantidadPer <8 || Club.ClubServicio.CantidadPer >30 )
                    return false;
                return true;
            }
    }
}