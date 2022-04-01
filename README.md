# MultiTenancy demo project

## Add and run migrations
- Run migrations
```console
dotnet ef migrations add AddCategory -s Multitenant.Api -p Infrastructure
```

- Update databases for all tenant
This command will update tenants databases once
```console
dotnet ef database update InitialMigration -s Multitenant.Api -p Infrastructure
```