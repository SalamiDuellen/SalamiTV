namespace SalamiTV
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyContext : DbContext
    {
        public MyContext()
            : base("MyContext")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<TvChannel> TvChannels { get; set; }
        public virtual DbSet<TvProgram> TvPrograms { get; set; }
        public virtual DbSet<TvProgramCategory> TvProgramCategories { get; set; }
        public virtual DbSet<UserTablau> UserTablaus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.UserTablaus)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.AspNetUsersID);

            modelBuilder.Entity<Category>()
                .Property(e => e.Genre)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.TvProgramCategories)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TvChannel>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<TvChannel>()
                .HasMany(e => e.TvPrograms)
                .WithRequired(e => e.TvChannel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TvProgram>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<TvProgram>()
                .Property(e => e.Details)
                .IsUnicode(false);

            modelBuilder.Entity<TvProgram>()
                .HasMany(e => e.TvProgramCategories)
                .WithRequired(e => e.TvProgram)
                .WillCascadeOnDelete(false);
        }
    }
}
