﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["IMS/IMS.csproj", "IMS/"]
RUN dotnet restore "IMS/IMS.csproj"
COPY . .
WORKDIR "/src/IMS"
RUN dotnet build "IMS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IMS.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IMS.dll"]
