using MongoDB.Bson;

using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Entities;

public class ObjetivoMigracao
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonRepresentation(BsonType.ObjectId)]
    public string ProjetoMigracaoId { get; set; } = string.Empty;

    public string Titulo { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public string Metricas { get; set; } = string.Empty;

    public DateTime DataFatal { get; set; }

    public ObjetivoTipo Tipo { get; set; }

    public ObjetivoPrioridade Prioridade { get; set; }

    public ObjetivoStatus Status { get; set; }
};