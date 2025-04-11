#!/bin/bash
echo "Aguardando SQL Server iniciar..."
sleep 20
/opt/mssql-tools/bin/sqlcmd -S sqlserver -U SA -P "dev#appgestao202134" -i /scripts/sql_gestao_clientes.sql