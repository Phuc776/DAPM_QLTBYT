using System;
using System.Collections.Generic;

namespace DAPM_QLTBYT.Models;

public partial class Phieuyeucaunhap
{
    public string MaPhieu { get; set; } = null!;

    public string? MaKhoa { get; set; }

    public string? MaNguoiTao { get; set; }

    public string? MaNguoiDuyet { get; set; }

    public string? MaNguoiXacNhan { get; set; }

    public DateOnly? NgayTao { get; set; }

    public DateOnly? NgayDuyet { get; set; }

    public DateOnly? NgayXacNhan { get; set; }

    public string? TrangThai { get; set; }

    public virtual ICollection<Chitietphieuyeucaunhap> Chitietphieuyeucaunhaps { get; set; } = new List<Chitietphieuyeucaunhap>();

    public virtual Khoa? MaKhoaNavigation { get; set; }

    public virtual Nguoidung? MaNguoiDuyetNavigation { get; set; }

    public virtual Nguoidung? MaNguoiTaoNavigation { get; set; }

    public virtual Nguoidung? MaNguoiXacNhanNavigation { get; set; }
}
