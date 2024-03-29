using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Interfaces
{
    public interface ITipoUsuario
    {
        Task<List<TipoUsuario>> Listar();
        Task<TipoUsuario> BuscarPorID(int id);
        Task<TipoUsuario> Salvar(TipoUsuario tipousuario);
        Task<TipoUsuario> Alterar(TipoUsuario tipousuario);
        Task<TipoUsuario> Excluir(TipoUsuario tipousuario);
    }
}