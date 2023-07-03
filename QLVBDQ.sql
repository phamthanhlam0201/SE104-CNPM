CREATE DATABASE QLVBDQ
GO

USE QLVBDQ
GO

CREATE TABLE DVT
(
    MaDVT CHAR(8) CONSTRAINT PK_DVT PRIMARY KEY,
    TenDVT NVARCHAR(20) NOT NULL,
)
--drop table DVT
CREATE TABLE LOAISANPHAM
(
    MaLSP CHAR(8) CONSTRAINT PK_LSP PRIMARY KEY,
    TenLSP NVARCHAR(20) NOT NULL,
    MaDVT CHAR(8) CONSTRAINT FK_LSP_DVT FOREIGN KEY REFERENCES DVT(MaDVT) NOT NULL,
    PhanTramLoiNhuan INT NOT NULL
)
-- drop table LOAISANPHAM
-- drop table CTPMH
-- drop table CTPBH
-- drop table SANPHAM
-- drop table BCTONKHO
CREATE TABLE SANPHAM
(
    MaSP CHAR(8) CONSTRAINT PK_SP PRIMARY KEY,
    TenSP NVARCHAR(20) NOT NULL,
    MaLSP CHAR(8) CONSTRAINT FK_SP_LSP FOREIGN KEY REFERENCES LOAISANPHAM(MaLSP) NOT NULL,
    DonGia FLOAT NOT NULL,
    TonKho INT NOT NULL
)

CREATE TABLE NHACUNGCAP
(
    MaNCC CHAR(8) CONSTRAINT PK_NCC PRIMARY KEY,
    TenNCC NVARCHAR(20) NOT NULL,
    DiaChi NVARCHAR(100) NOT NULL,
    SDT CHAR(10) NOT NULL
)

CREATE TABLE KHACHHANG
(
    MaKH CHAR(8) CONSTRAINT PK_KH PRIMARY KEY,
    TenKH NVARCHAR(20) NOT NULL,
    SDT CHAR(10)
)

CREATE TABLE LOAIDICHVU
(
    MaLDV CHAR(8) CONSTRAINT PK_LDV PRIMARY KEY,
    TenLDV NVARCHAR(20) NOT NULL,
    DonGia FLOAT NOT NULL
)

CREATE TABLE PHIEUBANHANG
(
    SoPhieu CHAR(8) CONSTRAINT PK_PBH PRIMARY KEY,
    NgayLap DATE NOT NULL,
    MaKH CHAR(8) CONSTRAINT FK_PBH_KH FOREIGN KEY REFERENCES KHACHHANG(MaKH) NOT NULL,
    TongTien FLOAT
)

CREATE TABLE CTPBH
(
    SoPhieu CHAR(8) CONSTRAINT FK_CTPBH_PBH FOREIGN KEY REFERENCES PHIEUBANHANG(SoPhieu),
    MaSP CHAR(8) CONSTRAINT FK_CTPBH_SP FOREIGN KEY REFERENCES SANPHAM(MaSP),
    CONSTRAINT PK_CTPBH PRIMARY KEY (SoPhieu,MaSP),
    SoLuong INT NOT NULL,
    DonGiaBH FLOAT,
    ThanhTien FLOAT
)

CREATE TABLE PHIEUMUAHANG
(
    SoPhieu CHAR(8) CONSTRAINT PK_PMH PRIMARY KEY,
    NgayLap DATE NOT NULL,
    MaNCC CHAR(8) CONSTRAINT FK_PMH_NCC FOREIGN KEY REFERENCES NHACUNGCAP(MaNCC) NOT NULL,
    TongTien FLOAT
)

CREATE TABLE CTPMH
(
    SoPhieu CHAR(8) CONSTRAINT FK_CTPMH_PMH FOREIGN KEY REFERENCES PHIEUMUAHANG(SoPhieu),
    MaSP CHAR(8) CONSTRAINT FK_CTPMH_SP FOREIGN KEY REFERENCES SANPHAM(MaSP),
    CONSTRAINT PK_CTPMH PRIMARY KEY (SoPhieu,MaSP),
    SoLuong INT NOT NULL,
    DonGiaMH FLOAT NOT NULL,
    ThanhTien FLOAT
)

