using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHVBDQ.DAO
{
    class KhachHangDAO
    {
        private static KhachHangDAO instance;
        public static KhachHangDAO Instance
        {
            get { if (instance == null) instance = new KhachHangDAO(); return instance; }
            set => Instance = value;
        }
        private KhachHangDAO() { }

        public int checkKhachHang(string TenKH, string SDT)
        {
            string query = String.Format("select count(MaKH) from KHACHHANG where TenKH = N'{0}' and SDT= '{1}'", TenKH, SDT);
            string data_SoPhieu = DataProvider.Instance.ExecuteScalar(query).ToString();                    
            int count_SoPhieu = int.Parse(data_SoPhieu);
            return count_SoPhieu;
        }
        public int countkKhachHang()
        {
            string query = String.Format("select count(MaKH) from KHACHHANG");
            string data_SoPhieu = DataProvider.Instance.ExecuteScalar(query).ToString();
            int count_SoPhieu = int.Parse(data_SoPhieu);
            return count_SoPhieu;
        }
        public void insertkKhachHang(string MaKH, string TenKH, string SDT)
        {
            string query = String.Format("Insert into KHACHHANG Values('{0}', N'{1}', N'{2}')", MaKH, TenKH, SDT);
            DataProvider.Instance.ExecuteNonQuery(query);
        }
        public DataTable get_MaKH_Ten_SDT(string TenKH, string SDT)
        {
            string query = String.Format("select MaKH from KHACHHANG where TenKH = N'{0}' and SDT= '{1}'", TenKH, SDT);
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public int Delete_KH(string MaKH)
        {
            string query = String.Format("delete from KHACHHANG where  MaKH = '{0}'", MaKH);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
            return data;
        }




    }
}
