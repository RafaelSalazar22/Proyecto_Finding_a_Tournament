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

            RuleFor(x => x.NombreClub).NotNull().NotEmpty().Length(5,20);
            RuleFor(x => x.Direccion).NotNull().NotEmpty().Length(10, 40);
            RuleFor(x => x.TelefonoContacto).NotNull().NotEmpty().Length(10,13);
            RuleFor(x => x.Diciplina).NotNull().NotEmpty();
            RuleFor(x => x.Direccion).Must(NotExistDireccion).WithMessage("La Direccion  debe ser única");
        }

        public bool NotExistDireccion(string Direccion) 
        {
            return !_repository.Exist(p => p.Direccion == Direccion);
        }
         
    }
}