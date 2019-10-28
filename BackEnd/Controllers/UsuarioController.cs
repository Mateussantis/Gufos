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
    public class UsuarioController : ControllerBase
    {
        
        // GufosContext _contexto = new GufosContext();
        UsuarioRepository _repositorio = new UsuarioRepository();

        // GET: api/Usuario
        /// <summary>
        ///  Retorna a lista de Usuarios
        /// </summary>
        /// <returns>Lista de obj</returns>
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get(){
            // ToListAsync (Select * from Usuario)
            var Usuarios = await _repositorio.Listar();
            
            if(Usuarios == null) {
                return NotFound();
            }
            return Usuarios;
        }

        // GET: api/Usuario2
        /// <summary>
        /// retorna o objeto pesquisado pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto especifico</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id){

            // FindAsync (Select * from Usuario where id = id)
            var Usuario = await _repositorio.BuscarPorID(id);
            
            if(Usuario == null) {
                return NotFound();
            }
            return Usuario;
        }

        // Post api/usuario
        /// <summary>
        /// Cria uma Usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>Usuario especifica pelo id</returns>
        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(Usuario usuario) {

            try {   
                await _repositorio.Salvar(usuario);
            }
            catch(DbUpdateConcurrencyException) {
                throw;
            }
            return usuario;
        }

        // Put api/usuario
        /// <summary>
        /// Atualiza objeto pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usuario"></param>
        /// <returns>Usuario atualizada</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Usuario usuario){

            if (id != usuario.UsuarioId) {
                return BadRequest();
            }

            try {
                await _repositorio.Alterar(usuario);
            }
            catch (DbUpdateConcurrencyException) {
                
                var usuario_valido = await _repositorio.BuscarPorID(id);

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

            var  usuario = await _repositorio.BuscarPorID(id);
            if(usuario == null) {
                return NotFound();
            }

            await _repositorio.Excluir(usuario);

            return usuario;
        }
    }
}