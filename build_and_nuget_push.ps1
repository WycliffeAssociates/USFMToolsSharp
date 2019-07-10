dotnet build -c Release
cd .\USFMToolsSharp\bin\Release
$PkgName = Get-ChildItem -Name -Path .\*.nupkg
dotnet nuget push $PkgName -k $NUGET_TOKEN -s https://api.nuget.org/v3/index.json
