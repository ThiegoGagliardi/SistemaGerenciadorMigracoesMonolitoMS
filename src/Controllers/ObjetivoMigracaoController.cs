using Microsoft.AspNetCore.Mvc;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Data;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Entities;
using MongoDB.Driver;

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.Controllers;

[ApiController]
[Route("[controller]")]
public class ObjetivoMigracaoController : ControllerBase
{
    private readonly IMongoCollection<ObjetivoMigracao> _objetivosCollection;

    public ObjetivoMigracaoController(IMigracaoMonolitoParaMSDBContext dbContext)
    {
        _objetivosCollection = dbContext.ObjetivosMigracao;
    }

    [HttpGet]
    public async Task<List<ObjetivoMigracao>> Get()
    {
        return await _objetivosCollection.Find(objetivo => true).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ObjetivoMigracao>> Get(string id)
    {
        var objetivo = await _objetivosCollection.Find(o => o.Id == id).FirstOrDefaultAsync();

        if (objetivo == null)
        {
            return NotFound();
        }

        return objetivo;
    }


    [HttpPost]
    public async Task<IActionResult> Post(ObjetivoMigracao novoObjetivo)
    {
        await _objetivosCollection.InsertOneAsync(novoObjetivo);
        return CreatedAtAction(nameof(Get), new { id = novoObjetivo.Id }, novoObjetivo);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var objetivoExistente = await _objetivosCollection.Find(o => o.Id == id).FirstOrDefaultAsync();

        if (objetivoExistente == null)
        {
            return NotFound();
        }

        await _objetivosCollection.DeleteOneAsync(o => o.Id == id);

        return NoContent();
    }       

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, ObjetivoMigracao objetivoAtualizado)
    {
        var objetivoExistente = await _objetivosCollection.Find(o => o.Id == id).FirstOrDefaultAsync();

        if (objetivoExistente == null)
        {
            return NotFound();
        }

        objetivoAtualizado.Id = objetivoExistente.Id; // Garante que o ID não seja alterado
        await _objetivosCollection.ReplaceOneAsync(o => o.Id == id, objetivoAtualizado);

        return NoContent();
    }
}
