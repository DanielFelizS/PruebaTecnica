using ProyectoCrudBasico.Data;
using Microsoft.EntityFrameworkCore;
using ProyectoCrudBasico.Models;

var builder = WebApplication.CreateBuilder(args);

// Agrega el servicio de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseInMemoryDatabase("ProyectosDb"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

    dbContext.Database.EnsureCreated();

    // Si no hay proyectos, los agrega por defecto
    if (!dbContext.Proyectos.Any())
    {
        dbContext.Proyectos.AddRange(
            new Proyecto
            {
                Nombre = "BillFast",
                Descripcion = "BillFast gestiona la emisión de facturas y la recaudación de pagos para " +
            "empresas de servicios públicos, con escalabilidad e integración en sectores como municipal, energía, agua y gas."
            },
            new Proyecto
            {
                Nombre = "Stamp",
                Descripcion = "Automatiza y digitaliza los procesos de trámites y " +
            "permisos municipales de la Oficina de Planeamiento Urbano."
            },
            new Proyecto
            {
                Nombre = "Field Service",
                Descripcion = "Optimiza la gestión de órdenes de trabajo en campo, mejorando la productividad mediante asignación de tareas, " +
                "geolocalización, captura de imágenes y administración de contratistas en tiempo real"
            }
        );
        dbContext.SaveChanges();
    }
}


// Aplica el middleware de CORS
app.UseCors("PermitirTodo");

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
