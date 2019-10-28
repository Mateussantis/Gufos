using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    // Definimos nossa rota do controler e dizemos que é um controller de API
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        
        GufosContext _contexto = new GufosContext();

        // GET: api/TipoUsuario
        [HttpGet]
        public async Task<ActionResult<List<TipoUsuario>>> Get(){
            // ToListAsync (Select * from TipoUsuario)
            var TipoUsuarios = await _contexto.TipoUsuario.ToListAsync();
            
            if(TipoUsuarios == null) {
                return NotFound();
            }
            return TipoUsuarios;
        }

        // GET: api/TipoUsuario2
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoUsuario>> Get(int id){

            // FindAsync (Select * from TipoUsuario where id = id)
            var TipoUsuario = await _contexto.TipoUsuario.FindAsync(id);
            
            if(TipoUsuario == null) {
                return NotFound();
            }
            return TipoUsuario;
        }

        // Post api/tipousuario
        [HttpPost]
        public async Task<ActionResult<TipoUsuario>> Post(TipoUsuario tipousuario) {

            try {   
                // Tratamos contra ataques de SQL INJECTION
                await _contexto.AddAsync(tipousuario);
                // Salvamos efetivamento o nosso objeto
                await _contexto.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) {
                throw;
            }
            return tipousuario;
        }

        // Put api/tipousuario
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, TipoUsuario tipousuario){

            if (id != tipousuario.TipoUsuarioId) {
                return BadRequest();
            }

            // Comparamos os atributos que foram modificados atraves do EntityFrameWork
            _contexto.Entry(tipousuario).State = EntityState.Modified;

            try {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                
                var tipousuario_valido = await _contexto.TipoUsuario.FindAsync(id);

                // Verificamos se o objeto inserido realmente existe no banco
                if(tipousuario_valido == null) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
        // NoContent() = Retorna 204, sem nada.
        return NoContent();
        }

        // Delet api/tipousuario
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoUsuario>> Delete(int id) {

            var  tipousuario = await _contexto.TipoUsuario.FindAsync(id);
            if(tipousuario == null) {
                return NotFound();
            }

            _contexto.TipoUsuario.Remove(tipousuario);
            await _contexto.SaveChangesAsync();
            return tipousuario;
        }
    }
}