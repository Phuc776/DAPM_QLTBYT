using System;
using System.Collections.Generic;

namespace DAPM_QLTBYT.Models;

public partial class Danhmucthietbi
{
    public string MaDm { get; set; } = null!;

    public string TenDm { get; set; } = null!;

    public string? MoTa { get; set; }

    public virtual ICollection<Thietbi> Thietbis { get; set; } = new List<Thietbi>();
}
