USE [MASTER]
GO

ALTER DATABASE PRN221_WMS SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

DROP DATABASE IF EXISTS PRN221_WMS
GO

CREATE DATABASE PRN221_WMS
GO

USE PRN221_WMS
GO

-- Create Accounts table
CREATE TABLE Account (
    AccountId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    AccountCode NVARCHAR(20) NOT NULL,
    Username NVARCHAR(100) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
	Name NVARCHAR(50) NOT NULL,
    Email NVARCHAR(50) DEFAULT N'account@example.com',
    Phone NVARCHAR(15) DEFAULT N'+84 354510912',
    Role INT NOT NULL DEFAULT 1,
    Status INT NOT NULL DEFAULT 1
);
GO

-- Create Categories table
CREATE TABLE Category (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    CategoryCode NVARCHAR(20) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) DEFAULT N'Mô tả chi tiết hoặc khái quát của danh mục hàng hóa',
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    Status INT DEFAULT 1
);

-- Create StorageAreas table
CREATE TABLE Storage (
    StorageId INT PRIMARY KEY IDENTITY(1,1),
    StorageCode NVARCHAR(20) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Province NVARCHAR(50) DEFAULT N'Hà Nội',
    Address NVARCHAR(255) DEFAULT N'A5 An Bình City - Phạm Văn Đồng - Quận Bắc Từ Liêm',
	MaxCapacity INT DEFAULT 1000,
	CurrentCapacity INT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    Status INT DEFAULT 1
);

-- Create Products table with Quantity and Unit
CREATE TABLE Product (
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    ProductCode NVARCHAR(20) NOT NULL,
    CategoryId INT NOT NULL FOREIGN KEY REFERENCES Category(CategoryId),
    StorageId INT NOT NULL FOREIGN KEY REFERENCES Storage(StorageId),
    Name NVARCHAR(100) NOT NULL,
    Quantity INT DEFAULT 0,
    Unit NVARCHAR(50) DEFAULT N'Đơn vị',
    Status INT DEFAULT 1
);

-- Create Partners table
CREATE TABLE [Partner] (
    PartnerId INT PRIMARY KEY IDENTITY(1,1),
    PartnerCode VARCHAR(50) NOT NULL,
    PartnerType INT,
    Name NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(15) DEFAULT N'+84 354510912',
    Email NVARCHAR(50) DEFAULT N'partner@example.com',
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    Status INT DEFAULT 1
);

-- Create Lots table
CREATE TABLE Lot (
    LotId INT PRIMARY KEY IDENTITY(1,1),
    LotCode NVARCHAR(20) NOT NULL,
    PartnerId INT NOT NULL DEFAULT 1 FOREIGN KEY REFERENCES [Partner](PartnerId) ,
    AccountId INT NOT NULL FOREIGN KEY REFERENCES Account(AccountId),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    Note NVARCHAR(255) DEFAULT '',
    Status INT DEFAULT 1
);

-- Create LotDetails table
CREATE TABLE LotDetail (
    LotDetailId INT PRIMARY KEY IDENTITY(1,1),
    LotId INT NOT NULL FOREIGN KEY REFERENCES Lot(LotId),
	PartnerId INT NOT NULL FOREIGN KEY REFERENCES [Partner](PartnerId),
    ProductId INT NOT NULL FOREIGN KEY REFERENCES Product(ProductId),
    Quantity INT DEFAULT 0,
    PackingType NVARCHAR(50) DEFAULT N'Đơn vị',
    Status INT DEFAULT 1
);

-- Create Stockouts table
CREATE TABLE StockOut (
    StockOutId INT PRIMARY KEY IDENTITY(1,1),
    StockOutCode NVARCHAR(20),
    AccountId INT NOT NULL FOREIGN KEY REFERENCES Account(AccountId),
    PartnerId INT NOT NULL FOREIGN KEY REFERENCES [Partner](PartnerId),
    Note NVARCHAR(255) DEFAULT '',
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    Status INT DEFAULT 1
);

