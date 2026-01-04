
using MongoDB.Driver;

using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Entities;

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.Data;

public interface IMigracaoMonolitoParaMSDBContext
{
    IMongoCollection<ProjetoMigracao> ProjetosMigracao { get; }
    IMongoCollection<ObjetivoMigracao> ObjetivosMigracao { get; }
    IMongoCollection<DominioNegocio> DominiosNegocio { get; }
    IMongoCollection<Microservico> Microservicos { get; }
    IMongoCollection<EtapaMigracao> EtapasMigracao { get; }
    IMongoCollection<Metricas> Metricas { get; }
    IMongoCollection<Equipe> Equipes { get; }
    IMongoCollection<MembroEquipe> MembrosEquipe { get; }
}