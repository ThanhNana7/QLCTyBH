namespace BH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonDatHang")]
    public partial class DonDatHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonDatHang()
        {
            CTDatHangs = new HashSet<CTDatHang>();
        }

        [Key]
        public int SoHD { get; set; }

        public int? MSKH { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDH { get; set; }

        [StringLength(50)]
        public string TenNgNhan { get; set; }

        [StringLength(50)]
        public string DiaChiNhan { get; set; }

        [StringLength(10)]
        public string SDTNhan { get; set; }

        [Column(TypeName = "money")]
        public decimal? TongCong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTDatHang> CTDatHangs { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}
