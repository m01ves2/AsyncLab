var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//Add middleware
app.Use(async (context, next) =>
{
    Console.WriteLine(
        $"BEFORE | Thread {Thread.CurrentThread.ManagedThreadId}");

    await next();

    Console.WriteLine(
        $"AFTER | Thread {Thread.CurrentThread.ManagedThreadId}");
});

app.MapControllers();

app.Run();
