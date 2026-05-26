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
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    try {
        Console.WriteLine("EXCEPTION MIDDLEWARE BEFORE");

        await next(context);

        Console.WriteLine("EXCEPTION MIDDLEWARE AFTER");
    }
    catch (Exception ex) {
        Console.WriteLine($"EXCEPTION CAUGHT: {ex.Message}");

        context.Response.StatusCode = 500;

        await context.Response.WriteAsync("Something went wrong");
    }
});

app.MapControllers();

app.Run();
