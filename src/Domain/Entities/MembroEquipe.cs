using MongoDB.Bson;

using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Entities;

public class MembroEquipe
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();    
    public List<string> EquipesIds { get; set; } = new List<string>();
    public string Nome { get; set; } = string.Empty;
    public PosicaoEquipe Posicao { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty; 
}