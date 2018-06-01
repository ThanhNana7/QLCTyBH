namespace BH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CTDatHang")]
    public partial class CTDatHang
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SoHD { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MSMH { get; set; }

        public int? SoLuong { get; set; }

        public double? DonGia { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double? ThanhTien { get; set; }

        public virtual DonDatHang DonDatHang { get; set; }

        public virtual MatHang MatHang { get; set; }
    }
}
