using System;
using System.Collections.Generic;

namespace DAPM_QLTBYT.Models;

public partial class Thietbi
{
    public string MaTb { get; set; } = null!;

    public string TenTb { get; set; } = null!;

    public int? SoLuong { get; set; }

    public string? Xuatxu { get; set; }

    public string? HinhAnh { get; set; }

    public string? NhaCungCap { get; set; }

    public string? MaDm { get; set; }

    public virtual ICollection<Chitietphieuyeucaucungcap> Chitietphieuyeucaucungcaps { get; set; } = new List<Chitietphieuyeucaucungcap>();
    public virtual ICollection<Chitietphieuyeucaunhap> Chitietphieuyeucaunhaps { get; set; } = new List<Chitietphieuyeucaunhap>();
    public virtual Danhmucthietbi? MaDmNavigation { get; set; }
}
