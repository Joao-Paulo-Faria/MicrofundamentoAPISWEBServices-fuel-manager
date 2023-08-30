using MicrofundamentoAPISWEBServices_fuel_manager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//as configura��es de banco de dados � um servi�o que ele vai adicionar via inje��o de dependencia


builder.Services.AddDbContext<AppDbContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))

);

//informo a classe appdbcontext.
//Use o padr�o Builder quando voc� quer que seu c�digo seja capaz de criar diferentes representa��es do mesmo produto (por exemplo, casas de pedra e madeira).
//ele vai configurar essa classe appdbcontext atrav�s da inje��o de dependencias

//preciso passar as configura��es desse sistema que � options


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

//starta as configura��es do projeto.
