using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace iWasHere.Domain.Models
{
    public partial class RobinContext : DbContext
    {
        public RobinContext()
        {
        }

        public RobinContext(DbContextOptions<RobinContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        ///public virtual DbSet<CurrencyConversion> CurrencyConversion { get; set; }
        public virtual DbSet<DictionaryAttractionType> DictionaryAttractionType { get; set; }
        public virtual DbSet<DictionaryAvailability> DictionaryAvailability { get; set; }
        public virtual DbSet<DictionaryCity> DictionaryCity { get; set; }
        public virtual DbSet<DictionaryCountry> DictionaryCountry { get; set; }
        public virtual DbSet<DictionaryCounty> DictionaryCounty { get; set; }
        public virtual DbSet<DictionaryCurrency> DictionaryCurrency { get; set; }
        public virtual DbSet<DictionaryLandmarkType> DictionaryLandmarkType { get; set; }
        public virtual DbSet<DictionaryTicketType> DictionaryTicketType { get; set; }
        public virtual DbSet<Landmark> Landmark { get; set; }
        public virtual DbSet<LandmarkImage> LandmarkImage { get; set; }
        public virtual DbSet<LandmarkRating> LandmarkRating { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=ts-internship-2019.database.windows.net;Initial Catalog=Robin;Persist Security Info=False;User ID=sa_admin;Password=A123456a;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            /*modelBuilder.Entity<CurrencyConversion>(entity =>
            {
                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.CurrencyConversion)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CurrencyC__Curre__2334397B");
            });*/

            modelBuilder.Entity<DictionaryAttractionType>(entity =>
            {
                entity.HasKey(e => e.AttractionTypeId)
                    .HasName("PK__Dictiona__02A3C177390B97B5");

                entity.HasIndex(e => e.AttractionCode)
                    .HasName("UQ__Dictiona__8295D9CC68923B3A")
                    .IsUnique();

                entity.Property(e => e.AttractionCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AttractionName).HasMaxLength(255);
            });

            modelBuilder.Entity<DictionaryAvailability>(entity =>
            {
                entity.HasKey(e => e.AvailabilityId)
                    .HasName("PK__Dictiona__DA3979B1D8C0C1DD");

                entity.Property(e => e.AvailabilityName).HasMaxLength(255);

                entity.Property(e => e.Schedule).HasColumnType("string");
            });

            modelBuilder.Entity<DictionaryCity>(entity =>
            {
                entity.HasKey(e => e.CityId)
                    .HasName("PK__Dictiona__F2D21B7612EBA97B");

                entity.Property(e => e.CityId).ValueGeneratedOnAdd();

                entity.Property(e => e.CityName).HasMaxLength(255);

                entity.HasOne(d => d.County)
                    .WithMany(p => p.DictionaryCity)
                    .HasForeignKey(d => d.CountyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Dictionar__Count__2DB1C7EE");
            });

            modelBuilder.Entity<DictionaryCountry>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("PK__Dictiona__10D1609FC54C32CC");

                entity.Property(e => e.CountryName).HasMaxLength(255);
            });

            modelBuilder.Entity<DictionaryCounty>(entity =>
            {
                entity.HasKey(e => e.CountyId)
                    .HasName("PK__Dictiona__B68F9D97263B04EC");

                entity.Property(e => e.CountyName).HasMaxLength(255);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.DictionaryCounty)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Dictionar__Count__251C81ED");
            });

            modelBuilder.Entity<DictionaryCurrency>(entity =>
            {
                entity.HasKey(e => e.CurrencyId)
                    .HasName("PK__Dictiona__14470AF06F5A89E9");

                entity.HasIndex(e => e.CurrencyCode)
                    .HasName("UQ__Dictiona__408426BF03BE9F15")
                    .IsUnique();

                entity.Property(e => e.CurrencyCode)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CurrencyName).HasMaxLength(255);
            });

            modelBuilder.Entity<DictionaryLandmarkType>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK__Dictiona__727E838BE45644B7");

                entity.HasIndex(e => e.ItemCode)
                    .HasName("UQ__Dictiona__3ECC0FEAB82F5ED8")
                    .IsUnique();

                entity.Property(e => e.ItemCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ItemName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DictionaryTicketType>(entity =>
            {
                entity.HasKey(e => e.TicketTypeId)
                    .HasName("PK__Dictiona__6CD68431257B8B72");

                entity.HasIndex(e => e.TicketCode)
                    .HasName("UQ__Dictiona__598CF7A360A3AA1F")
                    .IsUnique();

                entity.Property(e => e.TicketCode)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TicketName).HasMaxLength(255);
            });

            modelBuilder.Entity<Landmark>(entity =>
            {
                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");

                entity.HasOne(d => d.DictionaryAttractionType)
                    .WithMany(p => p.Landmark)
                    .HasForeignKey(d => d.DictionaryAttractionTypeId)
                    .HasConstraintName("FK__Landmark__Dictio__1D7B6025");

                entity.HasOne(d => d.DictionaryAvailability)
                    .WithMany(p => p.Landmark)
                    .HasForeignKey(d => d.DictionaryAvailabilityId)
                    .HasConstraintName("FK__Landmark__Dictio__1B9317B3");

                entity.HasOne(d => d.DictionaryCity)
                    .WithMany(p => p.Landmark)
                    .HasForeignKey(d => d.DictionaryCityId)
                    .HasConstraintName("FK__Landmark__Dictio__1F63A897");

                entity.HasOne(d => d.DictionaryItem)
                    .WithMany(p => p.Landmark)
                    .HasForeignKey(d => d.DictionaryItemId)
                    .HasConstraintName("FK__Landmark__Dictio__1C873BEC");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.Landmark)
                    .HasForeignKey(d => d.TicketId)
                    .HasConstraintName("FK__Landmark__Ticket__1A9EF37A");
            });

            modelBuilder.Entity<LandmarkImage>(entity =>
            {
                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasColumnName("ImageURL");

                entity.HasOne(d => d.Landmark)
                    .WithMany(p => p.LandmarkImage)
                    .HasForeignKey(d => d.LandmarkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LandmarkI__Landm__18B6AB08");
            });

            modelBuilder.Entity<LandmarkRating>(entity =>
            {
                entity.Property(e => e.ComentDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(900);

                entity.HasOne(d => d.Landmark)
                    .WithMany(p => p.LandmarkRating)
                    .HasForeignKey(d => d.LandmarkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LandmarkR__Landm__19AACF41");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.TicketId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.DictionaryCurrency)
                    .WithOne(p => p.Ticket)
                    .HasForeignKey<Ticket>(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__Currency__2EA5EC27");

                entity.HasOne(d => d.TicketType)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.TicketTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__TicketTy__214BF109");
            });
        }
    }
}
