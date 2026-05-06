using InventoryService.Application.Consumers;
using InventoryService.Domain.Interfaces;
using InventoryService.Infrastructure.Data;
using InventoryService.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Database Configuration
// 1. Veritabanı Yapılandırması
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));

// 2. Dependency Injection (DI) Registrations
// 2. Bağımlılık Enjeksiyonu Kayıtları
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// 2. MassTransit ve Consumer Yapılandırmasını Güncelle
var rabbitMqHost = builder.Configuration["RabbitMQHost"] ?? "localhost";

// 3. MassTransit & RabbitMQ Configuration
// 3. MassTransit ve RabbitMQ Yapılandırması
builder.Services.AddMassTransit(x =>
{
    // Consumer'ı kaydediyoruz
    x.AddConsumer<OrderCreatedEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        // Docker içindeki RabbitMQ hostuna bağlanıyoruz
        cfg.Host(rabbitMqHost, "/", h => {
            h.Username("guest");
            h.Password("guest");
        });

        // Endpoint'leri otomatik veya manuel yapılandırıyoruz
        // e.ConfigureConsumer satırını kullanmaya devam edebilirsin:
        cfg.ReceiveEndpoint("order-created-event-queue", e =>
        {
            e.ConfigureConsumer<OrderCreatedEventConsumer>(context);
        });

        // v9 için genel yapılandırmayı tamamla
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// 3. Veritabanını Otomatik Oluştur (app.Run'dan hemen önce)
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
        db.Database.Migrate();
        // Log: Database migration completed successfully.
        // Veritabanı taşıma işlemi başarıyla tamamlandı.
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
