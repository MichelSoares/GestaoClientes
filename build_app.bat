@echo off
echo Desativa conversao git
git config core.autocrlf false
echo Compilando projeto GestaoClientes.APP...
cd /d "%~dp0GestaoClientes\GestaoClientes.APP"

dotnet build

echo.
echo Build finalizado.
pause
