namespace BH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            DonDatHangs = new HashSet<DonDatHang>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MSKH { get; set; }

        [StringLength(50)]
        public string TenKH { get; set; }

        [StringLength(10)]
        public string TaiKhoang { get; set; }

        [StringLength(10)]
        public string MatKhau { get; set; }

        [StringLength(50)]
        public string DiaChi { get; set; }

        [StringLength(12)]
        public string SDT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonDatHang> DonDatHangs { get; set; }
    }
}
