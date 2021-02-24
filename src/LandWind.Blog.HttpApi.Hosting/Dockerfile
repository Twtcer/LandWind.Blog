#See http://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/LandWind.Blog.HttpApi.Hosting/LandWind.Blog.HttpApi.Hosting.csproj", "src/LandWind.Blog.HttpApi.Hosting/"]
COPY ["src/LandWind.Blog.Application/LandWind.Blog.Application.csproj", "src/LandWind.Blog.Application/"]
COPY ["src/LandWind.Blog.Application.Contracts/LandWind.Blog.Application.Contracts.csproj", "src/LandWind.Blog.Application.Contracts/"]
COPY ["src/LandWind.Blog.Domain.Shared/LandWind.Blog.Domain.Shared.csproj", "src/LandWind.Blog.Domain.Shared/"]
COPY ["src/LandWind.Blog.Domain/LandWind.Blog.Domain.csproj", "src/LandWind.Blog.Domain/"]
COPY ["src/LandWind.Blog.EntityFrameworkCore.DbMigrations/LandWind.Blog.EntityFrameworkCore.DbMigrations.csproj", "src/LandWind.Blog.EntityFrameworkCore.DbMigrations/"]
COPY ["src/LandWind.Blog.EntityFrameworkCore/LandWind.Blog.EntityFrameworkCore.csproj", "src/LandWind.Blog.EntityFrameworkCore/"]
COPY ["src/LandWind.Blog.HttpApi/LandWind.Blog.HttpApi.csproj", "src/LandWind.Blog.HttpApi/"]
RUN dotnet restore "src/LandWind.Blog.HttpApi.Hosting/LandWind.Blog.HttpApi.Hosting.csproj"
COPY . .
WORKDIR "/src/src/LandWind.Blog.HttpApi.Hosting"
RUN dotnet build "LandWind.Blog.HttpApi.Hosting.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LandWind.Blog.HttpApi.Hosting.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LandWind.Blog.HttpApi.Hosting.dll"]