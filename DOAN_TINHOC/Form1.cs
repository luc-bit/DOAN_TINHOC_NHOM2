using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json; 
using System.IO;




namespace DOAN_TINHOC
{
    public partial class FormQLSV : Form
    {
        private DanhMucSinhVien DMSinhVien = new DanhMucSinhVien();
        private int vitri = 0;
        public FormQLSV()
        {
            InitializeComponent();
            cbxLop.Enabled = false;
        }
        private void HienThiDanhSachSinhVien(DataGridView dgv, List<SinhVien> ds)
        {
            
            dgv.Columns.Clear();
            // Đặt AutoGenerateColumns = false để chỉ tạo các cột thủ công
            dgv.AutoGenerateColumns = false;
            // Tạo cột hiển thị "HienThiMaSinhVien"
            DataGridViewTextBoxColumn maSVDHColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HienThiMaSinhVien",
                HeaderText = "Mã SV"
            };
            dgv.Columns.Add(maSVDHColumn);

            // Thêm các cột khác với DataPropertyName để hiển thị đúng thuộc tính của lớp SinhVien
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenSV", HeaderText = "Tên SV" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Lop", HeaderText = "Lớp" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Khoa", HeaderText = "Khoa" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Khoas", HeaderText = "Khóa" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Diachi", HeaderText = "Địa chỉ" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoDT", HeaderText = "Số ĐT" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Gioitinh", HeaderText = "Giới tính" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NgaySinh", HeaderText = "Ngày sinh" });

            // Gán dữ liệu vào DataGridView
            dgv.DataSource = ds.ToList();
            
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            int ma;
            if (!int.TryParse(txtMaSV.Text, out ma))
            {
                MessageBox.Show("Mã Nhập Vào Không Phải Là Số Nguyên", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaSV.Clear();
                return;
            }
            int index=cbxLop.SelectedIndex;
            string ten = txtTenSV.Text;
            string lop = cbxLop.SelectedItem?.ToString();
            string khoa = cbxKhoa.SelectedItem?.ToString();
            string khoas=cbxKhoas.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(lop) || string.IsNullOrEmpty(khoa)||string.IsNullOrEmpty(khoas))
            {
                MessageBox.Show("Vui Lòng Chọn Khoa Và Lớp và Khóa", "Lỗi", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            } 
            string dc = txtDiaChi.Text;
            string dt = txtSoDT.Text;
            DateTime ns = dptNgaySinh.Value;
            if ((DateTime.Now.Year - ns.Year) < 18)
            {
                MessageBox.Show("Ngày Sinh Nhập vào dưới 18 tuổi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            string gioitinh;
            if (rdbNam.Checked)
            {
                gioitinh = "Nam";
            }
            else
            {
                gioitinh = "Nữ";
            }
            SinhVien sinhvien = new SinhVien(ma,ten, lop,khoa, khoas, dc, dt, ns, gioitinh);
            if (DMSinhVien.Them(sinhvien))
            {
                HienThiDanhSachSinhVien(dgvQLSV, DMSinhVien.DsSinhVien);
                MessageBox.Show("Đã thêm sinh viên", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Không thêm được sinh viên", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void dgvQLSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vitri = e.RowIndex;
            //
            if (DMSinhVien.DsSinhVien.Count>0)
            {
                SinhVien sv = new SinhVien();
                sv = DMSinhVien.DsSinhVien[vitri];
                txtMaSV.Text = sv.MaSV.ToString();
                txtTenSV.Text = sv.TenSV;
                cbxKhoas.SelectedItem = sv.Khoas;
                cbxKhoa.SelectedItem = sv.Khoa;
                cbxLop.SelectedItem = sv.Lop;
                txtDiaChi.Text = sv.Diachi;
                txtSoDT.Text = sv.SoDT;
                dptNgaySinh.Value = sv.NgaySinh;
                if (sv.Gioitinh == "Nam")
                {
                    rdbNam.Checked = true;
                }
                else
                {
                    rdbNu.Checked = true;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (DMSinhVien.DsSinhVien.Count > 0)
            {
                SinhVien sv = DMSinhVien.DsSinhVien[vitri];
                if (DMSinhVien.Xoa(sv, vitri))
                {
                    HienThiDanhSachSinhVien(dgvQLSV, DMSinhVien.DsSinhVien);
                    MessageBox.Show("Đã xóa sinh viên", "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (DMSinhVien.DsSinhVien.Count > 0)
            {
                SinhVien sv = DMSinhVien.DsSinhVien[vitri];
                if (DMSinhVien.Sua(sv, vitri))
                {
                    sv.TenSV = txtTenSV.Text;
                    sv.Lop = cbxLop.SelectedItem?.ToString();
                    sv.Khoa = cbxKhoa.SelectedItem?.ToString();
                    sv.Khoas= cbxKhoas.SelectedItem?.ToString();
                    if (string.IsNullOrEmpty(sv.Lop) || string.IsNullOrEmpty(sv.Khoa)||string.IsNullOrEmpty(sv.Khoas))
                    {
                        MessageBox.Show("Vui Lòng Chọn Khoa Và Lớp và Khóa ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    sv.Diachi = txtDiaChi.Text;
                    sv.SoDT = txtSoDT.Text;
                    sv.NgaySinh = dptNgaySinh.Value;
                    if ((DateTime.Now.Year - sv.NgaySinh.Year) < 18)
                    {
                        MessageBox.Show("Ngày Sinh Sửa  dưới 18 tuổi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        return;
                    }
                    if (rdbNam.Checked)
                    {
                        sv.Gioitinh = "Nam";
                    }
                    else
                    {
                        sv.Gioitinh = "Nữ";
                    }
                    HienThiDanhSachSinhVien(dgvQLSV, DMSinhVien.DsSinhVien);
                    MessageBox.Show("Đã sửa sinh viên", "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void LuuFileJson(string filePath)
        {
            try
            {
                // Chuyển đổi danh sách hiện tại thành JSON
                string json = JsonConvert.SerializeObject(DMSinhVien.DsSinhVien, Formatting.Indented);

                // Ghi dữ liệu vào file JSON
                File.WriteAllText(filePath, json);

                MessageBox.Show("Đã lưu danh sách sinh viên vào file JSON.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu file JSON: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Hàm tải danh sách sinh viên từ file .json
        private void LoadFileJson(string filePath)
        {
            try
            {
                // Đọc dữ liệu từ file JSON
                string json = File.ReadAllText(filePath);

                // Chuyển đổi JSON thành danh sách sinh viên
                var danhSach = JsonConvert.DeserializeObject<List<SinhVien>>(json);

                // Nếu danh sách hợp lệ, gán vào `DMSinhVien.DsSinhVien`
                if (danhSach != null)
                {
                    DMSinhVien.DsSinhVien = danhSach;
                }

                // Cập nhật DataGridView
                HienThiDanhSachSinhVien(dgvQLSV, DMSinhVien.DsSinhVien);

                MessageBox.Show("Đã tải danh sách sinh viên từ file JSON.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải file JSON: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện Click cho nút "Lưu File JSON"
        private void btnLuuFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON Files (*.json)|*.json",
                Title = "Lưu file JSON"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                LuuFileJson(saveFileDialog.FileName);
            }
        }

        // Sự kiện Click cho nút "Tải File JSON"
        

        private void btnTaiFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json",
                Title = "Mở file JSON"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadFileJson(openFileDialog.FileName);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muôn thoát không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) 
            { 
                Close();
            }
        }
        private void btnTimTheoMa_Click(object sender, EventArgs e)
        {
            int ma;
            if (!int.TryParse(txtTimTheoMa.Text,out ma))
            {
                MessageBox.Show("Mã Nhập Vào Không Phải Là Số Nguyên", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTimTheoMa.Clear();
                return;
            }
            DanhMucSinhVien dstimkiem=new DanhMucSinhVien();
            if (DMSinhVien.TimTheoMa(ma) != null) 
            {
                dstimkiem.Them(DMSinhVien.TimTheoMa(ma));
                HienThiDanhSachSinhVien(dgvTimSinhVien, dstimkiem.DsSinhVien);
            }
            else
            {
                MessageBox.Show("không có học sinh cần tìm ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CapNhatComboBoxLop()
        {
            cbxLop.Items.Clear(); // Xóa các lớp cũ

            string khoa = cbxKhoa.SelectedItem?.ToString();
            string khoas = cbxKhoas.SelectedItem?.ToString();

            // Gọi phương thức CapNhatDanhSachLop trong DanhMucSinhVien
            List<string> danhSachLop = DMSinhVien.CapNhatDanhSachLop(khoas, khoa);
            
            cbxLop.Enabled = true;
            // Thêm danh sách lớp vào ComboBox cbxLop
            cbxLop.Items.AddRange(danhSachLop.ToArray());
        }

        private void cbxKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatComboBoxLop();
        }

        private void cbxKhoas_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatComboBoxLop();
        }

        private void btnLocDS_Click(object sender, EventArgs e)
        {
            List<string> danhSachLop = DMSinhVien.DsSinhVien.Select(sv => sv.Lop).Distinct().ToList();
            List<string> danhSachKhoa = DMSinhVien.DsSinhVien.Select(sv => sv.Khoa).Distinct().ToList();
            List<string> danhSachKhoas = DMSinhVien.DsSinhVien.Select(sv => sv.Khoas).Distinct().ToList();
            Hide(); 
            FromHienThi frmhienthi= new FromHienThi(DMSinhVien, danhSachLop, danhSachKhoa, danhSachKhoas);
            frmhienthi.ShowDialog();
            Show();
        }
    }
}
