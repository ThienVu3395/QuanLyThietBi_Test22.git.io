angular.module("CommonApp")
    .controller("QltbCrl", function ($scope, CommonController) {
        //Lấy danh sách danh mục
        $scope.LayDanhSachDanhMuc = function () {
            var res = CommonController.getData(CommonController.urlAPI.API_LayDanhSachDanhMuc, "");
            res.then(
                function succ(response) {
                    $scope.DanhSachDanhMuc = response.data;
                    var param = "?MaDanhMuc=" + $scope.DanhSachDanhMuc[0].MaDanhMuc;
                    var res = CommonController.getData(CommonController.urlAPI.API_LayDanhSachNhomTheoDanhMuc, param);
                    res.then(
                        function succ(response) {
                            $scope.DanhSachNhom = response.data;
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
        $scope.ThietBi = { Serial: "", DonViTinh: "Cm", GhiChu: "", GiaTriHD: "", MaTaiSan: "", DanhSachLinhKien: [] };

        $scope.LayDanhSachNhom = function (MaDanhMuc) {
            var param = "?MaDanhMuc=" + MaDanhMuc;
            var res = CommonController.getData(CommonController.urlAPI.API_LayDanhSachNhomTheoDanhMuc, param);
            res.then(
                function succ(response) {
                    $scope.DanhSachNhom = response.data;
                    if ($scope.DanhSachNhom.length !== 0) {
                        $scope.ThietBi.MaDanhMuc = $scope.DanhSachNhom[0].MaDanhMuc;
                    }
                    else $scope.ThietBi.MaDanhMuc = MaDanhMuc;
                },

                function errorCallback(response) {
                    console.log(response.data.Message);
                }
            )
        }

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
                    var param = "?MaPhongBan=" + $scope.DanhSachPhongBan[0].MaPhongBan;
                    var res2 = CommonController.getData(CommonController.urlAPI.API_LayDanhSachNguoiDungTheoPhongBan, param);
                    res2.then(
                        function succ(response) {
                            $scope.DanhSachNguoiDung = response.data;
                            $scope.ThietBi.MaNguoiDung = $scope.DanhSachNguoiDung[0].MaNguoiDung
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

        // Lấy Danh Sách Nhà Sản Xuất
        $scope.LayDanhSachHangSanXuat = function () {
            var res = CommonController.getData(CommonController.urlAPI.API_LayDanhSachHangSanXuat, "");
            res.then(
                function succ(response) {
                    $scope.DanhSachHSX = response.data;
                    $scope.ThietBi.MaNhaCungCap = $scope.DanhSachHSX[0].MaNhaCungCap;
                },

                function errorCallback(response) {
                    console.log(response.data.Message);
                }
            )
        }

        // Lấy Danh Sách Người Dùng
        $scope.LayDanhSachNguoiDung = function (MaPhongBan) {
            var param = "?MaPhongBan=" + MaPhongBan;
            var res = CommonController.getData(CommonController.urlAPI.API_LayDanhSachNguoiDungTheoPhongBan, param);
            res.then(
                function succ(response) {
                    $scope.DanhSachNguoiDung = response.data;
                    $scope.ThietBi.MaNguoiDung = $scope.DanhSachNguoiDung[0].MaNguoiDung
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
                    $scope.ThietBi.MaTinhTrang = $scope.DanhSachTinhTrang[0].MaTinhTrang;
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
                    $scope.ThietBi.MaHopDong = $scope.DanhSachHopDong[0].MaHopDong;
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
        $scope.ChonNhom = function (MaDanhMuc) {
            $scope.ThietBi.MaDanhMuc = MaDanhMuc;
        }

        $scope.ChonHangSanXuat = function (MaNhaCungCap) {
            $scope.ThietBi.MaNhaCungCap = MaNhaCungCap;
        }

        $scope.ChonHopDong = function (MaHopDong) {
            $scope.ThietBi.MaHopDong = MaHopDong;
        }

        $scope.ChonNguoiDung = function (MaNguoiDung) {
            $scope.ThietBi.MaNguoiDung = MaNguoiDung;
        }

        $scope.ChonDonViTinh = function (DonViTinh) {
            $scope.ThietBi.DonViTinh = DonViTinh;
        }

        $scope.ChonTinhTrang = function (MaTinhTrang) {
            $scope.ThietBi.MaTinhTrang = MaTinhTrang;
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

        // Thêm Thiết Bị
        $scope.ThemThietBi = function () {
            $scope.listDanhSachThietBi = [];
            if ($scope.SoLuongThietBi == 1) {
                $scope.ThietBi.Serial = document.getElementById("Seri").value;
                $scope.listDanhSachThietBi.push($scope.ThietBi);
            }
            else {
                for (let i = 0; i < $scope.SoLuongThietBi; i++) {
                    $scope.ThietBiMoi = { ...$scope.ThietBi };
                    $scope.ThietBiMoi.Serial = document.getElementById("Serial" + (i + 1)).value;
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

        // Xem chi tiết thiết bị
        $scope.LayThongTinChiTiet = function (MaThietBi) {
            var param = "?MaThietBi=" + MaThietBi;
            var res = CommonController.getData(CommonController.urlAPI.API_LayChiTietThietBi, param);
            res.then(
                function succ(response) {
                    $scope.ThongTinThietBi = response.data;
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

        // Sửa thiết bị
        $scope.SuaThietBi = function (MaThietBi) {
            alert(MaThietBi);
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