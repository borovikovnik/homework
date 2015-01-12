@echo off
if "%status%"=="working" goto :EOF

git clone %gitURL% %rep_path% > %clone_err_log%
