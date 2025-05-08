# Limbo Migrations API

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/limbo-works/Limbo.Umbraco.Migrations.FormsApi/blob/v8/main/LICENSE.md)
[![NuGet](https://img.shields.io/nuget/vpre/Limbo.Umbraco.MigrationsApi.svg)](https://www.nuget.org/packages/Limbo.Umbraco.Migrations.FormsApi)
[![NuGet](https://img.shields.io/nuget/dt/Limbo.Umbraco.MigrationsApi.svg)](https://www.nuget.org/packages/Limbo.Umbraco.Migrations.FormsApi)
[![Limbo.Umbraco.MigrationsApi at packages.limbo.works](https://img.shields.io/badge/limbo-packages-blue)](https://packages.limbo.works/limbo.umbraco.migrationsapi/)

Adds an API to Umbraco 8 for exporting forms and form records.

<table>
  <tr>
    <td><strong>License:</strong></td>
    <td><a href="https://github.com/limbo-works/Limbo.Umbraco.Migrations.FormsApi/blob/v8/main/LICENSE.md"><strong>MIT License</strong></a></td>
  </tr>
  <tr>
    <td><strong>Umbraco:</strong></td>
    <td>8.6+</td>
  </tr>
  <tr>
    <td><strong>Umbraco Forms:</strong></td>
    <td>8.5.7+</td>
  </tr>
  <tr>
    <td><strong>Target Framework:</strong></td>
    <td>.NET Framework 4.7.2</td>
  </tr>
</table>










<br /><br />

## Installation

### Umbraco 8

Via  [**NuGet**](https://www.nuget.org/packages/Limbo.Umbraco.Migrations.FormsApi/8.0.0):

```
dotnet add package Limbo.Umbraco.Migrations.FormsApi --version 8.0.0
```

or:

```
Install-Package Limbo.Umbraco.Migrations.FormsApi -Version 8.0.0
```





<br /><br />

## Configuration

Since form records may expose sensitive information, the APi isn't enabled by default. To enable the API, add the following to the `<appSettings>` element of your `Web.config` file:

```xml
<add key="LimboMigrationsApiFormsEnabled" value="true" />
```

The API key and IP white listing can be configured as described for our [**`Limbo.Umbraco.MigrationsApi`**](https://github.com/limbo-works/Limbo.Umbraco.MigrationsApi#configuration) package.



<br /><br />

## Endpoints

```
GET /api/limbo/migrations/forms
GET /api/limbo/migrations/forms/{key}
GET /api/limbo/migrations/forms/{key}/records
```