-- Create StockoutDetails table
CREATE TABLE StockOutDetail (
    StockOutDetailId INT PRIMARY KEY IDENTITY(1,1),
    StockOutId INT NOT NULL FOREIGN KEY REFERENCES StockOut(StockOutId),
    ProductId INT NOT NULL FOREIGN KEY REFERENCES Product(ProductId),
    Quantity INT DEFAULT 0,
    PackingType NVARCHAR(50) DEFAULT N'Đơn vị',
    Status INT DEFAULT 1
);

-- Create History table
CREATE TABLE History (
    HistoryId INT PRIMARY KEY IDENTITY(1,1),
    HistoryType VARCHAR(50) DEFAULT N'Không xác định',
    StockoutId INT NULL FOREIGN KEY REFERENCES StockOut(StockOutId),
    LotId INT NULL FOREIGN KEY REFERENCES Lot(LotId),
    AccountId INT NOT NULL FOREIGN KEY REFERENCES Account(AccountId),
    Action NVARCHAR(50) DEFAULT N'Đã thay đổi/ cập nhật/ thêm/ sửa/ xóa/ ...',
    Timestamp DATETIME DEFAULT GETDATE()
);

-- Create Notifications table
CREATE TABLE Notification (
    NotificationId INT PRIMARY KEY IDENTITY(1,1),
    Message VARCHAR(255) DEFAULT N'Nguyễn Văn A Đã Yêu Cầu Nhập Hàng',
	Slug NVARCHAR(MAX) NOT NULL DEFAULT N'/manage/product',
    Status INT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE()
);

GO


CREATE TRIGGER trg_UpdateStorageCurrentCapacity
ON Product
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    -- Declare a table variable to store StorageId
    DECLARE @StorageIds TABLE (StorageId INT);

    -- Insert StorageIds from inserted rows (for INSERT or UPDATE operations)
    INSERT INTO @StorageIds (StorageId)
    SELECT DISTINCT StorageId FROM inserted;

    -- Insert StorageIds from deleted rows (for DELETE operations)
    INSERT INTO @StorageIds (StorageId)
    SELECT DISTINCT StorageId FROM deleted;

    -- Update CurrentCapacity in Storage for each StorageId
    UPDATE Storage
    SET CurrentCapacity = (
        SELECT ISNULL(SUM(Quantity), 0)
        FROM Product
        WHERE StorageId = s.StorageId
    )
    FROM Storage s
    WHERE s.StorageId IN (SELECT StorageId FROM @StorageIds);
END;

GO

INSERT INTO Account (AccountCode, Username, Password, Name, Email, Phone, Role, Status)
VALUES
( 'ACC001', 'admin', '123', N'Nguyễn Văn A', 'admin@example.com',  '+84 354510912', 0, 1),
( 'ACC002', 'manager1', '123', N'Nguyễn Văn B1', 'manager1@example.com','+84 354510913', 1, 1),
( 'ACC003', 'manager2', '123', N'Nguyễn Văn B2', 'manager2@example.com','+84 354510913', 1, 1),
( 'ACC004', 'manager3', '123', N'Nguyễn Văn B3', 'manager3@example.com','+84 354510913', 1, 1),
( 'ACC005', 'storekeeper1', '123', N'Nguyễn Văn C1', 'storekeeper1@example.com', '+84 354510914', 2, 1),
( 'ACC006', 'storekeeper2', '123', N'Nguyễn Văn C2', 'storekeeper2@example.com', '+84 354510914', 2, 1),
( 'ACC007', 'storekeeper3', '123', N'Nguyễn Văn C3', 'storekeeper3@example.com', '+84 354510914', 2, 1);

