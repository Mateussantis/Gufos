using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class UsuarioRepository : IUsuario
    {
        public async Task<Usuario> Alterar(Usuario usuario)
        {
            using(GufosContext _contexto = new GufosContext()) {
                // Comparamos os atributos que foram modificados atraves do EntityFrameWork
                _contexto.Entry(usuario).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
                return usuario;
            }
        }

        public async Task<Usuario> BuscarPorID(int id)
        {
            using(GufosContext _contexto = new GufosContext()) {
                return await _contexto.Usuario.Include(t => t.TipoUsuario).FirstOrDefaultAsync(x => x.UsuarioId == id);
            }
        }

        public async Task<Usuario> Excluir(Usuario usuario)
        {
            using(GufosContext _contexto = new GufosContext()) {
                _contexto.Usuario.Remove(usuario);
                await _contexto.SaveChangesAsync();
                return usuario;
            }
        }

        public async Task<List<Usuario>> Listar()
        {
            using(GufosContext _contexto = new GufosContext()) {
                return await _contexto.Usuario.Include(t => t.TipoUsuario).ToListAsync();
            }
        }

        public async Task<Usuario> Salvar(Usuario usuario)
        {
            using(GufosContext _contexto = new GufosContext()) {
                // Tratamos contra ataques de SQL INJECTION
                await _contexto.AddAsync(usuario);
                // Salvamos efetivamento o nosso objeto
                await _contexto.SaveChangesAsync();
                return usuario;
            }
        }
    }
}