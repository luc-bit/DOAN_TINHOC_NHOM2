using DOAN_TINHOC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace DOAN_TINHOC
{
    [Serializable]
    public class DanhMucSinhVien
    {
        private List<SinhVien> m_dsSinhVien;

        public List<SinhVien> DsSinhVien
        {
            get { return m_dsSinhVien; }
            set { m_dsSinhVien = value; }
        }

        public DanhMucSinhVien()
        {
            m_dsSinhVien = new List<SinhVien>();
        }
        public DanhMucSinhVien(List<SinhVien> dssinhvien)
        {
            m_dsSinhVien = dssinhvien; 
        }
        public bool KienTraMa(int ma)
        {
            foreach (SinhVien sv in m_dsSinhVien)
            {
                if (sv.MaSV.Equals(ma))
                    return true;
            }
            return false;
        }
        public bool Them(SinhVien sv)
        {
            if (KienTraMa(sv.MaSV))
            {
                return false;
            }
            else
            {
                m_dsSinhVien.Add(sv);
                return true;
            }
        }
        public bool Xoa(SinhVien sv, int vitri)
        {
            if (KienTraMa(sv.MaSV))
            {
                m_dsSinhVien.RemoveAt(vitri);
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool Sua(SinhVien sv, int vitri)
        {
            if (KienTraMa(sv.MaSV))
            {
                m_dsSinhVien[vitri] = sv;
                return true;
            }
            else
            {
                return false;
            }
        }
        public SinhVien TimTheoMa(int ma)
        {
            foreach (SinhVien sv in m_dsSinhVien)
            {
                if (sv.MaSV.Equals(ma))
                {
                    return sv;
                }
            }
            return null;
        }
        public List<string> CapNhatDanhSachLop(string khoas, string khoa)
        {
            List<string> danhSachLop = new List<string>();

            if (string.IsNullOrEmpty(khoas) || string.IsNullOrEmpty(khoa))
            {
                return danhSachLop; // Trả về danh sách rỗng nếu khóa hoặc khoa chưa được chọn
            }
            
            if (khoas == "D22" && khoa == "CNTT")
            {
                for (int i = 1; i <= 15; i++)
                {
                    danhSachLop.Add($"D22_TH{(i < 10 ? "0" + i : i.ToString())}");
                }
            }
            else if (khoas == "D21" && khoa == "CNTT")
            {
                for (int i = 1; i <= 15; i++)
                {
                    danhSachLop.Add($"D21_TH{(i < 10 ? "0" + i : i.ToString())}");
                }
            }
            else if (khoas == "D22" && khoa == "QTKD")
            {
                for (int i = 1; i <= 15; i++)
                {
                    danhSachLop.Add($"D22_QT{(i < 10 ? "0" + i : i.ToString())}");
                }
            }
            else if(khoas == "D21" && khoa == "QTKD")
            {
                for (int i = 1; i <= 15; i++)
                {
                    danhSachLop.Add($"D21_QT{(i < 10 ? "0" + i : i.ToString())}");
                } 
            }
            return danhSachLop;
        }
    }
}
