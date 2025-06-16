using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Hubs;
using ToDoList.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<BackgroundTaskService>(); // ✅ Registers background task

// Register SignalR
builder.Services.AddSignalR();

// Configure Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ToDoDB"));

// Configure CORS
builder.Services.AddCors(options =>
{

    options.AddPolicy("AllowReactClient", policy =>
policy.WithOrigins("http://localhost:3000") // ✅ Allow React app origin
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
});

// Configure Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment() || app.Environment.IsProduction()) // ✅ Allow Swagger in Docker
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactClient");
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();

// ✅ Correct Hub Mapping (DO NOT use full URL)
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chatHub"); // Correct URL format
    endpoints.MapControllers();
});

app.Run();
public partial class Program { }
