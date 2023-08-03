using Book.Core.Entities;
using Book.Core.Repositories;
using Book.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(BookDbContext context) : base(context)
        {
        }
    }
}
