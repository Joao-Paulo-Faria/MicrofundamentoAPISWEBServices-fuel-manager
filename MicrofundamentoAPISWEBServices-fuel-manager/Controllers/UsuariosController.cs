using BCrypt.Net;
using MicrofundamentoAPISWEBServices_fuel_manager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MicrofundamentoAPISWEBServices_fuel_manager.Controllers
{

    [Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;//variavel somente de leitura para receber o serviço de banco de dados

        public UsuariosController(AppDbContext context)//construtor recebe um objeto do tipo appdbcontext
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
            var model = await _context.Usuarios.ToListAsync();
            // a minha variavel _context é o banco de dados/meu contexto de dados
            //vou chamar a tabela veiculos com todos os veiculos que estão dentro dela e mando um tolistasync()
            //Agora vamos responder:
            return Ok(model);
            //entre as linhas 22 e 31 existe uma rota configurada.  
            //O método ToList tem como objetivo materializar uma lista de elementos em memória. Na prática, ele retorna uma nova instância de List<TSource> a partir de um IEnumerable<TSource>.
        }
        //Agora vou criar um novo veículo através da minha api
        [HttpPost]//vou utilizar o método post, fazer criação de novo item
        public async Task<ActionResult> Create(UsuarioDto model)

        {
            Usuario novo = new Usuario()
            {
                Nome = model.Nome,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Perfil = model.Perfil
            };
            
            _context.Usuarios.Add(novo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = novo.Id }, novo);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Usuarios.FirstOrDefaultAsync(c => c.Id == id);

            if (model == null) return NotFound();
          
            return Ok(model);
        }

        // FirstOrDefaultAsync --->Retorna de forma assíncrona o primeiro elemento de uma sequência que satisfaz uma condição especificada ou um valor padrão se nenhum elemento desse tipo for encontrado.
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UsuarioDto model)
        {
            if (id != model.Id) return BadRequest();
            //estou validando se a url na rota é a mesa no veiculo.
            var modeloDB = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (modeloDB == null) return NotFound();
            //asnotracking - quer apenas consultar sem alterar...(spenas visualizar)
            modeloDB.Nome = model.Nome;
            modeloDB.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            modeloDB.Perfil = model.Perfil;

            _context.Usuarios.Update(modeloDB); // atualiza no banco de dados
            await _context.SaveChangesAsync(); //salva no banco de dados
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            var model = await _context.Usuarios.FindAsync(id);

            if (model == null) return NotFound();

            _context.Usuarios.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult> Authenticate(AuthenticateDto model)
        {

            var usuarioDB = await _context.Usuarios.FindAsync(model.Id);

            if(usuarioDB == null || !BCrypt.Net.BCrypt.Verify(model.PassWord, usuarioDB.Password))
                return Unauthorized();

            var jwt = GenerateJwtToken(usuarioDB);

            return Ok(new {jwtToken = jwt });
        }

        private string GenerateJwtToken(Usuario model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("bHtnQYUp6GtK81Qh1FR37R0mMNVYnF6y");
            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                new Claim(ClaimTypes.Role, model.Perfil.ToString()),
            });
            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescripter);
            return tokenHandler.WriteToken(token);
        }


    }
}
