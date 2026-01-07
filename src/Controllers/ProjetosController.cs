using Microsoft.AspNetCore.Mvc;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Data;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Enum;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Entities;
using GerenciamentoMigracaoMonolitoParaMS.app.src.DTO;
using MongoDB.Driver; 

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjetosController : ControllerBase
{
    private readonly IMongoCollection<ProjetoMigracao> _projetosCollection;

    public ProjetosController(IMigracaoMonolitoParaMSDBContext dbContext)
    {
        _projetosCollection = dbContext.ProjetosMigracao;
    }

    [HttpGet]
    public async Task<List<ProjetoMigracaoRetornoDTO>> Get()
    {
        var projeto = await _projetosCollection.Find(projeto => true).ToListAsync();

        List<ProjetoMigracaoRetornoDTO> projetosDTO = new();

        foreach (var p in projeto)
        {
            ProjetoMigracaoRetornoDTO projetoDTO  = new()
            {
            Id  = p.Id,

            Nome = p.Nome,

            Descricao = p.Descricao,

            Responsavel = p.Responsavel,
            
            OrcamentoPrevisto = p.OrcamentoPrevisto,
            
            OrcamentoGasto= p.OrcamentoGasto,
            
            DataInicio = p.DataInicio,
            
            DataFimPrevisto = p.DataFimPrevisto,
            
            DataFimEfetiva = p.DataFimEfetiva,
            
            JustificativaNegocio = p.JustificativaNegocio,
            
            TecnologiasBackend = string.Join(",", p.TecnologiasBackEnd),
            
            TecnologiasFrontend = string.Join(",", p.TecnologiasFrontEnd),

            Status =  p.Status.ToString()                    
            };

           projetosDTO.Add(projetoDTO);   
        };

        return projetosDTO;        
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjetoMigracaoRetornoDTO>> Get(string id)
    {
        var projeto = await _projetosCollection.Find(p => p.Id == id).FirstOrDefaultAsync();

        if (projeto == null)
        {
            return NotFound();
        }

        ProjetoMigracaoRetornoDTO projetoDTO = new ()
        {
            Id  = projeto.Id,

            Nome = projeto.Nome,

            Descricao = projeto.Descricao,

            Responsavel = projeto.Responsavel,
            
            OrcamentoPrevisto = projeto.OrcamentoPrevisto,
            
            OrcamentoGasto= projeto.OrcamentoGasto,
            
            DataInicio = projeto.DataInicio,
            
            DataFimPrevisto = projeto.DataFimPrevisto,
            
            DataFimEfetiva = projeto.DataFimEfetiva,
            
            JustificativaNegocio = projeto.JustificativaNegocio,
            
            TecnologiasBackend = string.Join(",", projeto.TecnologiasBackEnd),
            
            TecnologiasFrontend = string.Join(",", projeto.TecnologiasFrontEnd),

            Status =  projeto.Status.ToString()            
        };

        return projetoDTO;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var projetoExistente = await _projetosCollection.Find(o => o.Id == id).FirstOrDefaultAsync();

        if (projetoExistente == null)
        {
            return NotFound();
        }

        await _projetosCollection.DeleteOneAsync(o => o.Id == id);

        return NoContent();
    }     

    [HttpPost]
    public async Task<IActionResult> Post( ProjetoMigracaoCadastroEnvioDTO novoProjetoDTO)
    {        
        ProjetoMigracao novoProjeto = new()
        {            
            Nome              = novoProjetoDTO.Nome,
            Descricao         = novoProjetoDTO.Descricao,
            Responsavel       = novoProjetoDTO.Responsavel,
            OrcamentoPrevisto = novoProjetoDTO.OrcamentoPrevisto,
            DataInicio = novoProjetoDTO.DataInicio,
            DataFimPrevisto = novoProjetoDTO.DataFimPrevisto,            
            JustificativaNegocio = novoProjetoDTO.JustificativaNegocio,
            TecnologiasBackEnd = novoProjetoDTO.TecnologiasBackEnd,
            TecnologiasFrontEnd = novoProjetoDTO.TecnologiasFrontEnd,
            Status              = (StatusProjeto)Enum.Parse(typeof(StatusProjeto),novoProjetoDTO.Status,true)            
        };  

        await _projetosCollection.InsertOneAsync(novoProjeto);
        return CreatedAtAction(nameof(Get), new { nome = novoProjeto.Nome, id = novoProjeto.Id }, novoProjeto);
    }
}
