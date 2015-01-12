@echo off
if "%status%"=="working" goto :EOF


for /F "tokens=*" %%f in (%paths%) do (
	if not exist %builded_path%\%%f (
			echo %%f not found >> %exist_log%
	)
)
