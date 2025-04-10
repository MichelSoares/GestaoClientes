#!/bin/bash
/opt/mssql/bin/sqlservr &

echo "Aguardando o SQL Server iniciar..."
sleep 20

curl -sSL https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
curl -sSL https://packages.microsoft.com/config/debian/10/prod.list > /etc/apt/sources.list.d/mssql-release.list

apt-get update
ACCEPT_EULA=Y apt-get install -y msodbcsql17 mssql-tools unixodbc-dev

export PATH="$PATH:/opt/mssql-tools/bin"

/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'dev#appgestao202134' -Q "CREATE DATABASE GestaoClientes"
sleep 2
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'dev#appgestao202134' -d GestaoClientes -i /scripts/sql_gestao_clientes.sql

tail -f /dev/null
