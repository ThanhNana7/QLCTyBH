namespace BH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MatHang")]
    public partial class MatHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MatHang()
        {
            CTDatHangs = new HashSet<CTDatHang>();
            CTPhieuGHs = new HashSet<CTPhieuGH>();
            CTPhieuTTs = new HashSet<CTPhieuTT>();
            TonKhoes = new HashSet<TonKho>();
        }

        [Key]
        public int MSMH { get; set; }

        public int? MSLH { get; set; }

        [StringLength(50)]
        public string TenHang { get; set; }

        public int? SoLuong { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        public double? DonGia { get; set; }

        public string HinhAnh { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayCapNhat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTDatHang> CTDatHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTPhieuGH> CTPhieuGHs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTPhieuTT> CTPhieuTTs { get; set; }

        public virtual LoaiHang LoaiHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TonKho> TonKhoes { get; set; }
    }
}