CREATE TABLE PHIEUDICHVU
(
    SoPhieu CHAR(8) CONSTRAINT PK_PDV PRIMARY KEY,
    NgayLap DATE NOT NULL,
    MaKH CHAR(8) CONSTRAINT FK_PDV_KH FOREIGN KEY REFERENCES KHACHHANG(MaKH) NOT NULL,
    TongTien FLOAT,
    TongTraTruoc FLOAT,
    TongConLai FLOAT,
    TinhTrang BIT
)

CREATE TABLE CTPDV
(
    SoPhieu CHAR(8) CONSTRAINT FK_CTPDV_PDV FOREIGN KEY REFERENCES PHIEUDICHVU(SoPhieu),
    MaLDV CHAR(8) CONSTRAINT FK_CTPDV_LDV FOREIGN KEY REFERENCES LOAIDICHVU(MaLDV),
    CONSTRAINT PK_CTPDV PRIMARY KEY (SoPhieu,MaLDV),
    ChiPhiRieng FLOAT, --khong nhap coi nhu bang 0
    SoLuong INT NOT NULL,
    DonGia FLOAT,
    ThanhTien FLOAT,
    TraTruoc FLOAT NOT NULL,
    ConLai FLOAT,
	NgayGiao DATE,
    TinhTrang BIT NOT  NULL --mac dinh la false 
	
)

CREATE TABLE THAMSO
(
    TenThamSo NVARCHAR(20) CONSTRAINT PK_TS PRIMARY KEY,
    GiaTri FLOAT NOT NULL
)
drop table THAMSO
-- Thêm cứng tham số, cái này ko được xóa
insert into THAMSO values(N'Tỉ lệ trả trước', 50)
--select TenThamSo as N'Tên tham số', GiaTri as N'Tỉ lệ trả trước' from THAMSO
CREATE TABLE BCTONKHO
(
    Thang INT,
    Nam INT,
    MaSP CHAR(8) CONSTRAINT FK_BCTK_SP FOREIGN KEY REFERENCES SANPHAM(MaSP),
    CONSTRAINT PK_BCTK PRIMARY KEY (Thang,Nam,MaSP),
    TonDau INT,
    SLMua INT,
    SLBan INT,
    TonCuoi INT    
)

CREATE TABLE NGUOIDUNG
(
    Email VARCHAR(40) CONSTRAINT PK_ND PRIMARY KEY,
    SDT CHAR(10) NOT NULL,
    MatKhau NVARCHAR(200) NOT NULL, 
    HoTen NVARCHAR(200) NOT NULL,
    NgaySinh DATE NOT NULL,
    GioiTinh BIT NOT NULL,
    LoaiNguoiDung INT
)


CREATE TABLE CHUCNANG
(
    MaChucNang CHAR(8) CONSTRAINT PK_CN PRIMARY KEY,
    TenChucNang VARCHAR(30) NOT NULL,
    TenManHinhDuocLoad VARCHAR(30) NOT NULL,
)

CREATE TABLE NHOMNGUOIDUNG
(
    MaNhom CHAR(8) CONSTRAINT PK_NND PRIMARY KEY,
    TenNhom VARCHAR(30) NOT NULL,
)

CREATE TABLE PHANQUYEN
(
    MaNhom CHAR(8) CONSTRAINT FK_PQ_NND FOREIGN KEY REFERENCES NHOMNGUOIDUNG(MaNhom),
    MaChucNang CHAR(8) CONSTRAINT FK_PQ_CN FOREIGN KEY REFERENCES CHUCNANG(MaChucNang),
    CONSTRAINT PK_PQ PRIMARY KEY (MaNhom, MaChucNang),
)

INSERT INTO NGUOIDUNG(Email, SDT, MatKhau, HoTen, NgaySinh, GioiTinh, LoaiNguoiDung)
VALUES ('0', '1234567', '1111', N'Nguyen Thi Thuy', '11-11-2011', 1, 0)

INSERT INTO NGUOIDUNG(Email, SDT, MatKhau, HoTen, NgaySinh, GioiTinh, LoaiNguoiDung)
VALUES ('1', '1234567', N'1111', N'Nguyễn Thị Thùy', '11-11-2011', 1, 1)

