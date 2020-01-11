Update-Database -TargetMigration $InitialDatabase -Force


Default values
1. after migration generation
Add ... to TestFlag ... x4

, defaultValueSql: "'FALSE'"


Getting Started:
-----------------------------------------------
1. Update-Database
2. Boostrap
- Users
- Looksups
- Providers [Hold]
- Employees [Hold]
- Skus
- Schedule
3. Import
- Load ProviderGroup
- Load Provider
- Load Owner
- Load Pet