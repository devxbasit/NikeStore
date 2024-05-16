--------------------------------------------------------------------
-- AuthApi
--------------------------------------------------------------------
USE [NikeStore.AuthApiDb]
SELECT * FROM AspNetUsers
SELECT * FROM AspNetUserRoles
SELECT * FROM AspNetUserLogins
SELECT * FROM AspNetRoles
SELECT * FROM AspNetUserClaims
SELECT * FROM AspNetRoleClaims
SELECT * FROM AspNetUserTokens



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


















