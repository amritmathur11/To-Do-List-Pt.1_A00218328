using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using To_Do_List_A00218328.Models;

namespace To_Do_List_A00218328.DB
{
    public class TodoContext : DbContext
    {
        public TodoContext()
        {
        }

        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        { }

        public DbSet<TodoItem> TodoLists { get; set; }
       
    }
}
