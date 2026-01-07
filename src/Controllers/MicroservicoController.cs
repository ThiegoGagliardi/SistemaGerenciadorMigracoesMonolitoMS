using Microsoft.AspNetCore.Mvc;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Data;
using GerenciamentoMigracaoMonolitoParaMS.app.src.DTO;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Enum;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Entities;
using MongoDB.Driver;

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.Controllers;

[ApiController]
[Route("[controller]")]
public class MicroservicoController : ControllerBase
{
    private readonly IMongoCollection<Microservico> _microservicosCollection;

    public MicroservicoController(IMigracaoMonolitoParaMSDBContext dbContext)
    {
        _microservicosCollection = dbContext.Microservicos;
    }

    [HttpGet]
    public async Task<List<Microservico>> Get()
    {
        return await _microservicosCollection.Find(ms => true).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Microservico>> Get(string id)
    {
        var microservico = await _microservicosCollection.Find(ms => ms.Id == id).FirstOrDefaultAsync();

        if (microservico == null)
        {
            return NotFound();
        }

        return microservico;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var microservicoExistente = await _microservicosCollection.Find(ms => ms.Id == id).FirstOrDefaultAsync();

        if (microservicoExistente == null)
        {
            return NotFound();
        }

        await _microservicosCollection.DeleteOneAsync(ms => ms.Id == id);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Post(MicroservicoCadastroEnvioDTO novoMicroservicoDTO)
    {
        Console.WriteLine(novoMicroservicoDTO.Tecnologias);

        Microservico novoMicroservico =  new ()
        {
            ProjetoMigracaoId = novoMicroservicoDTO.ProjetoMigracaoId,
            
            DominioId = novoMicroservicoDTO.DominioId, 
            
            EquipeResponsavelId = novoMicroservicoDTO.EquipeResponsavelId,

            Nome = novoMicroservicoDTO.Nome,
 
            Descricao = novoMicroservicoDTO.Descricao,
            
            RepositorioCodigo = novoMicroservicoDTO.RepositorioCodigo,
           
            Tecnologias = novoMicroservicoDTO.Tecnologias.Split(',').ToList(),

            Status = (MicroservicoStatus)Enum.Parse(typeof(MicroservicoStatus),  novoMicroservicoDTO.Status, true),
            
            EtapaAtualMigracao = novoMicroservicoDTO.EtapaAtualMigracao, 
            
            DataPrevisaoConclusao = novoMicroservicoDTO.DataPrevisaoConclusao,
            
            DataRealConclusao = novoMicroservicoDTO.DataRealConclusao,
            
            Dependencias = novoMicroservicoDTO.Dependencias.Split(',').ToList()
        };

        await _microservicosCollection.InsertOneAsync(novoMicroservico);
        return CreatedAtAction(nameof(Get), new { id = novoMicroservico.Id }, novoMicroservico);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, Microservico microservicoAtualizado)
    {
        var microservicoExistente = await _microservicosCollection.Find(ms => ms.Id == id).FirstOrDefaultAsync();

        if (microservicoExistente == null)
        {
            return NotFound();
        }

        microservicoAtualizado.Id = microservicoExistente.Id; 
        await _microservicosCollection.ReplaceOneAsync(ms => ms.Id == id, microservicoAtualizado);

        return NoContent();
    }    
}