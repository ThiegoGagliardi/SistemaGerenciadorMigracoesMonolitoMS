using Microsoft.AspNetCore.Mvc;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Data;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Entities;
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
    public async Task<List<ProjetoMigracao>> Get()
    {
        return await _projetosCollection.Find(projeto => true).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjetoMigracao>> Get(string id)
    {
        var projeto = await _projetosCollection.Find(p => p.Id == id).FirstOrDefaultAsync();

        if (projeto == null)
        {
            return NotFound();
        }

        return projeto;
    }

    [HttpPost]
    public async Task<IActionResult> Post(ProjetoMigracao novoProjeto)
    {
        await _projetosCollection.InsertOneAsync(novoProjeto);
        return CreatedAtAction(nameof(Get), new { id = novoProjeto.Id }, novoProjeto);
    }
}
