using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAPM_QLTBYT.Models;

public partial class Nguoidung
{
    public string MaNd { get; set; } = null!;
    public string TenNd { get; set; } = null!;
    public string Matkhau { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateOnly? NgaySinh { get; set; }
    public int? Cccd { get; set; }
    public string? DiaChi { get; set; }
    public bool? TrangThai { get; set; }
    public string? MaChucVu { get; set; }
    public string? MaKhoa { get; set; }
    public virtual Chucvu? MaChucVuNavigation { get; set; }
    public virtual Khoa? MaKhoaNavigation { get; set; }
    public virtual ICollection<Phieuyeucaucungcap> PhieuyeucaucungcapMaNguoiDuyetNavigations { get; set; } = new List<Phieuyeucaucungcap>();
    public virtual ICollection<Phieuyeucaucungcap> PhieuyeucaucungcapMaNguoiTaoNavigations { get; set; } = new List<Phieuyeucaucungcap>();
    public virtual ICollection<Phieuyeucaucungcap> PhieuyeucaucungcapMaNguoiXacNhanNavigations { get; set; } = new List<Phieuyeucaucungcap>();
    public virtual ICollection<Phieuyeucaunhap> PhieuyeucaunhapMaNguoiDuyetNavigations { get; set; } = new List<Phieuyeucaunhap>();
    public virtual ICollection<Phieuyeucaunhap> PhieuyeucaunhapMaNguoiTaoNavigations { get; set; } = new List<Phieuyeucaunhap>();
    public virtual ICollection<Phieuyeucaunhap> PhieuyeucaunhapMaNguoiXacNhanNavigations { get; set; } = new List<Phieuyeucaunhap>();
    
}
