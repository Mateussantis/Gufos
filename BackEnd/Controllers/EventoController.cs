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
    public class EventoController : ControllerBase
    {
        
        GufosContext _contexto = new GufosContext();
        

        // GET: api/Evento
        /// <summary>
        /// Listar todos os eventos
        /// </summary>
        /// <returns>Os objetos</returns>
        [HttpGet]
        public async Task<ActionResult<List<Evento>>> Get(){
            // Include - Adiciona a arvore de objetos relacionados
            var eventos = await _contexto.Evento.ToListAsync();
            
            if(eventos == null) {
                return NotFound();
            }
            return eventos;
        }

        // GET: api/Evento2
        /// <summary>
        /// Mostra um objeto selecionado pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> Get(int id){

            // FindAsync (Select * from Evento where id = id)
            var evento = await _contexto.Evento.Include("Categoria").Include("Localizacao").FirstOrDefaultAsync(e => e.EventoId == id);
            
            if(evento == null) {
                return NotFound();
            }
            return evento;
        }

        // Post api/evento
        /// <summary>
        /// Cria um novo evetno/
        /// </summary>
        /// <param name="evento"></param>
        /// <returns>Objeto criado</returns>
        [HttpPost]
        public async Task<ActionResult<Evento>> Post(Evento evento) {

            try {   
                // Tratamos contra ataques de SQL INJECTION
                await _contexto.AddAsync(evento);
                // Salvamos efetivamento o nosso objeto
                await _contexto.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) {
                throw;
            }
            return evento;
        }

        // Put api/evento
        /// <summary>
        /// Atualiza um evento
        /// </summary>
        /// <param name="id"></param>
        /// <param name="evento"></param>
        /// <returns>Nada</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Evento evento){

            if (id != evento.EventoId) {
                return BadRequest();
            }

            // Comparamos os atributos que foram modificados atraves do EntityFrameWork
            _contexto.Entry(evento).State = EntityState.Modified;

            try {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                
                var evento_valido = await _contexto.Evento.FindAsync(id);

                // Verificamos se o objeto inserido realmente existe no banco
                if(evento_valido == null) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
        // NoContent() = Retorna 204, sem nada.
        return NoContent();
        }

        // Delet api/evento
        /// <summary>
        /// Deleta um objeto pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto deletado</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Evento>> Delete(int id) {

            var  evento = await _contexto.Evento.FindAsync(id);
            if(evento == null) {
                return NotFound();
            }

            _contexto.Evento.Remove(evento);
            await _contexto.SaveChangesAsync();
            return evento;
        }
    }
}