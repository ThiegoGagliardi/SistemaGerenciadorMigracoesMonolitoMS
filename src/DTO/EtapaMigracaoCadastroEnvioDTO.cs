using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Enum;

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.DTO;
public class EtapaMigracaoCadastroEnvioDTO
{
    public string MicroservicoId { get; set; } = string.Empty;
    public string NomeEtapa { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFimPrevista { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Observacoes { get; set; } = string.Empty;

}