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

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Clubscontroller : ControllerBase
    {
         /*private readonly IPersonService _service;*/
       
        private readonly IHttpContextAccessor _httpContext;
        private readonly Iclubs _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<ClubsCreateRequest> _createValidator;

        public Clubscontroller(IHttpContextAccessor httpContext, Iclubs repository, IMapper mapper, IValidator<ClubsCreateRequest> createValidator)
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
            var clubs = await _repository.GetAll();
            var respuesta = _mapper.Map<IEnumerable<Club>,IEnumerable<ClubsResponse>>(clubs);
            return Ok(respuesta);
        } 

        [HttpGet]
        [Route("{IdClub:int}")]
        public async Task<IActionResult> GetById(int IdClub)
        {
            var club = await _repository.GetById(IdClub);

            if(club == null)
                return NotFound($"No se ha encontrado ningun resultado con el ID {IdClub}...");

            var respuesta = _mapper.Map<Club, ClubsResponse>(club);
            return Ok(respuesta);
        }

        [HttpGet]
        [Route("Find")]
        public async Task<IActionResult> GetByFilter(Club Clubs)
        {
            var club = await _repository.GetByFilter(Clubs); 
            var respuesta = _mapper.Map<IEnumerable<Club>,IEnumerable<ClubsResponse>>(club);
            return Ok(respuesta);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ClubsCreateRequest clubs)
        {
            var validationResult = await _createValidator.ValidateAsync(clubs);

            if(!validationResult.IsValid)
                return UnprocessableEntity(validationResult.Errors.Select(x => $"Error: {x.ErrorMessage}"));

            var entity = _mapper.Map<ClubsCreateRequest, Club>(clubs);
            var id = await _repository.Create(entity);

            if(id <= 0)
                return Conflict("No se ha realizado el registro, compruebe la informacion e intente de nuevo...");

            var urlresult = $"https://{_httpContext.HttpContext.Request.Host.Value}/api/Clubs/{id}";
            return Created(urlresult, id);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Club Club)
        {
            if(id <= 0)
                return NotFound("No se encontro el ningun registro con esa id...");

            Club.IdClub = id;

            var validate = ClubsServices.ValidateUpdate(Club);
            
            if(!validate)
                return UnprocessableEntity("Nn es posible modificar la informacion, por favor compruebe que todo este bien...");

            var update = await _repository.Update(id, Club);

            if(!update)
                return Conflict("Error: ha ocurrido algo al intentar modificar...");

            return NoContent();            
        }

        [HttpDelete]
        [Route("{IdClub:int}")]
        public async Task<IActionResult> Delete(int IdClub)
        {
            var Eliminar = await _repository.Delete(IdClub);
           

            return NoContent();
        }
     }

}