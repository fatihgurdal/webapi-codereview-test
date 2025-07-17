var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

// Products endpoint
app.MapGet("/products", () =>
{
    var products = new[]
    {
        new Product(1, "Laptop", "High-performance laptop", 999.99m),
        new Product(2, "Mouse", "Wireless optical mouse", 29.99m),
        new Product(3, "Keyboard", "Mechanical gaming keyboard", 149.99m),
        new Product(4, "Monitor", "27-inch 4K monitor", 399.99m),
        new Product(5, "Headphones", "Noise-cancelling headphones", 199.99m)
    };
    return products;
})
.WithName("GetProducts")
.WithOpenApi();

// Users endpoint
app.MapGet("/users", () =>
{
    var users = new[]
    {
        new User(1, "John Doe", "john.doe@example.com", "Admin"),
        new User(2, "Jane Smith", "jane.smith@example.com", "User"),
        new User(3, "Bob Johnson", "bob.johnson@example.com", "Moderator"),
        new User(4, "Alice Brown", "alice.brown@example.com", "User"),
        new User(5, "Charlie Wilson", "charlie.wilson@example.com", "Admin")
    };
    return users;
})
.WithName("GetUsers")
.WithOpenApi();

// Hatalı: IDisposable nesnesi using ile kullanılmıyor, resource leak riski!
app.MapGet("/buggy-using", () =>
{
    var stream = new MemoryStream();
    var writer = new StreamWriter(stream);
    writer.Write("Merhaba Dünya!");
    writer.Flush();
    // Normalde using ile kullanılmalı!
    return $"Yazıldı: {stream.Length} bytes";
});

// Hatalı: Null check yok, NullReferenceException riski!
app.MapGet("/buggy-null", () =>
{
    var users = new[]
    {
        new User(1, "John Doe", "john.doe@example.com", "Admin"),
        new User(2, "Jane Smith", "jane.smith@example.com", "User")
    };
    var user = users.FirstOrDefault(u => u.Name == "Ali"); // Bulamazsa null döner
    return user.Name; // Null ise exception fırlatır!
});

// Hatalı: try-catch yok, invalid cast hatası uygulamayı patlatabilir!
app.MapGet("/buggy-exception", () =>
{
    object price = "not-a-number";
    // Aşağıdaki satır exception fırlatır!
    var value = (decimal)price;
    return $"Fiyat: {value}";
});

// Status endpoint
app.MapGet("/status", () =>
{
    var status = new ServiceStatus(
        Service: "WebAPI Code Review Test",
        Version: "1.0.0",
        State: "Healthy",
        Timestamp: DateTime.UtcNow,
        Uptime: TimeSpan.FromMinutes(Random.Shared.Next(1, 1440))
    );
    return status;
})
.WithName("GetStatus")
.WithOpenApi();

app.Run();

// Data models
record Product(int Id, string Name, string Description, decimal Price);
record User(int Id, string Name, string Email, string Role);
record ServiceStatus(string Service, string Version, string State, DateTime Timestamp, TimeSpan Uptime);
