# Rhetos.FloydExtensions release notes

## 5.2.0 (TO BE RELEASED)

* Support for Hardcoded entity with Bool, Date or DateTime properties. This fixes typescript error "Type 'number' is not assignable to type 'boolean'." for Bool properties.
* Rhetos Action `SaveStorageItem` now deletes a record if passed Value is null or empty string.

## 5.1.0 (2023-03-16)

* Bugfix: **Hardcoded** items array is now properly typed.
* Bugfix: ID property is not generated for **Polymorphic**.

## 5.0.0 (2022-03-25)

### Breaking changes

1. Migrated from .NET Framework to .NET 5 (ASP.NET Core 5) and Rhetos 5.
2. IIS CrossOriginSupportModule is no longer implemented in this package.
   * Enable CORS in your application by following instruction at [Enable CORS in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-5.0).
   * Removed old CORS configuration from web.config (see Readme.md from older version of this package).

## 1.0.6 (2021-10-12)

* Constants for **Hardcoded** entity: `<Module>.<Entity>Ids` and `<Module>.<Entity>Items`.
