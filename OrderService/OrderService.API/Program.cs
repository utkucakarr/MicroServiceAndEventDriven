using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Commands;
using OrderService.Domain.Interfaces;
using OrderService.Infrastructure;
using OrderService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. PostgreSQL ve DbContext ayarı (Otomatik olarak SCOPED olur)
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));

// 3. MediatR Ayarı (Application katmanındaki Handlers'ları bulması için)
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));

// 4. Dependency Injection (Interface ile somut sınıfı eşleştiriyoruz)
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// 2. MassTransit Yapılandırmasını Güncelle
var rabbitMqHost = builder.Configuration["RabbitMQHost"] ?? "localhost";

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqHost, "/", h => {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// 3. Veritabanını Otomatik Oluştur (app.Run'dan hemen önce)
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
        db.Database.Migrate(); // Docker kalktığında tabloları otomatik oluşturur
    }
    catch (Exception ex)
    {
        // Log: An error occurred while migrating the database.
        // Veritabanı taşınırken bir hata oluştu.
        Console.WriteLine($"Migration Error: {ex.Message}");
    }

}

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
