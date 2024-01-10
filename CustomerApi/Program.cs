using CustomerApi.Business;
using CustomerApi.Business.Services;
using CustomerApi.Business.Middleware;
using Microsoft.EntityFrameworkCore;
using CustomerApi.Repository;
using CustomerApi.Domain.Interfaces;
using CustomerApi.Business.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Configure database
builder.Services.ConfigureDb();

// Add services to the container.
builder.Services.ConfigureServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Use exception custom middleware
app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
