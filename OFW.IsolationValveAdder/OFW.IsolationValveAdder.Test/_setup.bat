@echo off
echo %~f0 %1 %2

REM set installdir="C:\Program Files (x86)\Bentley\WaterGEMS\x64\"
set installdir="C:\Program Files (x86)\Bentley\WaterCAD\x64\"


pushd %installdir%
for %%I in (*.*) do (if not exist "%~1%%~nxI" mklink "%~1%%~nxI" "%%~fI")
popd