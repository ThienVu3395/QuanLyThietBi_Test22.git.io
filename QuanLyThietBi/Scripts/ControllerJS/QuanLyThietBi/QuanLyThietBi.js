﻿angular.module("CommonApp")
    .controller("QltbCrl", function ($scope, CommonController) {
        //Lấy danh sách danh mục
        $scope.LayDanhSachDanhMuc = function () {
            var res = CommonController.getData(CommonController.urlAPI.API_LayDanhSachDanhMuc, "");
            res.then(
                function succ(response) {
                    $scope.DanhSachDanhMuc = response.data;
                    $scope.MaCha = $scope.DanhSachDanhMuc[0];
                    var param = "?MaDanhMuc=" + $scope.DanhSachDanhMuc[0].MaDanhMuc;
                    var res = CommonController.getData(CommonController.urlAPI.API_LayDanhSachNhomTheoDanhMuc, param);
                    res.then(
                        function succ(response) {
                            $scope.DanhSachNhom = response.data;
                            $scope.MaCon = $scope.DanhSachNhom[0]
                            $scope.ThietBi.MaDanhMuc = $scope.DanhSachNhom[0].MaDanhMuc;
                        },

                        function errorCallback(response) {
                            console.log(response.data.Message);
                        }
                    )
                },

                function errorCallback(response) {
                    console.log(response.data.message)
                }
            )
        }

        $scope.getUser = function (index, tt) {
            let mpb = 1;
            if (tt == "sua") {
                mpb = document.getElementById("Mpb" + index).value;
            }
            else if (tt == "none") {
                mpb = document.getElementById("MaPhongBan" + index).value;
            }
            var param = "?MaPhongBan=" + mpb;
            var res = CommonController.getData(CommonController.urlAPI.API_LayDanhSachNguoiDungTheoPhongBan, param);
            res.then(
                function succ(response) {
                    let content = ``;
                    for (var i = 0; i < response.data.length; i++) {
                        content += "<option value='" + response.data[i].MaNguoiDung + "'>" + response.data[i].Ten + "</option>";
                    }
                    if (tt == "sua") {
                        document.getElementById("listUser" + index).innerHTML = content;
                    }
                    else if (tt == "none") {
                        document.getElementById("MaNguoiDung" + index).innerHTML = content;
                    }                
                },

                function errorCallback(response) {
                    console.log(response.data.Message);
                }
            )
        }

        // Lấy Danh Sách thiết bị
        $scope.currentPage = 1;
        $scope.itemsPerPage = 10;

        $scope.setPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };

        $scope.MaDanhMuc = 0;
        $scope.LayDanhSachThietBi = function (MaDanhMuc) {
            $scope.MaDanhMuc = MaDanhMuc;
            var param = "?MaDanhMuc=" + MaDanhMuc;
            var res = CommonController.getData(CommonController.urlAPI.API_LayDanhSachThietBi, param);
            res.then(
                function succ(response) {
                    $scope.DanhSachThietBi = response.data;
                    $scope.totalItems = $scope.DanhSachThietBi.length;
                    $scope.DanhSachThietBi.splice($scope.itemsPerPage, $scope.DanhSachThietBi.length);
                },

                function errorCallback(response) {
                    swal(response.data.Message, "", "error");
                }
            )
        }

        // Lấy danh sách thiết bị phân trang
        $scope.pageChanged = function () {
            //console.log($scope.MaDanhMuc)
            var offset = ($scope.currentPage - 1) * $scope.itemsPerPage;
            var param = "?offset=" + offset + "&limit=" + $scope.itemsPerPage;
            var res = CommonController.getData(CommonController.urlAPI.API_LayDanhSachThietBi_PhanTrang, param);
            res.then(
                function succ(response) {
                    $scope.DanhSachThietBi = response.data;
                },

                function errorCallback(response) {
                    console.log(response.data.Message);
                }
            )
        };

        // Lấy Danh Sách nhóm thiết bị
        $scope.SoLuongThietBi = 1;
        $scope.ThietBi = { Serial: "", DonViTinh: "Cm", GhiChu: "", SoHopDong: "", Gia: "", GiaTriHD: "", NgayKy: "", NgayNhapKho: "", ThoiGianBaoHanh: "", MaTaiSan: "", DanhSachLinhKien: [] };

        // Lấy Danh Sách Phòng Ban
        $scope.LayDanhSachPhongBan = function () {
            if (sessionStorage.length == 0) {
                var url = "http://" + window.location.host;
                window.location.href = url;
            }
            var res = CommonController.getData(CommonController.urlAPI.API_LayDanhSachPhongBan, "");
            res.then(
                function succ(response) {
                    $scope.DanhSachPhongBan = response.data;
                    $scope.DonVi = $scope.DanhSachPhongBan[0];
                    var param = "?MaPhongBan=" + $scope.DanhSachPhongBan[0].MaPhongBan;
                    var res2 = CommonController.getData(CommonController.urlAPI.API_LayDanhSachNguoiDungTheoPhongBan, param);
                    res2.then(
                        function succ(response) {
                            $scope.DanhSachNguoiDung = response.data;
                            $scope.MaNguoiDung = $scope.DanhSachNguoiDung[0];
                            $scope.ThietBi.MaNguoiDung = $scope.MaNguoiDung.MaNguoiDung;
                        },

                        function errorCallback(response) {
                            console.log(response.data.Message);
                        }
                    )
                },

                function errorCallback(response) {
                    console.log(response.data.Message);
                }
            )
        }

        // Lấy Danh Sách Người Dùng Theo Phòng Ban
        $scope.LayNguoiDung = function () {
            let maPhongBan = $scope.DonVi.MaPhongBan;
            var param = "?MaPhongBan=" + maPhongBan;
            var res = CommonController.getData(CommonController.urlAPI.API_LayDanhSachNguoiDungTheoPhongBan, param);
            res.then(
                function succ(response) {
                    $scope.DanhSachNguoiDung = response.data;
                    $scope.MaNguoiDung = $scope.DanhSachNguoiDung[0];
                    $scope.ThietBi.MaNguoiDung = $scope.MaNguoiDung.MaNguoiDung;
                },

                function errorCallback(response) {
                    console.log(response.data.Message);
                }
            )
        }

        // Chọn Người Dùng
        $scope.ChonNguoiDung = function () {
            $scope.ThietBi.MaNguoiDung = $scope.MaNguoiDung.MaNguoiDung;
        }

        // Lấy Danh Sách Nhà Sản Xuất
        $scope.LayDanhSachHangSanXuat = function () {
            var res = CommonController.getData(CommonController.urlAPI.API_LayDanhSachHangSanXuat, "");
            res.then(
                function succ(response) {
                    $scope.DanhSachHSX = response.data;
                    $scope.MaNhaCungCap = $scope.DanhSachHSX[0];
                },

                function errorCallback(response) {
                    console.log(response.data.Message);
                }
            )
        }

        // Lấy Danh Sách Tình Trạng
        $scope.LayDanhSachTinhTrang = function () {
            var res = CommonController.getData(CommonController.urlAPI.API_LayDanhSachTinhTrang, "");
            res.then(
                function succ(response) {
                    $scope.DanhSachTinhTrang = response.data;
                    $scope.MaTinhTrang = $scope.DanhSachTinhTrang[0];
                },

                function errorCallback(response) {
                    console.log(response.data.Message);
                }
            )
        }

        // Lấy Danh Sách Hợp Đồng
        $scope.LayDanhSachHopDong = function () {
            var res = CommonController.getData(CommonController.urlAPI.API_LayDanhSachHopDong, "");
            res.then(
                function succ(response) {
                    $scope.DanhSachHopDong = response.data;
                    $scope.MaHopDong = $scope.DanhSachHopDong[0];
                },

                function errorCallback(response) {
                    console.log(response.data.Message);
                }
            )
        }

        // Láy Danh Sách Loại Linh Kiện
        $scope.LayDanhSachLoaiLinhKien = function () {
            var res = CommonController.getData(CommonController.urlAPI.API_LayDanhSachLoaiThietBi, "");
            res.then(
                function succ(response) {
                    $scope.DanhSachLoaiLinhKien = response.data;
                },

                function errorCallback(response) {
                    console.log(response.data.Message);
                }
            )
        }

        // Các sự kiện lấy thông tin từ các list menu :
        $scope.ChonDonViTinh = function (DonViTinh) {
            $scope.ThietBi.DonViTinh = DonViTinh;
        }

        // Thêm Linh Kiện
        $scope.ThemLinhKien = function () {
            let LinhKien = {};
            LinhKien.MaLoaiLinhKien = document.getElementById("LoaiLinhKien").value;
            LinhKien.TenLoaiLinhKien = $scope.DanhSachLoaiLinhKien[LinhKien.MaLoaiLinhKien - 1].TenLinhKien;
            LinhKien.Serial = document.getElementById("Serial").value;
            LinhKien.Model = document.getElementById("Model").value;
            LinhKien.MaNhaCungCap = document.getElementById("MaNhaCungCap").value;
            LinhKien.TenNhaCungCap = $scope.DanhSachHSX[LinhKien.MaNhaCungCap - 1].Ten;
            LinhKien.GhiChu = document.getElementById("GhiChu").value;
            $scope.ThietBi.DanhSachLinhKien.push(LinhKien);
            document.getElementById("Serial").value = "";
            document.getElementById("Model").value = "";
            document.getElementById("GhiChu").value = "";
        }

        // Xóa Linh Kiện
        $scope.XoaLinhKien = function (index) {
            $scope.ThietBi.DanhSachLinhKien.splice(index, 1);
        }

        // Lấy Thông Tin Linh Kiện
        $scope.suaLinhKien = false;
        $scope.viTriLinhKien = 0;
        $scope.LayThongTinLinhKien = function (index) {
            $scope.suaLinhKien = true;
            $scope.viTriLinhKien = index;
            document.getElementById("LoaiLinhKien").value = $scope.ThietBi.DanhSachLinhKien[index].MaLoaiLinhKien;
            document.getElementById("Serial").value = $scope.ThietBi.DanhSachLinhKien[index].Serial;
            document.getElementById("Model").value = $scope.ThietBi.DanhSachLinhKien[index].Model;
            document.getElementById("GhiChu").value = $scope.ThietBi.DanhSachLinhKien[index].GhiChu;
            document.getElementById("MaNhaCungCap").value = $scope.ThietBi.DanhSachLinhKien[index].MaNhaCungCap;
        }

        // Sửa Linh Kiện
        $scope.SuaLinhKien = function () {
            $scope.ThietBi.DanhSachLinhKien[$scope.viTriLinhKien].MaLoaiLinhKien = document.getElementById("LoaiLinhKien").value;
            let ma = $scope.ThietBi.DanhSachLinhKien[$scope.viTriLinhKien].MaLoaiLinhKien;
            $scope.ThietBi.DanhSachLinhKien[$scope.viTriLinhKien].TenLoaiLinhKien = $scope.DanhSachLoaiLinhKien[ma - 1].TenLinhKien;
            $scope.ThietBi.DanhSachLinhKien[$scope.viTriLinhKien].Serial = document.getElementById("Serial").value;
            $scope.ThietBi.DanhSachLinhKien[$scope.viTriLinhKien].Model = document.getElementById("Model").value;
            $scope.ThietBi.DanhSachLinhKien[$scope.viTriLinhKien].MaNhaCungCap = document.getElementById("MaNhaCungCap").value;
            let ncc = $scope.ThietBi.DanhSachLinhKien[$scope.viTriLinhKien].MaNhaCungCap;
            $scope.ThietBi.DanhSachLinhKien[$scope.viTriLinhKien].TenNhaCungCap = $scope.DanhSachHSX[ncc - 1].Ten;
            $scope.ThietBi.DanhSachLinhKien[$scope.viTriLinhKien].GhiChu = document.getElementById("GhiChu").value;
        }

        // Hủy Sửa Linh Kiện 
        $scope.huySua = function () {
            $scope.suaLinhKien = false;
            document.getElementById("Serial").value = "";
            document.getElementById("Model").value = "";
            document.getElementById("GhiChu").value = "";
        }

        // Show dữ liệu serial theo số lượng
        $scope.showSerial = function () {
            if ($scope.SoLuongThietBi > 1) {
                $scope.ThietBi.Serial = "Nhập các số serial bên dưới...";

            }
            else $scope.ThietBi.Serial = "";
        }

        // Render người dùng theo phòng ban với số lượng > 1
        $scope.renderNguoiDung = function (event) {
            let pb = event.target.parentNode;
            let mpb = document.getElementById(pb.id).value;
            var param = "?MaPhongBan=" + mpb;
            var res = CommonController.getData(CommonController.urlAPI.API_LayDanhSachNguoiDungTheoPhongBan, param);
            res.then(
                function succ(response) {
                    let dsUser = response.data;
                    let content = ``;
                    for (let j = 0; j < dsUser.length; j++) {
                        content += `<option value=${dsUser[j].MaNguoiDung}>
                                            ${dsUser[j].Ten}
                                        </option>`;
                    }
                    document.getElementById("MaNguoiDung" + pb.name).innerHTML = content;
                },

                function errorCallback(response) {
                    console.log(response.data.Message);
                }
            )
        }

        // Thêm Thiết Bị
        $scope.ThemThietBi = function () {
            $scope.listDanhSachThietBi = [];
            $scope.ThietBi.MaNhaCungCap = $scope.MaNhaCungCap.MaNhaCungCap;
            $scope.ThietBi.MaHopDong = $scope.MaHopDong.MaHopDong;
            if ($scope.SoLuongThietBi == 1) {
                $scope.ThietBi.Serial = document.getElementById("Seri").value;
                $scope.ThietBi.MaTinhTrang = $scope.MaTinhTrang.MaTinhTrang;
                $scope.listDanhSachThietBi.push($scope.ThietBi);
            }
            else {
                for (let i = 0; i < $scope.SoLuongThietBi; i++) {
                    $scope.ThietBiMoi = { ...$scope.ThietBi };
                    $scope.ThietBiMoi.Serial = document.getElementById("Serial" + (i + 1)).value;
                    $scope.ThietBiMoi.MaTaiSan = document.getElementById("MaTaiSan" + (i + 1)).value;
                    $scope.ThietBiMoi.MaTinhTrang = document.getElementById("MaTinhTrang" + (i + 1)).value;
                    $scope.ThietBiMoi.MaNguoiDung = document.getElementById("MaNguoiDung" + (i + 1)).value;
                    $scope.listDanhSachThietBi.push($scope.ThietBiMoi);
                }
            }
            console.log($scope.listDanhSachThietBi);
            let closeBtn = document.getElementById("closeBTN");
            var res = CommonController.postData(CommonController.urlAPI.API_ThemThietBi, $scope.listDanhSachThietBi);
            res.then(
                function succ(response) {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: response.data,
                        showConfirmButton: false,
                        timer: 1500
                    })
                    closeBtn.click();
                    var param = "?MaDanhMuc=" + $scope.MaDanhMuc;
                    var ress = CommonController.getData(CommonController.urlAPI.API_LayDanhSachThietBi, param);
                    ress.then(
                        function succ(response) {
                            $scope.DanhSachThietBi = response.data;
                            $scope.totalItems = $scope.DanhSachThietBi.length;
                            $scope.DanhSachThietBi.splice($scope.itemsPerPage, $scope.DanhSachThietBi.length);
                        },

                        function errorCallback(response) {
                            swal(response.data.Message, "", "error");
                        }
                    )
                },

                function errorCallback(response) {
                    console.log(response.data.Message);
                }
            )
        }

        $scope.LayDsNhom = function (tt) {
            let maCha = $scope.MaCha.MaDanhMuc;
            if (tt == 'sua') {
                maCha = $scope.MaChaSua.MaDanhMuc;
            }
            var param = "?MaDanhMuc=" + maCha;
            var res = CommonController.getData(CommonController.urlAPI.API_LayDanhSachNhomTheoDanhMuc, param);
            res.then(
                function succ(response) {
                    if (tt == 'sua') {
                        $scope.DanhSachNhomSua = response.data;
                        if ($scope.DanhSachNhomSua.length == 0) {
                            $scope.DanhSachNhomSua = [{
                                MaDanhMuc: 0,
                                Ten: "-- Không --",
                                SoLuong: 50,
                                ParentID: 0,
                            }]
                            $scope.MaConSua = $scope.DanhSachNhomSua[0];
                            $scope.ThongTinThietBi.MaDanhMuc = maCha;
                        }
                        else {
                            $scope.MaConSua = $scope.DanhSachNhomSua[0];
                            $scope.ThongTinThietBi.MaDanhMuc = $scope.MaConSua.MaDanhMuc;
                        }
                    }

                    else {
                        $scope.DanhSachNhom = response.data;
                        if ($scope.DanhSachNhom.length == 0) {
                            $scope.DanhSachNhom = [{
                                MaDanhMuc: 0,
                                Ten: "-- Không --",
                                SoLuong: 50,
                                ParentID: 0,
                            }]
                            $scope.ThietBi.MaDanhMuc = maCha;
                            $scope.MaCon = $scope.DanhSachNhom[0];
                        }
                        else {
                            $scope.MaCon = $scope.DanhSachNhom[0];
                            $scope.ThietBi.MaDanhMuc = $scope.MaCon.MaDanhMuc;
                        }
                    }
                },

                function errorCallback(response) {
                    console.log(response.data.Message);
                }
            )
        }

        $scope.ChonDanhMuc = function () {
            $scope.ThietBi.MaDanhMuc = $scope.MaCon.MaDanhMuc;
        }

        // Xem chi tiết thiết bị
        $scope.LayThongTinChiTiet = function (MaThietBi) {
            var param = "?MaThietBi=" + MaThietBi;
            var res = CommonController.getData(CommonController.urlAPI.API_LayChiTietThietBi, param);
            res.then(
                function succ(response) {
                    $scope.ThongTinThietBi = response.data;
                    console.log($scope.ThongTinThietBi);
                    //for (let index = 0; index < $scope.ThongTinThietBi.LichSuThietBi.length; index++) {
                    //    let i = $scope.DanhSachPhongBan.findIndex(x => x.MaPhongBan == $scope.ThongTinThietBi.LichSuThietBi[index].MaDonVi);
                    //    $scope.ThongTinThietBi.LichSuThietBi[index].MaDonVi = $scope.DanhSachPhongBan[i];
                    //    var param = "?MaPhongBan=" + $scope.ThongTinThietBi.LichSuThietBi[index].MaDonVi.MaPhongBan;
                    //    var res = CommonController.getData(CommonController.urlAPI.API_LayDanhSachNguoiDungTheoPhongBan, param);
                    //    res.then(
                    //        function succ(response) {
                    //            let content = ``;
                    //            for (var i = 0; i < response.data.length; i++) {
                    //                content += "<option value='" + response.data[i].MaNguoiDung + "'>" + response.data[i].Ten + "</option>";
                    //            }
                    //            document.getElementById("listUser" + index).innerHTML = content;
                    //            document.getElementById("listUser" + index).value = $scope.ThongTinThietBi.LichSuThietBi[index].MaNguoiDung;
                    //            document.getElementById("tinhTrang" + index).value = $scope.ThongTinThietBi.LichSuThietBi[index].MaTinhTrang;
                    //        },

                    //        function errorCallback(response) {
                    //            console.log(response.data.Message);
                    //        }
                    //    )
                    //}
                    let index = $scope.DanhSachHSX.findIndex(x => x.MaNhaCungCap == $scope.ThongTinThietBi.MaNhaCungCap);
                    $scope.MaNCC = $scope.DanhSachHSX[index];
                    $scope.indexCha = $scope.DanhSachDanhMuc.findIndex(x => x.MaDanhMuc == $scope.ThongTinThietBi.MaDanhMucCha);
                    $scope.MaChaSua = $scope.DanhSachDanhMuc[$scope.indexCha];
                    var param = "?MaDanhMuc=" + $scope.ThongTinThietBi.MaDanhMucCha;
                    var res = CommonController.getData(CommonController.urlAPI.API_LayDanhSachNhomTheoDanhMuc, param);
                    res.then(
                        function succ(response) {
                            $scope.DanhSachNhomSua = response.data;
                            if ($scope.DanhSachNhomSua.length == 0) {
                                $scope.DanhSachNhomSua = [{
                                    MaDanhMuc: 0,
                                    Ten: "-- Không --",
                                    SoLuong: 50,
                                    ParentID: 0,
                                }]
                                $scope.MaConSua = $scope.DanhSachNhomSua[0];
                            }
                            else {
                                $scope.indexCon = $scope.DanhSachNhomSua.findIndex(x => x.MaDanhMuc == $scope.ThongTinThietBi.MaDanhMuc);
                                $scope.MaConSua = $scope.DanhSachNhomSua[$scope.indexCon];
                            }
                        },

                        function errorCallback(response) {
                            console.log(response.data.Message);
                        }
                    )
                },

                function errorCallback(response) {
                    console.log(response.data.Message);
                }
            )
        }

        // Xóa thiết bị
        $scope.XoaThietBi = function (MaThietBi, index) {
            Swal.fire({
                title: 'Bạn chắc chắn chứ ?',
                text: "Thiết bị sẽ không thể phục hồi",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Chắc chắn !'
            }).then((result) => {
                if (result.value) {
                    var param = "?MaThietBi=" + MaThietBi;
                    var res = CommonController.deleteData(CommonController.urlAPI.API_XoaThietBi, param);
                    res.then(
                        function succ(response) {
                            $scope.DanhSachThietBi.splice(index, 1);
                            Swal.fire(
                                'Thành Công',
                                response.data,
                                'success'
                            )
                            $scope.totalItems = $scope.DanhSachThietBi.length;
                        },

                        function errorCallback(response) {
                            console.log(response.data.Message);
                            Swal.fire(
                                'Có Lỗi',
                                response.data.Message,
                                'error'
                            )
                        }
                    )
                }
            })
        }

        // Thêm Thông Tin Update
        $scope.ThemThongTin = function (tt) {
            if (tt == "lstb") {
                $scope.lichSu = {
                    MaThietBi: $scope.ThongTinThietBi.MaThietBi,
                    MaTinhTrang: document.getElementById("tts").value,
                    MaNguoiDung: document.getElementById("nds").value,
                    MaDonVi: document.getElementById("pb").value,
                    ChiPhi: document.getElementById("cp").value,
                }
                $scope.ThongTinThietBi.LichSuThietBi.push($scope.lichSu);
            }
        }

        // Sửa thiết bị
        $scope.CapNhatTB = function (trangThai) {
            $scope.ThongTinThietBi.TrangThai = trangThai;
            let API = CommonController.urlAPI.API_CapNhatThietBi;

            if (trangThai == 'chung') {
                $scope.ThongTinThietBi.MaNhaCungCap = $scope.MaNCC.MaNhaCungCap;
                if ($scope.MaConSua.MaDanhMuc != 0) {
                    $scope.ThongTinThietBi.MaDanhMuc = $scope.MaConSua.MaDanhMuc;
                }
                console.log($scope.ThongTinThietBi);
            }

            else if (trangThai == 'lstb') {
                console.log($scope.ThongTinThietBi.LichSuThietBi);
                return;
            }

            var res = CommonController.postData(API, $scope.ThongTinThietBi);
            res.then(
                function succ(response) {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: response.data,
                        showConfirmButton: false,
                        timer: 1500
                    })
                    $scope.LayDanhSachThietBi($scope.MaDanhMuc);
                },

                function errorCallback(response) {
                    Swal.fire(response.data.Message, 'Xin vui lòng thử lại', 'error');
                }
            )

        }

        // Lọc thiết bị
        $scope.objLoc = { MaPhongBan: 0, MaTinhTrang: 0 }

        $scope.ChonDonVi = function (MaPhongBan) {
            $scope.objLoc.MaPhongBan = MaPhongBan;
        }

        $scope.ChonTinhTrang = function (MaTinhTrang) {
            $scope.objLoc.MaTinhTrang = MaTinhTrang;
        }

        $scope.LocThietBi = function () {
            var urlAPI = "";
            if ($scope.objLoc.MaPhongBan === 0 && $scope.objLoc.MaTinhTrang !== 0) {
                urlAPI += CommonController.urlAPI.API_LocThietBi_TheoTinhTrang;
            }
            else if ($scope.objLoc.MaPhongBan !== 0 && $scope.objLoc.MaTinhTrang === 0) {
                urlAPI += CommonController.urlAPI.API_LocThietBi_TheoPhongBan;
            }
            else if ($scope.objLoc.MaPhongBan === 0 && $scope.objLoc.MaTinhTrang === 0) {
                urlAPI += CommonController.urlAPI.API_LocThietBi_All;
            }
            else {
                urlAPI += CommonController.urlAPI.API_LocThietBi;
            }
            var res = CommonController.postData(urlAPI, $scope.objLoc);
            res.then(
                function succ(response) {
                    $scope.DanhSachThietBi = response.data;
                    $scope.totalItems = $scope.DanhSachThietBi.length;
                    if ($scope.DanhSachThietBi.length > $scope.itemsPerPage) {
                        $scope.DanhSachThietBi.splice($scope.itemsPerPage, $scope.DanhSachThietBi.length);
                    }
                },

                function errorCallback(response) {
                    console.log(response.data.Message);
                }
            )
        }

        // Đăng Nhập
        $scope.xemMatKhau = function () {
            var x = document.getElementById("PassDN");
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }

        $scope.TrangThaiDangNhap = sessionStorage.length;

        $scope.KiemTra = function () {
            if ($scope.TrangThaiDangNhap > 0) {
                $scope.ThongTinNguoiDung = JSON.parse(sessionStorage.getItem("UserID"));
            }
        }

        $scope.DangNhap = function () {
            var res = CommonController.postData(CommonController.urlAPI.API_DangNhap, $scope.DN);
            res.then(
                function succ(response) {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: "Đăng Nhập Thành Công",
                        showConfirmButton: false,
                        timer: 1500
                    })
                    sessionStorage.setItem("UserID", JSON.stringify(response.data));
                    $scope.ThongTinNguoiDung = JSON.parse(sessionStorage.getItem("UserID"));
                    $scope.TrangThaiDangNhap = sessionStorage.length;
                    var uri = "http://" + window.location.host + "/Pages/Index?MaDanhMuc=-1";
                    setTimeout(() => { window.location.href = uri; }, 500);
                },

                function errorCallback(response) {
                    Swal.fire(response.data.Message, 'Xin vui lòng thử lại', 'error');
                }
            )
        }
    })