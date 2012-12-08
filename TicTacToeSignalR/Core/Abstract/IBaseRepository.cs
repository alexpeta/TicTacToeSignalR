using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;

namespace TicTacToeSignalR.Core
{
    public interface IBaseRepository<T> where T : class
    {
        bool AddOrReplace(T entity);
        bool Delete(T entity);
    }
}
