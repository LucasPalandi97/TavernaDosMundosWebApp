#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TdM.Web/TdM.Web.csproj", "TdM.Web/"]
COPY ["TdM.Database/TdM.Database.csproj", "TdM.Database/"]
RUN dotnet restore "TdM.Web/TdM.Web.csproj"
COPY . .
WORKDIR "/src/TdM.Web"
RUN dotnet build "TdM.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TdM.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TdM.Web.dll"]