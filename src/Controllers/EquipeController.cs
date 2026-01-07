using Microsoft.AspNetCore.Mvc;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Data;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace GerenciamentoMigracaoMonolitoParaMS.app.src.Controllers;

[ApiController]
[Route("[controller]")] // A rota será /Equipe
public class EquipeController : ControllerBase
{
    private readonly IMongoCollection<Equipe> _equipesCollection;

    public EquipeController(IMigracaoMonolitoParaMSDBContext dbContext)
    {
        _equipesCollection = dbContext.Equipes;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Equipe>> Get(string id)
    {
        var equipe = await _equipesCollection.Find(e => e.Id == id).FirstOrDefaultAsync();

        if (equipe == null)
        {
            return NotFound();
        }

        return equipe;
    }

    [HttpGet]
    public async Task<ActionResult<List<Equipe>>> Get()
    {
        var equipe = await _equipesCollection.Find(projeto => true).ToListAsync();;

        if (equipe == null)
        {
            return NotFound();
        }

        return equipe;
    }    

    [HttpPost]
    public async Task<IActionResult> Post(Equipe novaEquipe)
    {
        await _equipesCollection.InsertOneAsync(novaEquipe);
        return CreatedAtAction(nameof(Get), new { id = novaEquipe.Id }, novaEquipe);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, Equipe equipeAtualizada)
    {
        var equipeExistente = await _equipesCollection.Find(e => e.Id == id).FirstOrDefaultAsync();

        if (equipeExistente == null)
        {
            return NotFound();
        }

        equipeAtualizada.Id = equipeExistente.Id; 
        await _equipesCollection.ReplaceOneAsync(e => e.Id == id, equipeAtualizada);

        return NoContent();
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var equipeExistente = await _equipesCollection.Find(e => e.Id == id).FirstOrDefaultAsync();

        if (equipeExistente == null)
        {
            return NotFound();
        }

        await _equipesCollection.DeleteOneAsync(e => e.Id == id);

        return NoContent();
    }
}
