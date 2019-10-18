using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Presenca = new HashSet<Presenca>();
        }

        [Key]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }
        [Required]
        [Column("nome")]
        [StringLength(255)]
        public string Nome { get; set; }
        [Required]
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [Column("senha")]
        [StringLength(255)]
        public string Senha { get; set; }
        [Column("tipo_usuario_id")]
        public int? TipoUsuarioId { get; set; }

        [ForeignKey(nameof(TipoUsuarioId))]
        [InverseProperty("Usuario")]
        public virtual TipoUsuario TipoUsuario { get; set; }
        [InverseProperty("Usuario")]
        public virtual ICollection<Presenca> Presenca { get; set; }
    }
}
