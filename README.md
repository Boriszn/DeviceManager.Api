# DeviceManager.Api [![Build status](https://ci.appveyor.com/api/projects/status/x1whwie6v68l8200?svg=true)](https://ci.appveyor.com/project/Boriszn/devicemanager-api) [![Gitter chat](https://badges.gitter.im/gitterHQ/gitter.png)](https://gitter.im/DeviceManager-Api/)

Web API Solution demonstrates mutliteantcy architecture, using Entity Framework, UnitOfWork, Repository patterns

![alt text](https://github.com/Boriszn/DeviceManager.Api/blob/feature/ISS-1-Add-Automapper/assets/arhitecture-diag.png "Logo Title Text 1")

## Installation

1. Clone repository
2. Apply Entity Framework migration. Run: `Update-DataBase`.
* (For Multitenancy testing) Change `DefaultConnection` to `;Database=DeviceDb-ten2;` in `appsettings.json`. Run EF migration `Update-DataBase`. It will create another database. 
* **Tenants Dabase configuration stored in [DataBaseManager](src/DeviceManager.Api/Data/Management/DataBaseManager.cs) (`tenantConfigurationDictionary`)**.
3. Fill up valid database connection string configuration option in `appsettings.json`.
4. Run UnitTests.
5. Build / Run.

## Contributing

1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request

## History

All changes can be easily found in [RELEASENOTES](ReleaseNotes.md)

## License

This project is licensed under the MIT License
