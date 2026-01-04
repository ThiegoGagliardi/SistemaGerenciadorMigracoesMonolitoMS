using Microsoft.AspNetCore.Mvc;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Data;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Entities;
using MongoDB.Driver;

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.Controllers;

[ApiController]
[Route("[controller]")]
public class MembroEquipeController : ControllerBase
{
    private readonly IMongoCollection<MembroEquipe> _membrosCollection;

    public MembroEquipeController(IMigracaoMonolitoParaMSDBContext dbContext)
    {
        _membrosCollection = dbContext.MembrosEquipe;
    }



    [HttpGet]
    public async Task<List<MembroEquipe>> Get()
    {
        return await _membrosCollection.Find(membro => true).ToListAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<MembroEquipe>> Get(string id)
    {
        var membro = await _membrosCollection.Find(m => m.Id == id).FirstOrDefaultAsync();

        if (membro == null)
           return NotFound();        

        return membro;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var membroExistente = await _membrosCollection.Find(m => m.Id == id).FirstOrDefaultAsync();

        if (membroExistente == null)
           return NotFound();
        
        await _membrosCollection.DeleteOneAsync(m => m.Id == id);

        return NoContent();
    }        
    [HttpPost]
    public async Task<IActionResult> Post(MembroEquipe novoMembro)
    {
        await _membrosCollection.InsertOneAsync(novoMembro);
        return CreatedAtAction(nameof(Get), new { id = novoMembro.Id }, novoMembro);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, MembroEquipe membroAtualizado)
    {
        var membroExistente = await _membrosCollection.Find(m => m.Id == id).FirstOrDefaultAsync();

        if (membroExistente == null)
        {
            return NotFound();
        }

        membroAtualizado.Id = membroExistente.Id;
        await _membrosCollection.ReplaceOneAsync(m => m.Id == id, membroAtualizado);

        return NoContent();
    }




}