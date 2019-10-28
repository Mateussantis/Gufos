using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Para adicionar a arvore de objetos adicionamos uma nova biblioteca Json
// dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson


namespace BackEnd.Controllers
{
    // Definimos nossa rota do controler e dizemos que é um controller de API
    [Route("api/[controller]")]
    [ApiController]
    public class PresencaController : ControllerBase
    {
        
        GufosContext _contexto = new GufosContext();

        // GET: api/Presenca
        [HttpGet]
        public async Task<ActionResult<List<Presenca>>> Get(){
            // Include - Adiciona a arvore de objetos relacionados
            var presencas = await _contexto.Presenca.ToListAsync();
            
            if(presencas == null) {
                return NotFound();
            }
            return presencas;
        }

        // GET: api/Presenca2
        [HttpGet("{id}")]
        public async Task<ActionResult<Presenca>> Get(int id){

            // FindAsync (Select * from Presenca where id = id)
            var presenca = await _contexto.Presenca.Include("Evento").Include("Usuario").FirstOrDefaultAsync(e => e.PresencaId == id);
            
            if(presenca == null) {
                return NotFound();
            }
            return presenca;
        }

        // Post api/presenca
        [HttpPost]
        public async Task<ActionResult<Presenca>> Post(Presenca presenca) {

            try {   
                // Tratamos contra ataques de SQL INJECTION
                await _contexto.AddAsync(presenca);
                // Salvamos efetivamento o nosso objeto
                await _contexto.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) {
                throw;
            }
            return presenca;
        }

        // Put api/presenca
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Presenca presenca){

            if (id != presenca.PresencaId) {
                return BadRequest();
            }

            // Comparamos os atributos que foram modificados atraves do EntityFrameWork
            _contexto.Entry(presenca).State = EntityState.Modified;

            try {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                
                var presenca_valido = await _contexto.Presenca.FindAsync(id);

                // Verificamos se o objeto inserido realmente existe no banco
                if(presenca_valido == null) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
        // NoContent() = Retorna 204, sem nada.
        return NoContent();
        }

        // Delet api/presenca
        [HttpDelete("{id}")]
        public async Task<ActionResult<Presenca>> Delete(int id) {

            var  presenca = await _contexto.Presenca.FindAsync(id);
            if(presenca == null) {
                return NotFound();
            }

            _contexto.Presenca.Remove(presenca);
            await _contexto.SaveChangesAsync();
            return presenca;
        }
    }
}