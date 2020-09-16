@echo off

echo =======================================================================
echo Cosmos.Dapper
echo =======================================================================

::go to parent folder
cd ..

::create nuget_packages
if not exist nuget_packages (
    md nuget_packages
    echo Created nuget_packages folder.
)

::clear nuget_packages
for /R "nuget_packages" %%s in (*) do (
    del "%%s"
)
echo Cleaned up all nuget packages.
echo.

::start to package all projects
dotnet pack src/Cosmos.Dapper/Cosmos.Dapper._build.csproj                                -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Dapper.MySql/Cosmos.Dapper.MySql._build.csproj                    -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Dapper.MySqlConnector/Cosmos.Dapper.MySqlConnector._build.csproj  -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Dapper.Oracle/Cosmos.Dapper.Oracle._build.csproj                  -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Dapper.PostgreSql/Cosmos.Dapper.PostgreSql._build.csproj          -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Dapper.SqlServer/Cosmos.Dapper.SqlServer._build.csproj            -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Dapper.Sqlite/Cosmos.Dapper.Sqlite._build.csproj                  -c Release -o nuget_packages --no-restore

for /R "nuget_packages" %%s in (*symbols.nupkg) do (
    del "%%s"
)

echo.
echo.

::push nuget packages to server
for /R "nuget_packages" %%s in (*.nupkg) do ( 	
    dotnet nuget push "%%s" -s "Release" --skip-duplicate
	echo.
)

::get back to build folder
cd build