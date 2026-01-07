using Microsoft.AspNetCore.Mvc;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Data;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Entities;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Enum;
using GerenciamentoMigracaoMonolitoParaMS.app.src.DTO;
using MongoDB.Driver;

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.Controllers;

[ApiController]
[Route("[controller]")]
public class EtapaMigracaoController : ControllerBase
{
    private readonly IMongoCollection<EtapaMigracao> _etapasCollection;

    public EtapaMigracaoController(IMigracaoMonolitoParaMSDBContext dbContext)
    {
        _etapasCollection = dbContext.EtapasMigracao;
    }

    [HttpGet]
    public async Task<List<EtapaMigracao>> Get()
    {
        return await _etapasCollection.Find(etapa => true).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EtapaMigracao>> Get(string id)
    {
        var etapa = await _etapasCollection.Find(e => e.Id == id).FirstOrDefaultAsync();

        if (etapa == null)
        {
            return NotFound();
        }

        return etapa;
    }

    [HttpPost]
    public async Task<IActionResult> Post(EtapaMigracaoCadastroEnvioDTO EtapaDTO)
    {
        EtapaMigracao novaEtapa = new()
        {
            MicroservicoId = EtapaDTO.MicroservicoId, 
            NomeEtapa  = EtapaDTO.NomeEtapa,
            DataInicio  = EtapaDTO.DataInicio,
            DataFimPrevista = EtapaDTO.DataFimPrevista, 
                        
            Status = (EtapaStatus)Enum.Parse(typeof(EtapaStatus),EtapaDTO.Status,true),
            Observacoes = EtapaDTO.Observacoes
        };

        await _etapasCollection.InsertOneAsync(novaEtapa);
        return CreatedAtAction(nameof(Get), new { id = novaEtapa.Id }, novaEtapa);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, EtapaMigracao etapaAtualizada)
    {
        var etapaExistente = await _etapasCollection.Find(e => e.Id == id).FirstOrDefaultAsync();

        if (etapaExistente == null)
           return NotFound();        

        etapaAtualizada.Id = etapaExistente.Id; // Garante que o ID não seja alterado
        await _etapasCollection.ReplaceOneAsync(e => e.Id == id, etapaAtualizada);

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var etapaExistente = await _etapasCollection.Find(e => e.Id == id).FirstOrDefaultAsync();

        if (etapaExistente == null)
           return NotFound();        

        await _etapasCollection.DeleteOneAsync(e => e.Id == id);

        return NoContent();
    }
}