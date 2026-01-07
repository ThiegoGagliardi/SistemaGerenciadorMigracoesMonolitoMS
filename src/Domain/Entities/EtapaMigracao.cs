using MongoDB.Bson;

using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Entities;

public class EtapaMigracao
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonRepresentation(BsonType.ObjectId)]
    public string MicroservicoId { get; set; } = string.Empty;
    public string NomeEtapa { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFimPrevista { get; set; }
    public DateTime? DataFimEfetiva { get; set; }
    public EtapaStatus Status { get; set; }
    public string Observacoes { get; set; } = string.Empty;
}
