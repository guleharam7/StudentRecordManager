using MongoDB.Driver;
using StudentRecordManagerAPI.Data;
using StudentRecordManagerAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());
});

// MongoDB Settings
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDB"));

// Mongo Client
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = builder.Configuration.GetSection("MongoDB")
        .Get<MongoDbSettings>()
        ?? throw new Exception("MongoDB configuration is missing in appsettings.json");

    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton<StudentService>();

var app = builder.Build();

app.UseStaticFiles();

app.UseCors("AllowAll");  

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();