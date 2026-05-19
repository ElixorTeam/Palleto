# ================= BUILD BASE =================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# ================= RESTORE LAYER =================
COPY ["src/Directory.Packages.props", "src/"]
COPY ["src/Directory.Build.props", "src/"]
COPY ["src/Directory.Build.targets", "src/"]

COPY ["src/Apps/Web/Pl.Admin.Api/Pl.Admin.Api.csproj", "src/Apps/Web/Pl.Admin.Api/"]
COPY ["src/Apps/Web/Pl.Admin.Models/Pl.Admin.Models.csproj", "src/Apps/Web/Pl.Admin.Models/"]

COPY ["src/Libs/Pl.Print/Pl.Print.csproj", "src/Libs/Pl.Print/"]
COPY ["src/Libs/Pl.Shared/Pl.Shared.csproj", "src/Libs/Pl.Shared/"]
COPY ["src/Libs/Pl.Shared.Web/Pl.Shared.Web.csproj", "src/Libs/Pl.Shared.Web/"]

COPY ["src/Infrastructure/Pl.Database/Pl.Database.csproj", "src/Infrastructure/Pl.Database/"]

# ================= RESTORE =================
RUN dotnet restore "src/Apps/Web/Pl.Admin.Api/Pl.Admin.Api.csproj"

# ================= SOURCE LAYER =================
COPY ["src/Apps/Web/Pl.Admin.Api/", "src/Apps/Web/Pl.Admin.Api/"]
COPY ["src/Apps/Web/Pl.Admin.Models/", "src/Apps/Web/Pl.Admin.Models/"]

COPY ["src/Libs/Pl.Print/", "src/Libs/Pl.Print/"]
COPY ["src/Libs/Pl.Shared/", "src/Libs/Pl.Shared/"]
COPY ["src/Libs/Pl.Shared.Web/", "src/Libs/Pl.Shared.Web/"]

COPY ["src/Infrastructure/Pl.Database/", "src/Infrastructure/Pl.Database/"]

RUN dotnet build "src/Apps/Web/Pl.Admin.Api/Pl.Admin.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "src/Apps/Web/Pl.Admin.Api/Pl.Admin.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false  --no-restore


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pl.Admin.Api.dll"]