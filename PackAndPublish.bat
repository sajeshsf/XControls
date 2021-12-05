set projectName=%1
set version=%2
cd %projectName%
set packageOutputPath=bin/PackageOutput
msbuild.exe /p:Configuration=Release /t:pack /p:PackageOutputPath="%packageOutputPath%" -p:Authors=Sajesh
C:\Tools\nuget push %packageOutputPath%\%projectName%.version=%.nupkg oy2ke5fy427zjikjvfxqfkxfxm3oh7ce5jaorskkkwdomi -Source https://api.nuget.org/v3/index.json
cd ..