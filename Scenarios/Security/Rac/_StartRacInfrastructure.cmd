cd .\Host
start "Tnf.Rac.Host.dll" dotnet Tnf.Rac.Host.dll
cd ..

cd .\Authorization
start "Tnf.Rac.Host.Authorization.dll" dotnet Tnf.Rac.Host.Authorization.dll
cd ..

cd .\Management
start "Tnf.Rac.Management.Api.dll" dotnet Tnf.Rac.Management.Api.dll
cd ..

pause