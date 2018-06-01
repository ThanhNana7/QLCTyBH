namespace BH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TonKho")]
    public partial class TonKho
    {
        [Key]
        [Column(Order = 0, TypeName = "date")]
        public DateTime NgayThang { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MSMH { get; set; }

        public int? SoLuongDau { get; set; }

        public int? SoLuongCuoi { get; set; }

        public int? TongSoLuong { get; set; }

        public virtual MatHang MatHang { get; set; }
    }
}
