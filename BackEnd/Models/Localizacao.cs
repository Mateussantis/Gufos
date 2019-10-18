using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public partial class Localizacao
    {
        public Localizacao()
        {
            Evento = new HashSet<Evento>();
        }

        [Key]
        [Column("localizacao_id")]
        public int LocalizacaoId { get; set; }
        [Required]
        [Column("cnpj")]
        [StringLength(14)]
        public string Cnpj { get; set; }
        [Required]
        [Column("razao_social")]
        [StringLength(255)]
        public string RazaoSocial { get; set; }
        [Required]
        [Column("endereco")]
        [StringLength(255)]
        public string Endereco { get; set; }

        [InverseProperty("Localizacao")]
        public virtual ICollection<Evento> Evento { get; set; }
    }
}
