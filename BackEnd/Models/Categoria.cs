using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Evento = new HashSet<Evento>();
        }

        [Key]
        [Column("categoria_id")]
        public int CategoriaId { get; set; }
        [Required]
        [Column("titulo")]
        [StringLength(255)]
        public string Titulo { get; set; }

        [InverseProperty("Categoria")]
        public virtual ICollection<Evento> Evento { get; set; }
    }
}
