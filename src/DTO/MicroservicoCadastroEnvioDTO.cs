

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.DTO;

public class MicroservicoCadastroEnvioDTO
{
    public string ProjetoMigracaoId { get; set; } = string.Empty;
    public string DominioId { get; set; } = string.Empty;
    public string EquipeResponsavelId { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string RepositorioCodigo { get; set; } = string.Empty;
    public string Tecnologias { get; set; }  = string.Empty;
    public string Status { get; set; }  = string.Empty;
    public string EtapaAtualMigracao { get; set; } = string.Empty;
    public string Dependencias { get; set; } = string.Empty;
    public DateTime DataPrevisaoConclusao { get; set; }
    public DateTime? DataRealConclusao { get; set; }
}