@echo off
dotnet build src/Limbo.Umbraco.Migrations.FormsApi --configuration Debug /t:rebuild /t:pack -p:PackageOutputPath=c:\nuget\Umbraco8