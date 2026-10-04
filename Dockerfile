FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["ApiWebFilme/ApiWebFilme.csproj", "ApiWebFilme/"]
RUN dotnet restore "ApiWebFilme/ApiWebFilme.csproj"

COPY . .
RUN dotnet publish "ApiWebFilme/ApiWebFilme.csproj" \
    --configuration Release \
    --output /app/publish \
    --no-restore \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .
COPY Assets /Assets

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "ApiWebFilme.dll"]
