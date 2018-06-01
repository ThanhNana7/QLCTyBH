namespace BH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaoCao")]
    public partial class BaoCao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BaoCao()
        {
            CTBaoCaos = new HashSet<CTBaoCao>();
        }

        [Key]
        public int MaBC { get; set; }

        [StringLength(50)]
        public string TenBC { get; set; }

        public int? MSNV { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ThangNam { get; set; }

        public virtual NVThanhToan NVThanhToan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTBaoCao> CTBaoCaos { get; set; }
    }
}
