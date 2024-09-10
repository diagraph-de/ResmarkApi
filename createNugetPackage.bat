@echo off
setlocal

rem Set the current directory to the batch file's path
cd /d "%~dp0"

rem Delete the file Diagraph-ResmarkAPI.1.0.0.nupkg if it exists
if exist "Diagraph-ResmarkAPI.1.0.0.nupkg" (
    del "Diagraph-ResmarkAPI.1.0.0.nupkg"
)

rem Pack the NuGet package
nuget pack "ResmarkAPI.sln.nuspec" -IncludeReferencedProjects -Properties Configuration=Release -Version 1.0.0

pause
