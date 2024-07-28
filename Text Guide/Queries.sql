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




 -- ***************************************
 -- ***************************************
 -- ***************************************
 -- ***************************************
 -- ***************************************

 
 -- *************************************** Create new role and assign it to user

DECLARE @AdminRoleId NVARCHAR(300) = 'c4198503-c3f5-47c4-a8ab-ab431bb72524', @AdminUserId NVARCHAR(300) = '734b1693-757e-44d7-9841-14a5fa77531d';
INSERT INTO AspNetRoles(Id, Name, NormalizedName,ConcurrencyStamp) VALUES(@AdminRoleId, 'ADMIN', 'ADMIN', NULL)
UPDATE AspNetUserRoles SET RoleId = @AdminRoleId WHERE UserId = @AdminUserId 
GO


-- *************************************** Data Script

-- *************************************** Auth Db Data Script
insert [AspNetUsers] ([Id],[Name],[UserName],[NormalizedUserName],[Email],[NormalizedEmail],[EmailConfirmed],[PasswordHash],[SecurityStamp],[ConcurrencyStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEnd],[LockoutEnabled],[AccessFailedCount])
select N'2831bfbc-5645-4a30-805f-521cea0dac4f',N'Basit',N'basit-as-admin@gmail.com',N'BASIT-AS-ADMIN@GMAIL.COM',N'basit-as-admin@gmail.com',N'BASIT-AS-ADMIN@GMAIL.COM',0,N'AQAAAAIAAYagAAAAEBjEg0jpTf5L3lVj6VkCfinZI6VdBGWlLOjv1GMZle1gjVLaN2M2wn29gDpu544pMw==',N'KV72CMMJCNXTWRHKOQDT2NP33YRI2HY4',N'6d50f2a8-54a3-443e-a73e-ef07d474f9e5',N'+917006111111',0,0,NULL,1,0 UNION ALL
select N'fba28484-2289-429e-9119-5bceb49fc329',N'Basit',N'basit-as-customer@gmail.com',N'BASIT-AS-CUSTOMER@GMAIL.COM',N'basit-as-customer@gmail.com',N'BASIT-AS-CUSTOMER@GMAIL.COM',0,N'AQAAAAIAAYagAAAAEDecEFpjniujvZsqDlPU6cdxHpOafeMrZpDRQMnFEAnRdbeOOy48Nf6zidRUnh+2OA==',N'WFTWD3HDFNOI4VTERXSIOQYWZCRELCO4',N'9e89d78b-62af-40a6-b84f-fdae61b858ed',N'+917006111111',0,0,NULL,1,0;

