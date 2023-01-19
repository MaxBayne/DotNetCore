#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DotNetCore.WebAppMVC/DotNetCore.WebAppMVC.csproj", "DotNetCore.WebAppMVC/"]
COPY ["DotNetCore.BusinessLogic/DotNetCore.BusinessLogic.csproj", "DotNetCore.BusinessLogic/"]
COPY ["DotNetCore.Common/DotNetCore.Common.csproj", "DotNetCore.Common/"]
COPY ["DotNetCore.DataAccess/DotNetCore.DataAccess.csproj", "DotNetCore.DataAccess/"]
RUN dotnet restore "DotNetCore.WebAppMVC/DotNetCore.WebAppMVC.csproj"
COPY . .
WORKDIR "/src/DotNetCore.WebAppMVC"
RUN dotnet build "DotNetCore.WebAppMVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetCore.WebAppMVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetCore.WebAppMVC.dll"]
