using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuanLyThietBi.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyThietBi.Controllers.APIs.QuanLyThietBi
{
    [RoutePrefix("API/QuanLyTaiSan")]
    public class QuanLyThietBiController : ApiController
    {
        QuanLyThietBiEntities dbContext = new QuanLyThietBiEntities();
        //private readonly string _cnn;
        //public QuanLyThietBiController()
        //{
        //    _cnn = System.Configuration.ConfigurationManager.ConnectionStrings["QuanLyTaiSanConnection"].ConnectionString;
        //}
        [HttpGet]
        [Route("LayDanhSachDanhMuc")]
        public IHttpActionResult LayDanhSachDanhMuc()
        {
            //using (IDbConnection db = new SqlConnection(_cnn))
            //{
            //    var parameters = new DynamicParameters();
            //    parameters.Add("@OrganId", par.OrganId);
            //    parameters.Add("@FileCatalog", par.FileCatalog);
            //    parameters.Add("@FileNotation", par.FileNotation);
            //    parameters.Add("@DocOrdinal", par.DocOrdinal);
            //    parameters.Add("@TypeName", par.TypeName);
            //    parameters.Add("@CodeNumber", par.CodeNumber);
            //    parameters.Add("@CodeNotation", par.CodeNotation);
            //    parameters.Add("@IssuedDate", par.IssuedDate);
            //    parameters.Add("@OrganName", par.OrganName);
            //    parameters.Add("@Subject", par.Subject);
            //    parameters.Add("@Language", par.Language);
            //    parameters.Add("@PageAmount", 1);
            //    parameters.Add("@Description", par.Description);
            //    parameters.Add("@Position", par.Position);
            //    parameters.Add("@Fullname", par.Fullname);
            //    parameters.Add("@Priority", par.Priority);
            //    parameters.Add("@IssuedAmount", par.IssuedAmount);
            //    parameters.Add("@DueDate", par.DueDate);
            //    parameters.Add("@SoVanBanID", par.SoVanBanID);
            //    parameters.Add("@MOREINFO1", par.MOREINFO1);
            //    parameters.Add("@MOREINFO2", par.MOREINFO2);
            //    parameters.Add("@MOREINFO3", par.MOREINFO3);
            //    parameters.Add("@MOREINFO4", par.MOREINFO4);
            //    parameters.Add("@MOREINFO5", par.MOREINFO5);

            //    if (db.State == System.Data.ConnectionState.Closed)
            //    {
            //        db.Open();
            //    }
            //}
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
                    thietBi.MaTaiSan = item.MaTaiSan;
                    thietBi.Loai = item.DanhMuc.Ten;
                    thietBi.MaTinhTrang = item.MaTinhTrang;
                    thietBi.NguoiSuDung = item.NguoiSuDung;
                    //var tenUser = dbContext.NguoiSuDungs.Where(x => x.UserName == item.NguoiSuDung).FirstOrDefault();
                    //thietBi.NguoiSuDung = tenUser?.Ten;
                    //var lstb = item.LichSuThietBis.Where(x => x.MaThietBi == thietBi.MaThietBi).OrderByDescending(x => x.ID).Take(1).FirstOrDefault();
                    ////thietBi.NguoiSuDung = lstb == null ? "" : lstb.NguoiSuDung.Ten;
                    //thietBi.MaTinhTrang = lstb == null ? -1 : lstb.MaTinhTrang;
                    //thietBi.MaNguoiDung = lstb == null ? 0 : lstb.MaNguoiDung;
                    thietBi.ViTri = item.PhongBan.Ten + " " + item.ViTri;
                    thietBi.MaDanhMuc = item.MaDanhMuc;
                    thietBi.Serial = item.Serial;
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
                    thietBi.Loai = item.DanhMuc.Ten;
                    thietBi.MaTaiSan = item.MaTaiSan;
                    thietBi.ViTri = item.PhongBan.Ten + " " + item.ViTri;
                    thietBi.MaDanhMuc = item.MaDanhMuc;
                    thietBi.Serial = item.Serial;
                    thietBi.MaTinhTrang = item.MaTinhTrang;
                    thietBi.NguoiSuDung = item.NguoiSuDung;
                    //var tenUser = dbContext.NguoiSuDungs.Where(x => x.UserName == item.NguoiSuDung).FirstOrDefault();
                    //thietBi.NguoiSuDung = tenUser?.Ten;
                    //var lstb = item.LichSuThietBis.Where(x => x.MaThietBi == thietBi.MaThietBi).OrderByDescending(x => x.ID).Take(1).FirstOrDefault();
                    ////thietBi.NguoiSuDung = lstb == null ? "" : lstb.NguoiSuDung.Ten;
                    //thietBi.MaTinhTrang = lstb == null ? -1 : lstb.MaTinhTrang;
                    //thietBi.MaNguoiDung = lstb == null ? 0 : lstb.MaNguoiDung;
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
                    nguoiDungModel.UserName = item.UserName;
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
                    thietBi.MaTaiSan = tb.MaTaiSan;
                    thietBi.Ten = tb.Ten;
                    thietBi.Gia = tb.Gia;
                    thietBi.Model = tb.Model;
                    thietBi.NgayNhapKho = null;
                    thietBi.NgayXuatKho = null;
                    thietBi.MaDanhMuc = tb.MaDanhMuc;
                    thietBi.MaHangSanXuat = tb.MaNhaCungCap;
                    thietBi.Serial = tb.Serial;
                    thietBi.GiaKhauHao = null;
                    thietBi.DonViTinh = null;
                    thietBi.GhiChu = tb.GhiChu;
                    thietBi.NgayBaoHanh = null;
                    thietBi.HangSanXuat = tb.HangSanXuat;
                    thietBi.NguoiSuDung = tb.NguoiSuDung;
                    thietBi.MaPhongBan = tb.MaPhongBan;
                    thietBi.IP = tb.IP;
                    thietBi.SoHopDong = tb.SoHopDong;
                    thietBi.NhaCungCap = tb.NhaCungCap;
                    thietBi.NgayMua = tb.NgayMua;
                    thietBi.NamBaoHanh = tb.NamBaoHanh;
                    thietBi.ViTri = tb.ViTri;
                    thietBi.NgayCapNhat = tb.NgayCapNhat;
                    thietBi.NguoiCapNhat = null;
                    thietBi.MaTinhTrang = tb.MaTinhTrang;
                    dbContext.ThietBis.Add(thietBi);
                    dbContext.SaveChanges();

                    LichSuThietBi lichSuThietBi = new LichSuThietBi();
                    lichSuThietBi.MaThietBi = thietBi.MaThietBi;
                    lichSuThietBi.MaTinhTrang = tb.MaTinhTrang;
                    lichSuThietBi.MaNguoiDung = tb.MaNguoiDung;
                    lichSuThietBi.Ngay = DateTime.Now;
                    lichSuThietBi.ChiPhi = null;
                    dbContext.LichSuThietBis.Add(lichSuThietBi);
                    dbContext.SaveChanges();

                    HopDongThietBi hopDongThietBi = new HopDongThietBi();
                    hopDongThietBi.MaThietBi = thietBi.MaThietBi;
                    hopDongThietBi.MaHopDong = tb.MaHopDong;
                    hopDongThietBi.GiaTriHD = tb.GiaTriHD;
                    hopDongThietBi.NgayKy = tb.NgayKy;
                    hopDongThietBi.SoHopDong = tb.SoHopDong;
                    hopDongThietBi.Ngay = DateTime.Now;
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
                            linhKien.NamBaoHanh = item.NamBaoHanh;
                            dbContext.LinhKiens.Add(linhKien);
                            dbContext.SaveChanges();

                            LinhKienThietBi linhKienThietBi = new LinhKienThietBi();
                            linhKienThietBi.MaLinhKien = linhKien.MaLinhKien;
                            linhKienThietBi.MaThietBi = thietBi.MaThietBi;
                            linhKienThietBi.Ngay = DateTime.Now;
                            dbContext.LinhKienThietBis.Add(linhKienThietBi);
                            dbContext.SaveChanges();

                            LichSuLinhKien lichSuLinhKien = new LichSuLinhKien();
                            lichSuLinhKien.MaLinhKien = linhKien.MaLinhKien;
                            lichSuLinhKien.MaTinhTrang = 1;
                            lichSuLinhKien.Ngay = DateTime.Now;
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
            //thietBiModel.NhaCungCap = thietBi.NhaCungCap.Ten;
            thietBiModel.NhaCungCap = thietBi.NhaCungCap;
            thietBiModel.MaNhaCungCap = thietBi.MaNhaCungCap;
            //thietBiModel.NgayNhapKho = thietBi.NgayMua;
            //thietBiModel.NgayXuatKho = thietBi.NgayXuatKho;
            thietBiModel.NgayMua = thietBi.NgayMua;
            thietBiModel.Serial = thietBi.Serial;
            thietBiModel.GiaKhauHao = thietBi.GiaKhauHao;
            thietBiModel.GiaTriHopDong = thietBi.GiaTriHopDong;
            thietBiModel.DonViTinh = thietBi.DonViTinh;
            thietBiModel.GhiChu = thietBi.GhiChu;
            thietBiModel.ThoiGianBaoHanh = thietBi.NgayBaoHanh;
            thietBiModel.NgayCapNhat = thietBi.NgayCapNhat;

            // Lấy thêm các trường thông tin cho cttb
            thietBiModel.NhaCungCap = thietBi.NhaCungCap;
            thietBiModel.HangSanXuat = thietBi.HangSanXuat;
            thietBiModel.MaHangSanXuat = thietBi.MaHangSanXuat;
            thietBiModel.ViTri = thietBi.ViTri;
            //var tenUser = dbContext.NguoiSuDungs.Where(x => x.UserName == thietBi.NguoiSuDung).FirstOrDefault();
            //thietBiModel.NguoiSuDung = tenUser.Ten;
            thietBiModel.NguoiSuDung = thietBi.NguoiSuDung;
            thietBiModel.TenPhongBan = thietBi.PhongBan.Ten;
            thietBiModel.MaPhongBan = thietBi.MaPhongBan;
            thietBiModel.MaTinhTrang = thietBi.MaTinhTrang;
            thietBiModel.TenTinhTrang = thietBi.TinhTrang.Ten;
            //var lstb = thietBi.LichSuThietBis.Where(x => x.MaThietBi == thietBi.MaThietBi).OrderByDescending(x => x.ID).Take(1).FirstOrDefault();
            //thietBiModel.NguoiSuDung = lstb == null ? "Đang Cập Nhật..." : lstb.NguoiSuDung.Ten;
            //thietBiModel.NgayHienTrang = lstb?.Ngay;
            //thietBiModel.MaNguoiDung = lstb == null ? 0 : lstb.MaNguoiDung;
            //thietBiModel.MaPhongBan = lstb == null ? thietBi.MaPhongBan : lstb.NguoiSuDung.MaPhongBan;
            //thietBiModel.TenPhongBan = lstb == null ? thietBi.PhongBan.Ten : lstb.NguoiSuDung.PhongBan.Ten;
            //thietBiModel.MaTinhTrang = lstb == null ? 0 : lstb.MaTinhTrang;
            //if (lstb == null)
            //{
            //    thietBiModel.TenTinhTrang = "Đang Cập Nhật...";
            //}
            //else
            //{
            //    var tentt = dbContext.TinhTrangs.Where(x => x.MaTinhTrang == lstb.MaTinhTrang).FirstOrDefault().Ten;
            //    thietBiModel.TenTinhTrang = tentt;
            //}
            thietBiModel.ChiTietTaiSan = thietBi.ChiTietTaiSan;
            thietBiModel.NamBaoHanh = thietBi.NamBaoHanh;
            thietBiModel.SoHopDong = thietBi.SoHopDong;
            thietBiModel.IP = thietBi.IP;
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
                    linhKienModel.MaLoaiLinhKien = linhKien.LoaiLinhKien.MaLoaiLinhKien;
                    linhKienModel.Serial = linhKien.Serial;
                    linhKienModel.MaThietBi = item.ThietBi.MaThietBi;
                    linhKienModel.Model = linhKien.Model;
                    linhKienModel.MaNhaCungCap = linhKien.MaNhaCungCap;
                    linhKienModel.TenNhaCungCap = linhKien.NhaCungCap.Ten;
                    linhKienModel.MaTinhTrang = linhKien.LichSuLinhKiens.Where(x => x.MaLinhKien == item.MaLinhKien).OrderByDescending(x => x.ID).Take(1).FirstOrDefault().MaTinhTrang;
                    linhKienModel.TenTinhTrang = linhKien.LichSuLinhKiens.Where(x => x.MaLinhKien == item.MaLinhKien).OrderByDescending(x => x.ID).Take(1).FirstOrDefault().TinhTrang.Ten;
                    linhKienModel.GhiChu = linhKien.GhiChu;
                    linhKienModel.NamBaoHanh = linhKien.NamBaoHanh;
                    linhKienModel.NhaCungCap = linhKien.NhaCungCap.Ten;
                    DanhSachLinhKien.Add(linhKienModel);
                }
            }
            thietBiModel.DanhSachLinhKien = DanhSachLinhKien;
            // Lịch sử sử dụng của thiết bị
            var lichSuThietBi = dbContext.LichSuThietBis.Where(x => x.MaThietBi == MaThietBi).OrderByDescending(x => x.ID).ToList();
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
                    lichSuThietBiModel.DonVi = nguoiDung.PhongBan.Ten;
                    lichSuThietBiModel.MaDonVi = nguoiDung.PhongBan.MaPhongBan;
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
        [Route("CapNhatThongTinThietBi")]
        public IHttpActionResult CapNhatThongTinThietBi(ChiTietThietBiModel tbModel)
        {
            try
            {
                var tb = dbContext.ThietBis.Where(x => x.MaThietBi == tbModel.MaThietBi).FirstOrDefault();
                tb.MaTaiSan = tbModel.MaTaiSan;
                tb.Ten = tbModel.Ten;
                tb.Gia = tbModel.Gia;
                tb.Model = tbModel.Model;
                tb.MaDanhMuc = tbModel.MaDanhMuc;
                tb.MaHangSanXuat = tbModel.MaNhaCungCap;
                var name = dbContext.NhaCungCaps.Where(x => x.MaNhaCungCap == tbModel.MaNhaCungCap).FirstOrDefault();
                tb.HangSanXuat = name.Ten;
                tb.NhaCungCap = tbModel.NhaCungCap;
                tb.Serial = tbModel.Serial;
                tb.NamBaoHanh = tbModel.NamBaoHanh;
                tb.GhiChu = tbModel.GhiChu;
                tb.NgayMua = tbModel.NgayMua?.AddDays(1);
                tb.NgayCapNhat = tbModel.NgayCapNhat?.AddDays(1);
                tb.NguoiCapNhat = tbModel.NguoiCapNhat;
                tb.MaPhongBan = tbModel.MaPhongBan;
                tb.IP = tbModel.IP;
                tb.SoHopDong = tbModel.SoHopDong;
                tb.NguoiSuDung = tbModel.NguoiSuDung;
                tb.MaTinhTrang = tbModel.MaTinhTrang;
                tb.ViTri = tbModel.ViTri;
                dbContext.SaveChanges();

                //if(tbModel.Check == 0)
                //{
                //LichSuThietBi lstb = new LichSuThietBi();
                //lstb.MaThietBi = tbModel.MaThietBi;
                //lstb.MaTinhTrang = tbModel.MaTinhTrang;
                //lstb.MaNguoiDung = tbModel.MaNguoiDung;
                //lstb.Ngay = tbModel.NgayHienTrangg == null ? tbModel.NgayHienTrangg : tbModel.NgayHienTrangg.Value.AddDays(1);
                //lstb.ChiPhi = tbModel.ChiPhi;
                //dbContext.LichSuThietBis.Add(lstb);
                //dbContext.SaveChanges();
                //}
                return Ok("Cập Nhật Thành Công");
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
                            tbModel.NguoiSuDung = thietBi.NguoiSuDung;
                            tbModel.ViTri = thietBi.ViTri;
                            tbModel.Serial = thietBi.Serial == null ? "Chưa Cập Nhật..." : thietBi.Serial;
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
                        tbModel.NguoiSuDung = thietBi.NguoiSuDung;
                        tbModel.ViTri = thietBi.ViTri;
                        tbModel.Serial = thietBi.Serial == null ? "Chưa Cập Nhật..." : thietBi.Serial;
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
                    tbModel.NguoiSuDung = thietBi.NguoiSuDung;
                    tbModel.ViTri = thietBi.ViTri;
                    tbModel.Serial = thietBi.Serial == null ? "Chưa Cập Nhật..." : thietBi.Serial;
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
                                tbModel.NguoiSuDung = thietBi.NguoiSuDung;
                                tbModel.ViTri = thietBi.ViTri;
                                tbModel.Serial = thietBi.Serial == null ? "Chưa Cập Nhật..." : thietBi.Serial;
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

        [HttpPost]
        [Route("ThemLinhKienThietBi")]
        public IHttpActionResult ThemLinhKienThietBi(LinhKienModel tbModel)
        {
            try
            {
                LinhKien linhKien = new LinhKien();
                linhKien.MaLoaiLinhKien = tbModel.MaLoaiLinhKien;
                linhKien.Serial = tbModel.Serial;
                linhKien.Model = tbModel.Model;
                linhKien.MaNhaCungCap = tbModel.MaNhaCungCap;
                linhKien.GhiChu = tbModel.GhiChu;
                linhKien.NamBaoHanh = tbModel.NamBaoHanh;
                dbContext.LinhKiens.Add(linhKien);
                dbContext.SaveChanges();

                LichSuLinhKien lslk = new LichSuLinhKien();
                lslk.MaLinhKien = linhKien.MaLinhKien;
                lslk.MaTinhTrang = tbModel.MaTinhTrang;
                lslk.Ngay = DateTime.Now;
                lslk.ChiPhi = null;
                dbContext.LichSuLinhKiens.Add(lslk);
                dbContext.SaveChanges();

                LinhKienThietBi lktb = new LinhKienThietBi();
                lktb.MaLinhKien = linhKien.MaLinhKien;
                lktb.MaThietBi = tbModel.MaThietBi;
                lktb.Ngay = DateTime.Now;
                dbContext.LinhKienThietBis.Add(lktb);
                dbContext.SaveChanges();

                return Ok(linhKien.MaLinhKien);
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

        [HttpDelete]
        [Route("XoaLinhKienThietBi")]
        public IHttpActionResult XoaLinhKienThietBi(int MaLinhKien)
        {
            try
            {
                var lslk = dbContext.LichSuLinhKiens.Where(x => x.MaLinhKien == MaLinhKien).ToList();
                if (lslk.Count > 0)
                {
                    foreach (var item in lslk)
                    {
                        dbContext.LichSuLinhKiens.Remove(item);
                        dbContext.SaveChanges();
                    }
                }
                var lktb = dbContext.LinhKienThietBis.Where(x => x.MaLinhKien == MaLinhKien).FirstOrDefault();
                dbContext.LinhKienThietBis.Remove(lktb);
                dbContext.SaveChanges();

                var lk = dbContext.LinhKiens.Where(x => x.MaLinhKien == MaLinhKien).FirstOrDefault();
                dbContext.LinhKiens.Remove(lk);
                dbContext.SaveChanges();
                return Ok("Linh Kiện Đã Được Xóa");
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
        [Route("CapNhatThongTinLinhKien")]
        public IHttpActionResult CapNhatThongTinLinhKien(LinhKienModel tbModel)
        {
            try
            {
                var lk = dbContext.LinhKiens.Where(x => x.MaLinhKien == tbModel.MaLinhKien).FirstOrDefault();
                if (lk != null)
                {
                    lk.MaLoaiLinhKien = tbModel.MaLoaiLinhKien;
                    lk.Serial = tbModel.Serial;
                    lk.Model = tbModel.Model;
                    lk.MaNhaCungCap = tbModel.MaNhaCungCap;
                    lk.GhiChu = tbModel.GhiChu;
                    lk.NamBaoHanh = tbModel.NamBaoHanh;
                    dbContext.SaveChanges();

                    if (tbModel.MaTinhTrang != tbModel.MaTinhTrangCu)
                    {
                        LichSuLinhKien ls = new LichSuLinhKien();
                        ls.MaLinhKien = tbModel.MaLinhKien;
                        ls.MaTinhTrang = tbModel.MaTinhTrang;
                        ls.Ngay = DateTime.Now;
                        dbContext.LichSuLinhKiens.Add(ls);
                        dbContext.SaveChanges();
                    }
                    return Ok("Cập Nhật Thông Tin Linh Kiện Thành Công");
                }
                return BadRequest("Có lỗi,xin vui lòng thử lại");
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

        [HttpDelete]
        [Route("XoaHangSanXuat")]
        public IHttpActionResult XoaHangSanXuat(int MaNhaCungCap)
        {
            try
            {
                var ncc = dbContext.NhaCungCaps.Where(x => x.MaNhaCungCap == MaNhaCungCap).FirstOrDefault();
                if (ncc != null)
                {
                    var tb = dbContext.ThietBis.Where(x => x.MaHangSanXuat == MaNhaCungCap).ToList();
                    if (tb.Count > 0)
                    {
                        return BadRequest("Không thể xóa,hãng sản xuất này đã tồn tại trên các thiết bị");
                    }
                    dbContext.NhaCungCaps.Remove(ncc);
                    dbContext.SaveChanges();
                    return Ok("Xóa hãng sản xuất thành công");
                }
                return BadRequest("Có lỗi xảy ra,xin vui lòng thử lại");
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
        [Route("ThemHangSanXuat")]
        public IHttpActionResult ThemHangSanXuat(HangSanXuatViewModel tbModel)
        {
            try
            {
                NhaCungCap ncc = new NhaCungCap();
                ncc.Ten = tbModel.Ten;
                dbContext.NhaCungCaps.Add(ncc);
                dbContext.SaveChanges();
                return Ok(ncc.MaNhaCungCap);
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
    }
}
