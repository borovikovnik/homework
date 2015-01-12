@echo off
if "%status%"=="working" goto :EOF

set gitURL="https://github.com/borovikovnik/Builder"
set msbuild_path="C:\Windows\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe"
set app_path="D:\Projects\git\Test\isParallelogram\isParallelogram.sln"
set rep_path="D:\Projects\git\Test"
set builded_path="D:\Projects\git\Test\isParallelogram\GUI\bin\Debug"
set tests_path="D:\Projects\git\Test\isParallelogram\Tests\bin\Debug\Tests.dll"
set paths=libs.txt
set nunit="C:\Program Files (x86)\NUnit 2.6.4\bin\nunit-console.exe"
set blat="C:\Program Files (x86)\Blat Mail\full\blat.exe"

set clone_err_log="clone_errors.log"
set build_err_log="build_errors.log"
set build_log="build.log"
set exist_log="exist.log"
set tests_log="tests.log"
set mail="mail.txt"
