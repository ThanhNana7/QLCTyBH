namespace BH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CTPhieuGH")]
    public partial class CTPhieuGH
    {
        public int SOPG { get; set; }

        public int MSMH { get; set; }

        public int? SoLuongGiao { get; set; }

        public double? DonGia { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double? ThanhTien { get; set; }

        [Key]
        public int SOCTPG { get; set; }

        public virtual MatHang MatHang { get; set; }

        public virtual PhieuGiaoHang PhieuGiaoHang { get; set; }
    }
}
