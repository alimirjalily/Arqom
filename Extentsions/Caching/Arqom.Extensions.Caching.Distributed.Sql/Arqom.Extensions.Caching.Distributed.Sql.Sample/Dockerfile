#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Arqom.Extensions.Caching.Distributed.Sql.Sample/Arqom.Extensions.Caching.Distributed.Sql.Sample.csproj", "Arqom.Extensions.Caching.Distributed.Sql.Sample/"]
RUN dotnet restore "Arqom.Extensions.Caching.Distributed.Sql.Sample/Arqom.Extensions.Caching.Distributed.Sql.Sample.csproj"
COPY . .
WORKDIR "/src/Arqom.Extensions.Caching.Distributed.Sql.Sample"
RUN dotnet build "Arqom.Extensions.Caching.Distributed.Sql.Sample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Arqom.Extensions.Caching.Distributed.Sql.Sample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Arqom.Extensions.Caching.Distributed.Sql.Sample.dll"]