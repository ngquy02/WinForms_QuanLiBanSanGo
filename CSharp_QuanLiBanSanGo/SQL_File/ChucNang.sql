--Chức năng:

/*View*/
--Câu 1: Hiện danh mục hàng hoá
GO
CREATE OR ALTER VIEW View_DMHangHoa AS
SELECT MaHang, TenHangHoa, TenLoaiGo, TenKichThuoc, TenDacDiem, TenCongDung, TenMau, TenNuocSX, SoLuong, DonGiaNhap, DonGiaBan, ThoiGianBaoHanh, Anh, GhiChu
FROM tDMHangHoa
	JOIN tLoaiGo ON tDMHangHoa.MaLoaiGo = tLoaiGo.MaLoaiGo
	JOIN tKichThuoc ON tDMHangHoa.MaKichThuoc = tKichThuoc.MaKichThuoc
	JOIN tDacDiem ON tDMHangHoa.MaDacDiem =  tDacDiem.MaDacDiem
	JOIN tCongDung ON tDMHangHoa.MaCongDung = tCongDung.MaCongDung
	JOIN tMauSac ON tDMHangHoa.MaMau = tMauSac.MaMau
	JOIN tNuocSanXuat ON tDMHangHoa.MaNuocSX = tNuocSanXuat.MaNuocSX
GO

--Câu 2: Hiện hoá đơn bán
GO
CREATE OR ALTER VIEW View_HoaDonBan AS
SELECT SoHDB, TenNV, NgayBan, TenKhach, TongTien
FROM tHoaDonBan
	JOIN tNhanVien ON tHoaDonBan.MaNV = tNhanVien.MaNV
	JOIN tKhachHang ON tHoaDonBan.MaKhach = tKhachHang.MaKhach
GO

--Câu 3: Hiện hoá đơn nhập
GO
CREATE OR ALTER VIEW View_HoaDonNhap AS
SELECT SoHDN, TenNV, NgayNhap, TenNCC, TongTien
FROM tHoaDonNhap
	JOIN tNhanVien ON tHoaDonNhap.MaNV = tNhanVien.MaNV
	JOIN tNhaCungCap ON tHoaDonNhap.MaNCC = tNhaCungCap.MaNCC
GO

--Câu 4: Hiện danh sách nhân viên
GO
CREATE OR ALTER VIEW View_NhanVien AS
SELECT MaNV, TenNV, GioiTinh, NgaySinh, DienThoai, DiaChi, TenCV
FROM tNhanVien JOIN tCongViec ON tNhanVien.MaCV = tCongViec.MaCV
GO

/*Trigger*/
--Câu 1: Số lượng trong bảng Danh mục hàng hoá được tự động cập nhật khi nhập hàng và bán hàng.
--Hoá đơn bán:
GO
CREATE OR ALTER TRIGGER Trigger_1_1 ON tChiTietHoaDonBan FOR INSERT, UPDATE AS 
BEGIN
	DECLARE @maHang NVARCHAR(20), @soLuongBan INT

	SELECT @maHang = MaHang, @soLuongBan = SoLuong
	FROM inserted

	UPDATE tDMHangHoa
	SET SoLuong = SoLuong - @soLuongBan
	WHERE MaHang = @maHang
END
GO

--Hoá đơn nhập:
GO
CREATE OR ALTER TRIGGER Trigger_1_2 ON tChiTietHoaDonNhap FOR INSERT, UPDATE AS
BEGIN
	DECLARE @maHang NVARCHAR(20), @soLuongNhap INT

	SELECT @maHang = MaHang, @soLuongNhap = SoLuong
	FROM inserted

	UPDATE tDMHangHoa
	SET SoLuong = SoLuong + @soLuongNhap
	WHERE MaHang = @maHang
END
GO

--SELECT MaHang, SoLuong FROM tDMHangHoa WHERE MaHang = N'MH001'
--SELECT * FROM tChiTietHoaDonBan

--INSERT INTO tChiTietHoaDonBan VALUES (N'HDB020', N'MH001', 200, NULL, NULL)
--INSERT INTO tChiTietHoaDonNhap VALUES (N'HDN005', N'MH001', 500, 359100.0000, NULL, NULL)

--DROP TRIGGER Trigger_1_1
--DROP TRIGGER Trigger_1_2

