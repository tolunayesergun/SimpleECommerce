using Core.Data;
using Core.Data.Abstracts;
using Core.Models.Constants;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.Api.Data;
using Order.Api.Mapper;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<OrderDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("OrderDBConnection")));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbContext, OrderDbContext>();

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
