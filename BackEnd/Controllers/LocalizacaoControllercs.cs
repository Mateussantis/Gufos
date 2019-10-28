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
    public class LocalizacaoController : ControllerBase
    {
        
        // GufosContext _contexto = new GufosContext();
        LocalizacaoRepository _repositorio = new LocalizacaoRepository();

        // GET: api/Localizacao
        /// <summary>
        ///  Retorna a lista de Localizacaos
        /// </summary>
        /// <returns>Lista de obj</returns>
        [HttpGet]
        public async Task<ActionResult<List<Localizacao>>> Get(){
            // ToListAsync (Select * from Localizacao)
            var Localizacaos = await _repositorio.Listar();
            
            if(Localizacaos == null) {
                return NotFound();
            }
            return Localizacaos;
        }

        // GET: api/Localizacao2
        /// <summary>
        /// retorna o objeto pesquisado pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto especifico</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Localizacao>> Get(int id){

            // FindAsync (Select * from Localizacao where id = id)
            var Localizacao = await _repositorio.BuscarPorID(id);
            
            if(Localizacao == null) {
                return NotFound();
            }
            return Localizacao;
        }

        // Post api/localizacao
        /// <summary>
        /// Cria uma Localizacao
        /// </summary>
        /// <param name="localizacao"></param>
        /// <returns>Localizacao especifica pelo id</returns>
        [HttpPost]
        public async Task<ActionResult<Localizacao>> Post(Localizacao localizacao) {

            try {   
                await _repositorio.Salvar(localizacao);
            }
            catch(DbUpdateConcurrencyException) {
                throw;
            }
            return localizacao;
        }

        // Put api/localizacao
        /// <summary>
        /// Atualiza objeto pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="localizacao"></param>
        /// <returns>Localizacao atualizada</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Localizacao localizacao){

            if (id != localizacao.LocalizacaoId) {
                return BadRequest();
            }

            try {
                await _repositorio.Alterar(localizacao);
            }
            catch (DbUpdateConcurrencyException) {
                
                var localizacao_valido = await _repositorio.BuscarPorID(id);

                // Verificamos se o objeto inserido realmente existe no banco
                if(localizacao_valido == null) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
        // NoContent() = Retorna 204, sem nada.
        return NoContent();
        }

        // Delet api/localizacao
        [HttpDelete("{id}")]
        public async Task<ActionResult<Localizacao>> Delete(int id) {

            var  localizacao = await _repositorio.BuscarPorID(id);
            if(localizacao == null) {
                return NotFound();
            }

            await _repositorio.Excluir(localizacao);

            return localizacao;
        }
    }
}