using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using NET.MED.MODELS;
using NET.MED.REPOSITORY.Repositories;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Configuração do MongoDB
builder.Services.AddSingleton<IMongoClient, MongoClient>(s =>
{
    var connectionString = builder.Configuration.GetConnectionString("MongoDb");
    return new MongoClient(connectionString);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NET MED API", Version = "v1" });
});

// Configuração do MongoDbContext
builder.Services.AddScoped(sp =>
{
    var mongoClient = sp.GetRequiredService<IMongoClient>();
    var databaseName = builder.Configuration["MongoDbDatabaseName"];
    return new MongoDbContext(mongoClient, databaseName);
});

// Configuração dos Repositórios
builder.Services.AddScoped<PatientRepository>();
builder.Services.AddScoped<DoctorRepository>();
builder.Services.AddScoped<AppointmentRepository>();

// Health Check com MongoDB
builder.Services.AddHealthChecks()
    .AddMongoDb(
        mongodbConnectionString: builder.Configuration.GetConnectionString("MongoDb"),
        name: "mongodb",
        timeout: TimeSpan.FromSeconds(5));

// Health Check UI
builder.Services.AddHealthChecksUI(options =>
{
    options.SetEvaluationTimeInSeconds(60);
    options.MaximumHistoryEntriesPerEndpoint(50);
    options.SetApiMaxActiveRequests(1);
}).AddInMemoryStorage();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NET MED API V1");
    });
}

app.UseHttpsRedirection();
app.MapControllers();

// Endpoint para Health Check
app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// Endpoint para Health Check UI
app.MapHealthChecksUI(options =>
{
    options.UIPath = "/health-ui";
    options.ApiPath = "/health-ui-api";
});

app.Run();