-- Insert data into Category table
INSERT INTO Category (CategoryCode, Name, Description, CreatedAt, UpdatedAt, Status)
VALUES
    (N'CAT001', N'Điện Tử', N'Mô tả chi tiết hoặc khái quát của danh mục Điện Tử', GETDATE(), GETDATE(), 1),
    (N'CAT002', N'Gia Dụng', N'Mô tả chi tiết hoặc khái quát của danh mục Gia Dụng', GETDATE(), GETDATE(), 1),
    (N'CAT003', N'Thời Trang', N'Mô tả chi tiết hoặc khái quát của danh mục Thời Trang', GETDATE(), GETDATE(), 1),
    (N'CAT004', N'Sách', N'Mô tả chi tiết hoặc khái quát của danh mục Sách', GETDATE(), GETDATE(), 1),
    (N'CAT005', N'Sức Khỏe', N'Mô tả chi tiết hoặc khái quát của danh mục Sức Khỏe', GETDATE(), GETDATE(), 1);

GO

-- Insert data into Storage table
INSERT INTO Storage (StorageCode, Name, Province, Address, MaxCapacity, CurrentCapacity, CreatedAt, UpdatedAt, Status)
VALUES
    (N'AREA001', N'Kho Hải Phòng', N'Hải Phòng', N'123 Đường Lê Hồng Phong - Hải An - Hải Phòng', 1000, 0, GETDATE(), GETDATE(), 1),
    (N'AREA002', N'Kho Hà Nội', N'Hà Nội', N'A5 An Bình City - Phạm Văn Đồng - Quận Bắc Từ Liêm', 1000, 0, GETDATE(), GETDATE(), 1),
    (N'AREA003', N'Kho Đà Nẵng', N'Đà Nẵng', N'456 Đường Nguyễn Văn Linh - Thanh Khê - Đà Nẵng', 1000, 0, GETDATE(), GETDATE(), 1),
    (N'AREA004', N'Kho Vũng Tàu', N'Vũng Tàu', N'789 Đường 30/4 - TP. Vũng Tàu', 1000, 0, GETDATE(), GETDATE(), 1),
    (N'AREA005', N'Kho Hồ Chí Minh', N'Hồ Chí Minh', N'101 Đường Nguyễn Thị Minh Khai - Quận 1 - Hồ Chí Minh', 1000, 0, GETDATE(), GETDATE(), 1);

GO

-- Insert data into Product table
INSERT INTO Product (ProductCode, CategoryId, StorageId, Name, Quantity, Unit, Status)
VALUES
	(N'P001-1', 1, 1, N'Tivi Samsung', 50, N'Cái', 1),
    (N'P002-1', 1, 2, N'Điện Thoại iPhone', 30, N'Cái', 1),
    (N'P003-1', 1, 3, N'Máy Tính Laptop Dell', 20, N'Cái', 1),
    (N'P004-1', 1, 4, N'Loa Bluetooth JBL', 40, N'Cái', 1),
    (N'P005-1', 1, 5, N'Đầu Đĩa Blu-ray Sony', 15, N'Cái', 1),
	(N'P001-2', 2, 1, N'Tủ Lạnh Samsung', 25, N'Cái', 1),
    (N'P002-2', 2, 2, N'Máy Giặt LG', 20, N'Cái', 1),
    (N'P003-2', 2, 3, N'Bàn Ủi Philips', 35, N'Cái', 1),
    (N'P004-2', 2, 4, N'Quạt Đứng Panasonic', 50, N'Cái', 1),
    (N'P005-2', 2, 5, N'Lò Vi Sóng Sharp', 18, N'Cái', 1),
	(N'P001-3', 3, 1, N'Áo Sơ Mi Nam', 100, N'Cái', 1),
    (N'P002-3', 3, 2, N'Váy Nữ', 80, N'Cái', 1),
    (N'P003-3', 3, 3, N'Giày Sneaker', 60, N'Đôi', 1),
    (N'P004-3', 3, 4, N'Áo Khoác', 45, N'Cái', 1),
    (N'P005-3', 3, 5, N'Quần Jean', 70, N'Cái', 1),
	(N'P001-4', 4, 1, N'Sách Lập Trình C', 25, N'Cuốn', 1),
    (N'P002-4', 4, 2, N'Sách Marketing', 30, N'Cuốn', 1),
    (N'P003-4', 4, 3, N'Sách Quản Lý Dự Án', 20, N'Cuốn', 1),
    (N'P004-4', 4, 4, N'Sách Kinh Tế', 15, N'Cuốn', 1),
    (N'P005-4', 4, 5, N'Sách Học Ngoại Ngữ', 40, N'Cuốn', 1),
	(N'P001-5', 5, 1, N'Vitamin C', 100, N'Hộp', 1),
    (N'P002-5', 5, 2, N'Sữa Tăng Cường Canxi', 75, N'Lon', 1),
    (N'P003-5', 5, 3, N'Thuốc Cảm', 50, N'Hộp', 1),
    (N'P004-5', 5, 4, N'Kem Chống Nắng', 60, N'Tuýp', 1),
    (N'P005-5', 5, 5, N'Thuốc Ho', 40, N'Hộp', 1);
