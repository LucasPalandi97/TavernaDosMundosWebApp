Create a certificate for HTTPS:

dotnet dev-certs https --clean
dotnet dev-certs https --check --verbose
dotnet dev-certs https
dotnet dev-certs https --trust
dotnet dev-certs https --check --trust

To run docker compose install docker desktop, follow the instalation instructions.
Open your developer terminal or powershel/bash and execute

docker-compose up --build
on your browser open https://localhost or https://localhost:52995