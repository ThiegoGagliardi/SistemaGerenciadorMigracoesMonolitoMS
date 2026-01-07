using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Enum;

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.DTO;

public class ProjetoMigracaoRetornoDTO
{
    public string Id { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public string Responsavel { get; set; } = string.Empty;

    public Decimal OrcamentoPrevisto { get; set; }

    public Decimal OrcamentoGasto { get; set; }

    public DateTime DataInicio { get; set; }

    public DateTime DataFimPrevisto { get; set; }

    public DateTime DataFimEfetiva { get; set; }

    public string JustificativaNegocio { get; set; } = string.Empty;

    public string TecnologiasBackend { get; set; } = string.Empty;

    public string TecnologiasFrontend { get; set; }= string.Empty;

    public string Status { get; set; } = string.Empty;
}