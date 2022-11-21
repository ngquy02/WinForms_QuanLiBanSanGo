/* 4 Trigger đầu tiên là tối thiểu */
--Cập nhật số lượng khi nhập
GO
CREATE OR ALTER TRIGGER CapNhatSoLuongNhap ON tChiTietHoaDonNhap FOR INSERT AS 
BEGIN
	DECLARE @maHang NVARCHAR(50), @sln INT
	SELECT @maHang = MaHang, @sln = SoLuong
	FROM inserted
	UPDATE dbo.tDMHangHoa SET SoLuong = ISNULL(SoLuong, 0) + (ISNULL(@sln, 0))
	WHERE @maHang = MaHang
END
GO

--Cập nhật số lượng khi bán
GO
CREATE OR ALTER TRIGGER CapNhatSoLuongBan ON tChiTietHoaDonBan FOR INSERT AS 
BEGIN
	DECLARE @maHang NVARCHAR(50), @slb INT
	SELECT @maHang = MaHang, @slb = SoLuong FROM inserted
	UPDATE dbo.tDMHangHoa SET SoLuong = ISNULL(SoLuong, 0) - ISNULL(@slb, 0)
	WHERE @maHang = MaHang
END
GO

--Cập nhật giá khi nhập
GO
CREATE OR ALTER TRIGGER CapNhatGiaNhap ON tChiTietHoaDonNhap FOR INSERT, UPDATE AS
BEGIN
	DECLARE @maHang NVARCHAR(20), @donGiaNhap MONEY

	SELECT @maHang = MaHang, @donGiaNhap = DonGia
	FROM inserted

	UPDATE tDMHangHoa
	SET DonGiaNhap = @donGiaNhap
	WHERE MaHang = @maHang
END
GO

--Cập nhật giá khi bán
GO
CREATE OR ALTER TRIGGER CapNhatGiaBan ON tDMHangHoa FOR INSERT, UPDATE AS
BEGIN
	DECLARE @maHang NVARCHAR(20)

	SELECT @maHang = MaHang
	FROM inserted

	UPDATE tDMHangHoa
	SET DonGiaBan = DonGiaNhap + (DonGiaNhap * 0.1)
	WHERE MaHang = @maHang
END
GO

--Cập nhật thành tiền trong bảng Chi tiết hoá đơn bán
GO
CREATE OR ALTER TRIGGER ThanhTienBan ON tChiTietHoaDonBan FOR INSERT, UPDATE AS
BEGIN
	DECLARE @soHDB NVARCHAR(20), @donGiaBan MONEY, @maHang NVARCHAR(20)
	SELECT @soHDB = SoHDB, @maHang = MaHang FROM inserted
	SELECT @donGiaBan = DonGiaBan FROM tDMHangHoa WHERE MaHang = @maHang
	UPDATE tChiTietHoaDonBan SET ThanhTien = (SoLuong * @donGiaBan) - (ISNULL((GiamGia * 0.01), 0) * (SoLuong * @donGiaBan))
	WHERE SoHDB = @soHDB AND MaHang = @maHang
END
GO

--Cập nhật thành tiền trong bảng Chi tiết hoá đơn nhập
GO
CREATE OR ALTER TRIGGER ThanhTienNhap ON tChiTietHoaDonNhap FOR INSERT, UPDATE AS
BEGIN
	DECLARE @soHDN NVARCHAR(20), @donGiaNhap MONEY, @maHang NVARCHAR(20)
	SELECT @soHDN = SoHDN, @maHang = MaHang FROM inserted
	UPDATE tChiTietHoaDonNhap SET ThanhTien = (SoLuong * DonGia) - (ISNULL(GiamGia, 0) * (SoLuong * DonGia))
	where SoHDN = @soHDN AND MaHang = @maHang
END
GO

