using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    // Definimos nossa rota do controler e dizemos que é um controller de API
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        
        // GufosContext _contexto = new GufosContext();
        EventoRepository _repositorio = new EventoRepository();

        // GET: api/Evento
        /// <summary>
        ///  Retorna a lista de Eventos
        /// </summary>
        /// <returns>Lista de obj</returns>
        [HttpGet]
        public async Task<ActionResult<List<Evento>>> Get(){
            // ToListAsync (Select * from Evento)
            var Eventos = await _repositorio.Listar();
            
            if(Eventos == null) {
                return NotFound();
            }
            return Eventos;
        }

        // GET: api/Evento2
        /// <summary>
        /// retorna o objeto pesquisado pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto especifico</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> Get(int id){

            // FindAsync (Select * from Evento where id = id)
            var Evento = await _repositorio.BuscarPorID(id);
            
            if(Evento == null) {
                return NotFound();
            }
            return Evento;
        }

        // Post api/evento
        /// <summary>
        /// Cria uma Evento
        /// </summary>
        /// <param name="evento"></param>
        /// <returns>Evento especifica pelo id</returns>
        [HttpPost]
        public async Task<ActionResult<Evento>> Post(Evento evento) {

            try {   
                await _repositorio.Salvar(evento);
            }
            catch(DbUpdateConcurrencyException) {
                throw;
            }
            return evento;
        }

        // Put api/evento
        /// <summary>
        /// Atualiza objeto pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="evento"></param>
        /// <returns>Evento atualizada</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Evento evento){

            if (id != evento.EventoId) {
                return BadRequest();
            }

            try {
                await _repositorio.Alterar(evento);
            }
            catch (DbUpdateConcurrencyException) {
                
                var evento_valido = await _repositorio.BuscarPorID(id);

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
        [HttpDelete("{id}")]
        public async Task<ActionResult<Evento>> Delete(int id) {

            var  evento = await _repositorio.BuscarPorID(id);
            if(evento == null) {
                return NotFound();
            }

            await _repositorio.Excluir(evento);

            return evento;
        }
    }
}