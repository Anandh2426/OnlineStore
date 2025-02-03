using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.DataAccess.Repository.IRepository
{
    public  interface ICategoryRepository : IRepository<Category>
    {
        Category GetFirstOrDefault(Func<object, bool> value);
        Category Get(System.Linq.Expressions.Expression<Func<Category, bool>> filter);
        void Update(Category obj);
       
    }
}
