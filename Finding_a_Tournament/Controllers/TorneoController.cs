using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Infrastructure.RepositoryClubs;
using Domain.dto;
using System.Linq;
using System.Text.Json;
using System.IO;
using Infrastructure;
using System.Threading.Tasks;
using Finding_a_Tournament.Domain.Entities;
using Finding_a_Tournament.Domain.interfaces;
using Microsoft.AspNetCore.Http;
using System;
using Finding_a_Tournament.Domain.dto.Requests;
using AutoMapper;
using FluentValidation;
using Finding_a_Tournament.Application.Services;
using Finding_a_Tournament.Domain.dto.Responses;
using System.Collections.Generic;
namespace Finding_a_Tournament.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TorneoController : ControllerBase
    {
         private readonly IHttpContextAccessor _httpContext;
        private readonly Itorneo _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<TorneoCreateRequest> _createValidator;

        public TorneoController(IHttpContextAccessor httpContext, Itorneo repository, IMapper mapper, IValidator<TorneoCreateRequest> createValidator)
        {
            this._httpContext = httpContext;
            _repository = repository;
            this._mapper = mapper;
            this._createValidator = createValidator;
        }
        [HttpGet]
        [Route("ALL")]
        public async Task<IActionResult> GetAll()
        {
            var torneos = await _repository.GetAllT();
            var respuesta = _mapper.Map<IEnumerable<Torneo>,IEnumerable<TorneosResponse>>(torneos);
            return Ok(respuesta);
        } 

        [HttpGet]
        [Route("{Idtorneo:int}")]
        public async Task<IActionResult> GetById(int Idtorneo)
        {
            var torneo = await _repository.GetByIdT(Idtorneo);

            if(torneo == null)
                return NotFound($"No se ha encontrado ningun resultado con el ID {Idtorneo}...");

            var respuesta = _mapper.Map<Torneo, TorneosResponse>(torneo);
            return Ok(respuesta);
        }

        [HttpGet]
        [Route("Find")]
        public async Task<IActionResult> GetByFilterT(Torneo torneo)
        {
            var tor = await _repository.GetByFilterT(torneo); 
            var respuesta = _mapper.Map<IEnumerable<Torneo>,IEnumerable<TorneosResponse>>(tor);
            return Ok(respuesta);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]TorneoCreateRequest torneoCreate)
        {
           var validationResult = await _createValidator.ValidateAsync(torneoCreate);

            if(!validationResult.IsValid)
                return UnprocessableEntity(validationResult.Errors.Select(x => $"Error: {x.ErrorMessage}"));

            var entity = _mapper.Map<TorneoCreateRequest , Torneo>(torneoCreate);
            var id = await _repository.CreateT(entity);

            if(id <= 0)
                return Conflict("No se ha realizado el registro, compruebe la informacion e intente de nuevo...");

            var urlresult = $"https://{_httpContext.HttpContext.Request.Host.Value}/api/Torneo/{id}";
            return Created(urlresult, id);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Torneo torneo)
        {
            if(id <= 0)
                return NotFound("No se encontro el ningun registro con esa id");

            torneo.IdTorneo = id;

            var validate = TorneoServices.ValidateUpdatet(torneo);
            
            if(!validate)
                return UnprocessableEntity("Nn es posible modificar la informacion, por favor compruebe que todo este bien...");

            var update = await _repository.UpdateT(id, torneo);

            if(!update)
                return Conflict("Error: ha ocurrido algo al intentar modificar...");

            return NoContent();            
        }

        [HttpDelete]
        [Route("{IdTorneo:int}")]
        public async Task<IActionResult> Delete(int IdTorneo)
        {
            var Eliminar = await _repository.DeleteT(IdTorneo);
           

            return NoContent();
        }
     }
}