INSERT INTO NGUOIDUNG(Email, SDT, MatKhau, HoTen, NgaySinh, GioiTinh, LoaiNguoiDung)
VALUES ('2', '1234567', N'1111', N'Nguyễn Thị Thùy', '11-11-2011', 1, 2)

INSERT INTO NGUOIDUNG(Email, SDT, MatKhau, HoTen, NgaySinh, GioiTinh, LoaiNguoiDung)
VALUES ('thuynt@gmail.com', '01234567', N'1111', N'Nguyễn Thị Thùy', '11-11-2011', 1, 2)

INSERT INTO NGUOIDUNG(Email, SDT, MatKhau, HoTen, NgaySinh, GioiTinh, LoaiNguoiDung)
VALUES ('ngocthu@gmail.com', '01234567', N'1', N'Nguyễn Ngọc Thức', '11-11-2011', 1, 2)

INSERT INTO NGUOIDUNG(Email, SDT, MatKhau, HoTen, NgaySinh, GioiTinh, LoaiNguoiDung)
VALUES ('minhhieu@gmail.com', '01234567', N'1', N'Hoàng Minh Hiếu', '11-11-2011', 1, 2)

INSERT INTO NGUOIDUNG(Email, SDT, MatKhau, HoTen, NgaySinh, GioiTinh, LoaiNguoiDung)
VALUES ('nhuy@gmail.com', '01234567', N'1', N'Lê Thị Như Ý', '11-11-2011', 1, 2)

--Select * from NGUOIDUNG
--where Email = 'thuy123@gmail.com' and MatKhau = '1111';

CREATE PROCEDURE GetAccountByEmailPassword
@email varchar(40), @password varchar(30)
as
begin
	Select * from NGUOIDUNG
	where Email = @email and MatKhau = @password;
end
go

--EXEC dbo.GetAccountByEmailPassword @email = 'Thuy123@gmail.com', @password = '1111'

CREATE PROCEDURE GetAccountByEmail
@email varchar(40)
as
begin
	Select * from NGUOIDUNG
	where Email = @email
end
go

--EXEC dbo.GetAccountByEmail @email = 'Thuy123@gmail.com'

CREATE PROCEDURE InsertAccount
@email varchar(40), @SDT char(10), @tenTK nvarchar(50), @ngaySinh date, @gioiTinh bit, @matKhau nvarchar(30)
as
begin
	INSERT INTO NGUOIDUNG(Email, SDT, MatKhau, HoTen, NgaySinh, GioiTinh, LoaiNguoiDung)
	VALUES (@email, @SDT, @matKhau, @tenTK, @ngaySinh, @gioiTinh, 0)
end
go

--drop procedure dbo.InsertAccount

create trigger trg_CTPDV_insert on CTPDV after insert as
Begin
	UPDATE PHIEUDICHVU
	set TongTien = TongTien + ( select ThanhTien from inserted where SoPhieu = PHIEUDICHVU.SoPhieu),
		TongTraTruoc = TongTraTruoc + ( select TraTruoc from inserted where SoPhieu = PHIEUDICHVU.SoPhieu),
		TongConLai = TongConLai + ( select ConLai from inserted where SoPhieu = PHIEUDICHVU.SoPhieu)
	from PHIEUDICHVU
	join inserted on PHIEUDICHVU.SoPhieu = inserted.SoPhieu

	declare @sp VARCHAR(100)
	set @sp = (select SoPhieu from inserted)
	Begin
		if ((select count(CTPDV.MaLDV) from CTPDV, PHIEUDICHVU where PHIEUDICHVU.SoPhieu = CTPDV.SoPhieu and CTPDV.SoPhieu = @sp) != 
			(select count(CTPDV.MaLDV) from CTPDV, PHIEUDICHVU where PHIEUDICHVU.SoPhieu = CTPDV.SoPhieu and CTPDV.SoPhieu = @sp and CTPDV.TinhTrang = 1))
			update PHIEUDICHVU set TinhTrang = 0 where SoPhieu = @sp
		else update PHIEUDICHVU set TinhTrang = 1 where SoPhieu = @sp
	end
