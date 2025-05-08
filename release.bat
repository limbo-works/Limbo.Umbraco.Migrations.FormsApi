@echo off
dotnet build src/Limbo.Umbraco.Migrations.FormsApi --configuration Release /t:rebuild /t:pack -p:PackageOutputPath=../../releases/nuget