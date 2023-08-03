
using Book.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Data.Contexts
{
    public class BookDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Category> Categories { get; set; }
      public DbSet<Books> Books { get; set; }
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {

        }
    }
}
