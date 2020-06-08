using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyThietBi.Models
{
    public class DanhMucModel
    {
        public int MaDanhMuc { get; set; }
        public string Ten { get; set; }
        public int SoLuong { get; set; }
        public int ParentID { get; set; }
        public List<NhomModel> DanhSachNhom { get; set; }
    }

    public class NhomModel
    {
        public int MaDanhMuc { get; set; }
        public string Ten { get; set; }
        public int SoLuong { get; set; }
    }

    public class ThietBiModel
    {
        //Dữ liệu chính trong db
        public int MaThietBi { get; set; }
        public string MaTaiSan { get; set; }
        public string Ten { get; set; }
        public decimal? Gia { get; set; }
        public string Model { get; set; }
        public int SoLuong { get; set; }
        public Nullable<System.DateTime> NgayNhapKho { get; set; }
        public Nullable<System.DateTime> NgayXuatKho { get; set; }
        public int MaDanhMuc { get; set; }
        public int MaHangSanXuat { get; set; }
        public string Serial { get; set; }
        public string DonViTinh { get; set; }
        public string ChiTietTaiSan { get; set; }
        public string NgayBaoHanh { get; set; }
        public string HangSanXuat { get; set; }
        public string NguoiSuDung { get; set; }
        public int MaPhongBan { get; set; }
        public string IP { get; set; }
        public string SoHopDong { get; set; }
        public string NhaCungCap { get; set; }
        public Nullable<System.DateTime> NgayMua { get; set; }
        public string NamBaoHanh { get; set; }
        public string ViTri { get; set; }
        public string GhiChu { get; set; }
        //Dữ liệu chính trong db

        public string Loai { get; set; }
        public decimal? GiaKhauHao { get; set; }
        public decimal GiaTriHD { get; set; }
        public int MaNguoiDung { get; set; }
        public int MaTinhTrang { get; set; }
        public int? MaNhaCungCap { get; set; }
        public int MaHopDong { get; set; }
        public Nullable<System.DateTime> NgayKy { get; set; }
        public Nullable<System.DateTime> ThoiGianBaoHanh { get; set; }
        public List<LichSuThietBiModel> LichSuThietBi { get; set; }
        public List<LinhKienModel> DanhSachLinhKien { get; set; }
    }


    public class ChiTietThietBiModel
    {
        public int MaThietBi { get; set; }
        public string Ten { get; set; }
        public decimal? Gia { get; set; }
        public decimal? GiaKhauHao { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }
        public int SoLuong { get; set; }
        public string GhiChu { get; set; }
        public string DanhMuc { get; set; }
        public int? MaNhaCungCap { get; set; }
        public int? MaHangSanXuat { get; set; }
        public string MaTaiSan { get; set; }
        public string NhaCungCap { get; set; }
        public string DonViTinh { get; set; }
        public int MaDanhMuc { get; set; }
        public int MaDanhMucCha { get; set; }
        public Nullable<System.DateTime> NgayNhapKho { get; set; }
        public Nullable<System.DateTime> NgayNhapKhoo { get; set; }
        public Nullable<System.DateTime> NgayXuatKho { get; set; }
        public Nullable<System.DateTime> NgayHienTrang { get; set; }
        public Nullable<System.DateTime> NgayHienTrangg { get; set; }
        public Nullable<System.DateTime> ThoiGianBaoHanh { get; set; }
        public List<LichSuThietBiModel> LichSuThietBi { get; set; }
        public List<LinhKienModel> DanhSachLinhKien { get; set; }
        public List<HopDongModel> DanhSachHopDong { get; set; }
        public string TrangThai { get; set; }
        public string ChiTietTaiSan { get; set; }
        public string NguoiSuDung { get; set; }
        public int MaNguoiDung { get; set; }
        public int? MaPhongBan { get; set; }
        public int MaTinhTrang { get; set; }
        public string TenTinhTrang { get; set; }
        public string TenPhongBan { get; set; }
        public decimal ChiPhi { get; set; }
        public string IP { get; set; }
        public string SoHopDong { get; set; }
        public string HangSanXuat { get; set; }
        public string ViTri { get; set; }
        public string NamBaoHanh { get; set; }

        // Biến kiểm tra để xét có add vào lịch sử thiết bị hay không
        public int Check { get; set; }
    }

    public class PhongBanModel
    {
        public int MaPhongBan { get; set; }
        public string Ten { get; set; }
        public List<NguoiDungModel> DanhSachNguoiDung { get; set; }
    }

    public class NguoiDungModel
    {
        public int MaNguoiDung { get; set; }
        public string Ten { get; set; }
    }

    public class NhaCungCapModel
    {
        public int MaNhaCungCap { get; set; }
        public string Ten { get; set; }
    }

    public class HopDongModel
    {
        public int MaHopDong { get; set; }
        public string Ten { get; set; }
        public decimal? GiaTriHD { get; set; }
        public Nullable<System.DateTime> NgayKy { get; set; }
        public string SoHopDong { get; set; }
        public Nullable<System.DateTime> Ngay { get; set; }
    }

    public class TinhTrangModel
    {
        public int MaTinhTrang { get; set; }
        public string Ten { get; set; }
    }

    public class LinhKienModel
    {
        public int MaLinhKien { get; set; }
        public int MaThietBi { get; set; }
        public int MaLoaiLinhKien { get; set; }
        public string TenLoaiLinhKien { get; set; }
        public string Serial { get; set; }
        public string GhiChu { get; set; }
        public string Model { get; set; }
        public string ThongSoKyThuat { get; set; }
        public int MaTinhTrang { get; set; }
        public int MaTinhTrangCu { get; set; }
        public string TenTinhTrang { get; set; }
        public string NamBaoHanh { get; set; }
        public Nullable<int> MaNhaCungCap { get; set; }
        public string TenNhaCungCap { get; set; }
        public string NhaCungCap { get; set; }
        public Nullable<int> DungLuong { get; set; }
        public Nullable<int> SoLuong { get; set; }
    }

    public class LichSuThietBiModel
    {
        public int MaThietBi { get; set; }
        public int MaTinhTrang { get; set; }
        public string TinhTrang { get; set; }
        public int MaNguoiDung { get; set; }
        public string NguoiDung { get; set; }
        public string DonVi { get; set; }
        public int MaDonVi { get; set; }
        public Nullable<System.DateTime> Ngay { get; set; }
        public decimal? ChiPhi { get; set; }
    }

    public class LoaiLinhKienModel
    {
        public int MaLoaiLinhKien { get; set; }
        public string TenLinhKien { get; set; }
    }

    public class HopDongThietBiModel
    {
        public int MaHopDong { get; set; }
        public int MaThietBi { get; set; }
        public decimal GiaTriHD { get; set; }
        public Nullable<System.DateTime> NgayKy { get; set; }
        public string SoHopDong { get; set; }
    }

    public class TimKiemModel
    {
        public int MaPhongBan { get; set; }
        public int MaTinhTrang { get; set; }
    }

    public class NguoiDungViewModel
    {
        public int MaNguoiDung { get; set; }
        public string Ten { get; set; }
        public int MaPhongBan { get; set; }
        public string TenPhongBan { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ChucVu { get; set; }
        public List<ChiTietThietBiModel> DanhSachThietBi { get; set; }
    }

    public class HangSanXuatViewModel
    {
        public int MaNhaCungCap { get; set; }
        public string Ten { get; set; }
    }
}