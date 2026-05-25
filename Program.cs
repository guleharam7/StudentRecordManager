using MongoDB.Driver;
using StudentRecordManager.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDB"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = builder.Configuration.GetSection("MongoDB")
        .Get<MongoDbSettings>();

    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton<StudentService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();  
app.UseAuthorization();

app.MapControllers();

app.Run();