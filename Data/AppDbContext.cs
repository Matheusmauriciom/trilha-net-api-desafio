using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TarefaAPI.Models;

namespace TarefaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet <TarefaModels> Tarefas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("DataSource=app.db;Cache=Shared");
    }
}