using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Interfaces
{
    public interface ICategory : IRepo<Category>
    {
        void Update(Category category);
        void Save();
    }
}
