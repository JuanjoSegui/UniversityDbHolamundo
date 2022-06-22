// using para Ett framework

using Microsoft.EntityFrameworkCore;
using Holamundo.DataAccess;






var builder = WebApplication.CreateBuilder(args);


// Conexión con la base de datos

const string connectionName = "UniversityDB";

var connectionString = builder.Configuration.GetConnectionString(connectionName);


//Añadir Contexto

builder.Services.AddDbContext<UniversityContext>(options => options.UseSqlServer(connectionString));



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
