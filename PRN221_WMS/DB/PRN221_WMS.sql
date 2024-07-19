USE Master
GO

IF DB_ID('PRN221_WMS') IS NOT NULL
BEGIN
    ALTER DATABASE PRN221_WMS SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE PRN221_WMS;
END
GO

CREATE DATABASE PRN221_WMS
GO

USE PRN221_WMS
GO


-- Tạo bảng Category
CREATE TABLE Category (
    CategoryId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    CategoryCode VARCHAR(20) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Status INT NOT NULL
);

-- Tạo bảng StorageArea
CREATE TABLE StorageArea (
    AreaId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    AreaCode VARCHAR(20) NOT NULL,
    AreaName NVARCHAR(100) NOT NULL,
    Status INT NOT NULL
);

-- Tạo bảng Account
CREATE TABLE Account (
    AccountId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    AccountCode VARCHAR(20) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Password VARCHAR(50) NOT NULL,
    Role INT NOT NULL,
    Phone VARCHAR(50),
    Status INT NOT NULL
);

-- Tạo bảng Partner
CREATE TABLE Partner (
    PartnerId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    PartnerCode VARCHAR(20) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Status INT NOT NULL
);

-- Tạo bảng Lot
CREATE TABLE Lot (
    LotId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    AccountId INT NOT NULL,
    PartnerId INT NOT NULL,
    LotCode VARCHAR(20) NOT NULL,
    DateIn DATE NOT NULL,
    Note TEXT,
    Status INT NOT NULL,
    FOREIGN KEY (AccountId) REFERENCES Account(AccountId),
    FOREIGN KEY (PartnerId) REFERENCES Partner(PartnerId)
);

-- Tạo bảng StockOut
CREATE TABLE StockOut (
    StockOutId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    AccountId INT NOT NULL,
    PartnerId INT NOT NULL,
    DateOut DATE NOT NULL,
    Note TEXT,
    Status INT NOT NULL,
    FOREIGN KEY (AccountId) REFERENCES Account(AccountId),
    FOREIGN KEY (PartnerId) REFERENCES Partner(PartnerId)
);

-- Tạo bảng Product
CREATE TABLE Product (
    ProductId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    CategoryId INT NOT NULL,
    AreaId INT NOT NULL,
    ProductCode VARCHAR(20) NOT NULL,
    Name NVARCHAR(100),
    Quantity INT NOT NULL,
    Status INT NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId),
    FOREIGN KEY (AreaId) REFERENCES StorageArea(AreaId)
);

-- Tạo bảng StockOutDetail
CREATE TABLE StockOutDetail (
    StockOutDetailId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    ProductId INT NOT NULL,
    StockOutId INT NOT NULL,
    Quantity INT NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId),
    FOREIGN KEY (StockOutId) REFERENCES StockOut(StockOutId)
);

-- Tạo bảng LotDetail
CREATE TABLE LotDetail (
    LotDetailId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    LotId INT NOT NULL,
    ProductId INT NOT NULL,
    PartnerId INT NOT NULL,
    Quantity INT NOT NULL,
    Status INT NOT NULL,
    FOREIGN KEY (LotId) REFERENCES Lot(LotId),
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId),
    FOREIGN KEY (PartnerId) REFERENCES Partner(PartnerId)
);
GO

SET IDENTITY_INSERT [dbo].[Category] ON
INSERT [dbo].[Category] ([CategoryId], [CategoryCode], [Name], [Status]) VALUES (1, N'CAT001', N'Đồ điện tử', 1)
INSERT [dbo].[Category] ([CategoryId], [CategoryCode], [Name], [Status]) VALUES (2, N'CAT002', N'Điện thoại di động', 1)
INSERT [dbo].[Category] ([CategoryId], [CategoryCode], [Name], [Status]) VALUES (3, N'CAT003', N'Điện gia dụng', 1)
INSERT [dbo].[Category] ([CategoryId], [CategoryCode], [Name], [Status]) VALUES (4, N'CAT004', N'Đồ điện tử gia dụng', 1)
INSERT [dbo].[Category] ([CategoryId], [CategoryCode], [Name], [Status]) VALUES (5, N'CAT005', N'Nội thất gỗ cao cấp', 1)
INSERT [dbo].[Category] ([CategoryId], [CategoryCode], [Name], [Status]) VALUES (6, N'CAT006', N'Đồ nội thất', 1)
INSERT [dbo].[Category] ([CategoryId], [CategoryCode], [Name], [Status]) VALUES (7, N'CAT007', N'Phụ kiện ô tô', 1)
INSERT [dbo].[Category] ([CategoryId], [CategoryCode], [Name], [Status]) VALUES (8, N'CAT008', N'Phụ kiện xe máy ', 1)
INSERT [dbo].[Category] ([CategoryId], [CategoryCode], [Name], [Status]) VALUES (9, N'CAT009', N'Phụ kiện xe đạp', 1)
INSERT [dbo].[Category] ([CategoryId], [CategoryCode], [Name], [Status]) VALUES (10, N'CAT010', N'Đồ chơi trẻ em', 1)
INSERT [dbo].[Category] ([CategoryId], [CategoryCode], [Name], [Status]) VALUES (11, N'CAT011', N'Đồ nhựa', 1)
INSERT [dbo].[Category] ([CategoryId], [CategoryCode], [Name], [Status]) VALUES (12, N'CAT012', N'Đồ Kim loại (Thép, inox, ...)', 1)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[StorageArea] ON 

