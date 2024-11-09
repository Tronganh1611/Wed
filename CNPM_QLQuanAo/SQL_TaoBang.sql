create database QLBH
go
use QLBH
go
/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2017                    */
/* Created on:     4/29/2024 11:13:01 AM                        */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ChiTietHoaDon') and o.name = 'FK_CHITIETH_CHITIETHO_HOA_DON')
alter table ChiTietHoaDon
   drop constraint FK_CHITIETH_CHITIETHO_HOA_DON
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ChiTietHoaDon') and o.name = 'FK_CHITIETH_CHITIETHO_SANPHAM')
alter table ChiTietHoaDon
   drop constraint FK_CHITIETH_CHITIETHO_SANPHAM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ChiTietPhieuNhap') and o.name = 'FK_CHITIETP_CHITIETPH_PHIEUNHA')
alter table ChiTietPhieuNhap
   drop constraint FK_CHITIETP_CHITIETPH_PHIEUNHA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ChiTietPhieuNhap') and o.name = 'FK_CHITIETP_CHITIETPH_SANPHAM')
alter table ChiTietPhieuNhap
   drop constraint FK_CHITIETP_CHITIETPH_SANPHAM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOA_DON') and o.name = 'FK_HOA_DON_DUYỆT_NHAN_VIE')
alter table HOA_DON
   drop constraint FK_HOA_DON_DUYỆT_NHAN_VIE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOA_DON') and o.name = 'FK_HOA_DON_NHẬN_KHACH_HA')
alter table HOA_DON
   drop constraint FK_HOA_DON_NHẬN_KHACH_HA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PhieuNhap') and o.name = 'FK_PHIEUNHA_NHẬP_NHAN_VIE')
alter table PhieuNhap
   drop constraint FK_PHIEUNHA_NHẬP_NHAN_VIE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SANPHAM') and o.name = 'FK_SANPHAM_THUOC_LOAISP')
alter table SANPHAM
   drop constraint FK_SANPHAM_THUOC_LOAISP
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ChiTietHoaDon')
            and   name  = 'ChiTietHoaDon_FK'
            and   indid > 0
            and   indid < 255)
   drop index ChiTietHoaDon.ChiTietHoaDon_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ChiTietHoaDon')
            and   name  = 'ChiTietHoaDon2_FK'
            and   indid > 0
            and   indid < 255)
   drop index ChiTietHoaDon.ChiTietHoaDon2_FK
go

if exists (select 1
from  sysobjects
           where  id = object_id('ChiTietHoaDon')
            and   type = 'U')
   drop table ChiTietHoaDon
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ChiTietPhieuNhap')
            and   name  = 'ChiTietPhieuNhap_FK'
            and   indid > 0
            and   indid < 255)
   drop index ChiTietPhieuNhap.ChiTietPhieuNhap_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ChiTietPhieuNhap')
            and   name  = 'ChiTietPhieuNhap2_FK'
            and   indid > 0
            and   indid < 255)
   drop index ChiTietPhieuNhap.ChiTietPhieuNhap2_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ChiTietPhieuNhap')
            and   type = 'U')
   drop table ChiTietPhieuNhap
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('HOA_DON')
            and   name  = 'Nhận_FK'
            and   indid > 0
            and   indid < 255)
   drop index HOA_DON.Nhận_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('HOA_DON')
            and   name  = 'Duyệt_FK'
            and   indid > 0
            and   indid < 255)
   drop index HOA_DON.Duyệt_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HOA_DON')
            and   type = 'U')
   drop table HOA_DON
go

if exists (select 1
            from  sysobjects
           where  id = object_id('KHACH_HANG')
            and   type = 'U')
   drop table KHACH_HANG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LoaiSP')
            and   type = 'U')
   drop table LoaiSP
go

if exists (select 1
            from  sysobjects
           where  id = object_id('NHAN_VIEN')
            and   type = 'U')
   drop table NHAN_VIEN
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PhieuNhap')
            and   name  = 'Nhập_FK'
            and   indid > 0
            and   indid < 255)
   drop index PhieuNhap.Nhập_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PhieuNhap')
            and   type = 'U')
   drop table PhieuNhap
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('SANPHAM')
            and   name  = 'Thuoc_FK'
            and   indid > 0
            and   indid < 255)
   drop index SANPHAM.Thuoc_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SANPHAM')
            and   type = 'U')
   drop table SANPHAM
go

/*==============================================================*/
/* Table: ChiTietHoaDon                                         */
/*==============================================================*/
create table ChiTietHoaDon (
   MaHD                 char(6)              not null,
   MaSP                 char(6)              not null,
SoLuong              int                  null,
   constraint PK_CHITIETHOADON primary key (MaHD, MaSP)
)
go

/*==============================================================*/
/* Index: ChiTietHoaDon2_FK                                     */
/*==============================================================*/




create nonclustered index ChiTietHoaDon2_FK on ChiTietHoaDon (MaSP ASC)
go

/*==============================================================*/
/* Index: ChiTietHoaDon_FK                                      */
/*==============================================================*/




create nonclustered index ChiTietHoaDon_FK on ChiTietHoaDon (MaHD ASC)
go

/*==============================================================*/
/* Table: ChiTietPhieuNhap                                      */
/*==============================================================*/
create table ChiTietPhieuNhap (
   MaPhieu              char(6)              not null,
   MaSP                 char(6)              not null,
   SoLuong              int                  null,
   GiaGoc               int                  null,
   constraint PK_CHITIETPHIEUNHAP primary key (MaPhieu, MaSP)
)
go

