using CapaDatos;
using CapaNegocio.Clases;
using CapaNegocio.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conf = builder.Configuration.GetConnectionString("StringConexion");
builder.Services.AddDbContext<DBIASYSTEMContext>(option => option.UseSqlServer(conf));

builder.Services.AddScoped<IRegistro, lResgistro>();
builder.Services.AddScoped<IDocumentInfo, lDocumentInfo>();

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
