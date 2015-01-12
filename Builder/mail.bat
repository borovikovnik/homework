@echo off
if "%status%"=="working" goto :EOF

type %clone_err_log% %build_err_log% %build_log% %exist_log% %tests_log% > %mail%
%blat% -body %mail% -to tnik44@gmail.com -subject "Builder inf" 