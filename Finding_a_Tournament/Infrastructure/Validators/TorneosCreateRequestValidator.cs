using System;
using FluentValidation;
using Finding_a_Tournament.Domain.dto.Requests;
using Finding_a_Tournament.Domain.interfaces;
namespace Finding_a_Tournament.Infrastructure.Validators
{
           public class TorneosCreateRequestValidator: AbstractValidator<TorneoCreateRequest>
         {
             private readonly Itorneo _repository;

            public TorneosCreateRequestValidator(Itorneo repository)
            {   
                this._repository = repository;

                RuleFor(x => x.NombreTorneo).NotNull().NotEmpty().Length(5,20);
                RuleFor(x => x.CantidadParticipantes).NotNull().ExclusiveBetween(7,31).WithMessage("La cantidad de participantes no puede ser menor que 8  Ni mayor que 30");
                RuleFor(x => x.AcepHabilidadesdistintas).NotNull();
                RuleFor(x => x.CantidadEquipos).NotNull().NotEmpty().ExclusiveBetween(1,31).WithMessage("La cantidad de equipos no puede ser menor que 2  Ni mayor que 30");
                RuleFor(x => x.NombreTorneo).Must(NotExistDireccion).WithMessage("EL NombreTorneo  debe ser única");
                RuleFor(x => x.TipoTorneo).NotNull().NotEmpty().Length(5,20);



            }

            public bool NotExistDireccion(string NombreTorneo) 
            {
                return !_repository.ExistT(p => p.NombreTorneo == NombreTorneo);
            }    
    }   
}