end
--drop trigger trg_CTPDV_update
create trigger trg_CTPDV_delete on CTPDV after delete as
Begin
	UPDATE PHIEUDICHVU
	set TongTien = TongTien - ( select ThanhTien from deleted where SoPhieu = PHIEUDICHVU.SoPhieu),
		TongTraTruoc = TongTraTruoc - ( select TraTruoc from deleted where SoPhieu = PHIEUDICHVU.SoPhieu),
		TongConLai = TongConLai - ( select ConLai from deleted where SoPhieu = PHIEUDICHVU.SoPhieu)
	from PHIEUDICHVU
	join deleted on PHIEUDICHVU.SoPhieu = deleted.SoPhieu

	declare @sp VARCHAR(100)
	set @sp = (select SoPhieu from deleted)
	Begin
		if ((select count(CTPDV.MaLDV) from CTPDV, PHIEUDICHVU where PHIEUDICHVU.SoPhieu = CTPDV.SoPhieu and CTPDV.SoPhieu = @sp) != 
			(select count(CTPDV.MaLDV) from CTPDV, PHIEUDICHVU where PHIEUDICHVU.SoPhieu = CTPDV.SoPhieu and CTPDV.SoPhieu = @sp and CTPDV.TinhTrang = 1))
			update PHIEUDICHVU set TinhTrang = 0 where SoPhieu = @sp
		else update PHIEUDICHVU set TinhTrang = 1 where SoPhieu = @sp
	end
end

create trigger trg_CTPDV_update on CTPDV after update as
begin
	--update PHIEUDICHVU
	declare @sp VARCHAR(100)
	set @sp = (select SoPhieu from inserted)
	Begin
		if ((select count(CTPDV.MaLDV) from CTPDV, PHIEUDICHVU where PHIEUDICHVU.SoPhieu = CTPDV.SoPhieu and CTPDV.SoPhieu = @sp) != 
			(select count(CTPDV.MaLDV) from CTPDV, PHIEUDICHVU where PHIEUDICHVU.SoPhieu = CTPDV.SoPhieu and CTPDV.SoPhieu = @sp and CTPDV.TinhTrang = 1))
			update PHIEUDICHVU set TinhTrang = 0 where SoPhieu = @sp
		else update PHIEUDICHVU set TinhTrang = 1 where SoPhieu = @sp
	end
end


-------------------
create trigger trg_CTPBH_insert on CTPBH after insert as
Begin
	UPDATE SANPHAM
	set TonKho = TonKho - (select SoLuong from inserted)
	where SANPHAM.MaSP = (select MaSP from inserted)
	UPDATE PHIEUBANHANG
	set TongTien = TongTien + (select ThanhTien from inserted)
	where SoPhieu = (select SoPhieu from inserted)
	
end

--drop trigger trg_CTPBH_delete
create trigger trg_CTPBH_delete on CTPBH after delete as
Begin
	UPDATE SANPHAM
	set TonKho = TonKho + (select SoLuong from deleted)
	where SANPHAM.MaSP = (select MaSP from deleted)
	UPDATE PHIEUBANHANG
	set TongTien = TongTien - (select ThanhTien from deleted)
	where SoPhieu = (select SoPhieu from deleted)
end

---------------------------------------

create trigger trg_CTPMH_insert on CTPMH after insert as
Begin
	UPDATE SANPHAM
	set TonKho = TonKho + (select SoLuong from inserted)
	where SANPHAM.MaSP = (select MaSP from inserted)
	UPDATE PHIEUMUAHANG
	set TongTien = TongTien + (select ThanhTien from inserted)
	where SoPhieu = (select SoPhieu from inserted)
end

--drop trigger trg_CTPMH_insert
create trigger trg_CTPMH_delete on CTPMH after delete as
Begin
	UPDATE SANPHAM
	set TonKho = TonKho - (select SoLuong from deleted)
	where SANPHAM.MaSP = (select MaSP from deleted)
	UPDATE PHIEUMUAHANG
	set TongTien = TongTien - (select ThanhTien from deleted)
	where SoPhieu = (select SoPhieu from deleted)
end

--delete from BAOCAO where Thang = 6
--delete from BCTONKHO where Thang = 6