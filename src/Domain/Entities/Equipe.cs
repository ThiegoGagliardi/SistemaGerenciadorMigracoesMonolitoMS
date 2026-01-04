using MongoDB.Bson;

using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Entities;

public class Equipe
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonRepresentation(BsonType.ObjectId)]
    public string ProjetoMigracaoId { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string LeadEquipe { get; set; } = string.Empty;
    public List<string> MembrosIds { get; set; } = new List<string>();
    public List<string> MicroservicosSobResponsabilidadeIds { get; set; } = new List<string>();
}