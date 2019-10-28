using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class CategoriaRepository : ICategoria
    {
        public async Task<Categoria> Alterar(Categoria categoria)
        {
            using(GufosContext _contexto = new GufosContext()) {
                // Comparamos os atributos que foram modificados atraves do EntityFrameWork
                _contexto.Entry(categoria).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
                return categoria;
            }
        }

        public async Task<Categoria> BuscarPorID(int id)
        {
            using(GufosContext _contexto = new GufosContext()) {
                return await _contexto.Categoria.FindAsync(id);
            }
        }

        public async Task<Categoria> Excluir(Categoria categoria)
        {
            using(GufosContext _contexto = new GufosContext()) {
                _contexto.Categoria.Remove(categoria);
                await _contexto.SaveChangesAsync();
                return categoria;
            }
        }

        public async Task<List<Categoria>> Listar()
        {
            using(GufosContext _contexto = new GufosContext()) {
                return await _contexto.Categoria.ToListAsync();
            }
        }

        public async Task<Categoria> Salvar(Categoria categoria)
        {
            using(GufosContext _contexto = new GufosContext()) {
                // Tratamos contra ataques de SQL INJECTION
                await _contexto.AddAsync(categoria);
                // Salvamos efetivamento o nosso objeto
                await _contexto.SaveChangesAsync();
                return categoria;
            }
        }
    }
}