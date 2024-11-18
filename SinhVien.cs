using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json; 


namespace DOAN_TINHOC
{
    [Serializable]
    public class SinhVien
    {
        //khai bao
        private int maSV;
        private string tenSV;
        private string lop;
        private string khoa;
        private string khoas;
        private string diachi;
        private string sodt;
        private DateTime ngaysinh;//kiem tra 
        private string gioitinh;
        //propreties
        public int MaSV
        {
            get { return maSV; }
            set { maSV = value; }
        }
        public string HienThiMaSinhVien
        {
            get { return "DH" + maSV.ToString(); }
        }
        public string TenSV
        {
            get { return tenSV; }
            set { tenSV = value; }
        }
        public string Lop
        {
            get { return lop; }
            set { lop = value; }
        }
        public string Khoa
        {
            get { return khoa; }
            set { khoa = value; }
        }
        public string Khoas
        {
            get { return khoas; }
            set { khoas = value; }
        }
        public string Diachi
        {
            get { return diachi; }
            set { diachi = value; }
        }
        public string SoDT
        {
            get { return sodt; }
            set { sodt = value; }
        }
        public string Gioitinh
        {
            get { return gioitinh; }
            set { gioitinh = value; }
        }
        public DateTime NgaySinh
        {
            get { return ngaysinh; }
            set { ngaysinh = value; }
        }
        //phuong thuc

        public SinhVien()
        {
            maSV = 0;
            tenSV = "";
            lop = "";
            khoa = "";
            khoas = "";
            diachi = "";
            sodt = "";
            ngaysinh = DateTime.Now;
            gioitinh = "";
        }
        public SinhVien(int ma, string ten, string lopp, string khoaa,string khoaS, string dc, string dt, DateTime NS, string GT)
        {
            maSV = ma;
            tenSV = ten;
            lop = lopp;
            khoa = khoaa;
            khoas= khoaS;
            diachi = dc;
            sodt = dt;
            ngaysinh = NS;
            gioitinh = GT;
        }
    }
}
