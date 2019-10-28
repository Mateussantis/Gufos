using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class PresencaRepository : IPresenca
    {
        public async Task<Presenca> Alterar(Presenca presenca)
        {
            using(GufosContext _contexto = new GufosContext()) {
                // Comparamos os atributos que foram modificados atraves do EntityFrameWork
                _contexto.Entry(presenca).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
                return presenca;
            }
        }

        public async Task<Presenca> BuscarPorID(int id)
        {
            using(GufosContext _contexto = new GufosContext()) {
                return await _contexto.Presenca.Include(c => c.Evento).Include(c => c.Usuario).FirstOrDefaultAsync(c => c.PresencaId == id);
            }
        }

        public async Task<Presenca> Excluir(Presenca presenca)
        {
            using(GufosContext _contexto = new GufosContext()) {
                _contexto.Presenca.Remove(presenca);
                await _contexto.SaveChangesAsync();
                return presenca;
            }
        }

        public async Task<List<Presenca>> Listar()
        {
            using(GufosContext _contexto = new GufosContext()) {
                return await _contexto.Presenca.Include(c => c.Evento).Include(c => c.Usuario).ToListAsync();
            }
        }

        public async Task<Presenca> Salvar(Presenca presenca)
        {
            using(GufosContext _contexto = new GufosContext()) {
                // Tratamos contra ataques de SQL INJECTION
                await _contexto.AddAsync(presenca);
                // Salvamos efetivamento o nosso objeto
                await _contexto.SaveChangesAsync();
                return presenca;
            }
        }
    }
}