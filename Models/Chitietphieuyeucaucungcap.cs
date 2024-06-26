using System;
using System.Collections.Generic;

namespace DAPM_QLTBYT.Models;

public partial class Chitietphieuyeucaucungcap
{
    public string MaPhieu { get; set; } = null!;

    public string MaTb { get; set; } = null!;

    public int? SoLuongYeuCau { get; set; }

    public int? SoLuongDuyet { get; set; }

    public string? LyDo { get; set; }

    public virtual Phieuyeucaucungcap MaPhieuNavigation { get; set; } = null!;

    public virtual Thietbi MaTbNavigation { get; set; } = null!;
}
