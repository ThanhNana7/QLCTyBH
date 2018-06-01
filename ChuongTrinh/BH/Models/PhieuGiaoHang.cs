namespace BH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuGiaoHang")]
    public partial class PhieuGiaoHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuGiaoHang()
        {
            CTPhieuGHs = new HashSet<CTPhieuGH>();
        }

        [Key]
        public int SOPG { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayLapPhieu { get; set; }

        public int? MSCH { get; set; }

        public int? MSNV { get; set; }

        [Column(TypeName = "money")]
        public decimal? TongCong { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTPhieuGH> CTPhieuGHs { get; set; }

        public virtual NVPhuTrach NVPhuTrach { get; set; }
    }
}
