using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHVBDQ.DAO
{
    class PDVDAO
    {
        private static PDVDAO instance;

        public static PDVDAO Instance
        {
            get { if (instance == null) instance = new PDVDAO(); return instance; }
            set => Instance = value;
        }
        private PDVDAO() { }

        public int DeletePDV(string sp)
        {
            CTPDVDAO.Instance.DeleteCTPDV(sp);
            string query = String.Format("delete from PHIEUDICHVU where SoPhieu = '{0}'", sp);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
            return data;
        }

        public int InsertPDV(string SoPhieu, string NgayLapPhieu, string TenKH)
        {
            string query = String.Format("select count(SoPhieu) from PHIEUDICHVU where SoPhieu = '{0}'", SoPhieu);
            string data_SoPhieu = DataProvider.Instance.ExecuteScalar(query).ToString();
            int count_SoPhieu = int.Parse(data_SoPhieu);
            query = String.Format("select count(MaKH) from KHACHHANG where MaKH = '{0}'", TenKH);
            string data_MaKH = DataProvider.Instance.ExecuteScalar(query).ToString();
            int count_MaKH = int.Parse(data_MaKH);
            if (count_SoPhieu > 0)
            {
                return 0;                
            }
            else if(count_MaKH == 0)
            {
                return 1;
            }
            else
            {
                query = String.Format("INSERT INTO PHIEUDICHVU VALUES('{0}', '{1}', '{2}', 0, 0, 0, 1)", SoPhieu, NgayLapPhieu, TenKH);
                if (DataProvider.Instance.ExecuteNonQuery(query) == 1) return 2;
                else return 3;
            }
        }
    }
}