GO

-- Insert data into Partner table
INSERT INTO Partner (PartnerCode, PartnerType, Name, Phone, Email, CreatedAt, UpdatedAt, Status)
VALUES
('P001', 1, N'Vingroup', N'+84 354510912', N'contact@vingroup.net', GETDATE(), GETDATE(), 1),
    ('P002', 1, N'Central Group', N'+84 354510912', N'contact@centralgroup.com.vn', GETDATE(), GETDATE(), 1),
    ('P003', 1, N'Saigon Co.op', N'+84 354510912', N'contact@saigoncoop.com.vn', GETDATE(), GETDATE(), 1),
    ('P004', 1, N'Big C', N'+84 354510912', N'contact@bigc.vn', GETDATE(), GETDATE(), 1),
    ('P005', 1, N'AEON', N'+84 354510912', N'contact@aeon.vn', GETDATE(), GETDATE(), 1),
    ('P006', 1, N'Lottemart', N'+84 354510912', N'contact@lottemart.com.vn', GETDATE(), GETDATE(), 1),
    ('P007', 1, N'VinMart', N'+84 354510912', N'contact@vinmart.com.vn', GETDATE(), GETDATE(), 1),
	('P008', 2, N'Temasek Holdings', N'+84 354510912', N'contact@temasek.com', GETDATE(), GETDATE(), 1),
    ('P009', 2, N'Sumitomo Corporation', N'+84 354510912', N'contact@sumitomo.com', GETDATE(), GETDATE(), 1),
    ('P010', 2, N'Kinh Do Corporation', N'+84 354510912', N'contact@kinhdo.com.vn', GETDATE(), GETDATE(), 1);
GO

-- Insert data into Lot table
INSERT INTO Lot (LotCode, PartnerId, AccountId, CreatedAt, UpdatedAt, Note, Status)
VALUES
	(N'LOT001', 1, 7, GETDATE(), GETDATE(), N'Lô hàng điện tử', 1),
    (N'LOT002', 2, 6, GETDATE(), GETDATE(), N'Lô hàng gia dụng', 1),
    (N'LOT003', 3, 5, GETDATE(), GETDATE(), N'Lô hàng thời trang', 1),
    (N'LOT004', 4, 7, GETDATE(), GETDATE(), N'Lô hàng sách', 1),
    (N'LOT005', 5, 6, GETDATE(), GETDATE(), N'Lô hàng sức khỏe', 1);
GO

