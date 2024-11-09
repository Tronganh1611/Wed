alter table KHACH_HANG
alter COLUMN GioiTinh bit

alter table NHAN_VIEN
alter column GioiTinh bit

alter table NHAN_VIEN
alter column ChucVu bit

alter table HOA_DON
alter column TrangThai bit

alter table SANPHAM
alter column TrangThai bit

-- Inserting data into KHACH_HANG
INSERT INTO KHACH_HANG (MaKH, TenKH, SDT, DiaChi, Email, MatKhau, GioiTinh) VALUES ('MKH001', N'Nguyen Van An', '0123456789', N'Quận 3, TP.HCM', 'nva@gmail.com', '123', 1);
INSERT INTO KHACH_HANG (MaKH, TenKH, SDT, DiaChi, Email, MatKhau, GioiTinh) VALUES ('MKH002', N'Le Thi Bình', '0123456790', N'Số 180 đường Võ Thị Sáu, Phường 7, Quận 3, Thành phố Hồ Chí Minh', 'ltb@gmail.com', '456', 0);
INSERT INTO KHACH_HANG (MaKH, TenKH, SDT, DiaChi, Email, MatKhau, GioiTinh) VALUES ('MKH003', N'Tran Van Cao', '0123456781', N'Phường 5, Quận 3, Thành phố Hồ Chí Minh', 'tvc@gmail.com', '789', 1);
INSERT INTO KHACH_HANG (MaKH, TenKH, SDT, DiaChi, Email, MatKhau, GioiTinh) VALUES ('MKH004', N'Phạm Thị Dao', '0123456782', N'Quận 5, TP.HCM', 'ptd@gmail.com', '101', 0);
INSERT INTO KHACH_HANG (MaKH, TenKH, SDT, DiaChi, Email, MatKhau, GioiTinh) VALUES ('MKH005', N'Võ Văn Thưởng', '0123456783', N'Số 202 đường Võ Thị Sáu, Phường 7, Quận 3, Thành phố Hồ Chí Minh', 'vve@gmail.com', '201', 1);

-- Inserting data into LoaiSP
INSERT INTO LoaiSP (MaLSP, TenLoai) VALUES ('LSP001', N'Quần');
INSERT INTO LoaiSP (MaLSP, TenLoai) VALUES ('LSP002', N'áo');
INSERT INTO LoaiSP (MaLSP, TenLoai) VALUES ('LSP003', N'váy');
INSERT INTO LoaiSP (MaLSP, TenLoai) VALUES ('LSP004', N'tất');
INSERT INTO LoaiSP (MaLSP, TenLoai) VALUES ('LSP005', N'Mũ');

-- Inserting data into NHAN_VIEN
INSERT INTO NHAN_VIEN (MaNV, TenNV, NgaySinh, GioiTinh, CMND, DiaChi, MatKhau, Email, ChucVu) VALUES ('MNV001', N'Nguyen Van Thưởng', '1990-01-01', 1, '0123456789', N'Số 135 đường Nam Kỳ Khởi Nghĩa, Thành phố Hồ Chí Minh', '123', 'nva@gmail.com', 1);
INSERT INTO NHAN_VIEN (MaNV, TenNV, NgaySinh, GioiTinh, CMND, DiaChi, MatKhau, Email, ChucVu) VALUES ('MNV002', N'Le Thi Gia', '1991-02-02', 0, '0123456790', N'Quận 4, TP.HCM', '456', 'ltb@gmail.com', 0);
INSERT INTO NHAN_VIEN (MaNV, TenNV, NgaySinh, GioiTinh, CMND, DiaChi, MatKhau, Email, ChucVu) VALUES ('MNV003', N'Tran Van Hương', '1992-03-03', 1, '0123456791', N'Phường Bến Thành, Quận 1, Thành phố Hồ Chí Minh, Việt Nam', '789', 'tvc@gmail.com', 1);
INSERT INTO NHAN_VIEN (MaNV, TenNV, NgaySinh, GioiTinh, CMND, DiaChi, MatKhau, Email, ChucVu) VALUES ('MNV004', N'Phạm Thị Thọ', '1993-04-04', 0, '0123456792', N'Số 7 đường Công Trường Lam Sơn, phường Bến Nghé, Quận 1, Thành phố Hồ Chí Minh', '101', 'ptd@gmail.com', 0);
INSERT INTO NHAN_VIEN (MaNV, TenNV, NgaySinh, GioiTinh, CMND, DiaChi, MatKhau, Email, ChucVu) VALUES ('MNV005', N'Võ Văn Cao', '1994-05-05', 1, '0123456793', N'Số 7 đường Công Trường Lam Sơn, phường Bến Nghé, Quận 1, TP.HCM', '201', 'vve@gmail.com', 1);


