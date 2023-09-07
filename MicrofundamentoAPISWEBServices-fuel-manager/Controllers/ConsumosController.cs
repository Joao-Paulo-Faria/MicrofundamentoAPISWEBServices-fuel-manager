using MicrofundamentoAPISWEBServices_fuel_manager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MicrofundamentoAPISWEBServices_fuel_manager.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumosController : ControllerBase
    {
        private readonly AppDbContext _context;//variavel somente de leitura para receber o serviço de banco de dados

        public ConsumosController(AppDbContext context)//construtor recebe um objeto do tipo appdbcontext
        {//no meu construtor eu falo que recebo um objeto do tipo appdbcontext
            _context = context;//injeção de dependencia
        }

        //vamos criar agora a rota de index que é para exibir todos os carros cadastrados.
        //vamos criar uma função:
        [HttpGet]
        public async Task<ActionResult> CetAll()
        {
            //se eu der um get ele vai no api/controller/httpget...
            //criar modelo:
            var model = await _context.Consumos.ToListAsync();
            // a minha variavel _context é o banco de dados/meu contexto de dados
            //vou chamar a tabela veiculos com todos os veiculos que estão dentro dela e mando um tolistasync()
            //Agora vamos responder:
            return Ok(model);
            //entre as linhas 22 e 31 existe uma rota configurada.  
            //O método ToList tem como objetivo materializar uma lista de elementos em memória. Na prática, ele retorna uma nova instância de List<TSource> a partir de um IEnumerable<TSource>.
        }
        //Agora vou criar um novo veículo através da minha api
        [HttpPost]//vou utilizar o método post, fazer criação de novo item
        public async Task<ActionResult> Create(Consumo model)
        {

            _context.Consumos.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = model.Id }, model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Consumos.FirstOrDefaultAsync(c => c.Id == id);

            if (model == null) return NotFound();

            GerarLinks(model);

            return Ok(model);
        }

        // FirstOrDefaultAsync --->Retorna de forma assíncrona o primeiro elemento de uma sequência que satisfaz uma condição especificada ou um valor padrão se nenhum elemento desse tipo for encontrado.
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Consumo model)
        {
            if (id != model.Id) return BadRequest();
            //estou validando se a url na rota é a mesa no veiculo.
            var modeloDB = await _context.Consumos.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (modeloDB == null) return NotFound();
            //asnotracking - quer apenas consultar sem alterar...(spenas visualizar)


            _context.Consumos.Update(model); // atualiza no banco de dados
            await _context.SaveChangesAsync(); //salva no banco de dados
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            var model = await _context.Consumos.FindAsync(id);

            if (model == null) return NotFound();

            _context.Consumos.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private void GerarLinks(Consumo model)
        {
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "self", metodo: "GET"));
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "update", metodo: "PUT"));
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "delete", metodo: "Delete"));
        }
    }
}
