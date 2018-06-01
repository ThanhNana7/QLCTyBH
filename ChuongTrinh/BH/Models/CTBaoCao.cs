namespace BH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CTBaoCao")]
    public partial class CTBaoCao
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaBC { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SOPTT { get; set; }

        public int? MSCH { get; set; }

        public string ThongTin { get; set; }

        [Column(TypeName = "money")]
        public decimal? TongCong { get; set; }

        public virtual BaoCao BaoCao { get; set; }

        public virtual PhieuThanhToan PhieuThanhToan { get; set; }
    }
}