insert [AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
select N'c4198503-c3f5-47c4-a8ab-ab431bb72524',N'ADMIN',N'ADMIN',NULL UNION ALL
select N'ff5dd05c-2a9a-4bc8-9319-5b9b297c3250',N'CUSTOMER',N'CUSTOMER',NULL;

insert [AspNetUserRoles] ([UserId],[RoleId])
select N'2831bfbc-5645-4a30-805f-521cea0dac4f',N'c4198503-c3f5-47c4-a8ab-ab431bb72524' UNION ALL
select N'fba28484-2289-429e-9119-5bceb49fc329',N'ff5dd05c-2a9a-4bc8-9319-5b9b297c3250';



-- *************************************** Coupon DB Data Script - all these are synced with Stripe 
-- "SAVE_100" this coupon should be present

set identity_insert [Coupons] on;

insert [Coupons] ([CouponId],[CouponCode],[DiscountAmount],[MinAmount],[CreatedDateTime],[LastUpdatedDateTime])
select 1,N'SAVE_100',100,101,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000' UNION ALL
select 2,N'200_OFF',200,300,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000' UNION ALL
select 3,N'LOYALTY_120',120,300,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000' UNION ALL
select 4,N'FRIEND_100',100,500,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000' UNION ALL
select 5,N'SHARE_99',99,500,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000' UNION ALL
select 6,N'SUMMER_500',500,2000,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000' UNION ALL
select 7,N'FLASHSALE',250,1000,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000' UNION ALL
select 8,N'CLEARANCE_350',350,2000,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000' UNION ALL
select 9,N'STUDENT_50',50,300,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000' UNION ALL
select 10,N'BIRTHDAYGIFT',99,500,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000' UNION ALL
select 11,N'NEWYEAR',500,2000,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000' UNION ALL
select 12,N'VIPACCESS',1000,6999,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000' UNION ALL
select 13,N'BUYMORE',50,999,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000' UNION ALL
select 14,N'WEEKENDDEAL',444,4444,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000' UNION ALL
select 15,N'PRODUCTLAUNCH_299',299,1999,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000' UNION ALL
select 16,N'FreeFall_600',600,6666,'0001-01-01 00:00:00.0000000','0001-01-01 00:00:00.0000000';

set identity_insert [Coupons] off;



-- *************************************** Product DB Data Script

set identity_insert [Products] on;


insert [Products] ([ProductId],[Name],[Price],[Description],[CategoryName],[ImageUrl],[ImageLocalPath])
select 1,N'Noise Pulse Watch',1100,N'Noise Pulse 2 Max 1.85" Display, Bluetooth Calling Smart Watch, 10 Days Battery, 550 NITS Brightness, Smart DND, 100 Sports Modes, Smartwatch for Men and Women (Rose Pink)',N'electronics',N'http://localhost:7000/ProductImages/1.jpg',N'wwwroot/ProductImages/1.jpg' UNION ALL
select 2,N'SAMSUNG EVO Plus 128GB SD Card',1500,N'SAMSUNG EVO Plus 128GB Micro SDXC w/SD Adaptor, Up-to 160MB/s, Expanded Storage for Gaming Devices, Android Tablets and Smart Phones, Memory Card, MB-MC128SA/I',N'electronics',N'http://localhost:7000/ProductImages/2.jpg',N'wwwroot/ProductImages/2.jpg' UNION ALL
select 3,N'Rellon Industries Study Table for Students',599,N'Rellon Industries Study Table for Students Bed Table for Study Foldable Laptop Table Portable & Lightweight Mini Table Bed Reading Table, Laptop Stands, Laptop Desk (A1)',N'furniture',N'http://localhost:7000/ProductImages/3.jpg',N'wwwroot/ProductImages/3.jpg' UNION ALL
select 4,N'pTron Bassbuds Duo',999,N'pTron Bassbuds Duo in-Ear Wireless Earbuds,Immersive Sound,32Hrs Playtime,Clear Calls Tws Earbuds,Bluetooth V5.1 Headphones,Type-C Fast Charging,Voice Assist&Ipx4 Water Resistant (Light Lilac)',N'electronics',N'http://localhost:7000/ProductImages/4.jpg',N'wwwroot/ProductImages/4.jpg' UNION ALL
select 5,N'HyperX Mechanical Gaming Keyboard',2099,N'HyperX Hx-Kb7Blx-Us Alloy Origins Core USB-C Ten Key Less Mechanical Gaming Keyboard Software Controlled RGB LED Backlit Light and Macro Customization Clicky Switch (Black)',N'electronics',N'http://localhost:7000/ProductImages/5.jpg',N'wwwroot/ProductImages/5.jpg' UNION ALL
select 6,N'EvoFox Gaming Mouse',999,N'EvoFox Blaze Programmable Gaming Mouse with 1000Hz Polling Rate | Ultra-responsive 7000fps | Gaming Grade Sensitive DPI Upto 12800 | RGB lights | Windows Software
',N'electronics',N'http://localhost:7000/ProductImages/6.jpg',N'wwwroot/ProductImages/6.jpg' UNION ALL
select 7,N'USB C to Lightning Cable 1M',699,N'USB C to Lightning Cable 1M [Apple MFi Certified] iPhone Fast Charger Cable USB-C Power Delivery Charging Cord for iPhone 14/13/12/12 PRO Max/12 Mini/11/11PRO/XS/Max/XR/X/8/8Plus/iPad pack of 1
',N'electronics',N'http://localhost:7000/ProductImages/7.jpg',N'wwwroot/ProductImages/7.jpg' UNION ALL
select 8,N'Dopamine Detox',399,N'Dopamine Detox : A Short Guide to Remove Distractions and Get Your Brain to Do Hard Things Paperback – 13 May 2023',N'books',N'http://localhost:7000/ProductImages/8.jpg',N'wwwroot/ProductImages/8.jpg' UNION ALL
select 9,N'Road to Better Habits',999,N'Road to Better Habits Paperback, How to free yourself, change your life and achieve real happiness Hardcover – 30 April 2018',N'books',N'http://localhost:7000/ProductImages/9.jpg',N'wwwroot/ProductImages/9.jpg' UNION ALL
select 10,N'Rich Dad Poor Dad',499,N'Rich Dad Poor Dad : What the Rich Teach Their Kids About Money Mass Market Paperback – Import, 1 January 2022',N'books',N'http://localhost:7000/ProductImages/10.jpg',N'wwwroot/ProductImages/10.jpg' UNION ALL
select 11,N'Voltonix Umbrella Automatic Open',699,N'Voltonix Umbrella Automatic Open Travel Umbrella with Wind Vent, Umbrella big size for men, Umbrella for girls, Umbrellas for rain, Windproof Umbrella Large for Man, Women',N'travel',N'http://localhost:7000/ProductImages/11.jpg',N'wwwroot/ProductImages/11.jpg' UNION ALL
select 12,N'MOKOBARA Polycarbonate ',3999,N'MOKOBARA Polycarbonate The Em Cabin Pro Luggage Small Size German Makrolon Poly-Carbonate Hardside 8 Hinimoto Wheels Suitcase Trolley For Travelling (We Meet Again Sunray) 56 Cm, Black',N'travel',N'http://localhost:7000/ProductImages/12.jpg',N'wwwroot/ProductImages/12.jpg' UNION ALL
select 13,N'Safari Small Size Backpack',899,N'Safari Small Size 15 Ltrs Unisex Standard Backpack - Sea Blue',N'travel',N'http://localhost:7000/ProductImages/13.jpg',N'wwwroot/ProductImages/13.jpg' UNION ALL
select 14,N'Anti Slip Front Door Mat',399,N'Status Contract Anti Slip Front Door Mat|(38x58cm) Living Room Rug for Entrance Door|Polypropylene Floor Mat for Home|Essential Small Rug for Office, Bedroom & Kitchen| (Brown)',N'furniture',N'http://localhost:7000/ProductImages/14.jpg',N'wwwroot/ProductImages/14.jpg' UNION ALL
select 15,N'CAROTE Knife Kitchen - Color Printing',600,N'CAROTE Knife Kitchen Knife Chef Knife Color Printing Santoku Knife & Non-Slip Handle with Blade Cover, Blue, 5 inch, Stainless Steel',N'grocery',N'http://localhost:7000/ProductImages/15.jpg',N'wwwroot/ProductImages/15.jpg' UNION ALL
select 16,N'FLAIR Pastel Hi-lighter Pouch Pack',250,N'FLAIR Pastel 5 Shades Hi-lighter Pouch Pack | Flexible Line Width | Quick Drying & Smudge Proof Pastel Ink | Non-Toxic Ink, Safe For Childrens | 5 Smoothing Colors, Pack of 1',N'books',N'http://localhost:7000/ProductImages/16.jpg',N'wwwroot/ProductImages/16.jpg' UNION ALL
select 17,N'Wolpin Sticky Notes 400 Sheets',360,N'Wolpin Sticky Notes 400 Sheets Cube (4 Colors x 100 Sheets Each) Self Adhesive for Reminders, Notes, School, Study, Office Organizing Memo Meetings Pad 7.6 cm x 7.6 cm, Pastel Morandi Colors',N'books',N'http://localhost:7000/ProductImages/17.jpg',N'wwwroot/ProductImages/17.jpg' UNION ALL
select 18,N'Bible Highlighters Aesthetic Cute',499,N'UCRAVO Aesthetic Cute Highlighters Bible Highlighters and Pens No Bleed, Mild Soft Chisel Tip Pastel Highlighters Marker Pens for Journaling Note Taking School Stationary Supplies',N'books',N'http://localhost:7000/ProductImages/18.jpg',N'wwwroot/ProductImages/18.jpg' UNION ALL
select 19,N'Classmate Pulse Notebook',369,N'Classmate Pulse 1 Subject Notebook - 240mm x 180mm, Soft Cover, 180 Pages, Single Line, Pack of 4',N'books',N'http://localhost:7000/ProductImages/19.jpg',N'wwwroot/ProductImages/19.jpg' UNION ALL
select 20,N'Book & Magazine Iron Stand',999,N'Zepdos Iron Triangle Slot Magazine Stand - File Rack Desktop Book Organizer, Tabletop Table Book Stand For Office - Magazine Files Stand, File Organiser, Tiered Shelf, 25.4 X 20.3 X 15.2 Cm
',N'books',N'http://localhost:7000/ProductImages/20.jpg',N'wwwroot/ProductImages/20.jpg' UNION ALL
select 21,N'Nivia Skipping Rope',599,N'Nivia Trainer Skipping Rope for Men, Women & Children, Jump Rope for Exercise, for Workout & Weight Loss, Exercise Rope, Skipping Rope for Training, Sports Fitness/Gym',N'sports',N'http://localhost:7000/ProductImages/21.jpg',N'wwwroot/ProductImages/21.jpg';

set identity_insert [Products] off;

