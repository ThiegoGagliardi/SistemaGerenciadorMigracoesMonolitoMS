FROM mcr.microsoft.com/dotnet/sdk:8.0 AS debug
WORKDIR /src

COPY ["GerenciamentoMigracaoMonolitoParaMS.app.csproj", "./"]
RUN dotnet restore

COPY . .

ENTRYPOINT [ "dotnet", "watch", "--project", "GerenciamentoMigracaoMonolitoParaMS.app.csproj", "run", "--urls", "http://+:8080" ]

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=publish /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "GerenciamentoMigracaoMonolitoParaMS.app.dll"]