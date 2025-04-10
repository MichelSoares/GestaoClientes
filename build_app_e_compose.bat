@echo off
echo =======================================
echo Desativando conversão automática de LF/CRLF no Git...
git config --global core.autocrlf false

echo.
echo =======================================
echo Convertendo arquivos .sh para LF...
powershell -Command "Get-ChildItem -Path './scripts' -Filter *.sh -Recurse | ForEach-Object { (Get-Content $_.FullName) -join \"`n\" | Set-Content -Encoding Ascii $_.FullName }"

echo.
echo =======================================
echo Compilando projeto GestaoClientes.APP...
cd /d "%~dp0GestaoClientes\GestaoClientes.APP"
dotnet build

echo.
echo =======================================
echo Subindo containers com Docker Compose...
cd /d "%~dp0"
docker-compose up --build

pause
