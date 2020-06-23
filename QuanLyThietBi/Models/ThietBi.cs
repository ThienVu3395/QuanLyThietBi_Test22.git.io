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
    
    public partial class ThietBi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThietBi()
        {
            this.HopDongThietBis = new HashSet<HopDongThietBi>();
            this.LichSuThietBis = new HashSet<LichSuThietBi>();
            this.LinhKienThietBis = new HashSet<LinhKienThietBi>();
        }
    
        public int MaThietBi { get; set; }
        public string MaTaiSan { get; set; }
        public string Ten { get; set; }
        public Nullable<decimal> Gia { get; set; }
        public string Model { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<System.DateTime> NgayNhapKho { get; set; }
        public Nullable<System.DateTime> NgayXuatKho { get; set; }
        public int MaDanhMuc { get; set; }
        public Nullable<int> MaNhaCungCap { get; set; }
        public Nullable<int> MaHangSanXuat { get; set; }
        public int MaTinhTrang { get; set; }
        public string Serial { get; set; }
        public Nullable<decimal> GiaKhauHao { get; set; }
        public string DonViTinh { get; set; }
        public string GhiChu { get; set; }
        public Nullable<System.DateTime> NgayBaoHanh { get; set; }
        public string ChiTietTaiSan { get; set; }
        public string HangSanXuat { get; set; }
        public string NguoiSuDung { get; set; }
        public Nullable<int> MaPhongBan { get; set; }
        public string IP { get; set; }
        public string SoHopDong { get; set; }
        public string NhaCungCap { get; set; }
        public Nullable<System.DateTime> NgayMua { get; set; }
        public string NamBaoHanh { get; set; }
        public string ViTri { get; set; }
        public Nullable<System.DateTime> NgayCapNhat { get; set; }
        public Nullable<decimal> GiaTriHopDong { get; set; }
        public string NguoiCapNhat { get; set; }
    
        public virtual DanhMuc DanhMuc { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HopDongThietBi> HopDongThietBis { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuThietBi> LichSuThietBis { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LinhKienThietBi> LinhKienThietBis { get; set; }
        public virtual NhaCungCap NhaCungCap1 { get; set; }
        public virtual PhongBan PhongBan { get; set; }
        public virtual TinhTrang TinhTrang { get; set; }
    }
}
