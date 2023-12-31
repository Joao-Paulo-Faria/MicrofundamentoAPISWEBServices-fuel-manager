﻿using MicrofundamentoAPISWEBServices_fuel_manager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MicrofundamentoAPISWEBServices_fuel_manager.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly AppDbContext _context;//variavel somente de leitura para receber o serviço de banco de dados

        public VeiculosController(AppDbContext context)//construtor recebe um objeto do tipo appdbcontext
        {//no meu construtor eu falo que recebo um objeto do tipo appdbcontext
            _context = context;//injeção de dependencia
        }

        //vamos criar agora a rota de index que é para exibir todos os carros cadastrados.
        //vamos criar uma função:
        [Authorize(Roles ="Usuario")]
        [HttpGet]
        public async Task<ActionResult> CetAll() 
        {
            //se eu der um get ele vai no api/controller/httpget...
            //criar modelo:
            var model = await _context.Veiculos.ToListAsync();
                // a minha variavel _context é o banco de dados/meu contexto de dados
                //vou chamar a tabela veiculos com todos os veiculos que estão dentro dela e mando um tolistasync()
            //Agora vamos responder:
            return Ok(model);
            //entre as linhas 22 e 31 existe uma rota configurada.  
            //O método ToList tem como objetivo materializar uma lista de elementos em memória. Na prática, ele retorna uma nova instância de List<TSource> a partir de um IEnumerable<TSource>.
        }
        [Authorize(Roles = "Administrador, Usuario")]
        //Agora vou criar um novo veículo através da minha api
        [HttpPost]//vou utilizar o método post, fazer criação de novo item
        public async Task<ActionResult> Create(Veiculo model)
        {

            if(model.AnoFabricacao <= 0 || model.AnoModelo <= 0)
            {
                return BadRequest(new { message = "Ano de Fabricação e ano do modelo devem ser maiores do que zero " });
            }
            _context.Veiculos.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new {id=model.Id}, model);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Veiculos
                .Include(x => x.Usuarios).ThenInclude(t=>t.Usuario)
                .Include(t=> t.Consumos).FirstOrDefaultAsync(c => c.Id == id);

            if(model == null) return NotFound();
            GerarLinks(model);
            return Ok(model);
        }

        // FirstOrDefaultAsync --->Retorna de forma assíncrona o primeiro elemento de uma sequência que satisfaz uma condição especificada ou um valor padrão se nenhum elemento desse tipo for encontrado.
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Veiculo model)
        {
            if (id != model.Id) return BadRequest();
            //estou validando se a url na rota é a mesa no veiculo.
            var modeloDB = await _context.Veiculos.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id) ;
            if (modeloDB == null) return NotFound();
            //asnotracking - quer apenas consultar sem alterar...(spenas visualizar)
           

            _context.Veiculos.Update(model); // atualiza no banco de dados
            await _context.SaveChangesAsync(); //salva no banco de dados
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            var model = await _context.Veiculos.FindAsync(id);

            if (model == null) return NotFound();

            _context.Veiculos.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private void GerarLinks(Veiculo model)
        {
            model.Links.Add(new LinkDto(model.Id,Url.ActionLink(), rel: "self", metodo: "GET"));
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "update", metodo: "PUT"));
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "delete", metodo: "Delete"));
        }
        [HttpPost("{id}/usuarios")]
        public async Task<ActionResult> AddUsuario(int id, VeiculoUsuarios model)
        {
            if(id != model.VeiculoId) return BadRequest();

            _context.VeiculoUsuarios.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = model.VeiculoId }, model);
        }

        [HttpDelete("{id}/usuarios/{usuarioId}")]
        public async Task<ActionResult> DeleteUsuario(int id, int usuarioId)
        {
            var model = await _context.VeiculoUsuarios
               .Where(c => c.VeiculoId == id && c.UsuarioId == usuarioId)
               .FirstOrDefaultAsync();

            if(model == null) return NotFound();

            _context.VeiculoUsuarios.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }

}




//criou uma api com configurações basicas
//essa classe VeiculosController vai herdar do ControllerBase
// [Route("api/[controller]")]- esta falando qual vai ser a rota desse controlador
//vai ser o nome do controlador que é veículos, seguido da palabra api.
//então para acessar essa rota preciso colocar o nome do meu dominio/api/veiculos
//Esse  [ApiController] já vai configurar a minha web api com varias caracteristicas,
//uma delas é que se o meu modelo de dados não for valido eu não precisarei criar um codigo
//para validar ele, automaticamente vai dar um erro 400 e que as configurações x são
//obrigatorias
//readonly somente leitura de dados
//objeto privado - private
//chamar ele de _context
//propriedade -->  private readonly AppDbContext _context;
//toda vez que precisar acessar o banco de dados para leitura ou gravação vou utilizar a variavel
//_context;

//toda classe que precisar utilizar o banco de dados basta eu fazer a configuração entre as linhas 12 e 17
// em asp.net para retornar retorno das operações de requisições é utilizado o ActionResult 
//Isso que dizer que não preciso pegar a resposta do meu banco de dados e converter ele para um arquivo json ou para um html
//ele configura isso através das views ou no caso da api é sempre json e se tiver erro ele consegue configurar essa resposta de erro
//async Task é pq estou utilizando um processamento assincrona, onde faz uma consulta no banco de dados e ele vai esperar a resposta vim
//Task é para falar que é uma tred(tarefa) que está sendo executada 
//Uma tarefa (ou task) representa uma unidade de trabalho que deverá ser realizada. Esta unidade de trabalho pode rodar em uma thread separada e é também possível iniciar uma task de forma sincronizada a
//qual resulta em uma espera pela thread chamada.
//Threads permitem que múltiplas execuções ocorram no mesmo ambiente do aplicativo com um grande grau de independência uma da outra, portanto, se temos muitas threads executando em paralelo no sistema é análogo a múltiplos aplicativos executando em paralelo em um computador.