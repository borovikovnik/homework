@echo off

set status="working"

call settings.bat

call clean.bat

call clone.bat
if errorlevel 1 goto :mail
call build.bat
if errorlevel 1 goto :mail
call check.bat
if errorlevel 1 goto :mail
call tests.bat
if not errorlevel 1 goto :EOF
:mail
call mail.bat
