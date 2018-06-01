namespace BH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CuaHang")]
    public partial class CuaHang
    {
        [Key]
        public int MSCH { get; set; }

        [Required]
        [StringLength(50)]
        public string TenCH { get; set; }

        public int MSLH { get; set; }

        [StringLength(50)]
        public string DiaChi { get; set; }

        public int? NvPhuTrach { get; set; }

        [StringLength(12)]
        public string SDT { get; set; }

        public virtual LoaiHang LoaiHang { get; set; }

        public virtual NVPhuTrach NVPhuTrach1 { get; set; }
    }
}
