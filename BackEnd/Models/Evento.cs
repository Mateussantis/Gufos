using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public partial class Evento
    {
        public Evento()
        {
            Presenca = new HashSet<Presenca>();
        }

        [Key]
        [Column("evento_id")]
        public int EventoId { get; set; }
        [Required]
        [Column("titulo")]
        [StringLength(255)]
        public string Titulo { get; set; }
        [Column("categoria_id")]
        public int? CategoriaId { get; set; }
        [Required]
        [Column("acesso_livro")]
        public bool? AcessoLivro { get; set; }
        [Column("data_evento", TypeName = "datetime")]
        public DateTime DataEvento { get; set; }
        [Column("localizacao_id")]
        public int? LocalizacaoId { get; set; }

        [ForeignKey(nameof(CategoriaId))]
        [InverseProperty("Evento")]
        public virtual Categoria Categoria { get; set; }
        [ForeignKey(nameof(LocalizacaoId))]
        [InverseProperty("Evento")]
        public virtual Localizacao Localizacao { get; set; }
        [InverseProperty("Evento")]
        public virtual ICollection<Presenca> Presenca { get; set; }
    }
}
