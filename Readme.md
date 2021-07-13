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

In order to enable CORS requests from the browser you should register CrossOriginSupportModule and enable CORS custom headers in the web.config: 

```xml
  <system.webServer>
    <modules runAllManagedModulesForAllRequests="true">
	  <add name="CrossOriginSupportModule" type="Rhetos.FloydExtensions.CrossOriginSupportModule, Rhetos.FloydExtensions" />
    </modules>
  </system.webServer>

  <httpProtocol>
    <customHeaders>
      <add name="Access-Control-Allow-Origin" value="http://localhost:4200" /> <!-- root url of your application goes here -->
      <add name="Access-Control-Allow-Methods" value="POST, GET, PUT, DELETE, OPTIONS" />
      <add name="Access-Control-Allow-Headers" value="Content-Type, Authorization, Accept" />
      <add name="Access-Control-Allow-Credentials" value="true" />
    </customHeaders>
  </httpProtocol>
```

FloydExtensions plugin adds following claims (every user in the system should have permission for this claim):

* *ClaimResource*: 'Floyd.GetStructureMetadata',  *ClaimRight*: 'Execute'
* *ClaimResource*: 'Floyd.MyClaims',  *ClaimRight*: 'Read'
* *ClaimResource*: 'Floyd.GetStorage',  *ClaimRight*: 'Read'
* *ClaimResource*: 'Floyd.SaveStorageItem',  *ClaimRight*: 'Execute'

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
