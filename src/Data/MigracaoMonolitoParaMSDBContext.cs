
using Microsoft.Extensions.Options;
using MongoDB.Driver;

using GerenciamentoMigracaoMonolitoParaMS.app.src.Data.Configuration;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Entities;

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.Data;

public class MigracaoMonolitoParaMSDBContext : IMigracaoMonolitoParaMSDBContext
{
    private readonly IMongoDatabase _database;

    public MigracaoMonolitoParaMSDBContext(IOptions<MongoDbSettings> settings)
    {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<ProjetoMigracao> ProjetosMigracao => _database.GetCollection<ProjetoMigracao>("ProjetosMigracao");
    public IMongoCollection<ObjetivoMigracao> ObjetivosMigracao => _database.GetCollection<ObjetivoMigracao>("ObjetivosMigracao");
    public IMongoCollection<DominioNegocio> DominiosNegocio => _database.GetCollection<DominioNegocio>("DominiosNegocio");
    public IMongoCollection<Microservico> Microservicos => _database.GetCollection<Microservico>("Microservicos");
    public IMongoCollection<EtapaMigracao> EtapasMigracao => _database.GetCollection<EtapaMigracao>("EtapasMigracao");
    public IMongoCollection<Metricas> Metricas => _database.GetCollection<Metricas>("Metricas");
    public IMongoCollection<Equipe> Equipes => _database.GetCollection<Equipe>("Equipes");
    public IMongoCollection<MembroEquipe> MembrosEquipe => _database.GetCollection<MembroEquipe>("MembrosEquipe");
    
}