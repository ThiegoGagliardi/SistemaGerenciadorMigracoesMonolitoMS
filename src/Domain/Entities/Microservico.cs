using MongoDB.Bson;

using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Entities;

public class Microservico
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonRepresentation(BsonType.ObjectId)]
    public string ProjetoMigracaoId { get; set; } = string.Empty;

    [BsonRepresentation(BsonType.ObjectId)]
    public string DominioId { get; set; } = string.Empty;

    [BsonRepresentation(BsonType.ObjectId)]
    public string EquipeResponsavelId { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string RepositorioCodigo { get; set; } = string.Empty;
    public List<string> Tecnologias { get; set; } = new List<string>();
    public MicroservicoStatus Status { get; set; }
    public string EtapaAtualMigracao { get; set; } = string.Empty;
    public DateTime DataPrevisaoConclusao { get; set; }
    public DateTime? DataRealConclusao { get; set; }
    public List<string> Dependencias { get; set; } = new List<string>();   
    public List<string> MetricasEspecificasIds { get; set; } = new List<string>();
}