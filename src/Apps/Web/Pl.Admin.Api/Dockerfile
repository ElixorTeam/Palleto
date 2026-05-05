# ================= BUILD BASE =================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY . .

# ================= DEV =================
FROM build AS dev-build
RUN dotnet publish -c Develop -o /app/dev

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS dev
WORKDIR /app
EXPOSE 8080

COPY --from=dev-build /app/dev .
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "Pl.Admin.Api.dll"]

# ================= PROD =================
FROM build AS prod-build
RUN dotnet publish -c Release -o /app/prod

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=prod-build /app/prod .
ENTRYPOINT ["dotnet", "Pl.Admin.Api.dll"]