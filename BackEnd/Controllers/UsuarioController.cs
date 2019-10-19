using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Para adicionar a arvore de objetos adicionamos uma nova biblioteca Json
// dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson


namespace BackEnd.Controllers
{
    // Definimos nossa rota do controler e dizemos que é um controller de API
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        
        GufosContext _contexto = new GufosContext();

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get(){
            // Include - Adiciona a arvore de objetos relacionados
            var usuarios = await _contexto.Usuario.ToListAsync();
            
            if(usuarios == null) {
                return NotFound();
            }
            return usuarios;
        }

        // GET: api/Usuario2
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id){

            // FindAsync (Select * from Usuario where id = id)
            var usuario = await _contexto.Usuario.Include("TipoUsuario").FirstOrDefaultAsync(e => e.UsuarioId == id);
            
            if(usuario == null) {
                return NotFound();
            }
            return usuario;
        }

        // Post api/usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(Usuario usuario) {

            try {   
                // Tratamos contra ataques de SQL INJECTION
                await _contexto.AddAsync(usuario);
                // Salvamos efetivamento o nosso objeto
                await _contexto.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) {
                throw;
            }
            return usuario;
        }

        // Put api/usuario
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Usuario usuario){

            if (id != usuario.UsuarioId) {
                return BadRequest();
            }

            // Comparamos os atributos que foram modificados atraves do EntityFrameWork
            _contexto.Entry(usuario).State = EntityState.Modified;

            try {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                
                var usuario_valido = await _contexto.Usuario.FindAsync(id);

                // Verificamos se o objeto inserido realmente existe no banco
                if(usuario_valido == null) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
        // NoContent() = Retorna 204, sem nada.
        return NoContent();
        }

        // Delet api/usuario
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> Delete(int id) {

            var  usuario = await _contexto.Usuario.FindAsync(id);
            if(usuario == null) {
                return NotFound();
            }

            _contexto.Usuario.Remove(usuario);
            await _contexto.SaveChangesAsync();
            return usuario;
        }
    }
}