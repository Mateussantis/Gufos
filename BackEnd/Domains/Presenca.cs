using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Domains
{
    public partial class Presenca
    {
        [Key]
        [Column("presenca_id")]
        public int PresencaId { get; set; }
        [Column("evento_id")]
        public int? EventoId { get; set; }
        [Column("usuario_id")]
        public int? UsuarioId { get; set; }
        [Required]
        [Column("presenca_status")]
        [StringLength(255)]
        public string PresencaStatus { get; set; }

        [ForeignKey(nameof(EventoId))]
        [InverseProperty("Presenca")]
        public virtual Evento Evento { get; set; }
        [ForeignKey(nameof(UsuarioId))]
        [InverseProperty("Presenca")]
        public virtual Usuario Usuario { get; set; }
    }
}
