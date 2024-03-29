﻿using System;
using System.Collections.Generic;

namespace BackEnd
{
    public partial class Evento
    {
        public Evento()
        {
            Presenca = new HashSet<Presenca>();
        }

        public int EventoId { get; set; }
        public string Titulo { get; set; }
        public int? CategoriaId { get; set; }
        public bool? AcessoLivro { get; set; }
        public DateTime DataEvento { get; set; }
        public int? LocalizacaoId { get; set; }

        public virtual Categoria Categoria { get; set; }
        public virtual Localizacao Localizacao { get; set; }
        public virtual ICollection<Presenca> Presenca { get; set; }
    }
}
