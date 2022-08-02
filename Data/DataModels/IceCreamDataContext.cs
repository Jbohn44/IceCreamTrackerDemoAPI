using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Data.DataModels
{
    public partial class IceCreamDataContext : DbContext
    {

        public IceCreamDataContext(DbContextOptions<IceCreamDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<IceCream> IceCreams { get; set; }
        public virtual DbSet<IceCreamCategoryJunction> IceCreamCategoryJunctions { get; set; }
        public virtual DbSet<IceCreamImage> IceCreamImages { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<RatingType> RatingTypes { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceType> ServiceTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=IceCreamData;Trusted_Connection=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryName).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_User");
            });

            modelBuilder.Entity<IceCream>(entity =>
            {
                entity.Property(e => e.ReviewDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<IceCreamCategoryJunction>(entity =>
            {
                entity.HasKey(e => new { e.IceCreamId, e.CategoryId });

                entity.ToTable("IceCreamCategoryJunction");

                entity.Property(e => e.IceCreamCategoryId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.IceCreamCategoryJunctions)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IceCreamCategoryJunction_Category");

                entity.HasOne(d => d.IceCream)
                    .WithMany(p => p.IceCreamCategoryJunctions)
                    .HasForeignKey(d => d.IceCreamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IceCreamCategoryJunction_IceCreams");
            });

            modelBuilder.Entity<IceCreamImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK_Image");

                entity.ToTable("IceCreamImage");

                entity.Property(e => e.ImagePath).IsRequired();

                entity.HasOne(d => d.IceCream)
                    .WithMany(p => p.IceCreamImages)
                    .HasForeignKey(d => d.IceCreamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Image_IceCream");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Location");

                entity.Property(e => e.Lat).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.LocationId).ValueGeneratedOnAdd();

                entity.Property(e => e.Long).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasIndex(e => e.IceCreamId, "IX_Ratings_IceCreamId");

                entity.HasOne(d => d.IceCream)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.IceCreamId);

                entity.HasOne(d => d.RatingType)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.RatingTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ratings_RatingType");
            });

            modelBuilder.Entity<RatingType>(entity =>
            {
                entity.ToTable("RatingType");

                entity.Property(e => e.RatingName)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.HasOne(d => d.IceCream)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.IceCreamId)
                    .HasConstraintName("FK_IceCream_Service");

                entity.HasOne(d => d.ServiceType)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.ServiceTypeId)
                    .HasConstraintName("FK_Service_ServiceType");
            });

            modelBuilder.Entity<ServiceType>(entity =>
            {
                entity.ToTable("ServiceType");

                entity.Property(e => e.ServiceName).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Provider)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
