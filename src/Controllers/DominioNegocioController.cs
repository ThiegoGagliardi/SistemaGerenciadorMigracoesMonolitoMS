using Microsoft.AspNetCore.Mvc;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Data;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.Controllers;

[ApiController]
[Route("[controller]")] // A rota será /DominioNegocio
public class DominioNegocioController : ControllerBase
{
    private readonly IMongoCollection<DominioNegocio> _dominiosCollection;

    public DominioNegocioController(IMigracaoMonolitoParaMSDBContext dbContext)
    {
        _dominiosCollection = dbContext.DominiosNegocio;
    }


    [HttpGet]
    public async Task<List<DominioNegocio>> Get()
    {
        return await _dominiosCollection.Find(dominio => true).ToListAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<DominioNegocio>> Get(string id)
    {
        var dominio = await _dominiosCollection.Find(d => d.Id == id).FirstOrDefaultAsync();

        if (dominio == null)
        {
            return NotFound();
        }

        return dominio;
    }


    [HttpPost]
    public async Task<IActionResult> Post(DominioNegocio novoDominio)
    {
        await _dominiosCollection.InsertOneAsync(novoDominio);
        return CreatedAtAction(nameof(Get), new { id = novoDominio.Id }, novoDominio);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, DominioNegocio dominioAtualizado)
    {
        var dominioExistente = await _dominiosCollection.Find(d => d.Id == id).FirstOrDefaultAsync();

        if (dominioExistente == null)
        {
            return NotFound();
        }

        dominioAtualizado.Id = dominioExistente.Id; // Garante que o ID não seja alterado
        await _dominiosCollection.ReplaceOneAsync(d => d.Id == id, dominioAtualizado);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var dominioExistente = await _dominiosCollection.Find(d => d.Id == id).FirstOrDefaultAsync();

        if (dominioExistente == null)
        {
            return NotFound();
        }

        await _dominiosCollection.DeleteOneAsync(d => d.Id == id);

        return NoContent();
    }
}