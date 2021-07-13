# Rhetos.FloydExtensions release notes

## 5.0.0 (TO BE RELEASED)

### Breaking changes

1. Migrated from .NET Framework to .NET 5 (ASP.NET Core 5) and Rhetos 5.
2. IIS CrossOriginSupportModule is no longer implemented in this package.
   * Enable CORS in your application by following instruction at [Enable CORS in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-5.0).
   * Removed old CORS configuration from web.config (see Readme.md from older version of this package).
