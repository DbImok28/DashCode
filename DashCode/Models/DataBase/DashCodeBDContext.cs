using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DashCode.Models.DataBase
{
    public partial class DashCodeBDContext : DbContext
    {//Scaffold-DbContext "Server=DESKTOP-2EFHGBU; DataBase=DashCodeBD; Trusted_Connection=True" Microsoft.EntityFrameworkCore.SqlServer
        public DashCodeBDContext()
        {
        }

        public DashCodeBDContext(DbContextOptions<DashCodeBDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChatMessages> ChatMessages { get; set; }
        public virtual DbSet<ChatUser> ChatUser { get; set; }
        public virtual DbSet<Chats> Chats { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //                optionsBuilder.UseSqlServer("Server=DESKTOP-2EFHGBU; DataBase=DashCodeBD; Trusted_Connection=True");
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatMessages>(entity =>
            {
                entity.ToTable("CHAT_MESSAGES");

                entity.Property(e => e.ChatMessagesId).HasColumnName("CHAT_MESSAGES_ID");

                entity.Property(e => e.ChatId).HasColumnName("CHAT_ID");

                entity.Property(e => e.MessageText)
                    .IsRequired()
                    .HasColumnName("MESSAGE_TEXT")
                    .HasMaxLength(128);

                entity.Property(e => e.SendDate)
                    .HasColumnName("SEND_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.Chat)
                    .WithMany(p => p.ChatMessages)
                    .HasForeignKey(d => d.ChatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CHAT_MESSAGES_CHAT_ID_FK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ChatMessages)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CHAT_MESSAGES_USER_ID_FK");
            });

            modelBuilder.Entity<ChatUser>(entity =>
            {
                entity.HasKey(e => new { e.ChatId, e.UserId })
                    .HasName("CHAT_USER_PK");

                entity.ToTable("CHAT_USER");

                entity.Property(e => e.ChatId).HasColumnName("CHAT_ID");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.Chat)
                    .WithMany(p => p.ChatUser)
                    .HasForeignKey(d => d.ChatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CHAT_USER_USER_FK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ChatUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CHAT_USER_CHAT_FK");
            });

            modelBuilder.Entity<Chats>(entity =>
            {
                entity.HasKey(e => e.ChatId)
                    .HasName("CHAT_ID_PK");

                entity.ToTable("CHATS");

                entity.Property(e => e.ChatId).HasColumnName("CHAT_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("USER_ID_PK");

                entity.ToTable("USERS");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("LOGIN")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnName("PASSWORD_HASH")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Photo).HasColumnName("PHOTO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
