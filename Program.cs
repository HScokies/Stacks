using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Stacks_rework.Data;
using Stacks_rework.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



builder.Services.AddSingleton<AppDbContext>();

builder.Services.AddSingleton<IStacksService, StacksService>();



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
