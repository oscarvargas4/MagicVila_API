#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MagicVila_VilaAPI/MagicVila_VilaAPI.csproj", "MagicVila_VilaAPI/"]
RUN dotnet restore "MagicVila_VilaAPI/MagicVila_VilaAPI.csproj"
COPY . .
WORKDIR "/src/MagicVila_VilaAPI"
RUN dotnet build "MagicVila_VilaAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MagicVila_VilaAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MagicVila_VilaAPI.dll"]

#FROM mcr.microsoft.com/mssql/server:2022-latest
#ENV ACCEPT_EULA=Y
#ENV SA_PASSWORD=Your!rOOT
#EXPOSE 1433