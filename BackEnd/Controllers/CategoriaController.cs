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
    public class CategoriaController : ControllerBase
    {
        
        // GufosContext _contexto = new GufosContext();
        CategoriaRepository _repositorio = new CategoriaRepository();

        // GET: api/Categoria
        /// <summary>
        ///  Retorna a lista de Categorias
        /// </summary>
        /// <returns>Lista de obj</returns>
        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Get(){
            // ToListAsync (Select * from Categoria)
            var Categorias = await _repositorio.Listar();
            
            if(Categorias == null) {
                return NotFound();
            }
            return Categorias;
        }

        // GET: api/Categoria2
        /// <summary>
        /// retorna o objeto pesquisado pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto especifico</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> Get(int id){

            // FindAsync (Select * from Categoria where id = id)
            var Categoria = await _repositorio.BuscarPorID(id);
            
            if(Categoria == null) {
                return NotFound();
            }
            return Categoria;
        }

        // Post api/categoria
        /// <summary>
        /// Cria uma Categoria
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns>Categoria especifica pelo id</returns>
        [HttpPost]
        public async Task<ActionResult<Categoria>> Post(Categoria categoria) {

            try {   
                await _repositorio.Salvar(categoria);
            }
            catch(DbUpdateConcurrencyException) {
                throw;
            }
            return categoria;
        }

        // Put api/categoria
        /// <summary>
        /// Atualiza objeto pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoria"></param>
        /// <returns>Categoria atualizada</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Categoria categoria){

            if (id != categoria.CategoriaId) {
                return BadRequest();
            }

            try {
                await _repositorio.Alterar(categoria);
            }
            catch (DbUpdateConcurrencyException) {
                
                var categoria_valido = await _repositorio.BuscarPorID(id);

                // Verificamos se o objeto inserido realmente existe no banco
                if(categoria_valido == null) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
        // NoContent() = Retorna 204, sem nada.
        return NoContent();
        }

        // Delet api/categoria
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> Delete(int id) {

            var  categoria = await _repositorio.BuscarPorID(id);
            if(categoria == null) {
                return NotFound();
            }

            await _repositorio.Excluir(categoria);

            return categoria;
        }
    }
}