-- Inserting data into SANPHAM
INSERT INTO SANPHAM (MaSP, MaLSP, TenSP, SoLuong, Size, Mau, Gia, AnhSP, TrangThai, MoTa) 
VALUES ('MSP001', 'LSP005', N'Giày bata sọc', 120, 'L', 'Blue', 15000, 'product-1.jpg', 1, N'Đẹp');
INSERT INTO SANPHAM (MaSP, MaLSP, TenSP, SoLuong, Size, Mau, Gia, AnhSP, TrangThai, MoTa) 
VALUES ('MSP002', 'LSP002', N'Áo jean', 120, 'L', 'Blue', 15000, 'product-2.jpg', 1, N'Đẹp');
-- Thêm sản phẩm thứ 3
INSERT INTO SANPHAM (MaSP, MaLSP, TenSP, SoLuong, Size, Mau, Gia, AnhSP, TrangThai, MoTa) 
VALUES ('MSP003', 'LSP005', N'Giày bata đen', 120, 'L', 'Blue', 15000, 'product-3.jpg', 1, N'Đẹp');

-- Thêm sản phẩm thứ 4
INSERT INTO SANPHAM (MaSP, MaLSP, TenSP, SoLuong, Size, Mau, Gia, AnhSP, TrangThai, MoTa) 
VALUES ('MSP004', 'LSP002', N'Áo ghi lê', 140, 'XL', 'Green', 20000, 'product-4.jpg', 1, N'Gọn');

-- Thêm sản phẩm thứ 5
INSERT INTO SANPHAM (MaSP, MaLSP, TenSP, SoLuong, Size, Mau, Gia, AnhSP, TrangThai, MoTa) 
VALUES ('MSP005', 'LSP002', N'Áo thun đen', 160, 'XXL', 'Yellow', 25000, 'product-5.jpg', 1, N'Nhẹ');

-- Inserting data into HOA_DON
INSERT INTO HOA_DON (MaHD, MaNV, MaKH, NgayDat, TrangThai) VALUES ('MHD001', 'MNV001', 'MKH001', '2024-05-01', 1);
INSERT INTO HOA_DON (MaHD, MaNV, MaKH, NgayDat, TrangThai) VALUES ('MHD002', 'MNV002', 'MKH002', '2024-05-02', 0);
INSERT INTO HOA_DON (MaHD, MaNV, MaKH, NgayDat, TrangThai) VALUES ('MHD003', 'MNV003', 'MKH003', '2024-05-03', 1);
INSERT INTO HOA_DON (MaHD, MaNV, MaKH, NgayDat, TrangThai) VALUES ('MHD004', 'MNV004', 'MKH004', '2024-05-04', 0);
INSERT INTO HOA_DON (MaHD, MaNV, MaKH, NgayDat, TrangThai) VALUES ('MHD005', 'MNV005', 'MKH005', '2024-05-05', 1);

-- Inserting data into ChiTietHoaDon
INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong) VALUES ('MHD001', 'MSP001', 10);
INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong) VALUES ('MHD002', 'MSP002', 15);
INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong) VALUES ('MHD003', 'MSP003', 20);
INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong) VALUES ('MHD004', 'MSP004', 25);
INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong) VALUES ('MHD005', 'MSP005', 30);

-- Inserting data into PhieuNhap
INSERT INTO PhieuNhap (MaPhieu, MaNV, NgayNhap, TenNhaCung, DiaChi) VALUES ('MPN001', 'MNV001', '2024-05-01', 'Chanel', N'Quận 1, TP.HCM');
INSERT INTO PhieuNhap (MaPhieu, MaNV, NgayNhap, TenNhaCung, DiaChi) VALUES ('MPN002', 'MNV002', '2024-05-02', 'Gucci', N'Quận 2, TP.HCM');
INSERT INTO PhieuNhap (MaPhieu, MaNV, NgayNhap, TenNhaCung, DiaChi) VALUES ('MPN003', 'MNV003', '2024-05-03', 'Huawei', N'Quận 3, TP.HCM');
INSERT INTO PhieuNhap (MaPhieu, MaNV, NgayNhap, TenNhaCung, DiaChi) VALUES ('MPN004', 'MNV004', '2024-05-04', 'Coolmate', N'Quận 4, TP.HCM');
INSERT INTO PhieuNhap (MaPhieu, MaNV, NgayNhap, TenNhaCung, DiaChi) VALUES ('MPN005', 'MNV005', '2024-05-05', 'vuiton', N'Quận 5, TP.HCM');

-- Inserting data into ChiTietPhieuNhap
INSERT INTO ChiTietPhieuNhap (MaPhieu, MaSP, SoLuong, GiaGoc) VALUES ('MPN001', 'MSP001', 50, 10000);
INSERT INTO ChiTietPhieuNhap (MaPhieu, MaSP, SoLuong, GiaGoc) VALUES ('MPN002', 'MSP002', 60, 15000);
INSERT INTO ChiTietPhieuNhap (MaPhieu, MaSP, SoLuong, GiaGoc) VALUES ('MPN003', 'MSP003', 70, 20000);
INSERT INTO ChiTietPhieuNhap (MaPhieu, MaSP, SoLuong, GiaGoc) VALUES ('MPN004', 'MSP004', 80, 25000);
INSERT INTO ChiTietPhieuNhap (MaPhieu, MaSP, SoLuong, GiaGoc) VALUES ('MPN005', 'MSP005', 90, 30000);
