#!/bin/bash

echo "Waiting for SQL Server to start"
until /opt/mssql-tools/bin/sqlcmd -S TavernaDb -U sa -P "PastelDosMundos@123" -Q "SELECT 1"; do
  echo "SQL Server is not ready yet..."
  sleep 1s
done

echo "TavernaDb is now ready. Waiting for AuthDb to start"
until /opt/mssql-tools/bin/sqlcmd -S TavernaAuthDb -U sa -P "PastelDosMundos@123" -Q "SELECT 1"; do
  echo "AuthDb is not ready yet..."
  sleep 1s
done

echo "Both SQL Server containers are now ready. Applying migrations..."
cd app/TdM.Web
dotnet ef database update --context TavernaDbContext --connection "Server=localhost;Database=TavernaDb;User Id=sa;Password=PastelDosMundos@123;TrustServerCertificate=True"
dotnet ef database update --context AuthDbContext --connection "Server=localhost, 1435;Database=TavernaAuthDb;User Id=sa;Password=PastelDosMundos@123;TrustServerCertificate=True"
