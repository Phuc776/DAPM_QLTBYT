using System;
using System.Collections.Generic;

namespace DAPM_QLTBYT.Models;

public partial class Khoa
{
    public string MaKhoa { get; set; } = null!;

    public string TenKhoa { get; set; } = null!;

    public string? MoTa { get; set; }

    public virtual ICollection<Nguoidung> Nguoidungs { get; set; } = new List<Nguoidung>();

    public virtual ICollection<Phieuyeucaucungcap> Phieuyeucaucungcaps { get; set; } = new List<Phieuyeucaucungcap>();

    public virtual ICollection<Phieuyeucaunhap> Phieuyeucaunhaps { get; set; } = new List<Phieuyeucaunhap>();
}
