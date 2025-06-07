using Microsoft.EntityFrameworkCore;
using ProyectoCrudBasico.Models;
using System.Collections.Generic;

namespace ProyectoCrudBasico.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Proyecto> Proyectos => Set<Proyecto>();
}
