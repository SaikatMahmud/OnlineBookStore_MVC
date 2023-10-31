using BookStore.DataAccess.Data;
using BookStore.DataAccess.Interfaces;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repos
{
    public class CategoryRepo : Repository<Category>, ICategory
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepo(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
        public void Save()
        {
           _db.SaveChanges();
        }

        public void Update(Category category)
        {
            _db.Update(category);
        }
    }
}
