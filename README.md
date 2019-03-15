
![alt text](https://raw.githubusercontent.com/Boriszn/DeviceManager.Api/develop/assets/logos/device-manager-main-logo.png "The main logo")

Web API Solution demonstrates mutliteantcy architecture, using Entity Framework, UnitOfWork,Repository patterns

# [![Build status](https://ci.appveyor.com/api/projects/status/x1whwie6v68l8200?svg=true)](https://ci.appveyor.com/project/Boriszn/devicemanager-api) [![Gitter chat](https://badges.gitter.im/gitterHQ/gitter.png)](https://gitter.im/DeviceManager-Api/) [![Build status](https://img.shields.io/docker/pulls/boriszn/devicemanagerapi.svg)](https://hub.docker.com/r/boriszn/devicemanagerapi/)

![arh-diagram](https://raw.githubusercontent.com/Boriszn/DeviceManager.Api/develop/assets/arhitecture-diag.png)

## Project

Todo list, accomplished tasks, can be [found Here.](https://github.com/Boriszn/DeviceManager.Api/projects/1)

## Installation

1. Clone repository
2. Apply Entity Framework migration. Run: `Update-DataBase`.
* (For Multitenancy testing) Change `DefaultConnection` to `;Database=DeviceDb-ten2;` in `appsettings.json`. Run EF migration `Update-DataBase`. It will create another database.
* **Tenants Database configuration stored in [DataBaseManager](src/DeviceManager.Api/Data/Management/DataBaseManager.cs) (`tenantConfigurationDictionary`)**.
3. Fill up valid database connection string configuration option in `appsettings.json`.
4. Run UnitTests.
5. (Optional) Run API integration tests
6. Build / Run.

## Database Connection

`DatabaseType` field is used to specify the database type the application should connect.
Currently, the framework contains connection information for:

- _MsSql_ (MS SQLServer, Sql Express)
- _Postgres_ NoSql ([Information how to setup Postgres can be found here](http://www.npgsql.org/efcore/))

The value of `DatabaseType` should come from `DatabaseType` enum (src\DeviceManager.Api\Configuration\Settings) and should match the class name inside (src\DeviceManager.Api\Configuration\DatabaseTypes) and implement `IDatabaseType` interface.

To add a new database type, just add a class implementing `IDatabaseType` and add the same name inside `DatabaseType` and change connection string in the `DefaultConnection` property and `DatabaseType` to new database type.

## Localization Support

Application supports localization support though resource files. Currently, shared resource file is used to support support for `English` and `German` languages.
According to ([Microsoft docs](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-2.1)), to use a UI culture pass it as a query parameter (`ui-culture=de-DE`).
All the resource values for each UI culture should be added to a resource file under [Resources](src/DeviceManager.Api/Resources) folder. The file name should include culture code.
Text values from resource files based on the UI culture is obtained from using the instance of `IStringLocalizer<SharedResource> sharedLocalizer` which is injected via constructor. Then use this object to get resource values using `sharedLocalizer["ResourceKey"].Value`.

Check `Ping` action in [BaseController](src/DeviceManager.Api/Controllers/BaseController.cs).

**Note:** If only one of `culture` or `ui-culture` is sent in the query parameter then `dotnetcore` uses same value for the other one.

## Data Seeding

To seed database with initial data update `SeedData` method in [DataSeeder](src/DeviceManager.Api/Data/DataSeed/DataSeeder.cs)class.

There can be multiple data seeding classes. To create a new data seeding class:

1. Create a new data seeding class in the same folder inheriting from [IDataSeeder](src/DeviceManager.Api/Data/DataSeed/IDataSeeder.cs) interface.
2. Register new class in the [IocContainerConfiguration](src/DeviceManager.Api/Configuration/IocContainerConfiguration.cs) class by replacing `DataSeeder` with new class name.

## Docker support

App images available in Docker Hub Registry: https://hub.docker.com/r/boriszn/devicemanagerapi/ (**LINUX and Windows images are available**)

You can pull image:
- Linux: `docker pull boriszn/devicemanagerapi:1.0.4`
- Windows `docker pull boriszn/devicemanagerapi:latest`

The solution was migrated to the docker container and support docker compose orchestration.

You can run docker using following options:

- Visual Studio 2017 Debug mode. Run VS (`F5`) will run docker container.
- Docker CLI. Run in PS `docker run -p 8080:80 --rm -d boriszn/devicemanagerapi:latest` will run docker container in background (or `docker run -p 8080:80 boriszn/devicemanagerapi:latest`)

You can access the the Web API locally via URL: http://localhost:8080

You can also **Build** container from solution. To do so run `docker build -t  boriszn/devicemanagerapi:1.0.4 .` or run it in VS.

**IMPORTANT. To debug solution in Visual Studio replace file `Dockerfile` with `Dockerfile-VsDebug` file (or replace content of `Dockerfile` from `Dockerfile-VsDebug`) This is temporary work around to be able to run containers in command line and in Visual Studio.  It will fixed (consolidated in one docker file) in upcoming PRs**

**To run/build project without docker, switch from `Docker` to `DeviceManagerApi` (specified in `launchSettings.json`)**

## Kubernates (minikube cluster) setup

- Setup minikube (tested on `v0.34.1`) ([This article should help](https://medium.com/containers-101/local-kubernetes-for-windows-minikube-vs-docker-desktop-25a1c6d3b766))
- Start minikube (for example: `minikube start --vm-driver=hyperv --hyperv-virtual-switch=testCluster`. My setup used Windows 10 Pro + Hyper-V Hypervisor)
- To create deployments run `kubectl create -f ks-deployment.yaml`
- Check if deployments created successfully, run `kubectl get deployments`
- Create service to expose app, run `kubectl expose deployment devicemanagerapi --type=NodePort` (as minikube doesn't contain ingress/load balancer)
- To receive URL run `minikube service devicemanagerapi --url`
- Finally you can test all setup, run minikube dashboard `minikube dashboard`. Dashboard is displayed on image below.

![minikube-dashboard](https://raw.githubusercontent.com/Boriszn/DeviceManager.Api/feature/kubernates-integration/assets/docker-ks/minikube-dashboard.png)

Or you can use [This plugin](https://marketplace.visualstudio.com/items?itemName=ms-kubernetes-tools.vscode-kubernetes-tools) for VS Code to manage/monitor minikube cluster

![vs-code-plugin](https://raw.githubusercontent.com/Boriszn/DeviceManager.Api/feature/kubernates-integration/assets/docker-ks/vs-code-plugin.png)

**IMPORTANT: Ensure that you are switched to LINUX docker container. Because minikube support only LINUX based containers (at least `v0.34.1`. In future it can be changed)**

## Known issues

- **In case of running from Docker** Connection string should be changed to use IP addresses or real server domain names for `Server` parameter. Also `User Id` and `Password` should be added.
For example: `Server=192.168.1.36,1433;Database=DeviceDb;User Id=MyUser;Password=MySuperStringPassword;Trusted_Connection=True;MultipleActiveResultSets=true`

**Please be aware connection string like: `Server=(localdb)\\mssqllocaldb;Database=DeviceDb;Trusted_Connection=True;MultipleActiveResultSets=true` will NOT work if app running from Docker container**

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
