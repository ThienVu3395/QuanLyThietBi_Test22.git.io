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
    
    public partial class LinhKienThietBi
    {
        public int MaLinhKien { get; set; }
        public int MaThietBi { get; set; }
        public Nullable<System.DateTime> Ngay { get; set; }
    
        public virtual ThietBi ThietBi { get; set; }
        public virtual LinhKien LinhKien { get; set; }
    }
}
