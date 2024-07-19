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
DECLARE @AdminRoleId NVARCHAR(300) = '3ee3f0c8-3a48-41b7-b8e3-e8798938e24a', @AdminUserId NVARCHAR(300) = 'e0df0a9c-ab3c-495c-98b6-900b75ef3c1c';
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



DELETE FROM Coupons




--------------------------------------------------------------------
-- ProductApi
--------------------------------------------------------------------
USE [NikeStore.ProductApiDb]
SELECT * FROM Products
DELETE FROM Products


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


















