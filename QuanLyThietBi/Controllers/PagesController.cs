﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyThietBi.Models;

namespace QuanLyThietBi.Controllers
{
    public class PagesController : Controller
    {
        QuanLyThietBiEntities dbContext = new QuanLyThietBiEntities();
        // GET: Pages
        public ActionResult Index(int MaDanhMuc)
        {
            ViewBag.MaDanhMuc = MaDanhMuc;
            ViewBag.Status1 = "";
            ViewBag.Status2 = "open active";
            ViewBag.Title = "Quản Lý Thiết Bị";
            ViewBag.Status3 = "open";
            return View();
        }

        public ActionResult HangSanXuat()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public PartialViewResult ModelChiTiet()
        {
            return PartialView();
        }

        public PartialViewResult ModelThem()
        {
            return PartialView();
        }

        public PartialViewResult ModelSua()
        {
            return PartialView();
        }

        public PartialViewResult ModelHSXThem()
        {
            return PartialView();
        }

        public PartialViewResult ModelHSXSua()
        {
            return PartialView();
        }
    }
}