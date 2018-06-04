using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiboxWebApi.Repository
{
    public interface IRepository<T> where T : class
    {
        int Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        T GetEntityById(int id);
        IEnumerable<T> GetAll();
    }
}
