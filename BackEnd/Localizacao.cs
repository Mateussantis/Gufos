using System;
using System.Collections.Generic;

namespace BackEnd
{
    public partial class Localizacao
    {
        public Localizacao()
        {
            Evento = new HashSet<Evento>();
        }

        public int LocalizacaoId { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string Endereco { get; set; }

        public virtual ICollection<Evento> Evento { get; set; }
    }
}
