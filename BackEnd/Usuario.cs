using System;
using System.Collections.Generic;

namespace BackEnd
{
    public partial class Usuario
    {
        public Usuario()
        {
            Presenca = new HashSet<Presenca>();
        }

        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int? TipoUsuarioId { get; set; }

        public virtual TipoUsuario TipoUsuario { get; set; }
        public virtual ICollection<Presenca> Presenca { get; set; }
    }
}
