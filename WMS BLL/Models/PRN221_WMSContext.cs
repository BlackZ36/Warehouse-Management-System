using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace WMS_BLL.Models
{
    public partial class PRN221_WMSContext : DbContext
    {
        public PRN221_WMSContext()
        {
        }

        public PRN221_WMSContext(DbContextOptions<PRN221_WMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<History> Histories { get; set; } = null!;
        public virtual DbSet<Lot> Lots { get; set; } = null!;
        public virtual DbSet<LotDetail> LotDetails { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Partner> Partners { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<StockOut> StockOuts { get; set; } = null!;
        public virtual DbSet<StockOutDetail> StockOutDetails { get; set; } = null!;
        public virtual DbSet<Storage> Storages { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountCode).HasMaxLength(20);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'account@example.com')");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .HasDefaultValueSql("(N'+84 354510912')");

                entity.Property(e => e.Role).HasDefaultValueSql("((1))");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.Username).HasMaxLength(100);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryCode).HasMaxLength(20);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("(N'Mô tả chi tiết hoặc khái quát của danh mục hàng hóa')");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.ToTable("History");

                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'Đã thay đổi/ cập nhật/ thêm/ sửa/ xóa/ ...')");

                entity.Property(e => e.HistoryType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'Không xác định')");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Histories)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__History__Account__7B5B524B");

                entity.HasOne(d => d.Lot)
                    .WithMany(p => p.Histories)
                    .HasForeignKey(d => d.LotId)
                    .HasConstraintName("FK__History__LotId__7A672E12");

                entity.HasOne(d => d.Stockout)
                    .WithMany(p => p.Histories)
                    .HasForeignKey(d => d.StockoutId)
                    .HasConstraintName("FK__History__Stockou__797309D9");
            });

            modelBuilder.Entity<Lot>(entity =>
            {
                entity.ToTable("Lot");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LotCode).HasMaxLength(20);

                entity.Property(e => e.Note)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PartnerId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Lots)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lot__AccountId__5AEE82B9");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.Lots)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lot__PartnerId__59FA5E80");
            });

            modelBuilder.Entity<LotDetail>(entity =>
            {
                entity.ToTable("LotDetail");

                entity.Property(e => e.PackingType)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'Đơn vị')");

                entity.Property(e => e.PartnerId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((0))");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Lot)
                    .WithMany(p => p.LotDetails)
                    .HasForeignKey(d => d.LotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LotDetail__LotId__619B8048");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.LotDetails)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LotDetail__Partn__6383C8BA");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.LotDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LotDetail__Produ__6477ECF3");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Message)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'Nguyễn Văn A Đã Yêu Cầu Nhập Hàng')");

                entity.Property(e => e.Slug).HasDefaultValueSql("(N'/manage/product')");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Partner>(entity =>
            {
                entity.ToTable("Partner");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'partner@example.com')");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.PartnerCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .HasDefaultValueSql("(N'+84 354510912')");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.ProductCode).HasMaxLength(20);

                entity.Property(e => e.Quantity).HasDefaultValueSql("((0))");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.Unit)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'Đơn vị')");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Categor__4BAC3F29");

                entity.HasOne(d => d.Storage)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.StorageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Storage__4CA06362");
            });

            modelBuilder.Entity<StockOut>(entity =>
            {
                entity.ToTable("StockOut");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Note)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.StockOutCode).HasMaxLength(20);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.StockOuts)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StockOut__Accoun__6A30C649");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.StockOuts)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StockOut__Partne__6B24EA82");
            });

            modelBuilder.Entity<StockOutDetail>(entity =>
            {
                entity.ToTable("StockOutDetail");

                entity.Property(e => e.PackingType)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'Đơn vị')");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((0))");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.StockOutDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StockOutD__Produ__72C60C4A");

                entity.HasOne(d => d.StockOut)
                    .WithMany(p => p.StockOutDetails)
                    .HasForeignKey(d => d.StockOutId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StockOutD__Stock__71D1E811");
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.ToTable("Storage");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("(N'A5 An Bình City - Phạm Văn Đồng - Quận Bắc Từ Liêm')");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentCapacity).HasDefaultValueSql("((0))");

                entity.Property(e => e.MaxCapacity).HasDefaultValueSql("((1000))");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Province)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'Hà Nội')");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.StorageCode).HasMaxLength(20);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
