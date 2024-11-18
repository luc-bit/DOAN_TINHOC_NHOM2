using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOAN_TINHOC
{
    public partial class FromHienThi : Form
    {
        private DanhMucSinhVien DMSinhVien;
        private List<string> danhSachLop;
        private List<string> danhSachKhoa;
        private List<string> danhSachKhoas;
        public FromHienThi(DanhMucSinhVien danhMucSinhVien, List<string> dsLop, List<string> dsKhoa, List<string> dsKhoas)
        {
            InitializeComponent();
            DMSinhVien = danhMucSinhVien;
            danhSachLop = dsLop;
            danhSachKhoa = dsKhoa;
            danhSachKhoas = dsKhoas;
            CapNhatComboBoxLop();
            CapNhatComboBoxKhoa();
            CapNhatComboBoxKhoas();
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muôn thoát không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Close();
            }
        }

        private void btnLoc3YeuTo_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ các ComboBox
            string selectedLop = cbxLop.SelectedItem?.ToString();
            string selectedKhoa = cbxKhoa.SelectedItem?.ToString();
            string selectedKhoas = cbxKhoas.SelectedItem?.ToString();

            // Kiểm tra nếu tất cả các ComboBox đều chưa được chọn
            if (string.IsNullOrEmpty(selectedLop) && string.IsNullOrEmpty(selectedKhoa) && string.IsNullOrEmpty(selectedKhoas))
            {
                MessageBox.Show("Vui lòng chọn ít nhất một tiêu chí để lọc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo danh sách kết quả
            List<SinhVien> filteredList = new List<SinhVien>();

            // Duyệt qua danh sách sinh viên
            foreach (SinhVien sv in DMSinhVien.DsSinhVien)
            {
                // Kiểm tra các điều kiện
                bool matchLop = string.IsNullOrEmpty(selectedLop) || sv.Lop == selectedLop;
                bool matchKhoa = string.IsNullOrEmpty(selectedKhoa) || sv.Khoa == selectedKhoa;
                bool matchKhoas = string.IsNullOrEmpty(selectedKhoas) || sv.Khoas == selectedKhoas;
                // Nếu tất cả các điều kiện đều thỏa mãn
                if (matchLop && matchKhoa && matchKhoas)
                {
                    filteredList.Add(sv);
                }
            }

            // Cập nhật DataGridView với danh sách đã lọc
            dgvDanhSachSinhVien.DataSource = null;
            dgvDanhSachSinhVien.DataSource = filteredList;

            // Hiển thị thông báo nếu không có sinh viên nào thỏa mãn điều kiện
            if (filteredList.Count == 0)
            {
                MessageBox.Show("Không tìm thấy sinh viên nào phù hợp với tiêu chí lọc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            cbxKhoa.SelectedIndex = -1;  // Xóa lựa chọn hiện tại của ComboBox Khoa
            cbxKhoas.SelectedIndex = -1; // Xóa lựa chọn hiện tại của ComboBox Khóa
            cbxLop.SelectedIndex = -1;   // Xóa lựa chọn hiện tại của ComboBox Lớp
        }
        private void CapNhatComboBoxLop()
        {
            cbxLop.Items.Clear(); // Xóa các lớp cũ
            cbxLop.Items.AddRange(danhSachLop.ToArray()); // Thêm lớp từ danh sách nhận được
            
        }
        private void CapNhatComboBoxKhoa()
        {
            cbxKhoa.Items.Clear(); // Xóa các khoa cũ
            cbxKhoa.Items.AddRange(danhSachKhoa.ToArray()); // Thêm khoa từ danh sách nhận được
            
        }
        private void CapNhatComboBoxKhoas()
        {
            cbxKhoas.Items.Clear(); // Xóa các khóa cũ
            cbxKhoas.Items.AddRange(danhSachKhoas.ToArray()); // Thêm khóa từ danh sách nhận được
            
        }
    }
}