INSERT [dbo].[StorageArea] ([AreaId], [AreaCode], [AreaName], [Status]) VALUES (1, N'AREA001', N'Kho A', 1)
INSERT [dbo].[StorageArea] ([AreaId], [AreaCode], [AreaName], [Status]) VALUES (2, N'AREA002', N'Kho B', 1)
INSERT [dbo].[StorageArea] ([AreaId], [AreaCode], [AreaName], [Status]) VALUES (3, N'AREA003', N'Kho C', 1)
INSERT [dbo].[StorageArea] ([AreaId], [AreaCode], [AreaName], [Status]) VALUES (4, N'AREA004', N'Kho D', 1)
SET IDENTITY_INSERT [dbo].[StorageArea] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (1, 1, 1, N'PROD001', N'Tivi LED Samsung 50 inch', 20, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (2, 2, 2, N'PROD002', N'Điện thoại iPhone 12', 15, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (3, 3, 3, N'PROD003', N'Máy giặt Electrolux', 27, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (4, 1, 1, N'PROD004', N'POCO X3 PRO', 12, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (5, 1, 1, N'PROD005', N'Samsung S21 ultra', 35, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (6, 2, 2, N'PROD006', N'Samsung S22 ultra', 12, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (7, 1, 1, N'PROD007', N'Iphone 15', 55, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (8, 3, 2, N'PROD008', N'Kệ giày inox', 21, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (9, 5, 1, N'PROD009', N'Giường king', 36, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (10, 1, 1, N'PROD010', N'Giường Qeen ', 26, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (11, 4, 2, N'PROD011', N'Sony Qled 55inch', 27, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (12, 2, 1, N'PROD012', N'XIAOMI k30 Pro', 25, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (13, 1, 1, N'PROD013', N'Lenovo IdeaPad 330', 23, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (14, 2, 1, N'PROD014', N'Ipad Air 2020', 58, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (15, 2, 1, N'PROD015', N'Ipad Pro 2023', 47, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (16, 2, 1, N'PROD016', N'Samsung A53', 24, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (17, 4, 3, N'PROD017', N'Samsung OLED 90inch', 57, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (18, 3, 4, N'PROD018', N'Quạt Sharp thông minh', 33, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (19, 3, 4, N'PROD019', N'Đèn học (vàng)', 82, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (20, 3, 3, N'PROD020', N'Đèn học (trắng)', 45, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (21, 11, 1, N'PROD021', N'Rổ bếp', 30, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (22, 8, 1, N'PROD022', N'Cụm thắng', 74, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (23, 8, 2, N'PROD023', N'Cụm côn tay', 114, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (24, 7, 3, N'PROD024', N'Bọc vô lăng', 78, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (25, 9, 4, N'PROD025', N'Đề Shimano ', 58, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (26, 10, 4, N'PROD026', N'Doraemon Lego', 127, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (27, 10, 2, N'PROD027', N'Xe đẩy cho bé', 46, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (28, 6, 1, N'PROD028', N'Ghế công thái học', 56, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (29, 6, 3, N'PROD029', N'Bàn công thái học', 47, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (30, 6, 1, N'PROD030', N'Tủ quần áo 2mx2m', 60, 1)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [AreaId], [ProductCode], [Name], [Quantity], [Status]) VALUES (31, 5, 4, N'PROD031', N'Tủ gỗ', 80, 1)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([AccountId], [AccountCode], [Email], [Name], [Password], [Role], [Phone], [Status]) VALUES (1, N'ACC001', N'storekeeper1@gmail.com', N'Hoàng Văn Bình', N'Matkhau123!', 1, N'0123456789', 1)
INSERT [dbo].[Account] ([AccountId], [AccountCode], [Email], [Name], [Password], [Role], [Phone], [Status]) VALUES (2, N'ACC002', N'manager1@gmail.com', N'Trần Huyền Diệp', N'Matkhau123!', 2, N'0123456789', 1)
INSERT [dbo].[Account] ([AccountId], [AccountCode], [Email], [Name], [Password], [Role], [Phone], [Status]) VALUES (3, N'ACC003', N'storekeeper2@gmail.com', N'Đinh Tiến Dũng', N'Matkhau123!', 1, N'0123456789', 1)

INSERT [dbo].[Account] ([AccountId], [AccountCode], [Email], [Name], [Password], [Role], [Phone], [Status]) VALUES (6, N'ACC004', N'admin@gmail.com', N'Nguyễn Duy Đức Chính', N'Matkhau123!', 0, N'0123456789', 1)

INSERT [dbo].[Account] ([AccountId], [AccountCode], [Email], [Name], [Password], [Role], [Phone], [Status]) VALUES (7, N'ACC005', N'storekeeper3@gmail.com', N'Lê Trọng Tấn', N'Matkhau123!', 1, N'0123456789', 1)
INSERT [dbo].[Account] ([AccountId], [AccountCode], [Email], [Name], [Password], [Role], [Phone], [Status]) VALUES (8, N'ACC006', N'storekeeper4@gmail.com', N'Bùi Anh Tuấn', N'Matkhau123!', 1, N'0123456789', 0)
INSERT [dbo].[Account] ([AccountId], [AccountCode], [Email], [Name], [Password], [Role], [Phone], [Status]) VALUES (9, N'ACC007', N'manager2@gmail.com', N'Lê Thị Thu', N'Matkhau123!', 2, N'0123456789', 0)
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Partner] ON 

INSERT [dbo].[Partner] ([PartnerId], [PartnerCode], [Name], [Status]) VALUES (1, N'PART001', N'Công ty TNHH Thương mại và Xuất nhập khẩu Đại Đồng', 1)
INSERT [dbo].[Partner] ([PartnerId], [PartnerCode], [Name], [Status]) VALUES (2, N'PART002', N'Công ty Cổ phần Thương mại và Xuất nhập khẩu Bách Hợp', 1)
INSERT [dbo].[Partner] ([PartnerId], [PartnerCode], [Name], [Status]) VALUES (3, N'PART003', N'Công ty TNHH Thương mại và Xuất nhập khẩu Điện tử - Gia dụng Minh Quang', 1)
INSERT [dbo].[Partner] ([PartnerId], [PartnerCode], [Name], [Status]) VALUES (4, N'PART004', N'CÔNG TY CỔ PHẦN DI CHUYỂN XANH VÀ THÔNG MINH GSM', 1)
INSERT [dbo].[Partner] ([PartnerId], [PartnerCode], [Name], [Status]) VALUES (5, N'PART005', N'Công ty cổ phần đầu tư hợp tác & phát triển kinh tế Việt Lào', 1)
INSERT [dbo].[Partner] ([PartnerId], [PartnerCode], [Name], [Status]) VALUES (6, N'PART006', N'Công ty cổ phần Vinhomes', 1)
INSERT [dbo].[Partner] ([PartnerId], [PartnerCode], [Name], [Status]) VALUES (7, N'PART007', N'Công ty Cổ phần Sữa Việt Nam', 1)
INSERT [dbo].[Partner] ([PartnerId], [PartnerCode], [Name], [Status]) VALUES (8, N'PART008', N'Công ty Cổ phần Tập đoàn Trường Hải', 1)
INSERT [dbo].[Partner] ([PartnerId], [PartnerCode], [Name], [Status]) VALUES (9, N'PART009', N'Công ty TNHH MTV Housing', 1)
INSERT [dbo].[Partner] ([PartnerId], [PartnerCode], [Name], [Status]) VALUES (10, N'PART010', N'Công ty TNHH Landing Home', 1)
INSERT [dbo].[Partner] ([PartnerId], [PartnerCode], [Name], [Status]) VALUES (11, N'PART011', N'CÔNG TY TNHH KINH DOANH THƯƠNG MẠI VÀ DỊCH VỤ VINFAST', 1)
SET IDENTITY_INSERT [dbo].[Partner] OFF
GO
SET IDENTITY_INSERT [dbo].[Lot] ON 

INSERT [dbo].[Lot] ([LotId], [AccountId], [PartnerId], [LotCode], [DateIn], [Note], [Status]) VALUES (1, 1, 1, N'LOT001', CAST(N'2023-01-01' AS Date), NULL, 1)
INSERT [dbo].[Lot] ([LotId], [AccountId], [PartnerId], [LotCode], [DateIn], [Note], [Status]) VALUES (2, 2, 2, N'LOT002', CAST(N'2023-02-01' AS Date), NULL, 1)
INSERT [dbo].[Lot] ([LotId], [AccountId], [PartnerId], [LotCode], [DateIn], [Note], [Status]) VALUES (3, 3, 3, N'LOT003', CAST(N'2023-03-01' AS Date), NULL, 1)
INSERT [dbo].[Lot] ([LotId], [AccountId], [PartnerId], [LotCode], [DateIn], [Note], [Status]) VALUES (4, 2, 1, N'LOT004', CAST(N'2023-12-14' AS Date), N'Input', 1)
INSERT [dbo].[Lot] ([LotId], [AccountId], [PartnerId], [LotCode], [DateIn], [Note], [Status]) VALUES (5, 2, 2, N'LOT005', CAST(N'2023-12-14' AS Date), N'Input', 1)
INSERT [dbo].[Lot] ([LotId], [AccountId], [PartnerId], [LotCode], [DateIn], [Note], [Status]) VALUES (6, 2, 3, N'LOT006', CAST(N'2023-12-14' AS Date), N'Input', 1)
INSERT [dbo].[Lot] ([LotId], [AccountId], [PartnerId], [LotCode], [DateIn], [Note], [Status]) VALUES (7, 2, 1, N'LOT007', CAST(N'2023-12-14' AS Date), N'Input', 1)
INSERT [dbo].[Lot] ([LotId], [AccountId], [PartnerId], [LotCode], [DateIn], [Note], [Status]) VALUES (8, 2, 1, N'LOT008', CAST(N'2023-12-14' AS Date), N'Input', 1)
INSERT [dbo].[Lot] ([LotId], [AccountId], [PartnerId], [LotCode], [DateIn], [Note], [Status]) VALUES (9, 2, 4, N'LOT009', CAST(N'2023-12-14' AS Date), N'Input', 1)
SET IDENTITY_INSERT [dbo].[Lot] OFF
GO
SET IDENTITY_INSERT [dbo].[StockOut] ON 

INSERT [dbo].[StockOut] ([StockOutId], [AccountId], [PartnerId], [DateOut], [Note], [Status]) VALUES (1, 1, 3, CAST(N'2023-12-13' AS Date), N'TV Samsung & iPhone 12', 1)
INSERT [dbo].[StockOut] ([StockOutId], [AccountId], [PartnerId], [DateOut], [Note], [Status]) VALUES (2, 2, 1, CAST(N'2023-12-13' AS Date), NULL, 1)
INSERT [dbo].[StockOut] ([StockOutId], [AccountId], [PartnerId], [DateOut], [Note], [Status]) VALUES (3, 3, 3, CAST(N'2023-03-15' AS Date), NULL, 1)
INSERT [dbo].[StockOut] ([StockOutId], [AccountId], [PartnerId], [DateOut], [Note], [Status]) VALUES (4, 2, 5, CAST(N'2023-12-14' AS Date), NULL, 1)
INSERT [dbo].[StockOut] ([StockOutId], [AccountId], [PartnerId], [DateOut], [Note], [Status]) VALUES (5, 2, 11, CAST(N'2023-12-14' AS Date), N'Vinfast', 1)
INSERT [dbo].[StockOut] ([StockOutId], [AccountId], [PartnerId], [DateOut], [Note], [Status]) VALUES (6, 2, 8, CAST(N'2023-12-14' AS Date), N'THACO - Trường Hải', 1)
SET IDENTITY_INSERT [dbo].[StockOut] OFF
GO
SET IDENTITY_INSERT [dbo].[LotDetail] ON 

INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (1, 1, 1, 1, 15, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (2, 2, 2, 2, 10, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (3, 3, 3, 3, 20, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (4, 4, 7, 1, 35, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (5, 4, 8, 1, 25, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (6, 4, 9, 1, 36, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (7, 4, 11, 1, 27, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (8, 4, 12, 1, 29, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (9, 5, 14, 2, 60, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (10, 5, 15, 2, 50, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (11, 5, 16, 2, 30, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (12, 5, 17, 2, 57, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (13, 5, 18, 2, 38, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (14, 6, 19, 3, 60, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (15, 6, 20, 3, 50, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (16, 6, 21, 3, 30, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (17, 6, 22, 3, 57, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (18, 6, 23, 3, 38, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (19, 7, 24, 1, 20, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (20, 7, 25, 1, 80, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (21, 7, 26, 1, 60, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (22, 7, 27, 1, 47, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (23, 7, 28, 1, 28, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (24, 8, 26, 1, 20, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (25, 8, 31, 1, 80, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (26, 8, 30, 1, 60, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (27, 8, 29, 1, 47, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (28, 8, 28, 1, 28, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (29, 9, 22, 4, 20, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (30, 9, 23, 4, 80, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (31, 9, 24, 4, 60, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (32, 9, 26, 4, 47, 1)
INSERT [dbo].[LotDetail] ([LotDetailId], [LotId], [ProductId], [PartnerId], [Quantity], [Status]) VALUES (33, 9, 19, 4, 28, 1)
SET IDENTITY_INSERT [dbo].[LotDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[StockOutDetail] ON 

INSERT [dbo].[StockOutDetail] ([StockOutDetailId], [ProductId], [StockOutId], [Quantity]) VALUES (1, 1, 1, 20)
INSERT [dbo].[StockOutDetail] ([StockOutDetailId], [ProductId], [StockOutId], [Quantity]) VALUES (2, 2, 1, 30)
INSERT [dbo].[StockOutDetail] ([StockOutDetailId], [ProductId], [StockOutId], [Quantity]) VALUES (3, 3, 2, 8)
INSERT [dbo].[StockOutDetail] ([StockOutDetailId], [ProductId], [StockOutId], [Quantity]) VALUES (4, 25, 4, 22)
INSERT [dbo].[StockOutDetail] ([StockOutDetailId], [ProductId], [StockOutId], [Quantity]) VALUES (5, 22, 4, 3)
INSERT [dbo].[StockOutDetail] ([StockOutDetailId], [ProductId], [StockOutId], [Quantity]) VALUES (6, 24, 4, 2)
INSERT [dbo].[StockOutDetail] ([StockOutDetailId], [ProductId], [StockOutId], [Quantity]) VALUES (7, 27, 4, 1)
INSERT [dbo].[StockOutDetail] ([StockOutDetailId], [ProductId], [StockOutId], [Quantity]) VALUES (8, 23, 4, 4)
INSERT [dbo].[StockOutDetail] ([StockOutDetailId], [ProductId], [StockOutId], [Quantity]) VALUES (9, 14, 5, 2)
INSERT [dbo].[StockOutDetail] ([StockOutDetailId], [ProductId], [StockOutId], [Quantity]) VALUES (10, 15, 5, 3)
INSERT [dbo].[StockOutDetail] ([StockOutDetailId], [ProductId], [StockOutId], [Quantity]) VALUES (11, 12, 5, 4)
INSERT [dbo].[StockOutDetail] ([StockOutDetailId], [ProductId], [StockOutId], [Quantity]) VALUES (12, 20, 5, 5)
INSERT [dbo].[StockOutDetail] ([StockOutDetailId], [ProductId], [StockOutId], [Quantity]) VALUES (13, 19, 5, 6)
INSERT [dbo].[StockOutDetail] ([StockOutDetailId], [ProductId], [StockOutId], [Quantity]) VALUES (14, 3, 6, 3)
INSERT [dbo].[StockOutDetail] ([StockOutDetailId], [ProductId], [StockOutId], [Quantity]) VALUES (15, 8, 6, 4)
INSERT [dbo].[StockOutDetail] ([StockOutDetailId], [ProductId], [StockOutId], [Quantity]) VALUES (16, 18, 6, 5)
INSERT [dbo].[StockOutDetail] ([StockOutDetailId], [ProductId], [StockOutId], [Quantity]) VALUES (17, 16, 6, 6)
INSERT [dbo].[StockOutDetail] ([StockOutDetailId], [ProductId], [StockOutId], [Quantity]) VALUES (18, 10, 6, 7)
SET IDENTITY_INSERT [dbo].[StockOutDetail] OFF
GO