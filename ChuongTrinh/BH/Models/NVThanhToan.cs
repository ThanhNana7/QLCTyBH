namespace BH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NVThanhToan")]
    public partial class NVThanhToan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NVThanhToan()
        {
            BaoCaos = new HashSet<BaoCao>();
            PhieuThanhToans = new HashSet<PhieuThanhToan>();
        }

        [Key]
        public int MSNV { get; set; }

        [Required]
        [StringLength(50)]
        public string HoTen { get; set; }

        [Required]
        [StringLength(50)]
        public string Phai { get; set; }

        [Column(TypeName = "date")]
        public DateTime NamSinh { get; set; }

        [StringLength(100)]
        public string DiaChi { get; set; }

        [Required]
        [StringLength(12)]
        public string SDT { get; set; }

        [Required]
        [StringLength(10)]
        public string TaiKhoan { get; set; }

        [Required]
        [StringLength(10)]
        public string MatKhau { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaoCao> BaoCaos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuThanhToan> PhieuThanhToans { get; set; }
    }
}
