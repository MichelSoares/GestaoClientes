#!/bin/bash
set -e

echo "??? Aguardando SQL Server iniciar (20s)..."
sleep 20

echo "???? Executando script SQL..."
/opt/mssql-tools/bin/sqlcmd -S sqlserver -U SA -P "dev#appgestao202134" -i /scripts/sql_gestao_clientes.sql

echo "??? Script SQL executado com sucesso!"
