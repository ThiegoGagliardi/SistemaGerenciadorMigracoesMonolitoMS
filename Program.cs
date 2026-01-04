
using MongoDB.Driver;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Data.Configuration;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Core.Events;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<MongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;

    var mongoClientSettings = MongoClientSettings.FromConnectionString(settings.ConnectionString);
    mongoClientSettings.ClusterConfigurator = cb =>
    {
        cb.Subscribe<CommandStartedEvent>(e =>
        {
            Console.WriteLine($"MongoDB Command Started: {e.CommandName} - {e.Command.ToJson()}");
        });
        cb.Subscribe<CommandFailedEvent>(e =>
        {
            Console.WriteLine($"MongoDB Command Failed: {e.CommandName} - {e.Failure.Message}");
        });
    };

    return new MongoClient(mongoClientSettings);
});


builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<MongoClient>();
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddSingleton<IMigracaoMonolitoParaMSDBContext, MigracaoMonolitoParaMSDBContext>();

builder.Services.AddHostedService<MongoDbInitializerService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, builder =>
                        {
                            builder.WithOrigins("http://localhost:8080", // Se o seu frontend for servido de outro lugar, adicione aqui
                                                "http://127.0.0.1:5500") // Exemplo se estiver usando Live Server do VS Code
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                        });
});

var app = builder.Build();

// Use esta linha DEPOIS de app.UseRouting(); e ANTES de app.UseAuthorization();
app.UseCors(MyAllowSpecificOrigins);

// Configurar o pipeline de requisições HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();