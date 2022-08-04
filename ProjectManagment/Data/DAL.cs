using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface DAL<T>
    {
        // A data access layer (DAL), provides simplified access to database
        // all methods that we will call later in the project

        // adds an argument t of type T
        void Add(T t);
        void Delete(T t);
        void Update(T t);
        T Get(T t);
        // returns a list
        List<T> GetAll();

       


    }
}
