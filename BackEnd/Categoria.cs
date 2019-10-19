using System;
using System.Collections.Generic;

namespace BackEnd
{
    public partial class Categoria
    {
        public Categoria()
        {
            Evento = new HashSet<Evento>();
        }

        public int CategoriaId { get; set; }
        public string Titulo { get; set; }

        public virtual ICollection<Evento> Evento { get; set; }
    }
}
