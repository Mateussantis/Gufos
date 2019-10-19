using System;
using System.Collections.Generic;

namespace BackEnd
{
    public partial class TipoUsuario
    {
        public TipoUsuario()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int TipoUsuarioId { get; set; }
        public string Titulo { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
