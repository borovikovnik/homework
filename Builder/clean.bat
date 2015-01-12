@echo off
if "%status%"=="working" goto :EOF

rmdir /s /q %rep_path%