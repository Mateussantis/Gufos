using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class EventoRepository : IEvento
    {
        public async Task<Evento> Alterar(Evento evento)
        {
            using(GufosContext _contexto = new GufosContext()) {
                // Comparamos os atributos que foram modificados atraves do EntityFrameWork
                _contexto.Entry(evento).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
                return evento;
            }
        }

        public async Task<Evento> BuscarPorID(int id)
        {
            using(GufosContext _contexto = new GufosContext()) {
                return await _contexto.Evento.Include(c => c.Categoria).Include(c => c.Localizacao).FirstOrDefaultAsync(c => c.EventoId == id);
            }
        }

        public async Task<Evento> Excluir(Evento evento)
        {
            using(GufosContext _contexto = new GufosContext()) {
                _contexto.Evento.Remove(evento);
                await _contexto.SaveChangesAsync();
                return evento;
            }
        }

        public async Task<List<Evento>> Listar()
        {
            using(GufosContext _contexto = new GufosContext()) {
                return await _contexto.Evento.Include(c => c.Categoria).Include(c => c.Localizacao).ToListAsync();
            }
        }

        public async Task<Evento> Salvar(Evento evento)
        {
            using(GufosContext _contexto = new GufosContext()) {
                // Tratamos contra ataques de SQL INJECTION
                await _contexto.AddAsync(evento);
                // Salvamos efetivamento o nosso objeto
                await _contexto.SaveChangesAsync();
                return evento;
            }
        }
    }
}