-- Insert data into LotDetail table
INSERT INTO LotDetail (LotId, ProductId, Quantity, PackingType, Status)
VALUES
	(1, 1, 100, N'Cái', 1),
    (1, 2, 50, N'Cái', 1),
    (1, 3, 30, N'Cái', 1),
    (1, 4, 60, N'Cái', 1),
    (1, 5, 20, N'Cái', 1),
    
    (2, 6, 25, N'Cái', 1),
    (2, 7, 20, N'Cái', 1),
    (2, 8, 35, N'Cái', 1),
    (2, 9, 50, N'Cái', 1),
    (2, 10, 18, N'Cái', 1),
    
    (3, 11, 100, N'Cái', 1),
    (3, 12, 80, N'Cái', 1),
    (3, 13, 60, N'Đôi', 1),
    (3, 14, 45, N'Cái', 1),
    (3, 15, 70, N'Cái', 1),
    
    (4, 16, 25, N'Cuốn', 1),
    (4, 17, 30, N'Cuốn', 1),
    (4, 18, 20, N'Cuốn', 1),
    (4, 19, 15, N'Cuốn', 1),
    (4, 20, 40, N'Cuốn', 1),
    
    (5, 21, 100, N'Hộp', 1),
    (5, 22, 75, N'Lon', 1),
    (5, 23, 50, N'Hộp', 1),
    (5, 24, 60, N'Tuýp', 1),
    (5, 25, 40, N'Hộp', 1);
GO

-- Insert data into StockOut table
INSERT INTO StockOut (StockOutCode, AccountId, PartnerId, Note, CreatedAt, UpdatedAt, Status)
VALUES
	(N'SO001', 5, 5, N'Xuất kho hàng điện tử', GETDATE(), GETDATE(), 1),
    (N'SO002', 6, 6, N'Xuất kho hàng gia dụng', GETDATE(), GETDATE(), 1),
    (N'SO003', 7, 7, N'Xuất kho hàng thời trang', GETDATE(), GETDATE(), 1),
    (N'SO004', 5, 8, N'Xuất kho hàng sách', GETDATE(), GETDATE(), 1),
    (N'SO005', 6, 9, N'Xuất kho hàng sức khỏe', GETDATE(), GETDATE(), 1);
GO

GO
-- Insert data into StockOutDetail table
INSERT INTO StockOutDetail (StockOutId, ProductId, Quantity, PackingType, Status)
VALUES
 (1, 1, 10, N'Cái', 1),
    (1, 2, 5, N'Cái', 1),
    (1, 3, 3, N'Cái', 1),
    (1, 4, 6, N'Cái', 1),
    (1, 5, 2, N'Cái', 1),
    
    (2, 6, 5, N'Cái', 1),
    (2, 7, 4, N'Cái', 1),
    (2, 8, 7, N'Cái', 1),
    (2, 9, 10, N'Cái', 1),
    (2, 10, 3, N'Cái', 1),
    
    (3, 11, 10, N'Cái', 1),
    (3, 12, 8, N'Cái', 1),
    (3, 13, 6, N'Đôi', 1),
    (3, 14, 4, N'Cái', 1),
    (3, 15, 7, N'Cái', 1),
    
    (4, 16, 5, N'Cuốn', 1),
    (4, 17, 6, N'Cuốn', 1),
    (4, 18, 4, N'Cuốn', 1),
    (4, 19, 3, N'Cuốn', 1),
    (4, 20, 8, N'Cuốn', 1),
    
    (5, 21, 10, N'Hộp', 1),
    (5, 22, 8, N'Lon', 1),
    (5, 23, 5, N'Hộp', 1),
    (5, 24, 6, N'Tuýp', 1),
    (5, 25, 4, N'Hộp', 1);
GO

-- Insert data into History table
INSERT INTO History (HistoryType, StockoutId, LotId, AccountId, Action, Timestamp)
VALUES
('Stock Out', 1, 1, 1, 'Created stock out for order #001', GETDATE()),
('Stock Out', 2, 2, 2, 'Created stock out for order #002', GETDATE());

GO
-- Insert data into Notification table
INSERT INTO Notification (Message, Slug, Status, CreatedAt)
VALUES
(N'Nguyễn Văn A Đã Yêu Cầu Nhập Hàng', '/manage/stockin', 1, GETDATE()),
(N'Nguyễn Văn B Đã Yêu Cầu Xuất Hàng', '/manage/stockout', 1, GETDATE());

GO