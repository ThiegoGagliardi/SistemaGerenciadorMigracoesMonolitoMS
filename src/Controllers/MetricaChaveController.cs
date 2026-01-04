using Microsoft.AspNetCore.Mvc;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Data;
using GerenciamentoMigracaoMonolitoParaMS.app.src.Domain.Entities;
using MongoDB.Driver;

namespace GerenciamentoMigracaoMonolitoParaMS.app.src.Controllers;

[ApiController]
[Route("[controller]")]
public class MetricaChaveController : ControllerBase
{
private readonly IMongoCollection<Metricas> _metricasCollection;

        public MetricaChaveController(IMigracaoMonolitoParaMSDBContext dbContext)
        {
            _metricasCollection = dbContext.Metricas;
        }
        [HttpGet]
        public async Task<List<Metricas>> Get()
        {
            return await _metricasCollection.Find(metrica => true).ToListAsync();
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Metricas>> Get(string id)
        {
            var metrica = await _metricasCollection.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (metrica == null)
            {
                return NotFound();
            }

            return metrica;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Metricas metricaAtualizada)
        {
            var metricaExistente = await _metricasCollection.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (metricaExistente == null)
            {
                return NotFound();
            }

            metricaAtualizada.Id = metricaExistente.Id; // Garante que o ID não seja alterado
            await _metricasCollection.ReplaceOneAsync(m => m.Id == id, metricaAtualizada);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var metricaExistente = await _metricasCollection.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (metricaExistente == null)
            {
                return NotFound();
            }

            await _metricasCollection.DeleteOneAsync(m => m.Id == id);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Metricas novaMetrica)
        {
            await _metricasCollection.InsertOneAsync(novaMetrica);
            return CreatedAtAction(nameof(Get), new { id = novaMetrica.Id }, novaMetrica);
        }        
}