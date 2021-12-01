using System;
using System.Collections.Generic;
using System.Linq;
using Finding_a_Tournament.Domain.Entities;
using Finding_a_Tournament.Infrastructure.Data;
using Finding_a_Tournament.Domain.interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.RepositoryClubs
{
    public class RepositoryClubs : Iclubs, Itorneo
    {
       private readonly Finding_a_TournamentContext _context;

        public RepositoryClubs( Finding_a_TournamentContext context)
        {

            _context =  context;

        }
        public async Task<IQueryable<Club>> GetAll()
        {
            //Origen|Colección Método Iterador
            var query = await _context.Club.AsQueryable<Club>().AsNoTracking().ToListAsync();
            return query.AsQueryable();
        }

        public async Task<Club> GetById(int idClub)
        {            
            var query = await _context.Club.Include(x => x.ClubServicio).FirstOrDefaultAsync(x => x.IdClub == idClub);
            return query;
        }

        public bool Exist(Expression<Func<Club, bool>> expression)
        {
            return _context.Club.Any(expression);
        }
        public async Task<IQueryable<Club>> GetByFilter(Club Club)
        {
            if(Club == null)
                return new List<Club>().AsQueryable();

            var query = _context.Club.AsQueryable();

            if(!string.IsNullOrEmpty(Club.NombreClub))
                query = query.Where(x => x.NombreClub.Contains(Club.NombreClub));

            if(!string.IsNullOrEmpty(Club.Direccion))
                query = query.Where(x => x.Direccion == Club.Direccion);

            if(!string.IsNullOrEmpty(Club.TelefonoContacto)) 
                query = query.Where(x => x.TelefonoContacto == Club.TelefonoContacto);

             if(Club.ClubServicio != null && !string.IsNullOrEmpty(Club.ClubServicio.Diciplina))
                 query = query.Where(x => x.ClubServicio.Diciplina == Club.ClubServicio.Diciplina);
            var result = await query.ToListAsync();

            return result.AsQueryable().AsNoTracking();
        }  
        
        public async Task<int> Create(Club club)
            {
                var entity = club;
                await _context.AddAsync(entity);
                var rows = await _context.SaveChangesAsync();

                if(rows <= 0)
                    throw new Exception("Error: El registro NO ha sido realizado...");

                return entity.IdClub;
            }

            public async Task<bool> Update(int idClub, Club club)
            {
                if(idClub <= 0 || club == null)
                    throw new ArgumentException("INFORMACION INCOMPLETA: llena los campos para realizar la modificacion...");

                var entity = await GetById(idClub);

                entity.NombreClub = club.NombreClub;
                entity.Direccion = club.Direccion;
                entity.TelefonoContacto = club.TelefonoContacto;
                entity.Geoubicacion = club.Geoubicacion;
                entity.Logotipo = club.Logotipo;

                if(club.ClubServicio != null)
                {
                    if(entity.ClubServicio == null)
                        entity.ClubServicio = new ClubServicio();

                    entity.ClubServicio.Diciplina = club.ClubServicio.Diciplina;
                    entity.ClubServicio.HorarioDiciplina = club.ClubServicio.HorarioDiciplina;
                    entity.ClubServicio.CantidadPer = club.ClubServicio.CantidadPer;
                    entity.ClubServicio.PersHabilidadesDiferentes = club.ClubServicio.PersHabilidadesDiferentes;
                }
                else if(entity.ClubServicio != null) 
                    _context.Remove(entity.ClubServicio);

                _context.Update(entity);

                var rows = await _context.SaveChangesAsync();
                return rows > 0;
            }
            public async Task<bool> Delete(int IdClub )
            {
                 if(IdClub <= 0 )
                    throw new ArgumentException("ERROR AL ELIMINAR:Falta información para continuar con el proceso de Eliminacion ...");

                var entity = await GetById(IdClub);
                _context.Remove(entity);
                var rows = await _context.SaveChangesAsync();
                return rows > 0;
            }

        public async Task<Torneo> GetByIdT(int IdTorneo)
        {           
            var query = await _context.Torneos.FirstOrDefaultAsync(x=> x.IdTorneo == IdTorneo);
            return query;
        }

        public async Task<IQueryable<Torneo>> GetAllT()
        {
            //Origen|Colección Método Iterador
            var query = await _context.Torneos.AsQueryable<Torneo>().AsNoTracking().ToListAsync();
            return query.AsQueryable();
        }
       public async Task<IQueryable<Torneo>> GetByFilterT(Torneo torneo)
        {
           if(torneo == null)
                return new List<Torneo>().AsQueryable();

            var query = _context.Torneos.AsQueryable();

            if(!string.IsNullOrEmpty(torneo.NombreTorneo))
                query = query.Where(x => x.NombreTorneo.Contains(torneo.NombreTorneo));

            if(!string.IsNullOrEmpty(torneo.TipoTorneo))
                query = query.Where(x => x.TipoTorneo == torneo.TipoTorneo);

            if(torneo.CantidadEquipos>0 ) 
                query = query.Where(x => x.CantidadEquipos == torneo.CantidadEquipos);

            var result = await query.ToListAsync();

            return result.AsQueryable().AsNoTracking();
        }

        public bool ExistT(Expression<Func<Torneo, bool>> expression)
        {
         return _context.Torneos.Any(expression);

        }

        public async Task<int> CreateT(Torneo torneo)
        {
            var entity = torneo;
                await _context.AddAsync(entity);
                var rows = await _context.SaveChangesAsync();

                if(rows <= 0)
                    throw new Exception("ERROR AL REGISTRAR: No ha sido posiblre realizar el registro...");

                return entity.IdTorneo;
        }
       public async Task<bool> UpdateT(int id, Torneo torneo)
        {
             if(id <= 0 || torneo == null)
                    throw new ArgumentException("ERROR AL MODIFICAR: ha ocurrido algo al intentar modificar...");

                var entity = await GetByIdT(id);

                entity.NombreTorneo = torneo.NombreTorneo;
                entity.CantidadParticipantes = torneo.CantidadParticipantes;
                entity.CantidadEquipos = torneo.CantidadEquipos;
                entity.TipoTorneo = torneo.TipoTorneo;
                entity.AcepHabilidadesdistintas = torneo.AcepHabilidadesdistintas;
                _context.Update(entity);

                var rows = await _context.SaveChangesAsync();
                return rows > 0;
        }
        public async Task<bool> DeleteT(int IdTorneo)
        {
           if(IdTorneo <= 0 )
                    throw new ArgumentException("ERROR AL ELIMINAR: La informacion para eliminar esta incompleta ...");

                var entity = await GetByIdT(IdTorneo);
                _context.Remove(entity);
                var rows = await _context.SaveChangesAsync();
                return rows > 0;
        }
    }
}

