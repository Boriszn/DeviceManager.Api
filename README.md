
![alt text](https://raw.githubusercontent.com/Boriszn/DeviceManager.Api/develop/assets/logos/device-manager-main-logo.png "The main logo")

Web API Solution demonstrates mutliteantcy architecture, using Entity Framework, UnitOfWork,Repository patterns

# [![Build status](https://ci.appveyor.com/api/projects/status/x1whwie6v68l8200?svg=true)](https://ci.appveyor.com/project/Boriszn/devicemanager-api) [![Gitter chat](https://badges.gitter.im/gitterHQ/gitter.png)](https://gitter.im/DeviceManager-Api/)

![alt text](https://github.com/Boriszn/DeviceManager.Api/blob/feature/ISS-1-Add-Automapper/assets/arhitecture-diag.png "Logo Title Text 1")

## Project

Todo list, accomplished tasks, can be [found Here.](https://github.com/Boriszn/DeviceManager.Api/projects/1)

## Installation

1. Clone repository
2. Apply Entity Framework migration. Run: `Update-DataBase`.
* (For Multitenancy testing) Change `DefaultConnection` to `;Database=DeviceDb-ten2;` in `appsettings.json`. Run EF migration `Update-DataBase`. It will create another database.
* **Tenants Dabase configuration stored in [DataBaseManager](src/DeviceManager.Api/Data/Management/DataBaseManager.cs) (`tenantConfigurationDictionary`)**.
3. Fill up valid database connection string configuration option in `appsettings.json`.
4. Run UnitTests.
5. (Optional) Run API integration tests
6. Build / Run.

## Database Connection

`DatabaseType` field is used to specify the database type the application should connect.
Currently, the framework contains connection information for:
- _MsSql_ (MS SQL Server, Sql Express)
- _Postgres_ NoSql ([Additional info can be found here](http://www.npgsql.org/efcore/))

The value of `DatabaseType` should come from `DatabaseType` enum in [Settings](src/DeviceManager.Api/Configuration/Settings) folder and should match the class name inside [DatabaseTypes](src/DeviceManager.Api/Configuration/DatabaseTypes) folder and implement `IDatabaseType` interface.

To add a new database type, just add a class implementing `IDatabaseType` and add the same name inside `DatabaseType` and change connection string in the `DefaultConnection` property and `DatabaseType` to new database type.

## Localization Support
Application supports localization support though resource files. Currently, shared resource file is used to support support for `English` and `German` languages. 
According to ([Microsoft docs](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-2.1)), to use a UI culture pass it as a query parameter (ui-culture=de-DE).
All the resource values for each UI culture should be added to a resource file under [Resources](src/DeviceManager.Api/Resources) folder. The file name should include culture code. 
Text values from resource files based on the UI culture is obtained from using the instance of `IStringLocalizer<SharedResource> sharedLocalizer` which is injected via constructor. Then use this object to get resource values using `sharedLocalizer["ResourceKey"].Value`.
Check `Ping` action in [BaseController](src/DeviceManager.Api/Controllers/BaseController.cs). 

**Note:** If only one of `culture` or `ui-culture` is sent in the query parameter then `dotnetcore` uses same value for the other one.

## Data Seeding
To seed database with initial data update `SeedData` method in [DataSeeder](src/DeviceManager.Api/Data/DataSeed/DataSeeder.cs) class. 

There can be multiple data seeding classes. To create a new data seeding class
1. Create a new data seeding class in the same folder inheriting from [IDataSeeder](src/DeviceManager.Api/Data/DataSeed/IDataSeeder.cs) interface.
2. Register new class in the [IocContainerConfiguration](src/DeviceManager.Api/Configuration/IocContainerConfiguration.cs) class by replacing `DataSeeder` with new class name.

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
