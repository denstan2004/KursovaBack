using Microsoft.EntityFrameworkCore;
using System;

namespace project_back.context
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options)
        {
        }

    }
}
