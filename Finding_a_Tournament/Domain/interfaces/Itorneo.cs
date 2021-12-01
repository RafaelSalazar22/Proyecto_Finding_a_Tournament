using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Finding_a_Tournament.Domain.Entities;
namespace Finding_a_Tournament.Domain.interfaces
{
    public interface Itorneo
    {
        Task<Torneo> GetByIdT(int IdTorneo);
        Task<IQueryable<Torneo>> GetAllT();
        Task<IQueryable<Torneo>> GetByFilterT(Torneo torneo);
        bool ExistT(Expression<Func<Torneo, bool>> expression);
        //Insert,Update,Delete
        Task<int> CreateT(Torneo torneo);
        Task<bool> UpdateT(int id, Torneo Torneo);
        Task<bool> DeleteT(int IdTorneo);
    }
}