--Câu 2: Giá nhập trong bảng Danh mục hàng hoá được tự động cập nhật khi nhập hàng.
GO
CREATE OR ALTER TRIGGER Trigger_2 ON tChiTietHoaDonNhap FOR INSERT, UPDATE AS
BEGIN
	DECLARE @maHang NVARCHAR(20), @donGiaNhap MONEY

	SELECT @maHang = MaHang, @donGiaNhap = DonGia
	FROM inserted

	UPDATE tDMHangHoa
	SET DonGiaNhap = @donGiaNhap
	WHERE MaHang = @maHang
END
GO

--Câu 3: Giá bán trong bảng DM hàng hoá được tự động cập nhật = 110% Giá nhập.
GO
CREATE OR ALTER TRIGGER Trigger_3 ON tDMHangHoa FOR INSERT, UPDATE AS
BEGIN
	DECLARE @maHang NVARCHAR(20)

	SELECT @maHang = MaHang
	FROM inserted

	UPDATE tDMHangHoa
	SET DonGiaBan = DonGiaNhap + (DonGiaNhap * 0.1)
	WHERE MaHang = @maHang
END
GO

--UPDATE tChiTietHoaDonNhap SET DonGia = 300000.0000 WHERE MaHang = N'MH020'
--SELECT MaHang, TenHangHoa, DonGiaNhap, DonGiaBan FROM tDMHangHoa WHERE MaHang = N'MH020'

--DROP TRIGGER Trigger_2
--DROP TRIGGER Trigger_3

--Câu 4: Cập nhật tự động cho trường Thành tiền trong bảng Hoá đơn bán với Thành tiền = Số lượng bán * Đơn giá bán.
GO
CREATE OR ALTER TRIGGER Trigger_4 ON tChiTietHoaDonBan FOR INSERT, UPDATE AS
BEGIN
	DECLARE @soHDB NVARCHAR(20), @maHang NVARCHAR(20), @donGia MONEY, @thanhTien MONEY

	SELECT @soHDB = SoHDB, @maHang = MaHang
	FROM inserted

	SELECT @donGia = DonGiaBan
	FROM tDMHangHoa
	WHERE MaHang = @maHang

	UPDATE tChiTietHoaDonBan
	SET ThanhTien = SoLuong * @donGia
	WHERE SoHDB = @soHDB AND MaHang = @maHang
END
GO

--SELECT * FROM tDMHangHoa
--SELECT * FROM tChiTietHoaDonBan
--INSERT INTO tChiTietHoaDonBan VALUES (N'HDB001', N'MH019', 500, NULL, NULL)
--DROP TRIGGER Trigger_4

--Câu 5: Cập nhật cho bảng Danh mục hàng hoá khi thực hiện hành động xóa sản phẩm thì không xóa bản ghi mà cập nhật trường số lượng là 0 và ghi chú là hết hàng.
GO
CREATE OR ALTER TRIGGER Trigger_5 ON tDMHangHoa INSTEAD OF DELETE AS
BEGIN
	DECLARE @maHang NVARCHAR(20)

	SELECT @maHang = MaHang
	FROM deleted

	UPDATE tDMHangHoa
	SET SoLuong = 0, GhiChu = N'Hết hàng'
	WHERE MaHang = @maHang
ENDGO--DELETE FROM tDMHangHoa WHERE MaHang = N'MH020'
--SELECT MaHang, TenHangHoa, SoLuong, GhiChu FROM tDMHangHoa
--DROP TRIGGER Trigger_5

--Câu 6: Cập nhật cho bảng Hoá đơn bán và Hoá đơn nhập khi thực hiện xóa các chi tiết hóa đơn từ bảng Chi tiết hoá đơn bán hoặc bảng Chi tiết hoá đơn nhập mỗi khi xóa hoá đơn.
--Hoá đơn bán:
GO
CREATE OR ALTER TRIGGER Trigger_6_1 ON tHoaDonBan INSTEAD OF DELETE AS
BEGIN
	DELETE FROM tChiTietHoaDonBan
	WHERE SoHDB IN (SELECT SoHDB FROM deleted)

	DELETE FROM tHoaDonBan
	WHERE SoHDB IN (SELECT SoHDB FROM deleted)
END
GO

--Hoá đơn nhập:
GO
CREATE OR ALTER TRIGGER Trigger_6_2 ON tHoaDonBan INSTEAD OF DELETE AS
BEGIN
	DELETE FROM tChiTietHoaDonBan
	WHERE SoHDB IN (SELECT SoHDB FROM deleted)

	DELETE FROM tHoaDonBan
	WHERE SoHDB IN (SELECT SoHDB FROM deleted)
