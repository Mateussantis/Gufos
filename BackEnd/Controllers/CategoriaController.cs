using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    // Definimos nossa rota do controler e dizemos que é um controller de API
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        
        GufosContext _contexto = new GufosContext();

        // GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Get(){
            // ToListAsync (Select * from Categoria)
            var Categorias = await _contexto.Categoria.ToListAsync();
            
            if(Categorias == null) {
                return NotFound();
            }
            return Categorias;
        }

        // GET: api/Categoria2
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> Get(int id){

            // FindAsync (Select * from Categoria where id = id)
            var Categoria = await _contexto.Categoria.FindAsync(id);
            
            if(Categoria == null) {
                return NotFound();
            }
            return Categoria;
        }

        // Post api/categoria
        [HttpPost]
        public async Task<ActionResult<Categoria>> Post(Categoria categoria) {

            try {   
                // Tratamos contra ataques de SQL INJECTION
                await _contexto.AddAsync(categoria);
                // Salvamos efetivamento o nosso objeto
                await _contexto.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) {
                throw;
            }
            return categoria;
        }

        // Put api/categoria
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Categoria categoria){

            if (id != categoria.CategoriaId) {
                return BadRequest();
            }

            // Comparamos os atributos que foram modificados atraves do EntityFrameWork
            _contexto.Entry(categoria).State = EntityState.Modified;

            try {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                
                var categoria_valido = await _contexto.Categoria.FindAsync(id);

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

            var  categoria = await _contexto.Categoria.FindAsync(id);
            if(categoria == null) {
                return NotFound();
            }

            _contexto.Categoria.Remove(categoria);
            await _contexto.SaveChangesAsync();
            return categoria;
        }
    }
}