using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Finding_a_Tournament.Domain.Entities;
namespace Finding_a_Tournament.Domain.interfaces
{
    public interface Iclubs
    {
      
        Task<Club> GetById(int idClub);
        Task<IQueryable<Club>> GetAll();
        Task<IQueryable<Club>> GetByFilter(Club Club);
        bool Exist(Expression<Func<Club, bool>> expression);
        //Insert,Update,Delete
        Task<int> Create(Club club);
        Task<bool> Update(int id, Club club);
        Task<bool> Delete(int IdClub);
    }
}