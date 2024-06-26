using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAPM_QLTBYT.Models;

public partial class DoAnPhanMemQltbytContext : DbContext
{
    public DoAnPhanMemQltbytContext()
    {
    }

    public DoAnPhanMemQltbytContext(DbContextOptions<DoAnPhanMemQltbytContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chitietphieuyeucaucungcap> Chitietphieuyeucaucungcaps { get; set; }

    public virtual DbSet<Chitietphieuyeucaunhap> Chitietphieuyeucaunhaps { get; set; }

    public virtual DbSet<Chucvu> Chucvus { get; set; }

    public virtual DbSet<Danhmucthietbi> Danhmucthietbis { get; set; }

    public virtual DbSet<Khoa> Khoas { get; set; }

    public virtual DbSet<Nguoidung> Nguoidungs { get; set; }

    public virtual DbSet<Phieuyeucaucungcap> Phieuyeucaucungcaps { get; set; }

    public virtual DbSet<Phieuyeucaunhap> Phieuyeucaunhaps { get; set; }

    public virtual DbSet<Thietbi> Thietbis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=PHUCDO\\MSSQLSERVER1;Initial Catalog=DoAnPhanMem_QLTBYT;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chitietphieuyeucaucungcap>(entity =>
        {
            entity.HasKey(e => new { e.MaPhieu, e.MaTb }).HasName("PK__CHITIETP__CE07973918ECBFE1");

            entity.ToTable("CHITIETPHIEUYEUCAUCUNGCAP");

            entity.Property(e => e.MaPhieu)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maPhieu");
            entity.Property(e => e.MaTb)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maTB");
            entity.Property(e => e.LyDo).HasMaxLength(255);
            entity.Property(e => e.SoLuongDuyet).HasColumnName("soLuongDuyet");
            entity.Property(e => e.SoLuongYeuCau).HasColumnName("soLuongYeuCau");

            entity.HasOne(d => d.MaPhieuNavigation).WithMany(p => p.Chitietphieuyeucaucungcaps)
                .HasForeignKey(d => d.MaPhieu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETPH__maPhi__5535A963");

            entity.HasOne(d => d.MaTbNavigation).WithMany(p => p.Chitietphieuyeucaucungcaps)
                .HasForeignKey(d => d.MaTb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETPHI__maTB__5629CD9C");
        });

        modelBuilder.Entity<Chitietphieuyeucaunhap>(entity =>
        {
            entity.HasKey(e => new { e.MaPhieu, e.MaTb }).HasName("PK__CHITIETP__CE079739296B13C8");

            entity.ToTable("CHITIETPHIEUYEUCAUNHAP");

            entity.Property(e => e.MaPhieu)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maPhieu");
            entity.Property(e => e.MaTb)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maTB");
            entity.Property(e => e.LyDo).HasMaxLength(255);
            entity.Property(e => e.SoLuongDuyet).HasColumnName("soLuongDuyet");
            entity.Property(e => e.SoLuongYeuCau).HasColumnName("soLuongYeuCau");

            entity.HasOne(d => d.MaPhieuNavigation).WithMany(p => p.Chitietphieuyeucaunhaps)
                .HasForeignKey(d => d.MaPhieu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETPH__maPhi__5165187F");

            entity.HasOne(d => d.MaTbNavigation).WithMany(p => p.Chitietphieuyeucaunhaps)
                .HasForeignKey(d => d.MaTb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETPHI__maTB__52593CB8");
        });

        modelBuilder.Entity<Chucvu>(entity =>
        {
            entity.HasKey(e => e.MaChucVu).HasName("PK__CHUCVU__6E42BCD982B3846A");

            entity.ToTable("CHUCVU");

            entity.Property(e => e.MaChucVu)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maChucVu");
            entity.Property(e => e.TenChucVu)
                .HasMaxLength(50)
                .HasColumnName("tenChucVu");
        });

        modelBuilder.Entity<Danhmucthietbi>(entity =>
        {
            entity.HasKey(e => e.MaDm).HasName("PK__DANHMUCT__7A3EF408B84DFEE9");

            entity.ToTable("DANHMUCTHIETBI");

            entity.Property(e => e.MaDm)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maDM");
            entity.Property(e => e.MoTa)
                .HasMaxLength(255)
                .HasColumnName("moTa");
            entity.Property(e => e.TenDm)
                .HasMaxLength(50)
                .HasColumnName("tenDM");
        });

        modelBuilder.Entity<Khoa>(entity =>
        {
            entity.HasKey(e => e.MaKhoa).HasName("PK__KHOA__C79B8C22B1CC661E");

            entity.ToTable("KHOA");

            entity.Property(e => e.MaKhoa)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maKhoa");
            entity.Property(e => e.MoTa)
                .HasMaxLength(255)
                .HasColumnName("moTa");
            entity.Property(e => e.TenKhoa)
                .HasMaxLength(50)
                .HasColumnName("tenKhoa");
        });

        modelBuilder.Entity<Nguoidung>(entity =>
        {
            entity.HasKey(e => e.MaNd).HasName("PK__NGUOIDUN__7A3EC7CBBF796535");

            entity.ToTable("NGUOIDUNG");

            entity.Property(e => e.MaNd)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maND");
            entity.Property(e => e.Cccd).HasColumnName("CCCD");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(255)
                .HasColumnName("diaChi");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.MaChucVu)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maChucVu");
            entity.Property(e => e.MaKhoa)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maKhoa");
            entity.Property(e => e.Matkhau)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("matkhau");
            entity.Property(e => e.NgaySinh).HasColumnName("ngaySinh");
            entity.Property(e => e.TenNd)
                .HasMaxLength(50)
                .HasColumnName("tenND");
            entity.Property(e => e.TrangThai).HasColumnName("trangThai");

            entity.HasOne(d => d.MaChucVuNavigation).WithMany(p => p.Nguoidungs)
                .HasForeignKey(d => d.MaChucVu)
                .HasConstraintName("FK__NGUOIDUNG__maChu__403A8C7D");

            entity.HasOne(d => d.MaKhoaNavigation).WithMany(p => p.Nguoidungs)
                .HasForeignKey(d => d.MaKhoa)
                .HasConstraintName("FK__NGUOIDUNG__maKho__412EB0B6");
        });

        modelBuilder.Entity<Phieuyeucaucungcap>(entity =>
        {
            entity.HasKey(e => e.MaPhieu).HasName("PK__PHIEUYEU__49A5B11F39BA3197");

            entity.ToTable("PHIEUYEUCAUCUNGCAP");

            entity.Property(e => e.MaPhieu)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maPhieu");
            entity.Property(e => e.MaKhoa)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maKhoa");
            entity.Property(e => e.MaNguoiDuyet)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maNguoiDuyet");
            entity.Property(e => e.MaNguoiTao)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maNguoiTao");
            entity.Property(e => e.MaNguoiXacNhan)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maNguoiXacNhan");
            entity.Property(e => e.NgayDuyet).HasColumnName("ngayDuyet");
            entity.Property(e => e.NgayTao).HasColumnName("ngayTao");
            entity.Property(e => e.NgayXacNhan).HasColumnName("ngayXacNhan");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasColumnName("trangThai");

            entity.HasOne(d => d.MaKhoaNavigation).WithMany(p => p.Phieuyeucaucungcaps)
                .HasForeignKey(d => d.MaKhoa)
                .HasConstraintName("FK__PHIEUYEUC__maKho__4AB81AF0");

            entity.HasOne(d => d.MaNguoiDuyetNavigation).WithMany(p => p.PhieuyeucaucungcapMaNguoiDuyetNavigations)
                .HasForeignKey(d => d.MaNguoiDuyet)
                .HasConstraintName("FK__PHIEUYEUC__maNgu__4CA06362");

            entity.HasOne(d => d.MaNguoiTaoNavigation).WithMany(p => p.PhieuyeucaucungcapMaNguoiTaoNavigations)
                .HasForeignKey(d => d.MaNguoiTao)
                .HasConstraintName("FK__PHIEUYEUC__maNgu__4BAC3F29");

            entity.HasOne(d => d.MaNguoiXacNhanNavigation).WithMany(p => p.PhieuyeucaucungcapMaNguoiXacNhanNavigations)
                .HasForeignKey(d => d.MaNguoiXacNhan)
                .HasConstraintName("FK__PHIEUYEUC__maNgu__4D94879B");
        });

        modelBuilder.Entity<Phieuyeucaunhap>(entity =>
        {
            entity.HasKey(e => e.MaPhieu).HasName("PK__PHIEUYEU__49A5B11F803C1840");

            entity.ToTable("PHIEUYEUCAUNHAP");

            entity.Property(e => e.MaPhieu)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maPhieu");
            entity.Property(e => e.MaKhoa)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maKhoa");
            entity.Property(e => e.MaNguoiDuyet)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maNguoiDuyet");
            entity.Property(e => e.MaNguoiTao)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maNguoiTao");
            entity.Property(e => e.MaNguoiXacNhan)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maNguoiXacNhan");
            entity.Property(e => e.NgayDuyet).HasColumnName("ngayDuyet");
            entity.Property(e => e.NgayTao).HasColumnName("ngayTao");
            entity.Property(e => e.NgayXacNhan).HasColumnName("ngayXacNhan");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasColumnName("trangThai");

            entity.HasOne(d => d.MaKhoaNavigation).WithMany(p => p.Phieuyeucaunhaps)
                .HasForeignKey(d => d.MaKhoa)
                .HasConstraintName("FK__PHIEUYEUC__maKho__440B1D61");

            entity.HasOne(d => d.MaNguoiDuyetNavigation).WithMany(p => p.PhieuyeucaunhapMaNguoiDuyetNavigations)
                .HasForeignKey(d => d.MaNguoiDuyet)
                .HasConstraintName("FK__PHIEUYEUC__maNgu__45F365D3");

            entity.HasOne(d => d.MaNguoiTaoNavigation).WithMany(p => p.PhieuyeucaunhapMaNguoiTaoNavigations)
                .HasForeignKey(d => d.MaNguoiTao)
                .HasConstraintName("FK__PHIEUYEUC__maNgu__44FF419A");

            entity.HasOne(d => d.MaNguoiXacNhanNavigation).WithMany(p => p.PhieuyeucaunhapMaNguoiXacNhanNavigations)
                .HasForeignKey(d => d.MaNguoiXacNhan)
                .HasConstraintName("FK__PHIEUYEUC__maNgu__46E78A0C");
        });

        modelBuilder.Entity<Thietbi>(entity =>
        {
            entity.HasKey(e => e.MaTb).HasName("PK__THIETBI__7A22626991A2FDF6");

            entity.ToTable("THIETBI");

            entity.Property(e => e.MaTb)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maTB");
            entity.Property(e => e.HinhAnh)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hinhAnh");
            entity.Property(e => e.MaDm)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maDM");
            entity.Property(e => e.NhaCungCap)
                .HasMaxLength(255)
                .HasColumnName("nhaCungCap");
            entity.Property(e => e.SoLuong).HasColumnName("soLuong");
            entity.Property(e => e.TenTb)
                .HasMaxLength(50)
                .HasColumnName("tenTB");
            entity.Property(e => e.Xuatxu)
                .HasMaxLength(255)
                .HasColumnName("xuatxu");

            entity.HasOne(d => d.MaDmNavigation).WithMany(p => p.Thietbis)
                .HasForeignKey(d => d.MaDm)
                .HasConstraintName("FK__THIETBI__maDM__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
