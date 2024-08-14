using Core.Data.Abstracts;
using Core.Data;
using Core.Models.Constants;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Stock.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StockDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("StockDBConnection")));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbContext, StockDbContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(RabbitMqConstants.Host, "/", c =>
        {
            c.Username(RabbitMqConstants.Username);
            c.Password(RabbitMqConstants.Password);
        });
    });
});



var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
    

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
