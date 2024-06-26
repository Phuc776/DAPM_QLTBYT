using System;
using System.Collections.Generic;

namespace DAPM_QLTBYT.Models;

public partial class Chucvu
{
    public string MaChucVu { get; set; } = null!;

    public string TenChucVu { get; set; } = null!;

    public virtual ICollection<Nguoidung> Nguoidungs { get; set; } = new List<Nguoidung>();
}
