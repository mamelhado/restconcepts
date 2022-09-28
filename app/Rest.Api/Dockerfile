#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["app/Rest.Api/Rest.Api.csproj", "app/Rest.Api/"]
RUN dotnet restore "app/Rest.Api/Rest.Api.csproj"
COPY . .
WORKDIR "/src/app/Rest.Api"
RUN dotnet build "Rest.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rest.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rest.Api.dll"]