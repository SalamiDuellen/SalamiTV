namespace SalamiTV.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SalamiTVDB : DbContext
    {
        public SalamiTVDB()
            : base("SalamiTVDB")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        //public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<TvChannel> TvChannels { get; set; }
        public virtual DbSet<TvProgram> TvPrograms { get; set; }
        public virtual DbSet<TvProgramCategory> TvProgramCategories { get; set; }
        public virtual DbSet<UserInfo> UserInfoes { get; set; }
        //public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserTablau> UserTablaus { get; set; } // ska det ändras till bara userid och tvid? 


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.Genre)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.TvProgramCategories)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Role>()
            //    .HasMany(e => e.UserRoles)
            //    .WithRequired(e => e.Role)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<TvChannel>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<TvChannel>()
                .HasMany(e => e.TvPrograms)
                .WithRequired(e => e.TvChannel)
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
                .Property(e => e.Broadcasting).HasColumnType("datetime");

            modelBuilder.Entity<TvProgram>()
                .Property(e => e.EndTime).HasColumnType("datetime");

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

            //modelBuilder.Entity<UserInfo>()
            //    .HasMany(e => e.UserRoles)
            //    .WithRequired(e => e.UserInfo)
            //    .HasForeignKey(e => e.AspNetUsersId)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInfo>()
                .HasMany(e => e.UserTablaus)
                .WithRequired(e => e.UserInfo)
                .HasForeignKey(e => e.AspNetUsersId)
                .WillCascadeOnDelete(false);
        }
    }
}
