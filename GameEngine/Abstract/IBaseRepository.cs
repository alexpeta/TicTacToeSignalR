using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Abstract
{
    public interface IBaseRepository<T> where T : class
    {
        bool AddOrReplace(T entity);
        bool Delete(T entity);
    }
}
