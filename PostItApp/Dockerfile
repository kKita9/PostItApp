FROM mcr.microsoft.com/mssql/server:2019-latest

# Zmieniamy użytkownika na root, aby uzyskać uprawnienia
USER root

# Instalacja wymaganych pakietów
RUN apt-get update && \
    apt-get install -y curl apt-transport-https unixodbc-dev gnupg && \
    curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.asc.gpg && \
    mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/microsoft.asc.gpg && \
    curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y mssql-tools && \
    echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc && \
    apt-get clean

# Powrót do domyślnego użytkownika
USER mssql

# Ustawiamy katalog roboczy
WORKDIR /usr/config

# Kopiowanie skryptów SQL
COPY ./Database/seed_data.sql /usr/config/seed_data.sql

# Uruchamianie SQL Server i ładowanie danych
ENTRYPOINT ["/bin/bash", "-c", "/opt/mssql/bin/sqlservr & sleep 30 && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'Password_123#' -i /usr/config/seed_data.sql && wait"]
