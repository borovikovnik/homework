@echo off
if "%status%"=="working" goto :EOF

%nunit% %tests_path% > %tests_log%