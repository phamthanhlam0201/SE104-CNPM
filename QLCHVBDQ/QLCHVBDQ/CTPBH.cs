//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLCHVBDQ
{
    using System;
    using System.Collections.Generic;
    
    public partial class CTPBH
    {
        public string SoPhieu { get; set; }
        public string MaSP { get; set; }
        public int SoLuong { get; set; }
        public Nullable<double> DonGiaBH { get; set; }
        public Nullable<double> ThanhTien { get; set; }
    
        public virtual PHIEUBANHANG PHIEUBANHANG { get; set; }
        public virtual SANPHAM SANPHAM { get; set; }
    }
}
