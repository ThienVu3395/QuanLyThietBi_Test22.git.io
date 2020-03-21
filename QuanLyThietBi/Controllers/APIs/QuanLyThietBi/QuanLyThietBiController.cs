using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuanLyThietBi.Models;

namespace QuanLyThietBi.Controllers.APIs.QuanLyThietBi
{
    [RoutePrefix("API/QuanLyTaiSan")]
    public class QuanLyThietBiController : ApiController
    {
        QuanLyThietBiEntities dbContext = new QuanLyThietBiEntities();
        [HttpGet]
        [Route("LayDanhSachDanhMuc")]
        public IHttpActionResult LayDanhSachDanhMuc()
        {
            var dsDanhMuc = dbContext.DanhMucs.Where(x => x.ParentID == 0).ToList();
            if (dsDanhMuc.Count > 0)
            {
                List<DanhMucModel> dsDanhMucModel = new List<DanhMucModel>();
                foreach (var item in dsDanhMuc)
                {
                    DanhMucModel danhMucModel = new DanhMucModel();
                    danhMucModel.MaDanhMuc = item.MaDanhMuc;
                    danhMucModel.Ten = item.Ten;
                    danhMucModel.SoLuong = item.SoLuong;
                    danhMucModel.ParentID = item.ParentID;
                    var listNhom = dbContext.DanhMucs.Where(x => x.ParentID == item.MaDanhMuc).ToList();
                    List<NhomModel> dsNhomModel = new List<NhomModel>();
                    foreach (var i in listNhom)
                    {
                        NhomModel nhomModel = new NhomModel();
                        nhomModel.MaDanhMuc = i.MaDanhMuc;
                        nhomModel.Ten = i.Ten;
                        nhomModel.SoLuong = i.SoLuong;
                        dsNhomModel.Add(nhomModel);
                    }
                    danhMucModel.DanhSachNhom = dsNhomModel;
                    dsDanhMucModel.Add(danhMucModel);
                }
                return Ok(dsDanhMucModel);
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("LayDanhSachNhomTheoDanhMuc")]
        public IHttpActionResult LayDanhSachNhomTheoDanhMuc(int MaDanhMuc)
        {
            var dsDanhMuc = dbContext.DanhMucs.Where(x => x.ParentID == MaDanhMuc).ToList();
            List<DanhMucModel> dsDanhMucModel = new List<DanhMucModel>();
            if (dsDanhMuc.Count > 0)
            {
                foreach (var item in dsDanhMuc)
                {
                    DanhMucModel danhMucModel = new DanhMucModel();
                    danhMucModel.MaDanhMuc = item.MaDanhMuc;
                    danhMucModel.Ten = item.Ten;
                    danhMucModel.SoLuong = item.SoLuong;
                    danhMucModel.ParentID = item.ParentID;
                    var listNhom = dbContext.DanhMucs.Where(x => x.ParentID == item.MaDanhMuc).ToList();
                    List<NhomModel> dsNhomModel = new List<NhomModel>();
                    foreach (var i in listNhom)
                    {
                        NhomModel nhomModel = new NhomModel();
                        nhomModel.MaDanhMuc = i.MaDanhMuc;
                        nhomModel.Ten = i.Ten;
                        nhomModel.SoLuong = i.SoLuong;
                        dsNhomModel.Add(nhomModel);
                    }
                    danhMucModel.DanhSachNhom = dsNhomModel;
                    dsDanhMucModel.Add(danhMucModel);
                }
                return Ok(dsDanhMucModel);
            }
            return Ok(dsDanhMucModel);
        }

        [HttpGet]
        [Route("LayDanhSachThietBi")]
        public IHttpActionResult LayDanhSachThietBi(int MaDanhMuc)
        {
            List<ThietBi> dsThietBi;
            if (MaDanhMuc == -1)
            {
                dsThietBi = dbContext.ThietBis.ToList();
            }
            else
            {
                dsThietBi = dbContext.ThietBis.Where(x => x.MaDanhMuc == MaDanhMuc).ToList();
            }
            if (dsThietBi.Count > 0)
            {
                List<ThietBiModel> _dsThietBi = new List<ThietBiModel>();
                foreach (var item in dsThietBi)
                {
                    ThietBiModel thietBi = new ThietBiModel();
                    thietBi.MaThietBi = item.MaThietBi;
                    thietBi.Ten = item.Ten;
                    thietBi.Gia = item.Gia;
                    thietBi.Model = item.Model;
                    thietBi.NgayNhapKho = item.NgayNhapKho;
                    thietBi.NgayXuatKho = item.NgayXuatKho;
                    thietBi.MaDanhMuc = item.MaDanhMuc;
                    _dsThietBi.Add(thietBi);
                }
                return Ok(_dsThietBi);
            }
            else
            {
                var dsDanhMuc = dbContext.DanhMucs.Where(x => x.ParentID == MaDanhMuc).ToList();
                List<ThietBiModel> _dsTb = new List<ThietBiModel>();
                if (dsDanhMuc.Count > 0)
                {
                    foreach (var i in dsDanhMuc)
                    {
                        var _dstb = dbContext.ThietBis.Where(x => x.MaDanhMuc == i.MaDanhMuc).ToList();
                        foreach (var j in _dstb)
                        {
                            ThietBiModel thietBi = new ThietBiModel();
                            thietBi.MaThietBi = j.MaThietBi;
                            thietBi.Ten = j.Ten;
                            thietBi.Gia = j.Gia;
                            thietBi.Model = j.Model;
                            thietBi.NgayNhapKho = j.NgayNhapKho;
                            thietBi.NgayXuatKho = j.NgayXuatKho;
                            thietBi.MaDanhMuc = j.MaDanhMuc;
                            _dsTb.Add(thietBi);
                        }
                    }
                }
                return Ok(_dsTb);
            }
        }

        [HttpGet]
        [Route("LayDanhSachThietBi_PhanTrang")]
        public IHttpActionResult LayDanhSachThietBi_PhanTrang(int offset, int limit)
        {
            List<ThietBi> dsThietBi;
            //if (MaDanhMuc == -1)
            //{
            dsThietBi = dbContext.ThietBis.ToList();
            //}
            //else
            //{
            //    dsThietBi = dbContext.ThietBis.Where(x => x.MaDanhMuc == MaDanhMuc).ToList();
            //}
            if (dsThietBi.Count > 0)
            {
                List<ThietBiModel> _dsThietBi = new List<ThietBiModel>();
                foreach (var item in dsThietBi)
                {
                    ThietBiModel thietBi = new ThietBiModel();
                    thietBi.MaThietBi = item.MaThietBi;
                    thietBi.Ten = item.Ten;
                    thietBi.Gia = item.Gia;
                    thietBi.Model = item.Model;
                    thietBi.NgayNhapKho = item.NgayNhapKho;
                    thietBi.NgayXuatKho = item.NgayXuatKho;
                    thietBi.MaDanhMuc = item.MaDanhMuc;
                    _dsThietBi.Add(thietBi);
                }
                return Ok(_dsThietBi.Skip(offset).Take(limit));
            }
            return BadRequest("K tồn tại");
        }


        [HttpGet]
        [Route("LayDanhSachPhongBan")]
        public IHttpActionResult LayDanhSachPhongBan()
        {
            var dsPhongBan = dbContext.PhongBans.ToList();
            if (dsPhongBan.Count > 0)
            {
                List<PhongBanModel> dsPhongBanModel = new List<PhongBanModel>();
                foreach (var item in dsPhongBan)
                {
                    PhongBanModel phongBanModel = new PhongBanModel();
                    phongBanModel.MaPhongBan = item.MaPhongBan;
                    phongBanModel.Ten = item.Ten;
                    List<NguoiDungModel> dsNguoiDungModel = new List<NguoiDungModel>();
                    var dsUser = dbContext.NguoiSuDungs.Where(x => x.MaPhongBan == item.MaPhongBan).FirstOrDefault();
                    if (dsUser != null)
                    {
                        NguoiDungModel nguoiDungModel = new NguoiDungModel();
                        nguoiDungModel.MaNguoiDung = dsUser.MaNguoiDung;
                        nguoiDungModel.Ten = dsUser.Ten;
                        dsNguoiDungModel.Add(nguoiDungModel);
                    }
                    phongBanModel.DanhSachNguoiDung = dsNguoiDungModel;
                    dsPhongBanModel.Add(phongBanModel);
                }
                return Ok(dsPhongBanModel);
            }
            return Ok("Có lỗi");
        }

        [HttpGet]
        [Route("LayDanhSachHangSanXuat")]
        public IHttpActionResult LayDanhSachHangSanXuat()
        {
            var dsHsx = dbContext.NhaCungCaps.ToList();
            List<NhaCungCapModel> dsHsxModel = new List<NhaCungCapModel>();
            if (dsHsx.Count > 0)
            {
                foreach (var item in dsHsx)
                {
                    NhaCungCapModel hsxModel = new NhaCungCapModel();
                    hsxModel.MaNhaCungCap = item.MaNhaCungCap;
                    hsxModel.Ten = item.Ten;
                    dsHsxModel.Add(hsxModel);
                }
            }
            return Ok(dsHsxModel);
        }

        [HttpGet]
        [Route("LayDanhSachHopDong")]
        public IHttpActionResult LayDanhSachHopDong()
        {
            var dsHD = dbContext.HopDongs.ToList();
            List<HopDongModel> dsHdModel = new List<HopDongModel>();
            if (dsHD.Count > 0)
            {
                foreach (var item in dsHD)
                {
                    HopDongModel hdModel = new HopDongModel();
                    hdModel.MaHopDong = item.MaHopDong;
                    hdModel.Ten = item.Ten;
                    dsHdModel.Add(hdModel);
                }
            }
            return Ok(dsHdModel);
        }

        [HttpGet]
        [Route("LayDanhSachTinhTrang")]
        public IHttpActionResult LayDanhSachTinhTrang()
        {
            var dsHD = dbContext.TinhTrangs.ToList();
            List<TinhTrangModel> dsTinhTrangModel = new List<TinhTrangModel>();
            if (dsHD.Count > 0)
            {
                foreach (var item in dsHD)
                {
                    TinhTrangModel tinhTrangModel = new TinhTrangModel();
                    tinhTrangModel.MaTinhTrang = item.MaTinhTrang;
                    tinhTrangModel.Ten = item.Ten;
                    dsTinhTrangModel.Add(tinhTrangModel);
                }
            }
            return Ok(dsTinhTrangModel);
        }

        [HttpGet]
        [Route("LayDanhSachLinhKien")]
        public IHttpActionResult LayDanhSachLinhKien()
        {
            var dsLoaiLinhKien = dbContext.LoaiLinhKiens.ToList();
            List<LoaiLinhKienModel> ds = new List<LoaiLinhKienModel>();
            if (dsLoaiLinhKien.Count > 0)
            {
                foreach (var item in dsLoaiLinhKien)
                {
                    LoaiLinhKienModel loaiLinhKien = new LoaiLinhKienModel();
                    loaiLinhKien.MaLoaiLinhKien = item.MaLoaiLinhKien;
                    loaiLinhKien.TenLinhKien = item.TenLinhKien;
                    ds.Add(loaiLinhKien);
                }
            }
            return Ok(ds);
        }

        [HttpGet]
        [Route("LayDanhSachNguoiDungTheoPhongBan")]
        public IHttpActionResult LayDanhSachNguoiDungTheoPhongBan(int MaPhongBan)
        {
            var dsNguoiDung = dbContext.NguoiSuDungs.Where(x => x.MaPhongBan == MaPhongBan).ToList();
            List<NguoiDungModel> dsNguoiDungModel = new List<NguoiDungModel>();
            if (dsNguoiDung.Count > 0)
            {
                foreach (var item in dsNguoiDung)
                {
                    NguoiDungModel nguoiDungModel = new NguoiDungModel();
                    nguoiDungModel.MaNguoiDung = item.MaNguoiDung;
                    nguoiDungModel.Ten = item.Ten;
                    dsNguoiDungModel.Add(nguoiDungModel);
                }
            }
            return Ok(dsNguoiDungModel);
        }

        [HttpPost]
        [Route("ThemThietBi")]
        public IHttpActionResult ThemThietBi(List<ThietBiModel> dsthietBiModel)
        {
            try
            {
                foreach (var tb in dsthietBiModel)
                {
                    ThietBi thietBi = new ThietBi();
                    thietBi.Ten = tb.Ten;
                    thietBi.Gia = tb.Gia;
                    thietBi.Model = tb.Model;
                    thietBi.NgayNhapKho = tb.NgayNhapKho;
                    thietBi.NgayXuatKho = null;
                    thietBi.MaTaiSan = tb.MaTaiSan;
                    thietBi.MaDanhMuc = tb.MaDanhMuc;
                    thietBi.MaNhaCungCap = tb.MaNhaCungCap;
                    thietBi.Serial = tb.Serial;
                    thietBi.GiaKhauHao = null;
                    thietBi.DonViTinh = tb.DonViTinh;
                    thietBi.GhiChu = tb.GhiChu;
                    thietBi.NgayBaoHanh = tb.ThoiGianBaoHanh;
                    dbContext.ThietBis.Add(thietBi);
                    dbContext.SaveChanges();

                    LichSuThietBi lichSuThietBi = new LichSuThietBi();
                    lichSuThietBi.MaThietBi = thietBi.MaThietBi;
                    lichSuThietBi.MaTinhTrang = tb.MaTinhTrang;
                    lichSuThietBi.MaNguoiDung = tb.MaNguoiDung;
                    lichSuThietBi.Ngay = null;
                    lichSuThietBi.ChiPhi = null;
                    dbContext.LichSuThietBis.Add(lichSuThietBi);
                    dbContext.SaveChanges();

                    HopDongThietBi hopDongThietBi = new HopDongThietBi();
                    hopDongThietBi.MaThietBi = thietBi.MaThietBi;
                    hopDongThietBi.MaHopDong = tb.MaHopDong;
                    hopDongThietBi.GiaTriHD = tb.GiaTriHD;
                    hopDongThietBi.NgayKy = tb.NgayKy;
                    hopDongThietBi.SoHopDong = tb.SoHopDong;
                    hopDongThietBi.Ngay = null;
                    dbContext.HopDongThietBis.Add(hopDongThietBi);
                    dbContext.SaveChanges();

                    if (tb.DanhSachLinhKien.Count > 0)
                    {
                        foreach (var item in tb.DanhSachLinhKien)
                        {
                            LinhKien linhKien = new LinhKien();
                            linhKien.Serial = item.Serial;
                            linhKien.MaLoaiLinhKien = item.MaLoaiLinhKien;
                            linhKien.Model = item.Model;
                            linhKien.MaNhaCungCap = item.MaNhaCungCap;
                            linhKien.GhiChu = item.GhiChu;
                            dbContext.LinhKiens.Add(linhKien);
                            dbContext.SaveChanges();

                            LinhKienThietBi linhKienThietBi = new LinhKienThietBi();
                            linhKienThietBi.MaLinhKien = linhKien.MaLinhKien;
                            linhKienThietBi.MaThietBi = thietBi.MaThietBi;
                            linhKienThietBi.Ngay = null;
                            dbContext.LinhKienThietBis.Add(linhKienThietBi);
                            dbContext.SaveChanges();

                            LichSuLinhKien lichSuLinhKien = new LichSuLinhKien();
                            lichSuLinhKien.MaLinhKien = linhKien.MaLinhKien;
                            lichSuLinhKien.MaTinhTrang = 1;
                            lichSuLinhKien.Ngay = null;
                            lichSuLinhKien.ChiPhi = null;
                            dbContext.LichSuLinhKiens.Add(lichSuLinhKien);
                            dbContext.SaveChanges();
                        }
                    }
                }
                return Ok("Thêm Thiết Bị Thành Công");
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                //throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                return BadRequest("Nội dung lỗi 01 : " + exceptionMessage + "Nội dung lỗi 02 :" + ex.EntityValidationErrors);
            }
        }

        [HttpGet]
        [Route("LayChiTietThietBi")]
        public IHttpActionResult LayChiTietThietBi(int MaThietBi)
        {
            var thietBi = dbContext.ThietBis.Where(x => x.MaThietBi == MaThietBi).FirstOrDefault();
            var danhMuc = dbContext.DanhMucs.Where(x => x.MaDanhMuc == thietBi.MaDanhMuc).FirstOrDefault();
            //var ncc = dbContext.NhaCungCaps.Where(x => x.MaNhaCungCap == thietBi.MaNhaCungCap).FirstOrDefault();
            // Lấy thông tin cơ bản
            ChiTietThietBiModel thietBiModel = new ChiTietThietBiModel();
            thietBiModel.MaThietBi = thietBi.MaThietBi;
            thietBiModel.Ten = thietBi.Ten;
            thietBiModel.Gia = thietBi.Gia;
            thietBiModel.Model = thietBi.Model;
            thietBiModel.MaTaiSan = thietBi.MaTaiSan;
            thietBiModel.DonViTinh = thietBi.DonViTinh;
            var danhMuc2 = dbContext.DanhMucs.Where(x => x.MaDanhMuc == danhMuc.ParentID).FirstOrDefault();
            if (danhMuc2 != null)
            {
                thietBiModel.DanhMuc = danhMuc2.Ten + " - " + danhMuc.Ten;
            }
            else
            {
                thietBiModel.DanhMuc = danhMuc.Ten;
            }
            thietBiModel.MaDanhMuc = thietBi.MaDanhMuc;
            thietBiModel.MaDanhMucCha = thietBi.DanhMuc.ParentID;
            thietBiModel.NhaCungCap = thietBi.NhaCungCap.Ten;
            thietBiModel.MaNhaCungCap = thietBi.MaNhaCungCap;
            thietBiModel.NgayNhapKho = thietBi.NgayNhapKho;
            thietBiModel.NgayXuatKho = thietBi.NgayXuatKho;
            thietBiModel.Serial = thietBi.Serial;
            thietBiModel.GiaKhauHao = thietBi.GiaKhauHao;
            thietBiModel.DonViTinh = thietBi.DonViTinh;
            thietBiModel.GhiChu = thietBi.GhiChu;
            thietBiModel.ThoiGianBaoHanh = thietBi.NgayBaoHanh;
            var linhKienThietBi = dbContext.LinhKienThietBis.Where(x => x.MaThietBi == MaThietBi).ToList();
            // Danh sách linh kiện theo thiết bị
            List<LinhKienModel> DanhSachLinhKien = new List<LinhKienModel>();
            if (linhKienThietBi.Count > 0)
            {
                foreach (var item in linhKienThietBi)
                {
                    var linhKien = dbContext.LinhKiens.Where(x => x.MaLinhKien == item.MaLinhKien).FirstOrDefault();
                    LinhKienModel linhKienModel = new LinhKienModel();
                    linhKienModel.MaLinhKien = linhKien.MaLinhKien;
                    //var tenLinhKien = dbContext.LoaiLinhKiens.Where(x => x.MaLoaiLinhKien == linhKien.MaLoaiLinhKien).FirstOrDefault();
                    linhKienModel.TenLoaiLinhKien = linhKien.LoaiLinhKien.TenLinhKien;
                    linhKienModel.Serial = linhKien.Serial;
                    linhKienModel.Model = linhKien.Model;
                    linhKienModel.MaNhaCungCap = linhKien.MaNhaCungCap;
                    linhKienModel.GhiChu = linhKien.GhiChu;
                    //var nhaCungCap = dbContext.NhaCungCaps.Where(x => x.MaNhaCungCap == linhKien.MaNhaCungCap).FirstOrDefault();
                    linhKienModel.NhaCungCap = linhKien.NhaCungCap.Ten;
                    DanhSachLinhKien.Add(linhKienModel);
                }
            }
            thietBiModel.DanhSachLinhKien = DanhSachLinhKien;
            // Lịch sử sử dụng của thiết bị
            var lichSuThietBi = dbContext.LichSuThietBis.Where(x => x.MaThietBi == MaThietBi).ToList();
            List<LichSuThietBiModel> danhSachLichSuThietBiModels = new List<LichSuThietBiModel>();
            if (lichSuThietBi.Count > 0)
            {
                foreach (var item in lichSuThietBi)
                {
                    LichSuThietBiModel lichSuThietBiModel = new LichSuThietBiModel();
                    lichSuThietBiModel.MaThietBi = item.MaThietBi;
                    lichSuThietBiModel.MaTinhTrang = item.MaTinhTrang;
                    var tinhTrang = dbContext.TinhTrangs.Where(x => x.MaTinhTrang == item.MaTinhTrang).FirstOrDefault();
                    lichSuThietBiModel.TinhTrang = tinhTrang.Ten;
                    lichSuThietBiModel.MaNguoiDung = item.MaNguoiDung;
                    var nguoiDung = dbContext.NguoiSuDungs.Where(x => x.MaNguoiDung == item.MaNguoiDung).FirstOrDefault();
                    lichSuThietBiModel.NguoiDung = nguoiDung.Ten;
                    var phongBan = dbContext.PhongBans.Where(x => x.MaPhongBan == nguoiDung.MaPhongBan).FirstOrDefault();
                    lichSuThietBiModel.DonVi = phongBan.Ten;
                    lichSuThietBiModel.Ngay = item.Ngay;
                    lichSuThietBiModel.ChiPhi = item.ChiPhi;
                    danhSachLichSuThietBiModels.Add(lichSuThietBiModel);
                }
            }
            thietBiModel.LichSuThietBi = danhSachLichSuThietBiModels;
            // Danh sách hợp đồng của thiết bị này
            var hopDongThietBi = dbContext.HopDongThietBis.Where(x => x.MaThietBi == MaThietBi).ToList();
            List<HopDongModel> danhSachHopDong = new List<HopDongModel>();
            if (hopDongThietBi.Count > 0)
            {
                foreach (var item in hopDongThietBi)
                {
                    HopDongModel hopDongModel = new HopDongModel();
                    hopDongModel.MaHopDong = item.MaHopDong;
                    var hopdong = dbContext.HopDongs.Where(x => x.MaHopDong == item.MaHopDong).FirstOrDefault();
                    hopDongModel.Ten = hopdong.Ten;
                    hopDongModel.GiaTriHD = item.GiaTriHD;
                    hopDongModel.NgayKy = item.NgayKy;
                    hopDongModel.SoHopDong = item.SoHopDong;
                    hopDongModel.Ngay = item.Ngay;
                    danhSachHopDong.Add(hopDongModel);
                }
            }
            thietBiModel.DanhSachHopDong = danhSachHopDong;
            return Ok(thietBiModel);
        }

        [HttpDelete]
        [Route("XoaThietBi")]
        public IHttpActionResult XoaThietBi(int MaThietBi)
        {
            try
            {
                var hopDongThietBi = dbContext.HopDongThietBis.Where(x => x.MaThietBi == MaThietBi).ToList();
                if (hopDongThietBi.Count > 0)
                {
                    foreach (var item in hopDongThietBi)    
                    {
                        dbContext.HopDongThietBis.Remove(item);
                        dbContext.SaveChanges();
                    }
                }
                var lichSuThietBi = dbContext.LichSuThietBis.Where(x => x.MaThietBi == MaThietBi).ToList();
                if (lichSuThietBi.Count > 0)
                {
                    foreach (var item in lichSuThietBi)
                    {
                        dbContext.LichSuThietBis.Remove(item);
                        dbContext.SaveChanges();
                    }
                }
                var linhKienThietBi = dbContext.LinhKienThietBis.Where(x => x.MaThietBi == MaThietBi).ToList();
                if (linhKienThietBi.Count > 0)
                {
                    foreach (var item in linhKienThietBi)
                    {
                        var lktb = dbContext.LinhKienThietBis.Where(x => x.MaLinhKien == item.MaLinhKien).SingleOrDefault();
                        dbContext.LinhKienThietBis.Remove(lktb);
                        dbContext.SaveChanges();
                        var linhSuLinhKien = dbContext.LichSuLinhKiens.Where(x => x.MaLinhKien == item.MaLinhKien).SingleOrDefault();
                        dbContext.LichSuLinhKiens.Remove(linhSuLinhKien);
                        dbContext.SaveChanges();
                        var linhKien = dbContext.LinhKiens.Where(x => x.MaLinhKien == item.MaLinhKien).SingleOrDefault();
                        dbContext.LinhKiens.Remove(linhKien);
                        dbContext.SaveChanges();
                    }
                }
                var thietBi = dbContext.ThietBis.Where(x => x.MaThietBi == MaThietBi).FirstOrDefault();
                if (thietBi != null)
                {
                    dbContext.ThietBis.Remove(thietBi);
                    dbContext.SaveChanges();
                    return Ok("Thiết bị đã được xóa");
                }
                return BadRequest("Có lỗi phát sinh");
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                //throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                return BadRequest("Nội dung lỗi 01 : " + exceptionMessage + "Nội dung lỗi 02 :" + ex.EntityValidationErrors);
            }
        }

        [HttpPost]
        [Route("LocThietBi_TheoPhongBan")]
        public IHttpActionResult LocThietBi_TheoPhongBan(TimKiemModel tkModel)
        {
            var dsNguoiDung = dbContext.NguoiSuDungs.Where(x => x.MaPhongBan == tkModel.MaPhongBan).ToList();
            List<ThietBiModel> dsThietBi = new List<ThietBiModel>();
            if (dsNguoiDung.Count > 0)
            {
                foreach (var item in dsNguoiDung)
                {
                    var dsLichSu = dbContext.LichSuThietBis.Where(x => x.MaNguoiDung == item.MaNguoiDung).ToList();
                    if (dsLichSu.Count > 0)
                    {
                        foreach (var j in dsLichSu)
                        {
                            var thietBi = dbContext.ThietBis.Where(x => x.MaThietBi == j.MaThietBi).FirstOrDefault();
                            ThietBiModel tbModel = new ThietBiModel();
                            tbModel.MaThietBi = thietBi.MaThietBi;
                            tbModel.Ten = thietBi.Ten;
                            tbModel.Gia = thietBi.Gia;
                            tbModel.Model = thietBi.Model;
                            //tbModel.SoLuong = thietBi.SoLuong;
                            tbModel.NgayNhapKho = thietBi.NgayNhapKho;
                            tbModel.NgayXuatKho = thietBi.NgayXuatKho;
                            tbModel.MaDanhMuc = thietBi.MaDanhMuc;
                            dsThietBi.Add(tbModel);
                        }
                    }
                }
                return Ok(dsThietBi);
            }
            return Ok(dsThietBi);
        }

        [HttpPost]
        [Route("LocThietBi_TheoTinhTrang")]
        public IHttpActionResult LocThietBi_TheoTinhTrang(TimKiemModel tkModel)
        {
            var dsLichSu = dbContext.LichSuThietBis.Where(x => x.MaTinhTrang == tkModel.MaTinhTrang).ToList();
            List<ThietBiModel> dsThietBi = new List<ThietBiModel>();
            if (dsLichSu.Count > 0)
            {
                foreach (var item in dsLichSu)
                {
                    var thietBi = dbContext.ThietBis.Where(x => x.MaThietBi == item.MaThietBi).FirstOrDefault();
                    if (thietBi != null)
                    {
                        ThietBiModel tbModel = new ThietBiModel();
                        tbModel.MaThietBi = thietBi.MaThietBi;
                        tbModel.Ten = thietBi.Ten;
                        tbModel.Gia = thietBi.Gia;
                        tbModel.Model = thietBi.Model;
                        //tbModel.SoLuong = thietBi.SoLuong;
                        tbModel.NgayNhapKho = thietBi.NgayNhapKho;
                        tbModel.NgayXuatKho = thietBi.NgayXuatKho;
                        tbModel.MaDanhMuc = thietBi.MaDanhMuc;
                        dsThietBi.Add(tbModel);
                    }
                }
                return Ok(dsThietBi);
            }
            return Ok(dsThietBi);
        }

        [HttpPost]
        [Route("LocThietBi_All")]
        public IHttpActionResult LocThietBi_All(TimKiemModel tkModel)
        {
            var dsThietBi = dbContext.ThietBis.ToList();
            List<ThietBiModel> ds = new List<ThietBiModel>();
            if (dsThietBi.Count > 0)
            {
                foreach (var item in dsThietBi)
                {
                    var thietBi = dbContext.ThietBis.Where(x => x.MaThietBi == item.MaThietBi).FirstOrDefault();
                    ThietBiModel tbModel = new ThietBiModel();
                    tbModel.MaThietBi = thietBi.MaThietBi;
                    tbModel.Ten = thietBi.Ten;
                    tbModel.Gia = thietBi.Gia;
                    tbModel.Model = thietBi.Model;
                    //tbModel.SoLuong = thietBi.SoLuong;
                    tbModel.NgayNhapKho = thietBi.NgayNhapKho;
                    tbModel.NgayXuatKho = thietBi.NgayXuatKho;
                    tbModel.MaDanhMuc = thietBi.MaDanhMuc;
                    ds.Add(tbModel);
                }
                return Ok(ds);
            }
            return Ok(ds);
        }

        [HttpPost]
        [Route("LocThietBi")]
        public IHttpActionResult LocThietBi(TimKiemModel tkModel)
        {
            var dsUser = dbContext.NguoiSuDungs.Where(x => x.MaPhongBan == tkModel.MaPhongBan).ToList();
            List<ThietBiModel> dsThietBi = new List<ThietBiModel>();
            if (dsUser.Count > 0)
            {
                foreach (var item in dsUser)
                {
                    var dsLichSu = dbContext.LichSuThietBis.Where(x => x.MaTinhTrang == tkModel.MaTinhTrang && x.MaNguoiDung == item.MaNguoiDung).ToList();
                    if (dsLichSu.Count > 0)
                    {
                        foreach (var i in dsLichSu)
                        {
                            var thietBi = dbContext.ThietBis.Where(x => x.MaThietBi == i.MaThietBi).FirstOrDefault();
                            if (thietBi != null)
                            {
                                ThietBiModel tbModel = new ThietBiModel();
                                tbModel.MaThietBi = thietBi.MaThietBi;
                                tbModel.Ten = thietBi.Ten;
                                tbModel.Gia = thietBi.Gia;
                                tbModel.Model = thietBi.Model;
                                //tbModel.SoLuong = thietBi.SoLuong;
                                tbModel.NgayNhapKho = thietBi.NgayNhapKho;
                                tbModel.NgayXuatKho = thietBi.NgayXuatKho;
                                tbModel.MaDanhMuc = thietBi.MaDanhMuc;
                                dsThietBi.Add(tbModel);
                            }
                        }
                    }
                }
                return Ok(dsThietBi);
            }
            return Ok(dsThietBi);
        }

        [HttpPost]
        [Route("DangNhap")]
        public IHttpActionResult DangNhap(NguoiDungViewModel thongTin)
        {
            var _User = dbContext.NguoiSuDungs.Where(x => x.UserName == thongTin.UserName && x.Password == thongTin.Password).FirstOrDefault();
            if (_User != null)
            {
                NguoiDungViewModel userInfo = new NguoiDungViewModel();
                userInfo.MaNguoiDung = _User.MaNguoiDung;
                userInfo.Ten = _User.Ten;
                userInfo.MaPhongBan = _User.MaPhongBan;
                var TenPhongBan = dbContext.PhongBans.Where(x => x.MaPhongBan == userInfo.MaPhongBan).FirstOrDefault();
                userInfo.TenPhongBan = TenPhongBan.Ten;
                userInfo.UserName = _User.UserName;
                userInfo.ChucVu = _User.ChucVu;
                return Ok(userInfo);
            }
            return BadRequest("Sai User hoặc Password");
        }

        [HttpGet]
        [Route("KiemTraNguoiDung")]
        public IHttpActionResult KiemTraNguoiDung(int MaNguoiDung)
        {
            var _User = dbContext.NguoiSuDungs.Where(x => x.MaNguoiDung == MaNguoiDung).FirstOrDefault();
            NguoiDungViewModel userInfo = new NguoiDungViewModel();
            if (_User != null)
            {
                userInfo.MaNguoiDung = _User.MaNguoiDung;
                userInfo.Ten = _User.Ten;
                userInfo.MaPhongBan = _User.MaPhongBan;
                var TenPhongBan = dbContext.PhongBans.Where(x => x.MaPhongBan == userInfo.MaPhongBan).FirstOrDefault();
                userInfo.TenPhongBan = TenPhongBan.Ten;
                userInfo.UserName = _User.UserName;
                userInfo.ChucVu = _User.ChucVu;
            }
            return Ok(userInfo);
        }
    }
}
