using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        ICategory Category { get; }
        IProduct Product { get; }
        void Save();
    }
}
