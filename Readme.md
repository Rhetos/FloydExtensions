# Rhetos.FloydExtensions

Rhetos.FloydExtensions is a DSL package (a plugin module) for [Rhetos development platform](https://github.com/Rhetos/Rhetos).
It provides an implementation of generating **TypeScript** model interfaces from **DSL model** and provider for structure metadata.

Contents:

1. [Installation and configuration](#installation-and-configuration)
2. [Usage](#usage)

See [rhetos.org](http://www.rhetos.org/) for more information on Rhetos.

## Installation and configuration

To install this package to a Rhetos server, add it to the Rhetos server's *RhetosPackages.config* file
and make sure the NuGet package location is listed in the *RhetosPackageSources.config* file.

* The package ID is "**Rhetos.FloydExtensions**".
  This package is available at the [NuGet.org](https://www.nuget.org/) online gallery.
  The Rhetos server can install the package directly from there, if the gallery is listed in *RhetosPackageSources.config* file.
* For more information, see [Installing plugin packages](https://github.com/Rhetos/Rhetos/wiki/Installing-plugin-packages).

FloydExtensions plugin adds 1 new claim:

* *ClaimResource*: 'Common.GetStructureMetadata',  *ClaimRight*: 'Execute' - claim which provides metadata for specified DataStructure. Every user in the system should have permission for this claim.

## Usage

Generated TypeScript model is placed in `bin\Generated\RhetosModel.ts` file. Simply include this file in your project. Here is sample of generated TypesScript model:

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

```json
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
