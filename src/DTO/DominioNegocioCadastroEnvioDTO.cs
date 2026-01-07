
namespace GerenciamentoMigracaoMonolitoParaMS.app.src.DTO;

public class DominioNegocioCadastroEnvioDTO
{
     public string ProjetoMigracaoId { get; set; } = string.Empty;
     public string Nome { get; set; } = string.Empty;
     public string Descricao { get; set; } = string.Empty;
     public string ResponsavelDominio { get; set; } = string.Empty;
     public string StatusMigracao { get; set; } = string.Empty;
}