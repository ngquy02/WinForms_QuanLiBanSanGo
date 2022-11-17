using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_QuanLiBanSanGo.Model
{
    internal class HangHoa
    {
        private string maHang;
        private string tenHang;
        private string maLoaiGo;
        private string maKichThuoc;
        private string maDacDiem;
        private string maCongDung;
        private string maMau;
        private string maNuocSX;
        private string soLuong;
        private string donGiaNhap;
        private string thoiGianBaoHanh;
        private byte[] anh;
        private string ghiChu;

        public string MaHang { get => maHang; set => maHang = value; }
        public string TenHang { get => tenHang; set => tenHang = value; }
        public string MaLoaiGo { get => maLoaiGo; set => maLoaiGo = value; }
        public string MaKichThuoc { get => maKichThuoc; set => maKichThuoc = value; }
        public string MaDacDiem { get => maDacDiem; set => maDacDiem = value; }
        public string MaCongDung { get => maCongDung; set => maCongDung = value; }
        public string MaMau { get => maMau; set => maMau = value; }
        public string MaNuocSX { get => maNuocSX; set => maNuocSX = value; }
        public string SoLuong { get => soLuong; set => soLuong = value; }
        public string DonGiaNhap { get => donGiaNhap; set => donGiaNhap = value; }
        public string ThoiGianBaoHanh { get => thoiGianBaoHanh; set => thoiGianBaoHanh = value; }
        public byte[] Anh { get => anh; set => anh = value; }
        public string GhiChu { get => ghiChu; set => ghiChu = value; }

        public HangHoa(string maHang, string tenHang, string maLoaiGo, string maKichThuoc, string maDacDiem, string maCongDung, string maMau, string maNuocSX, string soLuong, string donGiaNhap, string thoiGianBaoHanh, byte[] anh, string ghiChu)
        {
            this.maHang = maHang;
            this.tenHang = tenHang;
            this.maLoaiGo = maLoaiGo;
            this.maKichThuoc = maKichThuoc;
            this.maDacDiem = maDacDiem;
            this.maCongDung = maCongDung;
            this.maMau = maMau;
            this.maNuocSX = maNuocSX;
            this.soLuong = soLuong;
            this.donGiaNhap = donGiaNhap;
            this.thoiGianBaoHanh = thoiGianBaoHanh;
            this.anh = anh;
            this.ghiChu = ghiChu;
        }
    }
}
