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
    public class LocalizacaoController : ControllerBase
    {
        
        GufosContext _contexto = new GufosContext();

        // GET: api/Localizacao
        [HttpGet]
        public async Task<ActionResult<List<Localizacao>>> Get(){
            // ToListAsync (Select * from Localizacao)
            var Localizacoes = await _contexto.Localizacao.ToListAsync();
            
            if(Localizacoes == null) {
                return NotFound();
            }
            return Localizacoes;
        }

        // GET: api/Localizacao2
        [HttpGet("{id}")]
        public async Task<ActionResult<Localizacao>> Get(int id){

            // FindAsync (Select * from Localizacao where id = id)
            var Localizacao = await _contexto.Localizacao.FindAsync(id);
            
            if(Localizacao == null) {
                return NotFound();
            }
            return Localizacao;
        }

        // Post api/localizacao
        [HttpPost]
        public async Task<ActionResult<Localizacao>> Post(Localizacao localizacao) {

            try {   
                // Tratamos contra ataques de SQL INJECTION
                await _contexto.AddAsync(localizacao);
                // Salvamos efetivamento o nosso objeto
                await _contexto.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) {
                throw;
            }
            return localizacao;
        }

        // Put api/localizacao
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Localizacao localizacao){

            if (id != localizacao.LocalizacaoId) {
                return BadRequest();
            }

            // Comparamos os atributos que foram modificados atraves do EntityFrameWork
            _contexto.Entry(localizacao).State = EntityState.Modified;

            try {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                
                var localizacao_valido = await _contexto.Localizacao.FindAsync(id);

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

            var  localizacao = await _contexto.Localizacao.FindAsync(id);
            if(localizacao == null) {
                return NotFound();
            }

            _contexto.Localizacao.Remove(localizacao);
            await _contexto.SaveChangesAsync();
            return localizacao;
        }
    }
}