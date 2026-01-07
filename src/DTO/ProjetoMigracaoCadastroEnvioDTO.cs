using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Enum;

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.DTO;

public class ProjetoMigracaoCadastroEnvioDTO
{
    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public string Responsavel { get; set; } = string.Empty;

    public Decimal OrcamentoPrevisto { get; set; }

    public DateTime DataInicio { get; set; }

    public DateTime DataFimPrevisto { get; set; }

    public string JustificativaNegocio { get; set; } = string.Empty;

    public List<string> TecnologiasBackEnd { get; set; } = [];

    public List<string> TecnologiasFrontEnd { get; set; } = [];

    public string Status { get; set; } = string.Empty;
}