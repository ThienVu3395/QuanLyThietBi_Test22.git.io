//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyThietBi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LinhKien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LinhKien()
        {
            this.LichSuLinhKiens = new HashSet<LichSuLinhKien>();
            this.LinhKienThietBis = new HashSet<LinhKienThietBi>();
        }
    
        public int MaLinhKien { get; set; }
        public int MaLoaiLinhKien { get; set; }
        public string Serial { get; set; }
        public string Model { get; set; }
        public Nullable<int> MaNhaCungCap { get; set; }
        public string GhiChu { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuLinhKien> LichSuLinhKiens { get; set; }
        public virtual LoaiLinhKien LoaiLinhKien { get; set; }
        public virtual NhaCungCap NhaCungCap { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LinhKienThietBi> LinhKienThietBis { get; set; }
    }
}