--Cập nhật tổng tiền hoá đơn bán
GO
CREATE OR ALTER TRIGGER TongTienBan ON tChiTietHoaDonBan FOR INSERT, UPDATE, DELETE AS
BEGIN
	DECLARE @inSoHDB NVARCHAR(20), @deSoHDB NVARCHAR(20), @inThanhTien MONEY, @deThanhTien MONEY
	SELECT @inSoHDB = SoHDB, @inThanhTien = ThanhTien FROM inserted
	SELECT @deSoHDB = SoHDB, @deThanhTien = ThanhTien FROM deleted
	UPDATE tHoaDonBan SET TongTien = ISNULL(TongTien, 0) + ISNULL(@inThanhTien,0) - ISNULL(@deThanhTien, 0) WHERE SoHDB = ISNULL(@inSoHDB, @deSoHDB)
END
GO

--Cập nhật tổng tiền hoá đơn nhập
GO
CREATE OR ALTER TRIGGER TongTienNhap ON tChiTietHoaDonNhap FOR INSERT, UPDATE, DELETE AS
BEGIN
	DECLARE @inSoHDN NVARCHAR(20),@deSoHDN NVARCHAR(20), @inThanhTien MONEY, @deThanhTien MONEY
	SELECT @inSoHDN = SoHDN, @inThanhTien = ThanhTien FROM inserted
	SELECT @deSoHDN = SoHDN, @deThanhTien = ThanhTien FROM deleted
	UPDATE tHoaDonNhap SET TongTien = ISNULL(TongTien, 0) + ISNULL(@inThanhTien, 0) - ISNULL(@deThanhTien, 0) WHERE SoHDN = ISNULL(@inSoHDN, @deSoHDN)
END
GO

--Xoá hàng hoá sẽ ghi là hết hàng và đặt số lượng về 0
GO
CREATE OR ALTER TRIGGER XoaHangHoa ON tDMHangHoa INSTEAD OF DELETE AS
BEGIN
	DECLARE @maHang NVARCHAR(20)
	SELECT @maHang = MaHang FROM deleted
	UPDATE tDMHangHoa SET SoLuong = 0, GhiChu = N'Hết hàng' WHERE MaHang = @maHang
ENDGO

--Xoá hoá đơn bán
GO
CREATE OR ALTER TRIGGER XoaHDB ON tHoaDonBan INSTEAD OF DELETE AS
BEGIN
	DELETE FROM tChiTietHoaDonBan WHERE SoHDB IN (SELECT SoHDB FROM deleted)
	DELETE FROM tHoaDonBan WHERE SoHDB IN (SELECT SoHDB FROM deleted)
END
GO

--Xoá hoá đơn nhập
GO
CREATE OR ALTER TRIGGER XoaHDN ON tHoaDonNhap INSTEAD OF DELETE AS
BEGIN
	DELETE FROM tChiTietHoaDonNhap WHERE SoHDN IN (SELECT SoHDN FROM deleted)
	DELETE FROM tHoaDonNhap WHERE SoHDN IN (SELECT SoHDN FROM deleted)
END
GO

--Xoá nhân viên
GO
CREATE OR ALTER TRIGGER XoaNV ON tNhanVien INSTEAD OF DELETE AS
BEGIN
	DELETE FROM tHoaDonBan WHERE MaNV IN (SELECT MaNV FROM deleted)
	DELETE FROM tHoaDonNhap WHERE MaNV IN (SELECT MaNV FROM deleted)
	DELETE FROM tNhanVien WHERE MaNV IN (SELECT MaNV FROM deleted)
END
GO

--Xoá khách hàng
GO
CREATE OR ALTER TRIGGER XoaKH ON tKhachHang INSTEAD OF DELETE AS
BEGIN
	DELETE FROM tHoaDonBan WHERE MaKhach IN (SELECT MaKhach FROM deleted)
	DELETE FROM tKhachHang WHERE MaKhach IN (SELECT MaKhach FROM deleted)
END
GO

--Xoá nhà cung cấp
GO
CREATE OR ALTER TRIGGER XoaNCC ON tNhaCungCap INSTEAD OF DELETE AS
BEGIN
	DELETE FROM tHoaDonNhap WHERE MaNCC IN (SELECT MaNCC FROM deleted)
	DELETE FROM tNhaCungCap WHERE MaNCC IN (SELECT MaNCC FROM deleted)
END
GO
