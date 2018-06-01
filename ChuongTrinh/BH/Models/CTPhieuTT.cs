namespace BH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CTPhieuTT")]
    public partial class CTPhieuTT
    {
        public int SOPTT { get; set; }

        public int MSMH { get; set; }

        public int? SoLuongBan { get; set; }

        public double? DonGia { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double? ThanhTien { get; set; }

        [Key]
        public int SOCTPTT { get; set; }

        public virtual MatHang MatHang { get; set; }

        public virtual PhieuThanhToan PhieuThanhToan { get; set; }
    }
}
