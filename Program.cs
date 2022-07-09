// using para Ett framework

using Microsoft.EntityFrameworkCore;
using Holamundo.DataAccess;
using Holamundo.Services;

var builder = WebApplication.CreateBuilder(args);


// Conexión con la base de datos

const string connectionName = "UniversityDB";

var connectionString = builder.Configuration.GetConnectionString(connectionName);


//Añadir Contexto

builder.Services.AddDbContext<UniversityContext>(options => options.UseSqlServer(connectionString));


// Add JWT

builder.Services.AddJwtTokenServices(builder.Configuration);



// Add services to the container.
builder.Services.AddControllers();

//Add custom Services
builder.Services.AddScoped<IStudientService, StudientService>();

//Add the rest of services



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//TODO Config Swagger Autorization of JWT
builder.Services.AddSwaggerGen();


//CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CORSPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });

});



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



// Tell App to use CORS

app.UseCors("CORSPolicy");

app.Run();