/*==============================================================*/
/* Index: ChiTietPhieuNhap2_FK                                  */
/*==============================================================*/




create nonclustered index ChiTietPhieuNhap2_FK on ChiTietPhieuNhap (MaSP ASC)
go

/*==============================================================*/
/* Index: ChiTietPhieuNhap_FK                                   */
/*==============================================================*/




create nonclustered index ChiTietPhieuNhap_FK on ChiTietPhieuNhap (MaPhieu ASC)
go

/*==============================================================*/
/* Table: HOA_DON                                               */
/*==============================================================*/
create table HOA_DON (
   MaHD                 char(6)              not null,
   MaNV                 char(6)              not null,
   MaKH                 char(6)              not null,
   NgayDat              varchar(12)          null,
   TrangThai            binary(1)            null,
   constraint PK_HOA_DON primary key (MaHD)
)
go

/*==============================================================*/
/* Index: Duyệt_FK                                              */
/*==============================================================*/




create nonclustered index Duyệt_FK on HOA_DON (MaNV ASC)
go

/*==============================================================*/
/* Index: Nhận_FK                                               */
/*==============================================================*/




create nonclustered index Nhận_FK on HOA_DON (MaKH ASC)
go

/*==============================================================*/
/* Table: KHACH_HANG                                            */
/*==============================================================*/
create table KHACH_HANG (
   MaKH                 char(6)              not null,
   TenKH                nvarchar(60)         null,
   SDT                  varchar(12)          null,
   DiaChi               nvarchar(100)        null,
   Email                varchar(80)          null,
   MatKhau              varchar(80)          null,
   GioiTinh             binary(1)            null,
   constraint PK_KHACH_HANG primary key (MaKH)
)
go

/*==============================================================*/
/* Table: LoaiSP                                                */
/*==============================================================*/
create table LoaiSP (
   MaLSP                char(6)              not null,
   TenLoai              nvarchar(20)         null,
   constraint PK_LOAISP primary key (MaLSP)
)
go

/*==============================================================*/
/* Table: NHAN_VIEN                                             */
/*==============================================================*/
create table NHAN_VIEN (
   MaNV                 char(6)              not null,
   TenNV                nvarchar(60)         null,
   NgaySinh             varchar(12)          null,
   GioiTinh             binary(1)            null,
   CMND                 char(30)             null,
   DiaChi               nvarchar(100)        null,
   MatKhau              varchar(80)          null,
   Email                varchar(80)          null,
   ChucVu               binary(1)            null,
   constraint PK_NHAN_VIEN primary key (MaNV)
)
go

/*==============================================================*/
/* Table: PhieuNhap                                             */
/*==============================================================*/
create table PhieuNhap (
   MaPhieu              char(6)              not null,
   MaNV                 char(6)              not null,
   NgayNhap             varchar(12)          null,
   TenNhaCung           nvarchar(80)         null,
   DiaChi               nvarchar(100)        null,
   constraint PK_PHIEUNHAP primary key (MaPhieu)
)
go

/*==============================================================*/
/* Index: Nhập_FK                                               */
/*==============================================================*/




create nonclustered index Nhập_FK on PhieuNhap (MaNV ASC)
go

/*==============================================================*/
/* Table: SANPHAM                                               */
/*==============================================================*/
create table SANPHAM (
   MaSP                 char(6)              not null,
   MaLSP                char(6)              null,
   TenSP                nvarchar(60)         null,
   SoLuong              int                  null,
   Size                 varchar(10)          null,
   Mau                  nvarchar(30)         null,
Gia                  int                  null,
   AnhSP                varchar(60)          null,
   TrangThai            binary(1)            null,
   MoTa                 nvarchar(100)        null,
   constraint PK_SANPHAM primary key (MaSP)
)
go

/*==============================================================*/
/* Index: Thuoc_FK                                              */
/*==============================================================*/




create nonclustered index Thuoc_FK on SANPHAM (MaLSP ASC)
go

alter table ChiTietHoaDon
   add constraint FK_CHITIETH_CHITIETHO_HOA_DON foreign key (MaHD)
      references HOA_DON (MaHD)
go

alter table ChiTietHoaDon
   add constraint FK_CHITIETH_CHITIETHO_SANPHAM foreign key (MaSP)
      references SANPHAM (MaSP)
go

alter table ChiTietPhieuNhap
   add constraint FK_CHITIETP_CHITIETPH_PHIEUNHA foreign key (MaPhieu)
      references PhieuNhap (MaPhieu)
go

alter table ChiTietPhieuNhap
   add constraint FK_CHITIETP_CHITIETPH_SANPHAM foreign key (MaSP)
      references SANPHAM (MaSP)
go

alter table HOA_DON
   add constraint FK_HOA_DON_DUYỆT_NHAN_VIE foreign key (MaNV)
      references NHAN_VIEN (MaNV)
go

alter table HOA_DON
   add constraint FK_HOA_DON_NHẬN_KHACH_HA foreign key (MaKH)
      references KHACH_HANG (MaKH)
go

alter table PhieuNhap
   add constraint FK_PHIEUNHA_NHẬP_NHAN_VIE foreign key (MaNV)
      references NHAN_VIEN (MaNV)
go

alter table SANPHAM
   add constraint FK_SANPHAM_THUOC_LOAISP foreign key (MaLSP)
      references LoaiSP (MaLSP)
go
