@echo off
if "%status%"=="working" goto :EOF

%msbuild_path% %app_path% > %build_log% 2> %build_err_log%