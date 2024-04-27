using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EMailApp.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Insert(T t);
        void Delete(int id);
        void Update(T t);
        List<T> GetList();
        T GetById(int id);
        List<T> GetByFilter(Expression<Func<T, bool>> filter);
    }
}
