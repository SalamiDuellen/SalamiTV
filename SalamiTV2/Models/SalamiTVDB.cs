namespace SalamiTV2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SalamiTVDB : DbContext
    {
        public SalamiTVDB()
            : base("name=SalamiTVEntities")
        {
        }

        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<tvchannel> tvchannels { get; set; }
        public virtual DbSet<tvchannelprogram> tvchannelprograms { get; set; }
        public virtual DbSet<tvprogram> tvprograms { get; set; }
        public virtual DbSet<tvprogramcategory> tvprogramcategories { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<userrole> userroles { get; set; }
        public virtual DbSet<usertablau> usertablaus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<category>()
                .Property(e => e.genre)
                .IsUnicode(false);

            modelBuilder.Entity<category>()
                .HasMany(e => e.tvprogramcategories)
                .WithRequired(e => e.category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<role>()
                .Property(e => e.admin)
                .IsUnicode(false);

            modelBuilder.Entity<role>()
                .Property(e => e.customer)
                .IsUnicode(false);

            modelBuilder.Entity<role>()
                .HasMany(e => e.userroles)
                .WithRequired(e => e.role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tvchannel>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<tvchannel>()
                .HasMany(e => e.tvchannelprograms)
                .WithRequired(e => e.tvchannel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tvchannel>()
                .HasMany(e => e.usertablaus)
                .WithRequired(e => e.tvchannel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tvprogram>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<tvprogram>()
                .Property(e => e.details)
                .IsUnicode(false);

            modelBuilder.Entity<tvprogram>()
                .HasMany(e => e.tvchannelprograms)
                .WithRequired(e => e.tvprogram)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tvprogram>()
                .HasMany(e => e.tvprogramcategories)
                .WithRequired(e => e.tvprogram)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.userroles)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.usertablaus)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);
        }
    }
}
