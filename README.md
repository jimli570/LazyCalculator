# Lazy Calculator

Written in C# 7.9 .NET Core 2.1
The application reads UTF-8 encoded textfiles containing commands for the calculator.
Filepath can be specified through command line input, if no path is specified 'math_data_1.txt' will be defaulted.

Operations the calculator can not determine directly, will by evaluated at print. Therefore the name 'Lazy Calculator'.

Examples of accepted input can be found in 'math_data_1.txt', 'math_data_2.txt' & 'math_data_3.txt'. 

## Requirements

- .NET Core 2.1

## Running the application

Easiets way to run the application is by executing 'RunApplication.bat' which runs 'dotnet Calculator.dll' for you.
Tests can be ran through Visual Studio.

## Solution Structure

- 'Calculator/' contains the Application
- 'Test/' contains tests written using NUnit3.
