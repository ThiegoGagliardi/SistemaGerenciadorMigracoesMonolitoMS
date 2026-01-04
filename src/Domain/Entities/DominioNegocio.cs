using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Enum;

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Entities;

public class DominioNegocio
{
     [BsonId]
     [BsonRepresentation(BsonType.ObjectId)]
     public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

     [BsonRepresentation(BsonType.ObjectId)]
     public string ProjetoMigracaoId { get; set; } = string.Empty;
     public string Nome { get; set; } = string.Empty;
     public string Descricao { get; set; } = string.Empty;
     public string ResponsavelDominio { get; set; } = string.Empty;
     public DominioStatusMigracao StatusMigracao { get; set; }

     public List<string> MicroservicosAssociadosIds { get; set; } = new List<string>();
}