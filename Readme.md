# Rhetos.FloydExtensions

Rhetos.FloydExtensions is a DSL package (a plugin module) for [Rhetos development platform](https://github.com/Rhetos/Rhetos).
It provides an implementation of generating **TypeScript** model interfaces from **DSL model** and provider for structure metadata.

Contents:

1. [Installation and configuration](#installation-and-configuration)
2. [Usage](#usage)
3. [How to contribute](#how-to-contribute)

See [rhetos.org](http://www.rhetos.org/) for more information on Rhetos.

## Installation and configuration

Installing this package to a Rhetos application:

1. Add 'Rhetos.FloydExtensions' NuGet package, available at the [NuGet.org](https://www.nuget.org/) on-line gallery.
2. Enable CORS requests from the browser for your application.
   See [Enable CORS in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-5.0) for more info.

FloydExtensions plugin adds following claims (every user in the system should have permission for this claim):

* *ClaimResource*: 'Floyd.GetStructureMetadata',  *ClaimRight*: 'Execute'
* *ClaimResource*: 'Floyd.MyClaims',  *ClaimRight*: 'Read'
* *ClaimResource*: 'Floyd.GetStorage',  *ClaimRight*: 'Read'
* *ClaimResource*: 'Floyd.SaveStorageItem',  *ClaimRight*: 'Execute'

## Usage

Generated TypeScript model is placed in bin folder in `RhetosAssets\rhetos-model.ts` file. Simply include this file in your project. Here is sample of generated TypesScript model:

```typescript
export module Common {
    ///
    
    export const PrincipalPermissionKey = 'Common/PrincipalPermission'; //constant that represents a key for retrieving metadata via Common.GetStructureMetadata function
    export interface PrincipalPermission {
        ID?: string;
        PrincipalID: string;
        ClaimID: string;
        IsAuthorized?: boolean;
    }
    
    ///
}
```

In order to retrieve metadata for specified model (DataStructure) call **Common.GetStructureMetadata** function providing *Key* parameter. Function returns an object with `Value` property which contains JSON metadata like this:

```js
{
  key: 'Common/PrincipalPermission', 
  serviceType: 'Entity',
  properties: {
    'PrincipalID': {
      type: 'Reference', 
      referenceKey: 'Common/Principal', 
      required: true
    }, 
    'ClaimID': {
      type: 'Reference', 
      referenceKey: 'Common/Claim', 
      required: true
    }, 
    'IsAuthorized': {
      type: 'Bool', 
    }
  }
}
```

## How to contribute

Contributions are very welcome. The easiest way is to fork this repo, and then
make a pull request from your fork. The first time you make a pull request, you
may be asked to sign a Contributor Agreement.
For more info see [How to Contribute](https://github.com/Rhetos/Rhetos/wiki/How-to-Contribute) on Rhetos wiki.

**Building** the source code:

* Note: This package is already available at the [NuGet.org](https://www.nuget.org/) online gallery.
  You don't need to build it from source in order to use it in your application.
* To build the package from source, run `Clean.bat` and `Build.bat`.
* For the test script to work, you need to create an empty database and
  a settings file `test\TestApp\rhetos-app.local.settings.json`
  with the database connection string (configuration key "ConnectionStrings:RhetosConnectionString").
* The build output is a NuGet package in the "Install" subfolder.
