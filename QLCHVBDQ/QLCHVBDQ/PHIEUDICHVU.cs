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
    
    public partial class PHIEUDICHVU
    {
        public PHIEUDICHVU()
        {
            this.CTPDVs = new HashSet<CTPDV>();
        }
    
        public string SoPhieu { get; set; }
        public System.DateTime NgayLap { get; set; }
        public string MaKH { get; set; }
        public Nullable<double> TongTien { get; set; }
        public Nullable<double> TongTraTruoc { get; set; }
        public Nullable<double> TongConLai { get; set; }
        public Nullable<bool> TinhTrang { get; set; }
    
        public virtual ICollection<CTPDV> CTPDVs { get; set; }
        public virtual KHACHHANG KHACHHANG { get; set; }
    }
}