END
GO

--SELECT * FROM tHoaDonBan
--SELECT * FROM tChiTietHoaDonBan
--DELETE FROM tHoaDonBan WHERE SoHDB = 'HDB020'
--DROP TRIGGER Trigger_6_1
--DROP TRIGGER Trigger_6_2

/*Hàm*/
--Câu 1: Tạo hàm có đầu vào là mã khách, đầu ra là danh sách 2 hàng hoá được mua nhiều nhất từ người đó.
GO
CREATE OR ALTER FUNCTION Func_1(@maKhach NVARCHAR(20)) RETURNS TABLE AS
RETURN
	SELECT DISTINCT TOP(2) WITH TIES tDMHangHoa.MaHang, TenHangHoa, SUM(tChiTietHoaDonBan.SoLuong) AS Soluong
	FROM tKhachHang
		JOIN tHoaDonBan ON tHoaDonBan.MaKhach = tKhachHang.MaKhach
		JOIN tChiTietHoaDonBan ON tChiTietHoaDonBan.SoHDB = tHoaDonBan.SoHDB
		JOIN tDMHangHoa ON tDMHangHoa.MaHang = tChiTietHoaDonBan.MaHang
	WHERE tKhachHang.MaKhach = @maKhach
	GROUP BY tDMHangHoa.MaHang, TenHangHoa
	ORDER BY SoLuong DESC
GO

--SELECT * FROM Func_1(N'KH002')

--Câu 2: Tạo hàm có đầu vào là tháng và năm, đầu ra là danh sách hoá đơn và tổng tiền nhập hàng.
GO
CREATE OR ALTER FUNCTION Func_2(@thang INT, @nam INT) RETURNS TABLE AS
RETURN
	SELECT tHoaDonNhap.SoHDN, SUM(SoLuong * DonGia) AS TongTienNhap
	FROM tChiTietHoaDonNhap JOIN tHoaDonNhap ON tChiTietHoaDonNhap.SoHDN = tHoaDonNhap.SoHDN
	WHERE MONTH(NgayNhap) = @thang AND YEAR(NgayNhap) = @nam
	GROUP BY tHoaDonNhap.SoHDN
GO

--SELECT * FROM Func_2(6, 2021)

--Câu 3: Tạo hàm có đầu vào là năm và đầu ra là danh sách 5 hoá đơn có tổng tiền bán hàng nhiều nhất.
GO
CREATE OR ALTER FUNCTION Func_3(@nam INT) RETURNS TABLE AS
RETURN
	SELECT TOP(5) WITH TIES tHoaDonBan.SoHDB, MaNV, NgayBan, MaKhach, SUM(tChiTietHoaDonBan.SoLuong * tDMHangHoa.DonGiaBan) AS DoanhThu
	FROM tChiTietHoaDonBan
		JOIN tHoaDonBan ON tChiTietHoaDonBan.SoHDB = tHoaDonBan.SoHDB
		JOIN tDMHangHoa ON tChiTietHoaDonBan.MaHang = tDMHangHoa.MaHang
	WHERE YEAR(NgayBan) = @nam
	GROUP BY tHoaDonBan.SoHDB, MaNV, NgayBan, MaKhach
	ORDER BY DoanhThu DESC
GO

--SELECT * FROM Func_3(2021)

--Câu 4: Tạo hàm có đầu vào là tháng và năm, đầu ra là danh sách họ tên và tổng tiền của 2 nhân viên bán được nhiều tiền nhất.
GO
CREATE OR ALTER FUNCTION Func_4(@thang INT, @nam INT) RETURNS TABLE AS
RETURN
	SELECT TOP(2) WITH TIES tNhanVien.MaNV, TenNV, SUM(tChiTietHoaDonBan.SoLuong * tDMHangHoa.DonGiaBan) AS DoanhThu
	FROM tChiTietHoaDonBan
		JOIN tHoaDonBan ON tChiTietHoaDonBan.SoHDB = tHoaDonBan.SoHDB
		JOIN tNhanVien ON tHoaDonBan.MaNV = tNhanVien.MaNV
		JOIN tDMHangHoa ON tChiTietHoaDonBan.MaHang = tDMHangHoa.MaHang
	WHERE MONTH(NgayBan) = @thang AND YEAR(NgayBan) = @nam
	GROUP BY tNhanVien.MaNV, TenNV
	ORDER BY DoanhThu DESC
GO

--SELECT * FROM Func_4(9, 2021)