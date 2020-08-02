# DbTools
Some tools around Databases (Schema Export, ...)

## Projects
### Libraries
|Name|Description|TargetFramework
|------|---|---|
|Core|- |.Net Standard 2.0
|Core.MySql|- |.Net Standard 2.0
|DbStructure.Data|Classes representing Database objects (Table, Column) |.Net Standard 2.0
|DbStructure.Helpers|Some helpers around DbStructure.Data classes  |.Net Standard 2.0
|DbStructure.Services.MySql|Services providing Database objects for MySQL|.Net Standard 2.0
|InformationSchema.Data|Classes representing INFORMATION_SCHEMA data (Tables, Columns, ...)|.Net Standard 2.0
|InformationSchema.Helpers|Some helpers around InformationSchema.Data classes |.Net Standard 2.0

### Applications
#### DbStructure.ExportConsoleApp
.Net Core 3.1 Console Application

Export DB Structure (using DbStructure.Data) to XML or JSON

#### Usage
##### Arguments
|Option|Description|Required
|------|---|---|
|--connectionString | MySQL DB connection string |Yes
|--tableCatalog|Table Catalog | 
|--tableSchema |Table Schema |
|--outputPath| Output Path |Yes
|--help|Display version information|

##### Example
```
> DbTools.DbStructure.ExportConsoleApp.exe --connectionString="Host=hostname;Database=db;Username=yyy;Password=xxxx" --tableSchema="table_schema" --outputPath="C:\DbTools\Database.json"
```

## Getting Started
### Prerequisites
- Visual Studio 2019

## Authors

* **Amael BERTEAU**

