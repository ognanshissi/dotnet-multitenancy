# MultiTenancy demo project

## Add and run migrations
- Run migrations
```console
dotnet ef migrations add <MigrationName> -s Multitenant.Api -p Infrastructure
```

- Update databases

>This command will update all tenants databases once

```console
dotnet ef database update -s Multitenant.Api -p Infrastructure
```