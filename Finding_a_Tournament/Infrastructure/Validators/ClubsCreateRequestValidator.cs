using System;
using FluentValidation;
using Finding_a_Tournament.Domain.dto.Requests;
using Finding_a_Tournament.Domain.interfaces;

namespace Finding_a_Tournament.Infrastructure.Validators
{
    public class ClubsCreateRequestValidator : AbstractValidator<ClubsCreateRequest>
    {
        private readonly Iclubs _repository;

        public ClubsCreateRequestValidator(Iclubs repository)
        {
            this._repository = repository;

            RuleFor(x => x.NombreClub).NotNull().NotEmpty().Length(5,20).Must(NotExistClub).WithMessage("El nombre del club no puede ser igual a uno existente");
            RuleFor(x => x.Direccion).NotNull().NotEmpty().Length(10, 40).Must(NotExistDireccion).WithMessage("La Direccion  debe ser única");
            RuleFor(x => x.TelefonoContacto).NotNull().NotEmpty().Length(10,13).Must(NotExisTelefonoContacto).WithMessage("El TelefonoContacto del club no puede ser igual a uno existente");
            RuleFor(x => x.Geoubicacion).NotNull().NotEmpty();
            RuleFor(x => x.Logotipo).NotNull().NotEmpty().Must(NotExistLogotipo).WithMessage("El Logotipo del club no puede ser igual a uno existente");
            RuleFor(x => x.Diciplina).NotNull().NotEmpty().Length(5,20);
            RuleFor(x => x.HorarioDiciplina).NotEmpty();
            RuleFor(x => x.CantidadPer).NotEmpty().ExclusiveBetween(9,31);
            RuleFor(x => x.PersHabilidadesDiferentes).NotNull();

        }

        public bool NotExistDireccion(string Direccion) 
        {
            return !_repository.Exist(p => p.Direccion == Direccion);
        }
        public bool NotExistClub(string NombreClub) 
        {
            return !_repository.Exist(p => p.NombreClub == NombreClub);
        }
          public bool NotExistLogotipo(string Logotipo) 
        {
            return !_repository.Exist(p => p.Logotipo == Logotipo);
        }
        public bool NotExisTelefonoContacto(string TelefonoContacto) 
        {
            return !_repository.Exist(p => p.TelefonoContacto == TelefonoContacto);
        }
    }
}