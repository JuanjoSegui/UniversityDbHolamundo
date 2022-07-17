// using para Ett framework

using Microsoft.EntityFrameworkCore;
using Holamundo.DataAccess;
using Holamundo.Services;
using Holamundo;
using Microsoft.OpenApi.Models;

using Serilog;


var builder = WebApplication.CreateBuilder(args);

//Config Serilog
builder.Host.UseSerilog((hostBuilderCtx, loggerConf) =>
{
    loggerConf
    .WriteTo.Console()
    .WriteTo.Debug()
    .ReadFrom.Configuration(hostBuilderCtx.Configuration);
});


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

//Add Authentization
builder.Services.AddAuthentication(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequiredClaim("UserOnly", "User1"));
}
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//TODO Config Swagger Autorization of JWT
builder.Services.AddSwaggerGen(options =>
{
    //We define the security of Authorization
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Jwt Authorization Header using Bearer Scheme"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {{
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer",

            }
        },
        new string[]{}
        }});
}   
 );


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

// Tell app to use Serilog

app.UseSerilogRequestLogging();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



// Tell App to use CORS

app.UseCors("CORSPolicy");

app.Run();
