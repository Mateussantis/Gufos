using System;
using System.Collections.Generic;

namespace BackEnd
{
    public partial class Presenca
    {
        public int PresencaId { get; set; }
        public int? EventoId { get; set; }
        public int? UsuarioId { get; set; }
        public string PresencaStatus { get; set; }

        public virtual Evento Evento { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
