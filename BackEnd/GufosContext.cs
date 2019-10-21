using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BackEnd
{
    public partial class GufosContext : DbContext
    {
        public GufosContext()
        {
        }

        public GufosContext(DbContextOptions<GufosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Evento> Evento { get; set; }
        public virtual DbSet<Localizacao> Localizacao { get; set; }
        public virtual DbSet<Presenca> Presenca { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=N-1S-DEV-07\\SQLEXPRESS; Database=Gufos; User=sa; Password=132");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasIndex(e => e.Titulo)
                    .HasName("UQ__Categori__38FA640FDDCA2DD3")
                    .IsUnique();

                entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("titulo")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasIndex(e => e.Titulo)
                    .HasName("UQ__Evento__38FA640F27135756")
                    .IsUnique();

                entity.Property(e => e.EventoId).HasColumnName("evento_id");

                entity.Property(e => e.AcessoLivro)
                    .IsRequired()
                    .HasColumnName("acesso_livro")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");

                entity.Property(e => e.DataEvento)
                    .HasColumnName("data_evento")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocalizacaoId).HasColumnName("localizacao_id");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("titulo")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Evento)
                    .HasForeignKey(d => d.CategoriaId)
                    .HasConstraintName("FK__Evento__categori__45F365D3");

                entity.HasOne(d => d.Localizacao)
                    .WithMany(p => p.Evento)
                    .HasForeignKey(d => d.LocalizacaoId)
                    .HasConstraintName("FK__Evento__localiza__47DBAE45");
            });

            modelBuilder.Entity<Localizacao>(entity =>
            {
                entity.HasIndex(e => e.Cnpj)
                    .HasName("UQ__Localiza__35BD3E48072C9463")
                    .IsUnique();

                entity.HasIndex(e => e.RazaoSocial)
                    .HasName("UQ__Localiza__C6DE26A3E32946A9")
                    .IsUnique();

                entity.Property(e => e.LocalizacaoId).HasColumnName("localizacao_id");

                entity.Property(e => e.Cnpj)
                    .IsRequired()
                    .HasColumnName("cnpj")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Endereco)
                    .IsRequired()
                    .HasColumnName("endereco")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RazaoSocial)
                    .IsRequired()
                    .HasColumnName("razao_social")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Presenca>(entity =>
            {
                entity.Property(e => e.PresencaId).HasColumnName("presenca_id");

                entity.Property(e => e.EventoId).HasColumnName("evento_id");

                entity.Property(e => e.PresencaStatus)
                    .IsRequired()
                    .HasColumnName("presenca_status")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

                entity.HasOne(d => d.Evento)
                    .WithMany(p => p.Presenca)
                    .HasForeignKey(d => d.EventoId)
                    .HasConstraintName("FK__Presenca__evento__4AB81AF0");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Presenca)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK__Presenca__usuari__4BAC3F29");
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.ToTable("Tipo_usuario");

                entity.HasIndex(e => e.Titulo)
                    .HasName("UQ__Tipo_usu__38FA640F6740DC3D")
                    .IsUnique();

                entity.Property(e => e.TipoUsuarioId).HasColumnName("tipo_usuario_id");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("titulo")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Usuario__AB6E61647AF608B2")
                    .IsUnique();

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasColumnName("senha")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TipoUsuarioId).HasColumnName("tipo_usuario_id");

                entity.HasOne(d => d.TipoUsuario)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.TipoUsuarioId)
                    .HasConstraintName("FK__Usuario__tipo_us__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
