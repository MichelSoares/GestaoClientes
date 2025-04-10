@echo off
echo.
echo Compilando projeto GestaoClientes.APP...
cd /d "%~dp0GestaoClientes\GestaoClientes.APP"

dotnet build

echo.
echo Build finalizado.
pause
