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
    
    public partial class LOAISANPHAM
    {
        public LOAISANPHAM()
        {
            this.SANPHAMs = new HashSet<SANPHAM>();
        }
    
        public string MaLSP { get; set; }
        public string TenLSP { get; set; }
        public string MaDVT { get; set; }
        public int PhanTramLoiNhuan { get; set; }
    
        public virtual DVT DVT { get; set; }
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
