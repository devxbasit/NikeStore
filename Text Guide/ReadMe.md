

Install nuget packages In Coupon API
AutoMapper
AutoMapper.Extensions.Microsoft.DependencyInjection
Microsoft.EnitityFrameworkCore.SqlServer
Microsoft.EnitityFrameworkCore
Microsoft.EnitityFrameworkCore.Tools
Microsoft.AspNetCore.Authentication.JwtBearer


-- prepare for migration
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update




-- Auth Service
Install below nuget packages
Microsoft.AspNetCore.Identity.EntityFrameworkCore








