using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Repositories;
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
        
        // GufosContext _contexto = new GufosContext();
        PresencaRepository _repositorio = new PresencaRepository();

        // GET: api/Presenca
        /// <summary>
        ///  Retorna a lista de Presencas
        /// </summary>
        /// <returns>Lista de obj</returns>
        [HttpGet]
        public async Task<ActionResult<List<Presenca>>> Get(){
            // ToListAsync (Select * from Presenca)
            var Presencas = await _repositorio.Listar();
            
            if(Presencas == null) {
                return NotFound();
            }
            return Presencas;
        }

        // GET: api/Presenca2
        /// <summary>
        /// retorna o objeto pesquisado pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto especifico</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Presenca>> Get(int id){

            // FindAsync (Select * from Presenca where id = id)
            var Presenca = await _repositorio.BuscarPorID(id);
            
            if(Presenca == null) {
                return NotFound();
            }
            return Presenca;
        }

        // Post api/presenca
        /// <summary>
        /// Cria uma Presenca
        /// </summary>
        /// <param name="presenca"></param>
        /// <returns>Presenca especifica pelo id</returns>
        [HttpPost]
        public async Task<ActionResult<Presenca>> Post(Presenca presenca) {

            try {   
                await _repositorio.Salvar(presenca);
            }
            catch(DbUpdateConcurrencyException) {
                throw;
            }
            return presenca;
        }

        // Put api/presenca
        /// <summary>
        /// Atualiza objeto pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="presenca"></param>
        /// <returns>Presenca atualizada</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Presenca presenca){

            if (id != presenca.PresencaId) {
                return BadRequest();
            }

            try {
                await _repositorio.Alterar(presenca);
            }
            catch (DbUpdateConcurrencyException) {
                
                var presenca_valido = await _repositorio.BuscarPorID(id);

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

            var  presenca = await _repositorio.BuscarPorID(id);
            if(presenca == null) {
                return NotFound();
            }

            await _repositorio.Excluir(presenca);

            return presenca;
        }
    }
}