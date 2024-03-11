using DFC.SkillsCentral.Api.Infrastructure.Repositories;
using DFC.SkillsCentral.Api.Infrastructure;
using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using Dapper;
using DfE.SkillsCentral.Api.Application.Services;
using DfE.SkillsCentral.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// builder.Services.AddScoped<ILogger, Logger>();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

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

