using MongoDB.Driver;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Entities;

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.Data;

public class MongoDbInitializerService : IHostedService
{
    private readonly IMigracaoMonolitoParaMSDBContext _dbContext;
    private readonly ILogger<MongoDbInitializerService> _logger;

    public MongoDbInitializerService(IMigracaoMonolitoParaMSDBContext dbContext, ILogger<MongoDbInitializerService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando serviço de inicialização do MongoDB...");

        // Lógica de retry para garantir que o MongoDB esteja pronto
        int maxRetries = 5;
        int retryDelayMs = 5000; // 5 segundos
        for (int i = 0; i < maxRetries; i++)
        {
            try
            {

                await CreateIndexesAsync();
                _logger.LogInformation("Conexão com MongoDB estabelecida e índices criados/verificados.");

                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Falha ao conectar ou inicializar MongoDB. Tentativa {i + 1}/{maxRetries}. Reintentando em {retryDelayMs / 1000} segundos...");
                if (i == maxRetries - 1)
                {
                    _logger.LogCritical("Não foi possível conectar ao MongoDB após várias tentativas. Encerrando aplicação.");
                    throw;
                }
                await Task.Delay(retryDelayMs, cancellationToken);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Serviço de inicialização do MongoDB encerrado.");
        return Task.CompletedTask;
    }

    private async Task CreateIndexesAsync()
    {
        // --- ProjetoMigracao ---
        await _dbContext.ProjetosMigracao.Indexes.CreateOneAsync(
            new CreateIndexModel<ProjetoMigracao>(Builders<ProjetoMigracao>.IndexKeys.Ascending(p => p.Nome), new CreateIndexOptions { Unique = true }));
        await _dbContext.ProjetosMigracao.Indexes.CreateOneAsync(
            new CreateIndexModel<ProjetoMigracao>(Builders<ProjetoMigracao>.IndexKeys.Ascending(p => p.Responsavel)));
        await _dbContext.ProjetosMigracao.Indexes.CreateOneAsync(
            new CreateIndexModel<ProjetoMigracao>(Builders<ProjetoMigracao>.IndexKeys.Ascending(p => p.Status)));
        _logger.LogInformation("Índices para 'ProjetosMigracao' criados.");

        // --- ObjetivoMigracao ---
        await _dbContext.ObjetivosMigracao.Indexes.CreateOneAsync(
            new CreateIndexModel<ObjetivoMigracao>(Builders<ObjetivoMigracao>.IndexKeys.Ascending(o => o.ProjetoMigracaoId)));
        await _dbContext.ObjetivosMigracao.Indexes.CreateOneAsync(
            new CreateIndexModel<ObjetivoMigracao>(Builders<ObjetivoMigracao>.IndexKeys.Ascending(o => o.Titulo)));
        await _dbContext.ObjetivosMigracao.Indexes.CreateOneAsync(
            new CreateIndexModel<ObjetivoMigracao>(Builders<ObjetivoMigracao>.IndexKeys.Ascending(o => o.Status)));
        _logger.LogInformation("Índices para 'ObjetivosMigracao' criados.");

        // --- DominioNegocio ---
        await _dbContext.DominiosNegocio.Indexes.CreateOneAsync(
            new CreateIndexModel<DominioNegocio>(Builders<DominioNegocio>.IndexKeys.Ascending(d => d.ProjetoMigracaoId)));
        await _dbContext.DominiosNegocio.Indexes.CreateOneAsync(
            new CreateIndexModel<DominioNegocio>(Builders<DominioNegocio>.IndexKeys.Ascending(d => d.Nome), new CreateIndexOptions { Unique = true }));
        _logger.LogInformation("Índices para 'DominiosNegocio' criados.");

        // --- Equipe ---
        await _dbContext.Equipes.Indexes.CreateOneAsync(
            new CreateIndexModel<Equipe>(Builders<Equipe>.IndexKeys.Ascending(e => e.ProjetoMigracaoId)));
        await _dbContext.Equipes.Indexes.CreateOneAsync(
            new CreateIndexModel<Equipe>(Builders<Equipe>.IndexKeys.Ascending(e => e.Nome), new CreateIndexOptions { Unique = true }));
        await _dbContext.Equipes.Indexes.CreateOneAsync(
            new CreateIndexModel<Equipe>(Builders<Equipe>.IndexKeys.Ascending(e => e.LeadEquipe)));
        _logger.LogInformation("Índices para 'Equipes' criados.");

        // --- MembroEquipe ---
        await _dbContext.MembrosEquipe.Indexes.CreateOneAsync(
            new CreateIndexModel<MembroEquipe>(Builders<MembroEquipe>.IndexKeys.Ascending(m => m.Nome)));
        await _dbContext.MembrosEquipe.Indexes.CreateOneAsync(
            new CreateIndexModel<MembroEquipe>(Builders<MembroEquipe>.IndexKeys.Ascending(m => m.Email), new CreateIndexOptions { Unique = true }));
        await _dbContext.MembrosEquipe.Indexes.CreateOneAsync(
            new CreateIndexModel<MembroEquipe>(Builders<MembroEquipe>.IndexKeys.Ascending(m => m.Posicao)));
        _logger.LogInformation("Índices para 'MembrosEquipe' criados.");

        // --- Microservico ---
        await _dbContext.Microservicos.Indexes.CreateOneAsync(
            new CreateIndexModel<Microservico>(Builders<Microservico>.IndexKeys.Ascending(m => m.ProjetoMigracaoId)));
        await _dbContext.Microservicos.Indexes.CreateOneAsync(
            new CreateIndexModel<Microservico>(Builders<Microservico>.IndexKeys.Ascending(m => m.DominioId)));
        await _dbContext.Microservicos.Indexes.CreateOneAsync(
            new CreateIndexModel<Microservico>(Builders<Microservico>.IndexKeys.Ascending(m => m.EquipeResponsavelId)));
        await _dbContext.Microservicos.Indexes.CreateOneAsync(
            new CreateIndexModel<Microservico>(Builders<Microservico>.IndexKeys.Ascending(m => m.Nome), new CreateIndexOptions { Unique = true }));
        await _dbContext.Microservicos.Indexes.CreateOneAsync(
            new CreateIndexModel<Microservico>(Builders<Microservico>.IndexKeys.Ascending(m => m.Status)));
        _logger.LogInformation("Índices para 'Microservicos' criados.");

        // --- EtapaMigracao ---
        await _dbContext.EtapasMigracao.Indexes.CreateOneAsync(
            new CreateIndexModel<EtapaMigracao>(Builders<EtapaMigracao>.IndexKeys.Ascending(e => e.MicroservicoId)));
        await _dbContext.EtapasMigracao.Indexes.CreateOneAsync(
            new CreateIndexModel<EtapaMigracao>(Builders<EtapaMigracao>.IndexKeys.Ascending(e => e.NomeEtapa)));
        await _dbContext.EtapasMigracao.Indexes.CreateOneAsync(
            new CreateIndexModel<EtapaMigracao>(Builders<EtapaMigracao>.IndexKeys.Ascending(e => e.Status)));
        _logger.LogInformation("Índices para 'EtapasMigracao' criados.");

        // --- MetricaChave ---
        await _dbContext.Metricas.Indexes.CreateOneAsync(
            new CreateIndexModel<Metricas>(Builders<Metricas>.IndexKeys.Ascending(m => m.ProjetoMigracaoId)));
        await _dbContext.Metricas.Indexes.CreateOneAsync(
            new CreateIndexModel<Metricas>(Builders<Metricas>.IndexKeys.Ascending(m => m.MicroservicoId)));
        await _dbContext.Metricas.Indexes.CreateOneAsync(
            new CreateIndexModel<Metricas>(Builders<Metricas>.IndexKeys.Ascending(m => m.ObjetivoId)));
        await _dbContext.Metricas.Indexes.CreateOneAsync(
            new CreateIndexModel<Metricas>(Builders<Metricas>.IndexKeys.Ascending(m => m.Nome)));
        _logger.LogInformation("Índices para 'MetricasChave' criados.");


        _logger.LogInformation("Todos os índices foram criados/verificados.");
    }
}
