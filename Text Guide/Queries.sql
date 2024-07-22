--------------------------------------------------------------------
-- AuthApi
--------------------------------------------------------------------
USE [NikeStore.AuthApiDb]
SELECT * FROM AspNetUsers
SELECT * FROM AspNetRoles
SELECT * FROM AspNetUserRoles
SELECT * FROM AspNetUserLogins
SELECT * FROM AspNetUserClaims
SELECT * FROM AspNetRoleClaims
SELECT * FROM AspNetUserTokens
GO


-- Create new role and assign it to user
DECLARE @AdminRoleId NVARCHAR(300) = 'c4198503-c3f5-47c4-a8ab-ab431bb72524', @AdminUserId NVARCHAR(300) = '734b1693-757e-44d7-9841-14a5fa77531d';
INSERT INTO AspNetRoles(Id, Name, NormalizedName,ConcurrencyStamp) VALUES(@AdminRoleId, 'ADMIN', 'ADMIN', NULL)
UPDATE AspNetUserRoles SET RoleId = @AdminRoleId WHERE UserId = @AdminUserId 
GO


DELETE FROM AspNetUsers
DELETE FROM AspNetUserRoles
DELETE FROM AspNetUserLogins
DELETE FROM AspNetRoles
DELETE FROM AspNetUserClaims
DELETE FROM AspNetRoleClaims
DELETE FROM AspNetUserTokens



--------------------------------------------------------------------
-- CouponApi
--------------------------------------------------------------------
USE [NikeStore.CouponApiDb]
SELECT * FROM Coupons
--DELETE FROM Coupons




--------------------------------------------------------------------
-- ProductApi
--------------------------------------------------------------------
USE [NikeStore.ProductApiDb]
SELECT * FROM Products
--DELETE FROM Products


--------------------------------------------------------------------
-- Shopping Cart
--------------------------------------------------------------------
USE [NikeStore.ShoppingCartApiDb]
SELECT * FROM CartHeaders
SELECT * FROM CartDetails



DELETE FROM CartHeaders
DELETE FROM CartDetails




--------------------------------------------------------------------
-- Order Cart
--------------------------------------------------------------------
USE [NikeStore.OrderApiDb]
SELECT * FROM OrderHeaders
SELECT * FROM OrderDetails



DELETE FROM OrderHeaders
DELETE FROM OrderDetails






--------------------------------------------------------------------
-- Email Api
--------------------------------------------------------------------
USE [NikeStore.EmailApiDb]
SELECT * FROM DbMailLogs


DELETE FROM DbMailLogs










set identity_insert [#tempProducts] on;


insert [#tempProducts] ([ProductId],[Name],[Price],[Description],[CategoryName],[ImageUrl],[ImageLocalPath])
select 4013,N'Dell MS116 Wired Optical Mouse',499,N'Dell MS116 Wired Optical Mouse, 1000DPI, LED Tracking, Scrolling Wheel, Plug and Play',N'electronics',N'http://nikestoreproductapi.runasp.net/ProductImages/4013.jpg',N'wwwroot/ProductImages/4013.jpg' UNION ALL
select 4014,N'Lymio Men T-Shirt',367,N'Lymio Men T-Shirt || T-Shirt for Men || Plain T Shirt || T-Shirt (Polo-18-21)
',N'clothing',N'http://nikestoreproductapi.runasp.net/ProductImages/4014.jpg',N'wwwroot/ProductImages/4014.jpg' UNION ALL
select 4015,N'Lymio Men Cargo',586,N'Lymio Men Cargo || Men Cargo Pants || Men Cargo Pants Cotton || Cargos for Men (Cargo-01-04)
',N'clothing',N'http://nikestoreproductapi.runasp.net/ProductImages/4015.jpg',N'wwwroot/ProductImages/4015.jpg' UNION ALL
select 4016,N'American Tourister Fizz 32L',2569,N'American Tourister Fizz 32L Black Backpack School bag for travel with Organizer Bottle compartment water resistant backpack for Men, Women, Boys Laptop Backpack for College Gift for Men & Women
',N'sports',N'http://nikestoreproductapi.runasp.net/ProductImages/4016.jpg',N'wwwroot/ProductImages/4016.jpg' UNION ALL
select 4017,N'Safari Pentagon 65 Cms',1790,N'Safari Pentagon 65 Cms Medium Check-in Polypropylene (Pp) Hardshell Sided 4 Wheels 360 Degree Rotation Luggage/Suitcase/Inline Trolley Bag (Cyan Blue)
',N'sports',N'http://nikestoreproductapi.runasp.net/ProductImages/4017.jpg',N'wwwroot/ProductImages/4017.jpg' UNION ALL
select 4018,N'Redmi 12 5G Pastel Blue',11999,N'Redmi 12 5G Pastel Blue 4GB RAM 128GB ROM
',N'electronics',N'http://nikestoreproductapi.runasp.net/ProductImages/4018.jpg',N'wwwroot/ProductImages/4018.jpg' UNION ALL
select 4019,N'HP 64GB Pen Drive',359,N'HP v236w USB 2.0 64GB Pen Drive',N'electronics',N'http://nikestoreproductapi.runasp.net/ProductImages/4019.jpg',N'wwwroot/ProductImages/4019.jpg';

set identity_insert [#tempProducts] off;



set identity_insert [#tempCoupons] on;


insert [#tempCoupons] ([CouponId],[CouponCode],[DiscountAmount],[MinAmount],[CreatedDateTime],[LastUpdatedDateTime])
select 1,N'GUEST_100_OFF',100,1500,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000' UNION ALL
select 2,N'GUEST_200_OFF',200,2500,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000' UNION ALL
select 4016,N'20FreeFall',10,1000,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000' UNION ALL
select 4019,N'LOYALTY_120',120,1000,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000' UNION ALL
select 4021,N'FRIEND_2024',100,500,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000' UNION ALL
select 4022,N'SHARE_10',10,500,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000' UNION ALL
select 4023,N'SUMMER_1000',1000,5000,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000';

set identity_insert [#tempCoupons] off;