namespace SalamiTV2.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using SalamiTV2.Models;

    public partial class SalamiDB : DbContext
    {
        public SalamiDB()
            : base("name=SalamiDB")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<TvChannel> TvChannels { get; set; }
        public virtual DbSet<TvChannelProgram> TvChannelPrograms { get; set; }
        public virtual DbSet<TvProgram> TvPrograms { get; set; }
        public virtual DbSet<TvProgramCategory> TvProgramCategories { get; set; }
        public virtual DbSet<UserInfo> UserInfoes { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserTablau> UserTablaus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.Genre)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.TvProgramCategories)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TvChannel>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<TvChannel>()
                .HasMany(e => e.TvChannelPrograms)
                .WithRequired(e => e.TvChannel)
                .HasForeignKey(e => e.ProgramID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TvChannel>()
                .HasMany(e => e.UserTablaus)
                .WithRequired(e => e.TvChannel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TvProgram>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<TvProgram>()
                .Property(e => e.Details)
                .IsUnicode(false);

            modelBuilder.Entity<TvProgram>()
                .HasMany(e => e.TvChannelPrograms)
                .WithRequired(e => e.TvProgram)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TvProgram>()
                .HasMany(e => e.TvProgramCategories)
                .WithRequired(e => e.TvProgram)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.UserInfo)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInfo>()
                .HasMany(e => e.UserTablaus)
                .WithRequired(e => e.UserInfo)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);
        }
    }
}