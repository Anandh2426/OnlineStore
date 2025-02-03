using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {


        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
  
        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }

        Category ICategoryRepository.GetFirstOrDefault(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }

        public Category Get(System.Linq.Expressions.Expression<Func<Category, bool>> filter)
        {
            return _db.Categories.SingleOrDefault(filter);
        }
    }
}
