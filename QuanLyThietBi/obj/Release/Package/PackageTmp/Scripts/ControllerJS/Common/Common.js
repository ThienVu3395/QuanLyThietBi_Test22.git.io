//angular.module("CommonApp", ['ngSanitize', 'blockUI', 'ngMessages'])
//angular.module("CommonApp", ['ui.bootstrap'])
angular.module("CommonApp", ['ui.bootstrap'])
    //Cấu hình cho blockUI
    //.config(function (blockUIConfig) {
    //    'blockUI',
    //        blockUIConfig.delay = 200;
    //    blockUIConfig.autoBlock = true;
    //    blockUIConfig.blockBrowserNavigation = false;
    //})
    .factory("CommonController", ["$http",
        function ($http) {
            // Tạo UrlAPI động
            var baseURL = window.location.protocol + "//" + window.location.host + "/";
            var appURL = { pathAPI: baseURL };

            // Danh sách các APIs
            var urlAPI = {
                API_LayDanhSachDanhMuc: "API/QuanLyTaiSan/LayDanhSachDanhMuc",
                API_LayDanhSachNhomTheoDanhMuc: "API/QuanLyTaiSan/LayDanhSachNhomTheoDanhMuc",
                API_LayDanhSachThietBi: "API/QuanLyTaiSan/LayDanhSachThietBi",
                API_LayDanhSachThietBi_PhanTrang: "API/QuanLyTaiSan/LayDanhSachThietBi_PhanTrang",
                API_LayDanhSachPhongBan: "API/QuanLyTaiSan/LayDanhSachPhongBan",
                API_LayDanhSachNguoiDungTheoPhongBan: "API/QuanLyTaiSan/LayDanhSachNguoiDungTheoPhongBan",
                API_LayDanhSachHangSanXuat: "API/QuanLyTaiSan/LayDanhSachHangSanXuat",
                API_LayDanhSachHopDong: "API/QuanLyTaiSan/LayDanhSachHopDong",
                API_LayDanhSachTinhTrang: "API/QuanLyTaiSan/LayDanhSachTinhTrang",
                API_LayDanhSachLoaiThietBi: "API/QuanLyTaiSan/LayDanhSachLinhKien",
                API_ThemThietBi: "API/QuanLyTaiSan/ThemThietBi",
                API_LayChiTietThietBi: "API/QuanLyTaiSan/LayChiTietThietBi",
                API_XoaThietBi: "API/QuanLyTaiSan/XoaThietBi",
                // API lọc thiết bị theo các điều kiện
                API_LocThietBi: "API/QuanLyTaiSan/LocThietBi",
                API_LocThietBi_All: "API/QuanLyTaiSan/LocThietBi_All",
                API_LocThietBi_TheoTinhTrang: "API/QuanLyTaiSan/LocThietBi_All",
                API_LocThietBi_TheoPhongBan: "API/QuanLyTaiSan/LocThietBi_TheoPhongBan",
                // API đăng nhập / đăng xuất / đăng ký
                API_DangNhap: "API/QuanLyTaiSan/DangNhap",
                API_KiemTraNguoiDung: "API/QuanLyTaiSan/KiemTraNguoiDung"
            };

            // Hàm Tạo Ra Mã Xác Nhận Ngẫu Nhiên 
            //this.getCaptcha = () => {
            //    var result = "";
            //    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
            //    var charactersLength = characters.length;
            //    for (var i = 0; i < 6; i++) {
            //        var j = parseInt(Math.random() * 10000);
            //        result += characters.charAt(j % charactersLength);
            //    }
            //    return result;
            //};

            // Hàm Get API
            this.getData = (urlAPI, Param) => {
                var res = $http({
                    url: appURL.pathAPI + urlAPI + Param,
                    method: 'GET'
                });
                return res;
            };

            // Hàm Post API
            this.postData = (urlAPI, Data) => {
                var res = $http({
                    url: appURL.pathAPI + urlAPI,
                    method: 'POST',
                    data: Data,
                    headers: { 'Content-Type': 'application/json' }
                })
                return res
            };

            // Hàm Delete API   
            this.deleteData = (urlAPI, param) => {
                var res = $http({
                    url: appURL.pathAPI + urlAPI + param,
                    method: 'DELETE'
                })
                return res;
            }

            return {
                getData: this.getData,
                postData: this.postData,
                deleteData: this.deleteData,
                urlAPI: urlAPI,
                //captcha: this.getCaptcha,
            }
        }]);