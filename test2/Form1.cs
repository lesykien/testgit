using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test2
{
    public partial class Form1 : Form
    {
        List<PhongBan> listPB;
        public Form1()
        {
            InitializeComponent();
            listPB = new List<PhongBan>();
            listPB.Add(new PhongBan()
            {
                MaPB = "PB01",
                Name = "Hành Chính",
                DuAnList = new List<DuAn>()
                {
                    new DuAn() { MaDA = "HC01" , TenDA = "Phát Triển Công Ty"},
                    new DuAn() { MaDA = "HC02" , TenDA = "Tính Lương"}
                }
            });
            listPB.Add(new PhongBan()
            {
                MaPB = "PB02",
                Name = "Phát Triển",
                DuAnList = new List<DuAn>()
                {
                    new DuAn() { MaDA = "PT01" , TenDA = "Phát Triển Phần Mềm"},
                    new DuAn() { MaDA = "PT02" , TenDA = "Thiết Kế CSDL"}, 
                    new DuAn() { MaDA = "PT03" , TenDA = "Quản lý dự án"}
                }
            });
            cbo_MaPB.DataSource = listPB; 
            cbo_MaPB.DisplayMember = "Name";
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ColumnHeader[] columns = new ColumnHeader[]
             {
                new ColumnHeader(){ Text = "Mã NV " , Width = 50 },
                new ColumnHeader(){ Text = "Tên NV " , Width = 100 },
                new ColumnHeader(){ Text = "Ngày sinh" , Width = 100 },
                new ColumnHeader(){ Text = "Địa chỉ " , Width = 100 },
                new ColumnHeader(){ Text = "Lương" , Width = 100 },
                new ColumnHeader(){ Text = "Mã PB " , Width = 50 },
                new ColumnHeader(){ Text = "Mã DA " , Width = 50 }
             };
            lstv_DanhSach.Columns.AddRange(columns);
        }   
        public class DuAn
        {
            public string MaDA { get; set; }
            public string TenDA { get; set; }
        }
        public class PhongBan
        {
            public string MaPB { get; set; }
            public string Name { get; set; }
            public List<DuAn> DuAnList { get; set;}
        }

        private void cbo_MaPB_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cd = sender as ComboBox;
            if ( cd.SelectedValue != null)
            {
                PhongBan pb = cd.SelectedValue as PhongBan;
                cbo_MaDA.DataSource = pb.DuAnList;
                cbo_MaDA.DisplayMember = "TenDA"; 
            }
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            PhongBan phong = (PhongBan)cbo_MaPB.SelectedItem;
            DuAn duAn = (DuAn)cbo_MaDA.SelectedItem; 
            NhanVien nhanVien = new NhanVien();

            //Gán giá trị 
            nhanVien.MaNV = txt_MaNV.Text; 
            nhanVien.HoTen = txt_HoTen.Text;
            nhanVien.date = dtm_NgaySinh.Value; 
            nhanVien.Luong = double.Parse(txt_Luong.Text);
            nhanVien.DiaChi = txt_DiaChi.Text;
            nhanVien.MaPB = phong.MaPB.ToString();
            nhanVien.MaDA = duAn.MaDA.ToString();  
            //==============================================

            // add vào listView 
            ListViewItem lv = new ListViewItem(nhanVien.MaNV.ToString());

            lv.SubItems.Add(nhanVien.HoTen.ToString());
            lv.SubItems.Add(nhanVien.date.ToString("dd/MM/yyyy"));
            lv.SubItems.Add(nhanVien.DiaChi.ToString());
            lv.SubItems.Add(nhanVien.Luong.ToString("0,00,000"));
            lv.SubItems.Add(nhanVien.MaPB.ToString());
            lv.SubItems.Add(nhanVien.MaDA.ToString());

            lstv_DanhSach.Items.Add(lv);    
            //============================================
            txt_DiaChi.Clear();
            txt_HoTen.Clear();
            txt_Luong.Clear();
            txt_MaNV.Clear();
        }

        private void lstv_DanhSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView listView = sender as ListView;
            if ( lstv_DanhSach.SelectedItems.Count > 0 )
            {
                ListViewItem lv = listView.SelectedItems[0];
                txt_MaNV.Text = lv.Text;
                txt_HoTen.Text = lv.SubItems[1].Text; 
                dtm_NgaySinh.Text = lv.SubItems[2].Text; 
                txt_DiaChi.Text = lv.SubItems[3].Text; 
                txt_Luong.Text = lv.SubItems[4].Text; 
                foreach ( var item in listPB)
                {
                    if ( item.MaPB == lv.SubItems[5].Text )
                    {
                        cbo_MaPB.Text = item.Name; break;
                    }
                }
                foreach ( var item in listPB)
                {
                    foreach ( var item2 in item.DuAnList)
                    {
                        if ( item2.MaDA == lv.SubItems[6].Text)
                        {
                            cbo_MaDA.Text = item2.TenDA; break;
                        }
                    }
                }
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if ( lstv_DanhSach.SelectedIndices.Count > 0 )
            {
                foreach ( ListViewItem item in lstv_DanhSach.SelectedItems)
                {
                    lstv_DanhSach.Items.Remove(item);
                }
            }
            txt_DiaChi.Clear();
            txt_HoTen.Clear();
            txt_Luong.Clear();
            txt_MaNV.Clear();
        }
    }
}
