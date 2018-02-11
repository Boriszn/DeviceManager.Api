# DeviceManager.Api

Web API Solution demonstrates mutliteantcy architecture, using Entity Framework, UnitOfWork, Repository patterns

## Installation

1. Clone repository
2. Apply Entity Framework migration. Run: `Update-DataBase`.
    2.1. (For Multitenancy testing) Change `DefaultConnection` to `;Database=DeviceDb-ten2;` in `appsettings.json`. Run EF migration `Update-DataBase`. It will create another database.
    2.2  Tenants Dabase configuration stored in [DataBaseManager](src\DeviceManager.Api\Data\Management\DataBaseManager.cs)(`tenantConfigurationDictionary`).
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

All changes can be easily found in [CHANGELOG](CHANGELOG.md)

## License

This project is licensed under the MIT License
