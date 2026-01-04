using MongoDB.Bson;

using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Entities;

public class Metricas
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonRepresentation(BsonType.ObjectId)]
    public string ProjetoMigracaoId { get; set; } = string.Empty;

    [BsonRepresentation(BsonType.ObjectId)]
    public string? MicroservicoId { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string? ObjetivoId { get; set; }

    public string Nome { get; set; } = string.Empty;
    public string ValorMeta { get; set; } = string.Empty;
    public string ValorAtual { get; set; } = string.Empty;
}