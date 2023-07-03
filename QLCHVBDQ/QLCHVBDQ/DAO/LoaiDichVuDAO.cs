using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHVBDQ.DAO
{
    class LoaiDichVuDAO
    {
        private static LoaiDichVuDAO instance;

        public static LoaiDichVuDAO Instance
        {
            get { if (instance == null) instance = new LoaiDichVuDAO(); return instance; }
            set => Instance = value;
        }
        private LoaiDichVuDAO() { }


        public int Delete_LDV(string MaLDV)
        {
            string query = String.Format("delete from LOAIDICHVU where  MaLDV = '{0}'", MaLDV);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
            //query = String.Format("delete from THAMSO where  TenThamSo = '{0}'", MaLDV);
            //int data1 = DataProvider.Instance.ExecuteNonQuery(query);
            if (data != -1) return 1;
            return -1;
        }
    }
}
