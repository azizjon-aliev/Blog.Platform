FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

ENV ASPNETCORE_URLS http://*:9001
EXPOSE 9001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Content/Content.API/Content.API.csproj", "src/Services/Content/Content.API/"]
COPY ["src/Services/Content/Content.Application/Content.Application.csproj", "src/Services/Content/Content.Application/"]
COPY ["src/Services/Content/Content.Domain/Content.Domain.csproj", "src/Services/Content/Content.Domain/"]
COPY ["src/Services/Content/Content.Infrastructure/Content.Infrastructure.csproj", "src/Services/Content/Content.Infrastructure/"]
RUN dotnet restore "src/Services/Content/Content.API/Content.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Content/Content.API"
RUN dotnet build "Content.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Content.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Content.API.dll"]
