USE [CSharp_QuanLiBanSanGo]
GO
/****** Object:  Table [dbo].[tChiTietHoaDonBan]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tChiTietHoaDonBan](
	[SoHDB] [nvarchar](20) NOT NULL,
	[MaHang] [nvarchar](20) NOT NULL,
	[SoLuong] [int] NULL,
	[GiamGia] [int] NULL,
	[ThanhTien] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[SoHDB] ASC,
	[MaHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tDMHangHoa]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tDMHangHoa](
	[MaHang] [nvarchar](20) NOT NULL,
	[TenHangHoa] [nvarchar](100) NOT NULL,
	[MaLoaiGo] [nvarchar](20) NOT NULL,
	[MaKichThuoc] [nvarchar](20) NOT NULL,
	[MaDacDiem] [nvarchar](20) NOT NULL,
	[MaCongDung] [nvarchar](20) NOT NULL,
	[MaMau] [nvarchar](20) NOT NULL,
	[MaNuocSX] [nvarchar](20) NOT NULL,
	[SoLuong] [int] NULL,
	[DonGiaNhap] [money] NOT NULL,
	[DonGiaBan] [money] NULL,
	[ThoiGianBaoHanh] [int] NULL,
	[Anh] [nvarchar](max) NULL,
	[GhiChu] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tHoaDonBan]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tHoaDonBan](
	[SoHDB] [nvarchar](20) NOT NULL,
	[MaNV] [nvarchar](20) NOT NULL,
	[NgayBan] [datetime] NULL,
	[MaKhach] [nvarchar](20) NOT NULL,
	[TongTien] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[SoHDB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tKhachHang]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tKhachHang](
	[MaKhach] [nvarchar](20) NOT NULL,
	[TenKhach] [nvarchar](100) NOT NULL,
	[DiaChi] [nvarchar](100) NULL,
	[DienThoai] [nvarchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaKhach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[Report1]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   FUNCTION [dbo].[Report1](@maKhach NVARCHAR(20)) RETURNS TABLE AS
RETURN
	SELECT DISTINCT TOP(2) WITH TIES tKhachHang.MaKhach, TenKhach, tDMHangHoa.MaHang, TenHangHoa, SUM(tChiTietHoaDonBan.SoLuong) AS Soluong
	FROM tKhachHang
		JOIN tHoaDonBan ON tHoaDonBan.MaKhach = tKhachHang.MaKhach
		JOIN tChiTietHoaDonBan ON tChiTietHoaDonBan.SoHDB = tHoaDonBan.SoHDB
		JOIN tDMHangHoa ON tDMHangHoa.MaHang = tChiTietHoaDonBan.MaHang
	WHERE tKhachHang.MaKhach = @maKhach
	GROUP BY tKhachHang.MaKhach, TenKhach, tDMHangHoa.MaHang, TenHangHoa
	ORDER BY SoLuong DESC
GO
/****** Object:  Table [dbo].[tChiTietHoaDonNhap]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tChiTietHoaDonNhap](
	[SoHDN] [nvarchar](20) NOT NULL,
	[MaHang] [nvarchar](20) NOT NULL,
	[SoLuong] [int] NULL,
	[DonGia] [money] NOT NULL,
	[GiamGia] [int] NULL,
	[ThanhTien] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[SoHDN] ASC,
	[MaHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tHoaDonNhap]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tHoaDonNhap](
	[SoHDN] [nvarchar](20) NOT NULL,
	[MaNV] [nvarchar](20) NOT NULL,
	[NgayNhap] [datetime] NULL,
	[MaNCC] [nvarchar](20) NOT NULL,
	[TongTien] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[SoHDN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[Report2]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   FUNCTION [dbo].[Report2](@thang INT) RETURNS TABLE AS
RETURN
	SELECT tHoaDonNhap.SoHDN, SUM(SoLuong * DonGia) AS TongTienNhap, NgayNhap
	FROM tChiTietHoaDonNhap JOIN tHoaDonNhap ON tChiTietHoaDonNhap.SoHDN = tHoaDonNhap.SoHDN
	WHERE MONTH(NgayNhap) = @thang
	GROUP BY tHoaDonNhap.SoHDN, NgayNhap
GO
/****** Object:  UserDefinedFunction [dbo].[Report3]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   FUNCTION [dbo].[Report3](@nam INT) RETURNS TABLE AS
RETURN
	SELECT TOP(5) WITH TIES tHoaDonBan.SoHDB, MaNV, NgayBan, MaKhach, SUM(tChiTietHoaDonBan.SoLuong * tDMHangHoa.DonGiaBan) AS DoanhThu
	FROM tChiTietHoaDonBan
		JOIN tHoaDonBan ON tChiTietHoaDonBan.SoHDB = tHoaDonBan.SoHDB
		JOIN tDMHangHoa ON tChiTietHoaDonBan.MaHang = tDMHangHoa.MaHang
	WHERE YEAR(NgayBan) = @nam
	GROUP BY tHoaDonBan.SoHDB, MaNV, NgayBan, MaKhach
	ORDER BY DoanhThu DESC
GO
/****** Object:  Table [dbo].[tNhanVien]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tNhanVien](
	[MaNV] [nvarchar](20) NOT NULL,
	[TenNV] [nvarchar](100) NOT NULL,
	[GioiTinh] [nvarchar](5) NULL,
	[NgaySinh] [date] NULL,
	[DienThoai] [nvarchar](15) NULL,
	[DiaChi] [nvarchar](100) NULL,
	[MaCV] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[Report4]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   FUNCTION [dbo].[Report4](@thang INT) RETURNS TABLE AS
RETURN
	SELECT TOP(2) WITH TIES tNhanVien.MaNV, TenNV, SUM(tChiTietHoaDonBan.SoLuong * tDMHangHoa.DonGiaBan) AS DoanhThu, NgayBan
	FROM tChiTietHoaDonBan
		JOIN tHoaDonBan ON tChiTietHoaDonBan.SoHDB = tHoaDonBan.SoHDB
		JOIN tNhanVien ON tHoaDonBan.MaNV = tNhanVien.MaNV
		JOIN tDMHangHoa ON tChiTietHoaDonBan.MaHang = tDMHangHoa.MaHang
	WHERE MONTH(NgayBan) = @thang
	GROUP BY tNhanVien.MaNV, TenNV, NgayBan
	ORDER BY DoanhThu DESC
GO
/****** Object:  Table [dbo].[tCongDung]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tCongDung](
	[MaCongDung] [nvarchar](20) NOT NULL,
	[TenCongDung] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaCongDung] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tDacDiem]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tDacDiem](
	[MaDacDiem] [nvarchar](20) NOT NULL,
	[TenDacDiem] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDacDiem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tKichThuoc]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tKichThuoc](
	[MaKichThuoc] [nvarchar](20) NOT NULL,
	[TenKichThuoc] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaKichThuoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tLoaiGo]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tLoaiGo](
	[MaLoaiGo] [nvarchar](20) NOT NULL,
	[TenLoaiGo] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaLoaiGo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tMauSac]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tMauSac](
	[MaMau] [nvarchar](20) NOT NULL,
	[TenMau] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaMau] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tNuocSanXuat]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tNuocSanXuat](
	[MaNuocSX] [nvarchar](20) NOT NULL,
	[TenNuocSX] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNuocSX] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_DMHangHoa]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[View_DMHangHoa] AS
SELECT MaHang, TenHangHoa, TenLoaiGo, TenKichThuoc, TenDacDiem, TenCongDung, TenMau, TenNuocSX, SoLuong, DonGiaNhap, DonGiaBan, ThoiGianBaoHanh, Anh, GhiChu
FROM tDMHangHoa
	JOIN tLoaiGo ON tDMHangHoa.MaLoaiGo = tLoaiGo.MaLoaiGo
	JOIN tKichThuoc ON tDMHangHoa.MaKichThuoc = tKichThuoc.MaKichThuoc
	JOIN tDacDiem ON tDMHangHoa.MaDacDiem =  tDacDiem.MaDacDiem
	JOIN tCongDung ON tDMHangHoa.MaCongDung = tCongDung.MaCongDung
	JOIN tMauSac ON tDMHangHoa.MaMau = tMauSac.MaMau
	JOIN tNuocSanXuat ON tDMHangHoa.MaNuocSX = tNuocSanXuat.MaNuocSX
GO
/****** Object:  View [dbo].[View_HoaDonBan]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[View_HoaDonBan] AS
SELECT SoHDB, tHoaDonBan.MaNV, TenNV, NgayBan, tHoaDonBan.MaKhach, TenKhach, TongTien
FROM tHoaDonBan
	JOIN tNhanVien ON tHoaDonBan.MaNV = tNhanVien.MaNV
	JOIN tKhachHang ON tHoaDonBan.MaKhach = tKhachHang.MaKhach
GO
/****** Object:  Table [dbo].[tNhaCungCap]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tNhaCungCap](
	[MaNCC] [nvarchar](20) NOT NULL,
	[TenNCC] [nvarchar](100) NOT NULL,
	[DiaChi] [nvarchar](100) NULL,
	[DienThoai] [nvarchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNCC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_HoaDonNhap]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    VIEW [dbo].[View_HoaDonNhap] AS
SELECT SoHDN, tHoaDonNhap.MaNV, TenNV, NgayNhap, tHoaDonNhap.MaNCC, TenNCC, TongTien
FROM tHoaDonNhap
	JOIN tNhanVien ON tHoaDonNhap.MaNV = tNhanVien.MaNV
	JOIN tNhaCungCap ON tHoaDonNhap.MaNCC = tNhaCungCap.MaNCC
GO
/****** Object:  Table [dbo].[tCongViec]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tCongViec](
	[MaCV] [nvarchar](20) NOT NULL,
	[TenCV] [nvarchar](100) NOT NULL,
	[MucLuong] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaCV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_NhanVien]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[View_NhanVien] AS
SELECT MaNV, TenNV, GioiTinh, NgaySinh, DienThoai, DiaChi, tNhanVien.MaCV, TenCV, MucLuong
FROM tNhanVien JOIN tCongViec ON tNhanVien.MaCV = tCongViec.MaCV
GO
/****** Object:  Table [dbo].[tLogin]    Script Date: 22/11/2022 15:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tLogin](
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_tLogin] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tCongDung] ([MaCongDung], [TenCongDung]) VALUES (N'CD001', N'Lát sàn')
INSERT [dbo].[tCongDung] ([MaCongDung], [TenCongDung]) VALUES (N'CD002', N'Ốp tường')
INSERT [dbo].[tCongDung] ([MaCongDung], [TenCongDung]) VALUES (N'CD003', N'Ốp trần')
GO
INSERT [dbo].[tCongViec] ([MaCV], [TenCV], [MucLuong]) VALUES (N'CS', N'Chăm sóc khách hàng', 12000000.0000)
INSERT [dbo].[tCongViec] ([MaCV], [TenCV], [MucLuong]) VALUES (N'HT', N'Vận hành hệ thống', 20000000.0000)
INSERT [dbo].[tCongViec] ([MaCV], [TenCV], [MucLuong]) VALUES (N'KT', N'Kế toán', 12500000.0000)
INSERT [dbo].[tCongViec] ([MaCV], [TenCV], [MucLuong]) VALUES (N'QL', N'Quản lí', 25000000.0000)
INSERT [dbo].[tCongViec] ([MaCV], [TenCV], [MucLuong]) VALUES (N'TN', N'Thu ngân', 10000000.0000)
GO
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB001', N'MH001', 50, 0, 19750500.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB001', N'MH002', 60, 0, 23700600.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB001', N'MH003', 60, 0, 29403000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB002', N'MH004', 90, 0, 36531000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB002', N'MH005', 30, 0, 19305000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB003', N'MH006', 35, 0, 14899500.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB003', N'MH007', 70, 0, 38808000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB003', N'MH008', 110, 0, 52272000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB004', N'MH009', 20, 0, 7920000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB004', N'MH010', 55, 0, 13340250.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB004', N'MH011', 90, 0, 38313000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB005', N'MH012', 64, 0, 12988800.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB005', N'MH013', 30, 0, 14256000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB005', N'MH014', 140, 0, 66528000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB006', N'MH015', 130, 0, 66924000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB006', N'MH016', 10, 0, 1782000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB006', N'MH017', 55, 0, 12523500.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB006', N'MH018', 40, 0, 27324000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB007', N'MH019', 100, 0, 19800000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB007', N'MH020', 40, 0, 11000000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB008', N'MH001', 85, 0, 33575850.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB008', N'MH006', 30, 0, 12771000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB008', N'MH007', 20, 0, 11088000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB009', N'MH003', 50, 0, 24502500.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB009', N'MH006', 10, 0, 4257000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB009', N'MH009', 60, 0, 23760000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB010', N'MH007', 30, 0, 16632000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB010', N'MH011', 78, 0, 33204600.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB010', N'MH013', 60, 0, 28512000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB011', N'MH004', 20, 0, 8118000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB011', N'MH008', 100, 0, 47520000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB011', N'MH011', 50, 0, 21285000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB012', N'MH005', 30, 0, 19305000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB012', N'MH009', 40, 0, 15840000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB012', N'MH012', 50, 0, 10147500.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB013', N'MH002', 30, 0, 11850300.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB014', N'MH003', 50, 0, 24502500.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB015', N'MH004', 20, 0, 8118000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB016', N'MH006', 20, 0, 8514000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB017', N'MH015', 90, 0, 46332000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB018', N'MH012', 200, 0, 40590000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB019', N'MH002', 120, 0, 47401200.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB019', N'MH009', 250, 0, 99000000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB020', N'MH014', 250, 0, 118800000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB020', N'MH015', 550, 0, 283140000.0000)
INSERT [dbo].[tChiTietHoaDonBan] ([SoHDB], [MaHang], [SoLuong], [GiamGia], [ThanhTien]) VALUES (N'HDB020', N'MH020', 1000, 0, 275000000.0000)
GO
INSERT [dbo].[tChiTietHoaDonNhap] ([SoHDN], [MaHang], [SoLuong], [DonGia], [GiamGia], [ThanhTien]) VALUES (N'HDN001', N'MH001', 2000, 359100.0000, 0, 718200000.0000)
INSERT [dbo].[tChiTietHoaDonNhap] ([SoHDN], [MaHang], [SoLuong], [DonGia], [GiamGia], [ThanhTien]) VALUES (N'HDN001', N'MH002', 2000, 359100.0000, 0, 718200000.0000)
INSERT [dbo].[tChiTietHoaDonNhap] ([SoHDN], [MaHang], [SoLuong], [DonGia], [GiamGia], [ThanhTien]) VALUES (N'HDN001', N'MH003', 5000, 445500.0000, 0, 2227500000.0000)
INSERT [dbo].[tChiTietHoaDonNhap] ([SoHDN], [MaHang], [SoLuong], [DonGia], [GiamGia], [ThanhTien]) VALUES (N'HDN001', N'MH004', 2500, 369000.0000, 0, 922500000.0000)
INSERT [dbo].[tChiTietHoaDonNhap] ([SoHDN], [MaHang], [SoLuong], [DonGia], [GiamGia], [ThanhTien]) VALUES (N'HDN002', N'MH005', 5400, 585000.0000, 0, 3159000000.0000)
INSERT [dbo].[tChiTietHoaDonNhap] ([SoHDN], [MaHang], [SoLuong], [DonGia], [GiamGia], [ThanhTien]) VALUES (N'HDN002', N'MH006', 4500, 387000.0000, 0, 1741500000.0000)
INSERT [dbo].[tChiTietHoaDonNhap] ([SoHDN], [MaHang], [SoLuong], [DonGia], [GiamGia], [ThanhTien]) VALUES (N'HDN002', N'MH007', 800, 504000.0000, 0, 403200000.0000)
INSERT [dbo].[tChiTietHoaDonNhap] ([SoHDN], [MaHang], [SoLuong], [DonGia], [GiamGia], [ThanhTien]) VALUES (N'HDN002', N'MH008', 1200, 432000.0000, 0, 518400000.0000)
INSERT [dbo].[tChiTietHoaDonNhap] ([SoHDN], [MaHang], [SoLuong], [DonGia], [GiamGia], [ThanhTien]) VALUES (N'HDN003', N'MH009', 1100, 360000.0000, 0, 396000000.0000)
INSERT [dbo].[tChiTietHoaDonNhap] ([SoHDN], [MaHang], [SoLuong], [DonGia], [GiamGia], [ThanhTien]) VALUES (N'HDN003', N'MH010', 1800, 220500.0000, 0, 396900000.0000)
INSERT [dbo].[tChiTietHoaDonNhap] ([SoHDN], [MaHang], [SoLuong], [DonGia], [GiamGia], [ThanhTien]) VALUES (N'HDN003', N'MH011', 950, 387000.0000, 0, 367650000.0000)
INSERT [dbo].[tChiTietHoaDonNhap] ([SoHDN], [MaHang], [SoLuong], [DonGia], [GiamGia], [ThanhTien]) VALUES (N'HDN003', N'MH012', 2400, 184500.0000, 0, 442800000.0000)
INSERT [dbo].[tChiTietHoaDonNhap] ([SoHDN], [MaHang], [SoLuong], [DonGia], [GiamGia], [ThanhTien]) VALUES (N'HDN004', N'MH013', 1250, 432000.0000, 0, 540000000.0000)
INSERT [dbo].[tChiTietHoaDonNhap] ([SoHDN], [MaHang], [SoLuong], [DonGia], [GiamGia], [ThanhTien]) VALUES (N'HDN004', N'MH014', 2800, 432000.0000, 0, 1209600000.0000)
INSERT [dbo].[tChiTietHoaDonNhap] ([SoHDN], [MaHang], [SoLuong], [DonGia], [GiamGia], [ThanhTien]) VALUES (N'HDN004', N'MH015', 1700, 468000.0000, 0, 795600000.0000)
INSERT [dbo].[tChiTietHoaDonNhap] ([SoHDN], [MaHang], [SoLuong], [DonGia], [GiamGia], [ThanhTien]) VALUES (N'HDN004', N'MH016', 3100, 162000.0000, 0, 502200000.0000)
INSERT [dbo].[tChiTietHoaDonNhap] ([SoHDN], [MaHang], [SoLuong], [DonGia], [GiamGia], [ThanhTien]) VALUES (N'HDN005', N'MH017', 2300, 207000.0000, 0, 476100000.0000)
INSERT [dbo].[tChiTietHoaDonNhap] ([SoHDN], [MaHang], [SoLuong], [DonGia], [GiamGia], [ThanhTien]) VALUES (N'HDN005', N'MH018', 950, 621000.0000, 0, 589950000.0000)
INSERT [dbo].[tChiTietHoaDonNhap] ([SoHDN], [MaHang], [SoLuong], [DonGia], [GiamGia], [ThanhTien]) VALUES (N'HDN005', N'MH019', 1600, 180000.0000, 0, 288000000.0000)
INSERT [dbo].[tChiTietHoaDonNhap] ([SoHDN], [MaHang], [SoLuong], [DonGia], [GiamGia], [ThanhTien]) VALUES (N'HDN005', N'MH020', 3200, 250000.0000, 0, 800000000.0000)
GO
INSERT [dbo].[tDacDiem] ([MaDacDiem], [TenDacDiem]) VALUES (N'DD001', N'Chống cháy')
INSERT [dbo].[tDacDiem] ([MaDacDiem], [TenDacDiem]) VALUES (N'DD002', N'Chống xước')
INSERT [dbo].[tDacDiem] ([MaDacDiem], [TenDacDiem]) VALUES (N'DD003', N'Chống nước')
INSERT [dbo].[tDacDiem] ([MaDacDiem], [TenDacDiem]) VALUES (N'DD004', N'Chịu lực tốt')
GO
INSERT [dbo].[tDMHangHoa] ([MaHang], [TenHangHoa], [MaLoaiGo], [MaKichThuoc], [MaDacDiem], [MaCongDung], [MaMau], [MaNuocSX], [SoLuong], [DonGiaNhap], [DonGiaBan], [ThoiGianBaoHanh], [Anh], [GhiChu]) VALUES (N'MH001', N'Hamilton HM801 ', N'LG001', N'KT003', N'DD002', N'CD001', N'MS001', N'QG086', 6500, 359100.0000, 395010.0000, 10, N'HM8011.jpg', N'')
INSERT [dbo].[tDMHangHoa] ([MaHang], [TenHangHoa], [MaLoaiGo], [MaKichThuoc], [MaDacDiem], [MaCongDung], [MaMau], [MaNuocSX], [SoLuong], [DonGiaNhap], [DonGiaBan], [ThoiGianBaoHanh], [Anh], [GhiChu]) VALUES (N'MH002', N'Lamton AquaGuard AG1210', N'LG003', N'KT006', N'DD001', N'CD001', N'MS004', N'QG084', 8999, 359100.0000, 395010.0000, 15, N'AG1210.jpg', N'')
INSERT [dbo].[tDMHangHoa] ([MaHang], [TenHangHoa], [MaLoaiGo], [MaKichThuoc], [MaDacDiem], [MaCongDung], [MaMau], [MaNuocSX], [SoLuong], [DonGiaNhap], [DonGiaBan], [ThoiGianBaoHanh], [Anh], [GhiChu]) VALUES (N'MH003', N'Hillman Ambition H1041', N'LG004', N'KT001', N'DD003', N'CD001', N'MS004', N'QG060', 4100, 445500.0000, 490050.0000, 15, N'H10414.jpg', N'')
INSERT [dbo].[tDMHangHoa] ([MaHang], [TenHangHoa], [MaLoaiGo], [MaKichThuoc], [MaDacDiem], [MaCongDung], [MaMau], [MaNuocSX], [SoLuong], [DonGiaNhap], [DonGiaBan], [ThoiGianBaoHanh], [Anh], [GhiChu]) VALUES (N'MH004', N'Dongwha NT008', N'LG002', N'KT002', N'DD004', N'CD001', N'MS001', N'QG082', 5000, 369000.0000, 405900.0000, 20, N'NT008.jpg', N'')
INSERT [dbo].[tDMHangHoa] ([MaHang], [TenHangHoa], [MaLoaiGo], [MaKichThuoc], [MaDacDiem], [MaCongDung], [MaMau], [MaNuocSX], [SoLuong], [DonGiaNhap], [DonGiaBan], [ThoiGianBaoHanh], [Anh], [GhiChu]) VALUES (N'MH005', N'Vinasan OT1255', N'LG005', N'KT008', N'DD002', N'CD002', N'MS003', N'QG084', 8200, 585000.0000, 643500.0000, 15, N'Vinasan OT1255.jpg', N'')
INSERT [dbo].[tDMHangHoa] ([MaHang], [TenHangHoa], [MaLoaiGo], [MaKichThuoc], [MaDacDiem], [MaCongDung], [MaMau], [MaNuocSX], [SoLuong], [DonGiaNhap], [DonGiaBan], [ThoiGianBaoHanh], [Anh], [GhiChu]) VALUES (N'MH006', N'Artfloor Urban AU007 Paris', N'LG006', N'KT004', N'DD001', N'CD001', N'MS002', N'QG090', 7999, 387000.0000, 425700.0000, 15, N'AU007 Paris.jpg', N'')
INSERT [dbo].[tDMHangHoa] ([MaHang], [TenHangHoa], [MaLoaiGo], [MaKichThuoc], [MaDacDiem], [MaCongDung], [MaMau], [MaNuocSX], [SoLuong], [DonGiaNhap], [DonGiaBan], [ThoiGianBaoHanh], [Anh], [GhiChu]) VALUES (N'MH007', N'Versailles Sahara S177017 ', N'LG001', N'KT001', N'DD002', N'CD001', N'MS005', N'QG034', 1100, 504000.0000, 554400.0000, 20, N'S177017.jpg', N'')
INSERT [dbo].[tDMHangHoa] ([MaHang], [TenHangHoa], [MaLoaiGo], [MaKichThuoc], [MaDacDiem], [MaCongDung], [MaMau], [MaNuocSX], [SoLuong], [DonGiaNhap], [DonGiaBan], [ThoiGianBaoHanh], [Anh], [GhiChu]) VALUES (N'MH008', N'Floorpan Orange FP964 Andiroba', N'LG007', N'KT005', N'DD003', N'CD001', N'MS003', N'QG007', 3600, 432000.0000, 475200.0000, 15, N'FP9641.jpg', N'')
INSERT [dbo].[tDMHangHoa] ([MaHang], [TenHangHoa], [MaLoaiGo], [MaKichThuoc], [MaDacDiem], [MaCongDung], [MaMau], [MaNuocSX], [SoLuong], [DonGiaNhap], [DonGiaBan], [ThoiGianBaoHanh], [Anh], [GhiChu]) VALUES (N'MH009', N'Dongwha KO1205', N'LG002', N'KT006', N'DD004', N'CD001', N'MS002', N'QG082', 2900, 360000.0000, 396000.0000, 10, N'FP9512.jpg', N'')
INSERT [dbo].[tDMHangHoa] ([MaHang], [TenHangHoa], [MaLoaiGo], [MaKichThuoc], [MaDacDiem], [MaCongDung], [MaMau], [MaNuocSX], [SoLuong], [DonGiaNhap], [DonGiaBan], [ThoiGianBaoHanh], [Anh], [GhiChu]) VALUES (N'MH010', N'Kronotex Dynamic KT4568', N'LG003', N'KT008', N'DD003', N'CD002', N'MS006', N'QG049', 5000, 220500.0000, 242550.0000, 10, N'Kronotex Dynamic KT4568.jpg', N'')
INSERT [dbo].[tDMHangHoa] ([MaHang], [TenHangHoa], [MaLoaiGo], [MaKichThuoc], [MaDacDiem], [MaCongDung], [MaMau], [MaNuocSX], [SoLuong], [DonGiaNhap], [DonGiaBan], [ThoiGianBaoHanh], [Anh], [GhiChu]) VALUES (N'MH011', N'Artfloor Urban AU007', N'LG004', N'KT002', N'DD001', N'CD001', N'MS002', N'QG090', 1900, 387000.0000, 425700.0000, 15, N'AU007.jpg', N'')
INSERT [dbo].[tDMHangHoa] ([MaHang], [TenHangHoa], [MaLoaiGo], [MaKichThuoc], [MaDacDiem], [MaCongDung], [MaMau], [MaNuocSX], [SoLuong], [DonGiaNhap], [DonGiaBan], [ThoiGianBaoHanh], [Anh], [GhiChu]) VALUES (N'MH012', N'ShopHouse SH150', N'LG005', N'KT008', N'DD002', N'CD003', N'MS001', N'QG084', 8500, 184500.0000, 202950.0000, 8, N'Robina RB1248.jpg', N'')
INSERT [dbo].[tDMHangHoa] ([MaHang], [TenHangHoa], [MaLoaiGo], [MaKichThuoc], [MaDacDiem], [MaCongDung], [MaMau], [MaNuocSX], [SoLuong], [DonGiaNhap], [DonGiaBan], [ThoiGianBaoHanh], [Anh], [GhiChu]) VALUES (N'MH013', N'Floorpan Red FP33', N'LG001', N'KT001', N'DD003', N'CD001', N'MS002', N'QG007', 5500, 432000.0000, 475200.0000, 10, N'FP33.jpg', N'')
INSERT [dbo].[tDMHangHoa] ([MaHang], [TenHangHoa], [MaLoaiGo], [MaKichThuoc], [MaDacDiem], [MaCongDung], [MaMau], [MaNuocSX], [SoLuong], [DonGiaNhap], [DonGiaBan], [ThoiGianBaoHanh], [Anh], [GhiChu]) VALUES (N'MH014', N'Floorpan Orange FP951', N'LG002', N'KT002', N'DD004', N'CD001', N'MS003', N'QG007', 4600, 432000.0000, 475200.0000, 15, N'FP9512.jpg', N'')
INSERT [dbo].[tDMHangHoa] ([MaHang], [TenHangHoa], [MaLoaiGo], [MaKichThuoc], [MaDacDiem], [MaCongDung], [MaMau], [MaNuocSX], [SoLuong], [DonGiaNhap], [DonGiaBan], [ThoiGianBaoHanh], [Anh], [GhiChu]) VALUES (N'MH015', N'Syncro S177253', N'LG003', N'KT004', N'DD002', N'CD001', N'MS006', N'QG034', 5300, 468000.0000, 514800.0000, 15, N'S177253.jpg', N'')
INSERT [dbo].[tDMHangHoa] ([MaHang], [TenHangHoa], [MaLoaiGo], [MaKichThuoc], [MaDacDiem], [MaCongDung], [MaMau], [MaNuocSX], [SoLuong], [DonGiaNhap], [DonGiaBan], [ThoiGianBaoHanh], [Anh], [GhiChu]) VALUES (N'MH016', N'Thailife TL813', N'LG006', N'KT006', N'DD001', N'CD001', N'MS005', N'QG066', 6000, 162000.0000, 178200.0000, 15, N'KronoSwiss D3784.jpg', N'')
INSERT [dbo].[tDMHangHoa] ([MaHang], [TenHangHoa], [MaLoaiGo], [MaKichThuoc], [MaDacDiem], [MaCongDung], [MaMau], [MaNuocSX], [SoLuong], [DonGiaNhap], [DonGiaBan], [ThoiGianBaoHanh], [Anh], [GhiChu]) VALUES (N'MH017', N'Vanachai VF10711', N'LG007', N'KT007', N'DD003', N'CD001', N'MS002', N'QG066', 6100, 207000.0000, 227700.0000, 15, N'Vanachai VF10711.jpg', N'')
INSERT [dbo].[tDMHangHoa] ([MaHang], [TenHangHoa], [MaLoaiGo], [MaKichThuoc], [MaDacDiem], [MaCongDung], [MaMau], [MaNuocSX], [SoLuong], [DonGiaNhap], [DonGiaBan], [ThoiGianBaoHanh], [Anh], [GhiChu]) VALUES (N'MH018', N'KronoSwiss D3784', N'LG004', N'KT001', N'DD001', N'CD001', N'MS004', N'QG041', 2000, 621000.0000, 683100.0000, 25, N'KronoSwiss D3784.jpg', N'')
INSERT [dbo].[tDMHangHoa] ([MaHang], [TenHangHoa], [MaLoaiGo], [MaKichThuoc], [MaDacDiem], [MaCongDung], [MaMau], [MaNuocSX], [SoLuong], [DonGiaNhap], [DonGiaBan], [ThoiGianBaoHanh], [Anh], [GhiChu]) VALUES (N'MH019', N'Smartwood SW4890', N'LG001', N'KT008', N'DD002', N'CD003', N'MS003', N'QG066', 6600, 180000.0000, 198000.0000, 10, N'Smartwood SW4890.jpg', N'')
INSERT [dbo].[tDMHangHoa] ([MaHang], [TenHangHoa], [MaLoaiGo], [MaKichThuoc], [MaDacDiem], [MaCongDung], [MaMau], [MaNuocSX], [SoLuong], [DonGiaNhap], [DonGiaBan], [ThoiGianBaoHanh], [Anh], [GhiChu]) VALUES (N'MH020', N'Robina RB1248', N'LG002', N'KT005', N'DD004', N'CD001', N'MS004', N'QG060', 10000, 230000.0000, 253000.0000, 10, N'Robina RB1248.jpg', N'')
GO
INSERT [dbo].[tHoaDonBan] ([SoHDB], [MaNV], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB001', N'NV001', CAST(N'2019-08-11T00:00:00.000' AS DateTime), N'KH001', 72854100.0000)
INSERT [dbo].[tHoaDonBan] ([SoHDB], [MaNV], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB002', N'NV002', CAST(N'2019-08-13T00:00:00.000' AS DateTime), N'KH002', 55836000.0000)
INSERT [dbo].[tHoaDonBan] ([SoHDB], [MaNV], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB003', N'NV003', CAST(N'2019-11-25T00:00:00.000' AS DateTime), N'KH003', 105979500.0000)
INSERT [dbo].[tHoaDonBan] ([SoHDB], [MaNV], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB004', N'NV004', CAST(N'2019-12-20T00:00:00.000' AS DateTime), N'KH004', 59573250.0000)
INSERT [dbo].[tHoaDonBan] ([SoHDB], [MaNV], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB005', N'NV005', CAST(N'2020-02-14T00:00:00.000' AS DateTime), N'KH005', 93772800.0000)
INSERT [dbo].[tHoaDonBan] ([SoHDB], [MaNV], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB006', N'NV006', CAST(N'2020-02-14T00:00:00.000' AS DateTime), N'KH006', 108553500.0000)
INSERT [dbo].[tHoaDonBan] ([SoHDB], [MaNV], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB007', N'NV007', CAST(N'2020-03-10T00:00:00.000' AS DateTime), N'KH007', 30800000.0000)
INSERT [dbo].[tHoaDonBan] ([SoHDB], [MaNV], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB008', N'NV008', CAST(N'2020-03-21T00:00:00.000' AS DateTime), N'KH008', 57042810.0000)
INSERT [dbo].[tHoaDonBan] ([SoHDB], [MaNV], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB009', N'NV009', CAST(N'2020-03-26T00:00:00.000' AS DateTime), N'KH009', 52519500.0000)
INSERT [dbo].[tHoaDonBan] ([SoHDB], [MaNV], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB010', N'NV010', CAST(N'2020-04-02T00:00:00.000' AS DateTime), N'KH010', 78348600.0000)
INSERT [dbo].[tHoaDonBan] ([SoHDB], [MaNV], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB011', N'NV001', CAST(N'2021-07-11T00:00:00.000' AS DateTime), N'KH011', 76923000.0000)
INSERT [dbo].[tHoaDonBan] ([SoHDB], [MaNV], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB012', N'NV002', CAST(N'2021-08-20T00:00:00.000' AS DateTime), N'KH012', 45292500.0000)
INSERT [dbo].[tHoaDonBan] ([SoHDB], [MaNV], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB013', N'NV003', CAST(N'2021-08-12T00:00:00.000' AS DateTime), N'KH013', 11850300.0000)
INSERT [dbo].[tHoaDonBan] ([SoHDB], [MaNV], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB014', N'NV004', CAST(N'2021-09-12T00:00:00.000' AS DateTime), N'KH014', 24502500.0000)
INSERT [dbo].[tHoaDonBan] ([SoHDB], [MaNV], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB015', N'NV005', CAST(N'2021-09-22T00:00:00.000' AS DateTime), N'KH015', 8118000.0000)
INSERT [dbo].[tHoaDonBan] ([SoHDB], [MaNV], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB016', N'NV006', CAST(N'2021-09-28T00:00:00.000' AS DateTime), N'KH016', 8514000.0000)
INSERT [dbo].[tHoaDonBan] ([SoHDB], [MaNV], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB017', N'NV007', CAST(N'2021-11-12T00:00:00.000' AS DateTime), N'KH017', 46332000.0000)
INSERT [dbo].[tHoaDonBan] ([SoHDB], [MaNV], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB018', N'NV008', CAST(N'2021-11-22T00:00:00.000' AS DateTime), N'KH018', 40590000.0000)
INSERT [dbo].[tHoaDonBan] ([SoHDB], [MaNV], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB019', N'NV009', CAST(N'2022-01-12T00:00:00.000' AS DateTime), N'KH019', 146401200.0000)
INSERT [dbo].[tHoaDonBan] ([SoHDB], [MaNV], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB020', N'NV010', CAST(N'2022-02-20T00:00:00.000' AS DateTime), N'KH020', 676940000.0000)
GO
INSERT [dbo].[tHoaDonNhap] ([SoHDN], [MaNV], [NgayNhap], [MaNCC], [TongTien]) VALUES (N'HDN001', N'NV002', CAST(N'2021-06-13T00:00:00.000' AS DateTime), N'NCC001', 4586400000.0000)
INSERT [dbo].[tHoaDonNhap] ([SoHDN], [MaNV], [NgayNhap], [MaNCC], [TongTien]) VALUES (N'HDN002', N'NV001', CAST(N'2021-06-28T00:00:00.000' AS DateTime), N'NCC003', 5822100000.0000)
INSERT [dbo].[tHoaDonNhap] ([SoHDN], [MaNV], [NgayNhap], [MaNCC], [TongTien]) VALUES (N'HDN003', N'NV004', CAST(N'2021-06-01T00:00:00.000' AS DateTime), N'NCC002', 1603350000.0000)
INSERT [dbo].[tHoaDonNhap] ([SoHDN], [MaNV], [NgayNhap], [MaNCC], [TongTien]) VALUES (N'HDN004', N'NV005', CAST(N'2021-09-05T00:00:00.000' AS DateTime), N'NCC004', 3047400000.0000)
INSERT [dbo].[tHoaDonNhap] ([SoHDN], [MaNV], [NgayNhap], [MaNCC], [TongTien]) VALUES (N'HDN005', N'NV003', CAST(N'2021-09-10T00:00:00.000' AS DateTime), N'NCC005', 2154050000.0000)
GO
INSERT [dbo].[tKichThuoc] ([MaKichThuoc], [TenKichThuoc]) VALUES (N'KT001', N'1220mm x 197mm x 8mm')
INSERT [dbo].[tKichThuoc] ([MaKichThuoc], [TenKichThuoc]) VALUES (N'KT002', N'1222mm x 197mm x 8mm')
INSERT [dbo].[tKichThuoc] ([MaKichThuoc], [TenKichThuoc]) VALUES (N'KT003', N'1218mm x 198mm x 8mm')
INSERT [dbo].[tKichThuoc] ([MaKichThuoc], [TenKichThuoc]) VALUES (N'KT004', N'1218mm x 198mm x 12mm')
INSERT [dbo].[tKichThuoc] ([MaKichThuoc], [TenKichThuoc]) VALUES (N'KT005', N'803mm x 127mm x 8mm')
INSERT [dbo].[tKichThuoc] ([MaKichThuoc], [TenKichThuoc]) VALUES (N'KT006', N'1200mm x 122mm x 12mm')
INSERT [dbo].[tKichThuoc] ([MaKichThuoc], [TenKichThuoc]) VALUES (N'KT007', N'1200mm x 195mm x 8mm')
INSERT [dbo].[tKichThuoc] ([MaKichThuoc], [TenKichThuoc]) VALUES (N'KT008', N'808mm x 90mm x 8mm')
GO
INSERT [dbo].[tKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai]) VALUES (N'KH001', N'Nguyễn Thị Hải', N'Hà Nội', N'0923872389')
INSERT [dbo].[tKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai]) VALUES (N'KH002', N'Nguyễn Quang Chính', N'Hà Nội', N'0965231644')
INSERT [dbo].[tKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai]) VALUES (N'KH003', N'Trần Văn Quang', N'Thái Bình', N'0862316409')
INSERT [dbo].[tKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai]) VALUES (N'KH004', N'Phạm Văn Cường', N'Hà Nội', N'0832346484')
INSERT [dbo].[tKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai]) VALUES (N'KH005', N'Trần Thị Thu Hoài', N'Nam Định', N'0962114326')
INSERT [dbo].[tKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai]) VALUES (N'KH006', N'Mai Tuyết Linh', N'Bắc Ninh', N'0322316430')
INSERT [dbo].[tKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai]) VALUES (N'KH007', N'Trần Thu Yến', N'Đà Nẵng', N'0866623643')
INSERT [dbo].[tKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai]) VALUES (N'KH008', N'Hoàng Quốc Quân', N'Hà Nam', N'0842316420')
INSERT [dbo].[tKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai]) VALUES (N'KH009', N'Khuất Duy Hoàng', N'Phú Thọ', N'0822363804')
INSERT [dbo].[tKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai]) VALUES (N'KH010', N'Nguyễn Vân Anh', N'Hà Nam', N'0962317443')
INSERT [dbo].[tKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai]) VALUES (N'KH011', N'Nguyễn Lê Ánh Hồng', N'TP. Hồ Chí Minh', N'0972346420')
INSERT [dbo].[tKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai]) VALUES (N'KH012', N'Hoàng Anh Bắc', N'Nam Định', N'0832313415')
INSERT [dbo].[tKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai]) VALUES (N'KH013', N'Nguyễn Hồng Nhung', N'Thái Bình', N'0862431632')
INSERT [dbo].[tKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai]) VALUES (N'KH014', N'Nguyễn Văn Quyết', N'Hải Phòng', N'0862336420')
INSERT [dbo].[tKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai]) VALUES (N'KH015', N'Lương Thành Lai', N'Hà Nội', N'0823163832')
INSERT [dbo].[tKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai]) VALUES (N'KH016', N'Trần Thị Minh Ánh', N'Hà Nam', N'0862416284')
INSERT [dbo].[tKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai]) VALUES (N'KH017', N'Phạm Thị Mai Ánh', N'Quảng Ninh', N'0962416480')
INSERT [dbo].[tKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai]) VALUES (N'KH018', N'Lê Ngọc Anh', N'Bắc Ninh', N'0364313430')
INSERT [dbo].[tKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai]) VALUES (N'KH019', N'Nguyễn Đức Cảnh', N'Phú Thọ', N'0962364322')
INSERT [dbo].[tKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai]) VALUES (N'KH020', N'Nguyễn Quang Long', N'Hà Nội', N'0862314440')
GO
INSERT [dbo].[tLoaiGo] ([MaLoaiGo], [TenLoaiGo]) VALUES (N'LG001', N'Gỗ sồi')
INSERT [dbo].[tLoaiGo] ([MaLoaiGo], [TenLoaiGo]) VALUES (N'LG002', N'Gỗ giáng hương')
INSERT [dbo].[tLoaiGo] ([MaLoaiGo], [TenLoaiGo]) VALUES (N'LG003', N'Gỗ tràm bông vàng')
INSERT [dbo].[tLoaiGo] ([MaLoaiGo], [TenLoaiGo]) VALUES (N'LG004', N'Gỗ chiu liu')
INSERT [dbo].[tLoaiGo] ([MaLoaiGo], [TenLoaiGo]) VALUES (N'LG005', N'Gỗ teak')
INSERT [dbo].[tLoaiGo] ([MaLoaiGo], [TenLoaiGo]) VALUES (N'LG006', N'Gỗ căm xe')
INSERT [dbo].[tLoaiGo] ([MaLoaiGo], [TenLoaiGo]) VALUES (N'LG007', N'Gỗ lim')
GO
INSERT [dbo].[tLogin] ([Username], [Password]) VALUES (N'admin', N'admin')
INSERT [dbo].[tLogin] ([Username], [Password]) VALUES (N'user666', N'123')
GO
INSERT [dbo].[tMauSac] ([MaMau], [TenMau]) VALUES (N'MS001', N'Màu xám')
INSERT [dbo].[tMauSac] ([MaMau], [TenMau]) VALUES (N'MS002', N'Màu đỏ')
INSERT [dbo].[tMauSac] ([MaMau], [TenMau]) VALUES (N'MS003', N'Màu vàng')
INSERT [dbo].[tMauSac] ([MaMau], [TenMau]) VALUES (N'MS004', N'Màu nâu')
INSERT [dbo].[tMauSac] ([MaMau], [TenMau]) VALUES (N'MS005', N'Màu đen')
INSERT [dbo].[tMauSac] ([MaMau], [TenMau]) VALUES (N'MS006', N'Màu trắng')
GO
INSERT [dbo].[tNuocSanXuat] ([MaNuocSX], [TenNuocSX]) VALUES (N'QG007', N'Nga')
INSERT [dbo].[tNuocSanXuat] ([MaNuocSX], [TenNuocSX]) VALUES (N'QG034', N'Tây Ban Nha')
INSERT [dbo].[tNuocSanXuat] ([MaNuocSX], [TenNuocSX]) VALUES (N'QG041', N'Thuỵ Sĩ')
INSERT [dbo].[tNuocSanXuat] ([MaNuocSX], [TenNuocSX]) VALUES (N'QG044', N'Anh')
INSERT [dbo].[tNuocSanXuat] ([MaNuocSX], [TenNuocSX]) VALUES (N'QG049', N'Đức')
INSERT [dbo].[tNuocSanXuat] ([MaNuocSX], [TenNuocSX]) VALUES (N'QG060', N'Malaysia')
INSERT [dbo].[tNuocSanXuat] ([MaNuocSX], [TenNuocSX]) VALUES (N'QG066', N'Thái Lan')
INSERT [dbo].[tNuocSanXuat] ([MaNuocSX], [TenNuocSX]) VALUES (N'QG082', N'Hàn Quốc')
INSERT [dbo].[tNuocSanXuat] ([MaNuocSX], [TenNuocSX]) VALUES (N'QG084', N'Việt Nam')
INSERT [dbo].[tNuocSanXuat] ([MaNuocSX], [TenNuocSX]) VALUES (N'QG086', N'Trung Quốc')
INSERT [dbo].[tNuocSanXuat] ([MaNuocSX], [TenNuocSX]) VALUES (N'QG090', N'Thổ Nhĩ Kì')
GO
INSERT [dbo].[tNhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [DienThoai]) VALUES (N'NCC001', N'Công ty Sàn Gỗ Uyên Minh', N'TP. Hồ Chí Minh', N'0909071257')
INSERT [dbo].[tNhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [DienThoai]) VALUES (N'NCC002', N'Công ty Cổ phần Sàn Đẹp', N'Hà Nội', N'0918421902')
INSERT [dbo].[tNhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [DienThoai]) VALUES (N'NCC003', N'Công ty TNHH INOVAR', N'Hà Nội', N'0982986917')
INSERT [dbo].[tNhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [DienThoai]) VALUES (N'NCC004', N'Công ty TNHH Thương mại và Xây dựng Trần Gia Phát', N'Hải Phòng', N'0934561577')
INSERT [dbo].[tNhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [DienThoai]) VALUES (N'NCC005', N'Công ty TNHH Kiến Hưng', N'Bắc Ninh', N'0979450369')
GO
INSERT [dbo].[tNhanVien] ([MaNV], [TenNV], [GioiTinh], [NgaySinh], [DienThoai], [DiaChi], [MaCV]) VALUES (N'NV001', N'Lê Văn Cường', N'Nam', CAST(N'1999-01-14' AS Date), N'0928548266', N'Hà Nội', N'TN')
INSERT [dbo].[tNhanVien] ([MaNV], [TenNV], [GioiTinh], [NgaySinh], [DienThoai], [DiaChi], [MaCV]) VALUES (N'NV002', N'Trần Thu Hạ', N'Nữ', CAST(N'1997-04-11' AS Date), N'0828649271', N'Nam Định', N'KT')
INSERT [dbo].[tNhanVien] ([MaNV], [TenNV], [GioiTinh], [NgaySinh], [DienThoai], [DiaChi], [MaCV]) VALUES (N'NV003', N'Nguyễn Văn Hưng', N'Nam', CAST(N'1998-08-23' AS Date), N'0934648287', N'TP. Hồ Chí Minh', N'KT')
INSERT [dbo].[tNhanVien] ([MaNV], [TenNV], [GioiTinh], [NgaySinh], [DienThoai], [DiaChi], [MaCV]) VALUES (N'NV004', N'Lại Văn Sáng', N'Nam', CAST(N'1996-12-25' AS Date), N'0978638273', N'Đà Nẵng', N'HT')
INSERT [dbo].[tNhanVien] ([MaNV], [TenNV], [GioiTinh], [NgaySinh], [DienThoai], [DiaChi], [MaCV]) VALUES (N'NV005', N'Nguyễn Vân Anh', N'Nữ', CAST(N'1991-02-03' AS Date), N'0988248291', N'Hà Nội', N'KT')
INSERT [dbo].[tNhanVien] ([MaNV], [TenNV], [GioiTinh], [NgaySinh], [DienThoai], [DiaChi], [MaCV]) VALUES (N'NV006', N'Phạm Quang Khải', N'Nam', CAST(N'1994-07-07' AS Date), N'0938658217', N'Thái Bình', N'QL')
INSERT [dbo].[tNhanVien] ([MaNV], [TenNV], [GioiTinh], [NgaySinh], [DienThoai], [DiaChi], [MaCV]) VALUES (N'NV007', N'Nguyễn Hải Anh', N'Nữ', CAST(N'1990-02-23' AS Date), N'0928648277', N'Hà Nội', N'TN')
INSERT [dbo].[tNhanVien] ([MaNV], [TenNV], [GioiTinh], [NgaySinh], [DienThoai], [DiaChi], [MaCV]) VALUES (N'NV008', N'Trần Văn Chính', N'Nam', CAST(N'1992-05-09' AS Date), N'0828842247', N'Phú Thọ', N'HT')
INSERT [dbo].[tNhanVien] ([MaNV], [TenNV], [GioiTinh], [NgaySinh], [DienThoai], [DiaChi], [MaCV]) VALUES (N'NV009', N'Lê Bạch Yến', N'Nữ', CAST(N'1996-04-21' AS Date), N'0828548272', N'Hải Phòng', N'CS')
INSERT [dbo].[tNhanVien] ([MaNV], [TenNV], [GioiTinh], [NgaySinh], [DienThoai], [DiaChi], [MaCV]) VALUES (N'NV010', N'Trần Thanh Nga', N'Nữ', CAST(N'1995-10-20' AS Date), N'0921658290', N'Quảng Ninh', N'CS')
GO
ALTER TABLE [dbo].[tChiTietHoaDonBan]  WITH CHECK ADD FOREIGN KEY([MaHang])
REFERENCES [dbo].[tDMHangHoa] ([MaHang])
GO
ALTER TABLE [dbo].[tChiTietHoaDonBan]  WITH CHECK ADD FOREIGN KEY([SoHDB])
REFERENCES [dbo].[tHoaDonBan] ([SoHDB])
GO
ALTER TABLE [dbo].[tChiTietHoaDonNhap]  WITH CHECK ADD FOREIGN KEY([MaHang])
REFERENCES [dbo].[tDMHangHoa] ([MaHang])
GO
ALTER TABLE [dbo].[tChiTietHoaDonNhap]  WITH CHECK ADD FOREIGN KEY([SoHDN])
REFERENCES [dbo].[tHoaDonNhap] ([SoHDN])
GO
ALTER TABLE [dbo].[tDMHangHoa]  WITH CHECK ADD FOREIGN KEY([MaCongDung])
REFERENCES [dbo].[tCongDung] ([MaCongDung])
GO
ALTER TABLE [dbo].[tDMHangHoa]  WITH CHECK ADD FOREIGN KEY([MaDacDiem])
REFERENCES [dbo].[tDacDiem] ([MaDacDiem])
GO
ALTER TABLE [dbo].[tDMHangHoa]  WITH CHECK ADD FOREIGN KEY([MaKichThuoc])
REFERENCES [dbo].[tKichThuoc] ([MaKichThuoc])
GO
ALTER TABLE [dbo].[tDMHangHoa]  WITH CHECK ADD FOREIGN KEY([MaLoaiGo])
REFERENCES [dbo].[tLoaiGo] ([MaLoaiGo])
GO
ALTER TABLE [dbo].[tDMHangHoa]  WITH CHECK ADD FOREIGN KEY([MaMau])
REFERENCES [dbo].[tMauSac] ([MaMau])
GO
ALTER TABLE [dbo].[tDMHangHoa]  WITH CHECK ADD FOREIGN KEY([MaNuocSX])
REFERENCES [dbo].[tNuocSanXuat] ([MaNuocSX])
GO
ALTER TABLE [dbo].[tHoaDonBan]  WITH CHECK ADD FOREIGN KEY([MaKhach])
REFERENCES [dbo].[tKhachHang] ([MaKhach])
GO
ALTER TABLE [dbo].[tHoaDonBan]  WITH CHECK ADD FOREIGN KEY([MaNV])
REFERENCES [dbo].[tNhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[tHoaDonNhap]  WITH CHECK ADD FOREIGN KEY([MaNCC])
REFERENCES [dbo].[tNhaCungCap] ([MaNCC])
GO
ALTER TABLE [dbo].[tHoaDonNhap]  WITH CHECK ADD FOREIGN KEY([MaNV])
REFERENCES [dbo].[tNhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[tNhanVien]  WITH CHECK ADD FOREIGN KEY([MaCV])
REFERENCES [dbo].[tCongViec] ([MaCV])
GO
