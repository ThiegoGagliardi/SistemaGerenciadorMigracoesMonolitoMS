using MongoDB.Bson;

using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Entities;

public class ProjetoMigracao
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public string Responsavel { get; set; } = string.Empty;

    public Decimal OrcamentoPrevisto { get; set; }

    public Decimal OrcamentoGasto { get; set; }

    public DateTime DataInicio { get; set; }

    public DateTime DataFimPrevisto { get; set; }

    public DateTime DataFimEfetiva { get; set; }

    public string JustificativaNegocio { get; set; } = string.Empty;

    public List<string> TecnologiasBackEnd { get; set; } = [];

    public List<string> TecnologiasFrontEnd { get; set; } = [];

    public StatusProjeto Status { get; set; }

    public List<string> ObjetivosIds { get; set; } = new List<string>();
    public List<string> DominiosIds { get; set; } = new List<string>();
    public List<string> MicroservicosIds { get; set; } = new List<string>();
    public List<string> MetricasIds { get; set; } = new List<string>();
    public List<string> EquipesEnvolvidasIds { get; set; } = new List<string>();

}