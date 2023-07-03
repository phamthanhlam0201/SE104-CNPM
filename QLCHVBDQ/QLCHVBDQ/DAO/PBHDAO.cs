using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHVBDQ.DAO
{
    class PBHDAO
    {
        private static PBHDAO instance;

        public static PBHDAO Instance
        {
            get { if (instance == null) instance = new PBHDAO(); return instance; }
            set => Instance = value;
        }
        private PBHDAO() { }

        public int DeletePBH(string sp)
        {
            CTPDVDAO.Instance.DeleteCTPDV(sp);
            string query = String.Format("delete from PHIEUBANHANG where SoPhieu = '{0}'", sp);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
            return data;
        }

        public int InsertPBH(string SoPhieu, string NgayLapPhieu, string MaKH)
        {
            string query = String.Format("select count(SoPhieu) from PHIEUBANHANG where SoPhieu = '{0}'", SoPhieu);
            string data_SoPhieu = DataProvider.Instance.ExecuteScalar(query).ToString();
            int count_SoPhieu = int.Parse(data_SoPhieu);
            if (count_SoPhieu > 0)
            {
                return 0;
            }
            else
            {
                query = String.Format("INSERT INTO PHIEUBANHANG VALUES('{0}', '{1}', '{2}', 0)", SoPhieu, NgayLapPhieu, MaKH);
                if (DataProvider.Instance.ExecuteNonQuery(query) == 1) return 1;
                else return 2;
            }
        }
    }
}
